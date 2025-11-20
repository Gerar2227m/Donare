using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace Donare
{
    // Form de reportes generales del sistema Donare.
    // Aquí se manejan:
    // - Reportes de inventario de sangre
    // - Reportes de donaciones
    // - Reportes de solicitudes médicas
    // - Reportes de bolsas próximas a vencer
    // - Exportar a "Excel" (CSV) lo que se ve en el DataGridView
    public partial class frmReportes : Form
    {
        public frmReportes()
        {
            // Inicializa todos los controles del formulario (diseñador)
            InitializeComponent();
        }

        // --------------------------------------------------------------------
        // MÉTODO PARA MANEJAR LOS PANELES DE FILTROS
        // --------------------------------------------------------------------
        private void MostrarPanelFiltros(Panel panel)
        {
            // Esta función apaga todos los paneles de filtros
            // y solo deja visible el panel que le pasemos por parámetro.
            // Sirve para que solo se muestre el filtro de:
            // - Inventario
            // - Donaciones
            // - Solicitudes
            // - Vencimiento
            // según el botón del menú lateral que se presione.

            panelFiltrosInventario.Visible = false;
            panelFiltrosDonaciones.Visible = false;
            panelFiltrosSolicitudes.Visible = false;
            panelFiltrosVencimiento.Visible = false;

            // Este es el único panel que quedará visible
            panel.Visible = true;
        }

        // --------------------------------------------------------------------
        // CARGAR TIPOS DE SANGRE EN COMBOBOX
        // --------------------------------------------------------------------
        private void CargarTiposDeSangre(ComboBox cmb)
        {
            // Llena un ComboBox con los tipos de sangre desde la tabla TipoSangre.
            // Se usa tanto para filtros de inventario como para donaciones por tipo.

            try
            {
                // Se crea la conexión con la clase cConexion que ya usa la cadena del proyecto
                SqlConnection conn = new cConexion().ConexionServer();

                // Consulta súper simple, solo para cargar los tipos de sangre
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT IdTipoSangre, Grupo + Rh AS Tipo FROM TipoSangre",
                    conn
                );
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Asignamos el DataTable al ComboBox que nos mandan
                cmb.DataSource = dt;
                cmb.DisplayMember = "Tipo";       // Lo que se ve (ej: "O+")
                cmb.ValueMember = "IdTipoSangre"; // Lo que se usa internamente (ej: 1)
            }
            catch (Exception ex)
            {
                // Si algo sale mal, solo mostramos un mensaje.
                MessageBox.Show("Error al cargar tipos de sangre: " + ex.Message);
            }
        }

        // --------------------------------------------------------------------
        // MÉTODO GENÉRICO PARA CARGAR DATOS EN EL GRID
        // --------------------------------------------------------------------
        private void MostrarDatos(string query)
        {
            // Este método ejecuta cualquier SELECT que le mandemos
            // y pone el resultado directamente en el DataGridView "dgvReportes".

            try
            {
                SqlConnection conn = new cConexion().ConexionServer();
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Asignamos el resultado al grid
                dgvReportes.DataSource = dt;

                // Para que las columnas se ajusten al ancho del grid
                dgvReportes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message);
            }
        }

        // --------------------------------------------------------------------
        // EVENTO LOAD DEL FORM
        // --------------------------------------------------------------------
        private void frmReportes_Load(object sender, EventArgs e)
        {
            // Cuando se abre el formulario de reportes:

            // 1. Mostramos por defecto el panel de inventario
            MostrarPanelFiltros(panelFiltrosInventario);

            // 2. Cargamos los tipos de sangre para el filtro de inventario
            CargarTiposDeSangre(cmbTipoInventario);

            // 3. Llenamos el combo de Estado de Solicitud
            //    Estos valores deben coincidir con el CHECK de la BD:
            //    ([Estado]='Atendida' OR [Estado]='Rechazada' OR [Estado]='Aprobada' OR [Estado]='Pendiente')
            cmbEstadoSolicitud.Items.Clear();
            cmbEstadoSolicitud.Items.Add("Pendiente");
            cmbEstadoSolicitud.Items.Add("Aprobada");
            cmbEstadoSolicitud.Items.Add("Atendida");
            cmbEstadoSolicitud.Items.Add("Rechazada");

            if (cmbEstadoSolicitud.Items.Count > 0)
                cmbEstadoSolicitud.SelectedIndex = 0; // Seleccionamos el primero por defecto

            // 4. Valor por defecto para días de vencimiento (solo por si está en 0)
            if (numDiasVencimiento.Value == 0)
                numDiasVencimiento.Value = 15;

            // 5. Cargamos el reporte de inventario completo al iniciar
            //    (Simulamos como si se hubiera hecho click en el botón Inventario)
            btnInventario_Click_1(null, null);
        }

        // --------------------------------------------------------------------
        // BOTÓN: INVENTARIO (menú lateral)
        // --------------------------------------------------------------------
        private void btnInventario_Click_1(object sender, EventArgs e)
        {
            // Cuando se hace click en "Inventario":

            // Activamos el panel de filtros de inventario
            MostrarPanelFiltros(panelFiltrosInventario);

            // Query base para ver TODAS las bolsas de sangre
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

            // Mostramos en el grid
            MostrarDatos(query);
        }

        // --------------------------------------------------------------------
        // BOTÓN: DONACIONES (menú lateral)
        // --------------------------------------------------------------------
        private void btnDonaciones_Click_1(object sender, EventArgs e)
        {
            // Cuando se hace click en "Donaciones":

            // Activamos el panel de filtros de donaciones
            MostrarPanelFiltros(panelFiltrosDonaciones);

            // Query base de donaciones con total de volumen por donación
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

        // --------------------------------------------------------------------
        // BOTÓN: SOLICITUDES (menú lateral)
        // --------------------------------------------------------------------
        private void btnSolicitados_Click(object sender, EventArgs e)
        {
            // Cuando se hace click en "Solicitudes":

            // Activamos el panel de filtros de solicitudes
            MostrarPanelFiltros(panelFiltrosSolicitudes);

            // Query base para ver todas las solicitudes médicas
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

        // --------------------------------------------------------------------
        // BOTÓN: PRÓXIMAS A VENCER (menú lateral)
        // --------------------------------------------------------------------
        private void btnVencimiento_Click_1(object sender, EventArgs e)
        {
            // Cuando se hace click en "Próximas a vencer":

            // Activamos el panel de filtros de vencimiento
            MostrarPanelFiltros(panelFiltrosVencimiento);

            // Obtenemos los días que el usuario puso en el NumericUpDown
            int dias = (int)numDiasVencimiento.Value;
            if (dias <= 0) dias = 15; // Por si está en 0 o algún valor raro

            // Query para ver las bolsas de sangre que vencen en X días o menos
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

        // --------------------------------------------------------------------
        // HANDLERS VACÍOS / CÓDIGO VIEJO (NO USADOS)
        // Estos métodos existen porque en algún momento el diseñador los creó,
        // pero después cambiamos a las versiones con sufijo "_Click_1".
        // Se dejan aquí por si algo del diseñador aún los referencia,
        // pero actualmente no tienen lógica.
        // --------------------------------------------------------------------

        private void btnFiltrarInventario_Click(object sender, EventArgs e)
        {
            // >> Handler antiguo para filtrar inventario (no se usa).
            // Ahora se usa: btnFiltrarInventario_Click_1
        }

        private void btnDonacionesFecha_Click(object sender, EventArgs e)
        {
            // >> Handler antiguo para filtro por fecha en donaciones (no se usa).
            // Ahora se usa: btnDonacionesFecha_Click_1
        }

        private void btnDonacionesDonante_Click(object sender, EventArgs e)
        {
            // >> Handler antiguo para filtro por donante (no se usa).
            // Ahora se usa: btnDonacionesDonante_Click_1
        }

        private void btnFiltrarSolicitudes_Click(object sender, EventArgs e)
        {
            // >> Handler antiguo para filtro de solicitudes (no se usa).
            // Ahora se usa: btnFiltrarSolicitudes_Click_1
        }

        // --------------------------------------------------------------------
        // FILTRO DE VENCIMIENTO (botón dentro del panel de filtros)
        // --------------------------------------------------------------------
        private void btnFiltrarVencimiento_Click(object sender, EventArgs e)
        {
            // Esta versión es un botón de "Aplicar" dentro del panel de vencimiento.
            // Hace prácticamente lo mismo que el botón del menú lateral,
            // pero permitiendo que el usuario cambie el número de días.

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

        // --------------------------------------------------------------------
        // HANDLERS DE EVENTOS VISUALES QUE DE MOMENTO NO HACEN NADA
        // (Se dejan por si se necesitan en el futuro)
        // --------------------------------------------------------------------
        private void panelFiltrosVencimiento_Paint(object sender, PaintEventArgs e)
        {
            // Evento de pintado del panel de vencimiento (no se utiliza actualmente).
        }

        private void dtDonInicio_Click(object sender, EventArgs e)
        {
            // Click sobre el label de "Fecha de inicio" (no hace nada por ahora).
        }

        private void numDiasVencimiento_ValueChanged(object sender, EventArgs e)
        {
            // Evento cuando cambia el valor del NumericUpDown de días.
            // Por ahora no se hace nada automático.
        }

        // --------------------------------------------------------------------
        // FILTRO: INVENTARIO POR TIPO DE SANGRE (botón FILTRAR en panel inventario)
        // --------------------------------------------------------------------
        private void btnFiltrarInventario_Click_1(object sender, EventArgs e)
        {
            // Filtro que muestra solamente las bolsas del tipo de sangre seleccionado en cmbTipoInventario.

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
                    // Usamos el ValueMember del combo (IdTipoSangre)
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

        // --------------------------------------------------------------------
        // FILTRO: DONACIONES POR FECHA (botón en panel de donaciones)
        // --------------------------------------------------------------------
        private void btnDonacionesFecha_Click_1(object sender, EventArgs e)
        {
            // Filtro que trae donaciones entre dos fechas (inicio y fin).
            // Se usa >= fechaInicio y < fechaFin+1 día para no perder registros por la hora.

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
                    // dateTimePicker1 = fecha de inicio
                    // dtDonFin        = fecha final
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

        // --------------------------------------------------------------------
        // FILTRO: DONACIONES POR NOMBRE DE DONANTE (botón en panel de donaciones)
        // --------------------------------------------------------------------
        private void btnDonacionesDonante_Click_1(object sender, EventArgs e)
        {
            // Filtro de donaciones por nombre del donante.
            // Si el usuario deja el TextBox vacío, se muestran todas las donaciones.

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
                    // Si @name está vacío, se ignora el filtro y trae todo.
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

        // --------------------------------------------------------------------
        // FILTRO: SOLICITUDES POR ESTADO (botón en panel de solicitudes)
        // --------------------------------------------------------------------
        private void btnFiltrarSolicitudes_Click_1(object sender, EventArgs e)
        {
            // Filtro de solicitudes por Estado (Pendiente, Aprobada, Atendida, Rechazada)
            // usando el valor seleccionado en cmbEstadoSolicitud.

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

        // --------------------------------------------------------------------
        // BOTÓN: EXPORTAR A "EXCEL" (GENERA CSV desde el DataGridView)
        // --------------------------------------------------------------------
        private void generarExcel_Click(object sender, EventArgs e)
        {
            // Este botón exporta lo que está actualmente en el DataGridView
            // a un archivo CSV, que se puede abrir sin problema en Excel.

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

                // Encabezados de columnas
                for (int i = 0; i < dgvReportes.Columns.Count; i++)
                {
                    sb.Append(dgvReportes.Columns[i].HeaderText);
                    if (i < dgvReportes.Columns.Count - 1) sb.Append(",");
                }
                sb.AppendLine();

                // Filas de datos
                foreach (DataGridViewRow row in dgvReportes.Rows)
                {
                    if (!row.IsNewRow) // Evitar la fila en blanco al final
                    {
                        for (int i = 0; i < row.Cells.Count; i++)
                        {
                            // Reemplazamos comas por espacios para no romper el formato CSV
                            string value = row.Cells[i].Value?.ToString().Replace(",", " ") ?? "";
                            sb.Append(value);
                            if (i < row.Cells.Count - 1) sb.Append(",");
                        }
                        sb.AppendLine();
                    }
                }

                // Guardamos el archivo en disco con codificación UTF-8
                File.WriteAllText(saveFile.FileName, sb.ToString(), Encoding.UTF8);

                MessageBox.Show("Excel generado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
