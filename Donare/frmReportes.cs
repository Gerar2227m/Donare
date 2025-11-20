using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;
using System.Text;


namespace Donare
{
    public partial class frmReportes : Form
    {
        public frmReportes()
        {
            InitializeComponent();
        }

      

        private void MostrarPanelFiltros(Panel panel)
        {
           

            panelFiltrosInventario.Visible = false;
            panelFiltrosDonaciones.Visible = false;
            panelFiltrosSolicitudes.Visible = false;
            panelFiltrosVencimiento.Visible = false;

            panel.Visible = true;
        }

        private void CargarTiposDeSangre(ComboBox cmb)
        {
            try
            {
                SqlConnection conn = new cConexion().ConexionServer();
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT IdTipoSangre, Grupo + Rh AS Tipo FROM TipoSangre",
                    conn
                );
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmb.DataSource = dt;
                cmb.DisplayMember = "Tipo";
                cmb.ValueMember = "IdTipoSangre";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar tipos de sangre: " + ex.Message);
            }
        }

        private void MostrarDatos(string query)
        {
            try
            {
                SqlConnection conn = new cConexion().ConexionServer();
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvReportes.DataSource = dt;
                dgvReportes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message);
            }
        }
        private void frmReportes_Load(object sender, EventArgs e)
        {
           
            MostrarPanelFiltros(panelFiltrosInventario);

     
            CargarTiposDeSangre(cmbTipoInventario);

            cmbEstadoSolicitud.Items.Clear();
            cmbEstadoSolicitud.Items.Add("Pendiente");
            cmbEstadoSolicitud.Items.Add("Aprobada");
            cmbEstadoSolicitud.Items.Add("Atendida");
            cmbEstadoSolicitud.Items.Add("Rechazada");
            if (cmbEstadoSolicitud.Items.Count > 0)
                cmbEstadoSolicitud.SelectedIndex = 0;

            // Valor por defecto para días de vencimiento
            if (numDiasVencimiento.Value == 0)
                numDiasVencimiento.Value = 15;

            // Cargar inventario de entrada
            btnInventario_Click_1(null, null);
        }

        private void btnInventario_Click_1(object sender, EventArgs e)
        {
            MostrarPanelFiltros(panelFiltrosInventario);

            string query = @"
            SELECT
                BS.CodigoBolsa,
                TS.Grupo + TS.Rh AS TipoSangre,
                BS.FechaExtraccion,
                BS.FechaVencimiento,
                BS.VolumenML,
                BS.Estado,
                BS.Ubicacion
            FROM BolsaSangre BS
            INNER JOIN TipoSangre TS ON BS.IdTipoSangre = TS.IdTipoSangre";

            MostrarDatos(query);
        }

        // Donaciones
        private void btnDonaciones_Click_1(object sender, EventArgs e)
        {
            MostrarPanelFiltros(panelFiltrosDonaciones);

            string query = @"
            SELECT 
                D.IdDonacion,
                Don.NombreCompleto AS Donante,
                D.FechaDonacion,
                D.TipoDonacion,
                D.Componente,
                SUM(BS.VolumenML) AS VolumenTotal
            FROM Donacion D
            INNER JOIN Donante Don ON D.IdDonante = Don.IdDonante
            LEFT JOIN BolsaSangre BS ON BS.IdDonacion = D.IdDonacion
            GROUP BY 
                D.IdDonacion, Don.NombreCompleto, D.FechaDonacion,
                D.TipoDonacion, D.Componente
            ORDER BY D.FechaDonacion DESC";

            MostrarDatos(query);
        }

        // Solicitudes médicas
        private void btnSolicitados_Click(object sender, EventArgs e)
        {
            MostrarPanelFiltros(panelFiltrosSolicitudes);

            string query = @"
            SELECT 
                IdSolicitud,
                CentroSolicitante,
                MedicoSolicitante,
                CantidadUnidades,
                Prioridad,
                FechaSolicitud,
                Estado
            FROM SolicitudMedica";

            MostrarDatos(query);
        }

        // Próximas a vencer (default 15 días)
        private void btnVencimiento_Click_1(object sender, EventArgs e)
        {
            MostrarPanelFiltros(panelFiltrosVencimiento);

            int dias = (int)numDiasVencimiento.Value;
            if (dias <= 0) dias = 15;

            string query = @"
            SELECT 
                BS.CodigoBolsa,
                TS.Grupo + TS.Rh AS TipoSangre,
                BS.FechaExtraccion,
                BS.FechaVencimiento,
                BS.Estado
            FROM BolsaSangre BS
            INNER JOIN TipoSangre TS ON TS.IdTipoSangre = BS.IdTipoSangre
            WHERE BS.FechaVencimiento <= DATEADD(day, @dias, GETDATE())
            ORDER BY BS.FechaVencimiento ASC";

            try
            {
                SqlConnection conn = new cConexion().ConexionServer();
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@dias", dias);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvReportes.DataSource = dt;
                dgvReportes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar próximas a vencer: " + ex.Message);
            }
        }

        private void btnFiltrarInventario_Click(object sender, EventArgs e)
        {
            
        }

        private void btnDonacionesFecha_Click(object sender, EventArgs e)
        {
            
        }

  
        private void btnDonacionesDonante_Click(object sender, EventArgs e)
        {
            
        }

        private void btnFiltrarSolicitudes_Click(object sender, EventArgs e)
        {
            
        }

        private void btnFiltrarVencimiento_Click(object sender, EventArgs e)
        {
            int dias = (int)numDiasVencimiento.Value;
            if (dias <= 0) dias = 15;

            string query = @"
            SELECT 
                BS.CodigoBolsa,
                TS.Grupo + TS.Rh AS TipoSangre,
                BS.FechaExtraccion,
                BS.FechaVencimiento,
                BS.Estado
            FROM BolsaSangre BS
            INNER JOIN TipoSangre TS ON TS.IdTipoSangre = BS.IdTipoSangre
            WHERE BS.FechaVencimiento <= DATEADD(day, @dias, GETDATE())
            ORDER BY BS.FechaVencimiento ASC";

            try
            {
                SqlConnection conn = new cConexion().ConexionServer();
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@dias", dias);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvReportes.DataSource = dt;
                dgvReportes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar próximas a vencer: " + ex.Message);
            }
        }

        private void panelFiltrosVencimiento_Paint(object sender, PaintEventArgs e)
        {
        }

        private void dtDonInicio_Click(object sender, EventArgs e)
        {
        }

        private void numDiasVencimiento_ValueChanged(object sender, EventArgs e)
        {
        }

        private void btnFiltrarInventario_Click_1(object sender, EventArgs e)
        {
            string query = @"
    SELECT
        BS.CodigoBolsa,
        TS.Grupo + TS.Rh AS TipoSangre,
        BS.FechaExtraccion,
        BS.FechaVencimiento,
        BS.VolumenML,
        BS.Estado,
        BS.Ubicacion
    FROM BolsaSangre BS
    INNER JOIN TipoSangre TS ON BS.IdTipoSangre = TS.IdTipoSangre
    WHERE BS.IdTipoSangre = @tipo";

            try
            {
                using (SqlConnection conn = new cConexion().ConexionServer())
                using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                {
                    da.SelectCommand.Parameters.AddWithValue("@tipo", cmbTipoInventario.SelectedValue);

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvReportes.DataSource = dt;
                    dgvReportes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar inventario: " + ex.Message);
            }
        }

        private void btnDonacionesFecha_Click_1(object sender, EventArgs e)
        {
            string query = @"
    SELECT 
        D.IdDonacion,
        Don.NombreCompleto AS Donante,
        D.FechaDonacion,
        D.TipoDonacion,
        D.Componente,
        SUM(BS.VolumenML) AS VolumenTotal
    FROM Donacion D
    INNER JOIN Donante Don ON D.IdDonante = Don.IdDonante
    LEFT JOIN BolsaSangre BS ON BS.IdDonacion = D.IdDonacion
    WHERE D.FechaDonacion >= @i
      AND D.FechaDonacion <  DATEADD(day, 1, @f)
    GROUP BY 
        D.IdDonacion, Don.NombreCompleto, D.FechaDonacion,
        D.TipoDonacion, D.Componente
    ORDER BY D.FechaDonacion DESC";

            try
            {
                using (SqlConnection conn = new cConexion().ConexionServer())
                using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                {
                    da.SelectCommand.Parameters.AddWithValue("@i", dateTimePicker1.Value.Date);
                    da.SelectCommand.Parameters.AddWithValue("@f", dtDonFin.Value.Date);

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvReportes.DataSource = dt;
                    dgvReportes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar donaciones por fecha: " + ex.Message);
            }
        }

        private void btnDonacionesDonante_Click_1(object sender, EventArgs e)
        {
            string nombre = txtDonante.Text.Trim();

            string query = @"
    SELECT 
        D.IdDonacion,
        Don.NombreCompleto AS Donante,
        D.FechaDonacion,
        D.TipoDonacion,
        D.Componente,
        SUM(BS.VolumenML) AS VolumenTotal
    FROM Donacion D
    INNER JOIN Donante Don ON D.IdDonante = Don.IdDonante
    LEFT JOIN BolsaSangre BS ON BS.IdDonacion = D.IdDonacion
    WHERE (@name = '' OR Don.NombreCompleto LIKE @pattern)
    GROUP BY 
        D.IdDonacion, Don.NombreCompleto, D.FechaDonacion,
        D.TipoDonacion, D.Componente
    ORDER BY D.FechaDonacion DESC";

            try
            {
                using (SqlConnection conn = new cConexion().ConexionServer())
                using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                {
                    da.SelectCommand.Parameters.AddWithValue("@name", nombre);
                    da.SelectCommand.Parameters.AddWithValue("@pattern", "%" + nombre + "%");

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvReportes.DataSource = dt;
                    dgvReportes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar donaciones por donante: " + ex.Message);
            }
        }

        private void btnFiltrarSolicitudes_Click_1(object sender, EventArgs e)
        {
            string query = @"
            SELECT 
                IdSolicitud,
                CentroSolicitante,
                MedicoSolicitante,
                CantidadUnidades,
                Prioridad,
                FechaSolicitud,
                Estado
            FROM SolicitudMedica
            WHERE Estado = @estado";

            try
            {
                SqlConnection conn = new cConexion().ConexionServer();
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@estado", cmbEstadoSolicitud.Text);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvReportes.DataSource = dt;
                dgvReportes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar solicitudes: " + ex.Message);
            }
        }

        private void generarExcel_Click(object sender, EventArgs e)
        {
            if (dgvReportes.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.", "Excel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Excel CSV (*.csv)|*.csv";
            saveFile.FileName = "Reporte.csv";

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                StringBuilder sb = new StringBuilder();

                // Encabezados
                for (int i = 0; i < dgvReportes.Columns.Count; i++)
                {
                    sb.Append(dgvReportes.Columns[i].HeaderText);
                    if (i < dgvReportes.Columns.Count - 1) sb.Append(",");
                }
                sb.AppendLine();

                // Filas
                foreach (DataGridViewRow row in dgvReportes.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        for (int i = 0; i < row.Cells.Count; i++)
                        {
                            string value = row.Cells[i].Value?.ToString().Replace(",", " ") ?? "";
                            sb.Append(value);
                            if (i < row.Cells.Count - 1) sb.Append(",");
                        }
                        sb.AppendLine();
                    }
                }

                File.WriteAllText(saveFile.FileName, sb.ToString(), Encoding.UTF8);

                MessageBox.Show("Excel generado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
