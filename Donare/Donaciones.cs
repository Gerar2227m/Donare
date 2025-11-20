using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Donare
{
    public partial class Donaciones : Form
    {
        private readonly cConexion conexionDB = new cConexion();
        private string tipoSangreDonante = "";
        private bool esModoEdicion = false;
        private string idDonacionActual = "";

        public Donaciones()
        {
            InitializeComponent();
            ConfigurarFormulario();
        }

        private void CargarBolsas()
        {
            dgvBolsas.Rows.Clear();

            using (SqlConnection con = conexionDB.ConexionServer())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT CodigoBolsa, VolumenML, FechaExtraccion, FechaVencimiento, Estado, Ubicacion FROM BolsaSangre WHERE IdDonacion = @id", con);
        
                cmd.Parameters.AddWithValue("@id", txtNoDonacion.Text);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    dgvBolsas.Rows.Add(
                        dr["CodigoBolsa"].ToString(),
                        tipoSangreDonante,
                        dr["VolumenML"].ToString(),
                        Convert.ToDateTime(dr["FechaExtraccion"]).ToString("dd/MM/yyyy"),
                        Convert.ToDateTime(dr["FechaVencimiento"]).ToString("dd/MM/yyyy"),
                        dr["Estado"].ToString(),
                        dr["Ubicacion"].ToString()
                    );
                }
                dr.Close();
            }
        }


        private void CargarDatosDonacion()
        {
            try
            {
                using (SqlConnection con = conexionDB.ConexionServer())
                {
                    con.Open();

                    string sql = @"
                SELECT d.IdDonacion, d.IdDonante, don.NombreCompleto, 
                       d.TipoDonacion, d.Componente, d.Observaciones, d.FechaDonacion
                FROM Donacion d
                INNER JOIN Donante don ON d.IdDonante = don.IdDonante
                WHERE d.IdDonacion = @IdDonacion";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@IdDonacion", txtNoDonacion.Text.Trim());

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        txtCodDonante.Text = dr["IdDonante"].ToString();
                        txtNombre.Text = dr["NombreCompleto"].ToString();

                        cboTipoDonacion.Text = dr["TipoDonacion"].ToString();
                        cboComponente.Text = dr["Componente"].ToString();

                        txtObservaciones.Text = dr["Observaciones"]?.ToString() ?? "";
                        dtpFecha.Value = Convert.ToDateTime(dr["FechaDonacion"]);
                    }

                    dr.Close();
                }

                // después de llenar los datos, cargar las bolsas asociadas:
                CargarBolsas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos de la donación: " + ex.Message);
            }
        }



        private void CargarDatosDonante()
        {
            try
            {
                using (SqlConnection con = conexionDB.ConexionServer())
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand(@"
                SELECT TOP 1 d.NombreCompleto, d.Direccion, d.Telefono,
                       ts.Grupo + ts.Rh AS TipoSangre
                FROM Donante d
                INNER JOIN EvaluacionMedica em ON d.IdDonante = em.IdDonante AND em.Apto = 1
                INNER JOIN TipoSangre ts ON em.IdTipoSangre = ts.IdTipoSangre
                WHERE d.IdDonante = @cod
                ORDER BY em.FechaEvaluacion DESC", con);

                    cmd.Parameters.AddWithValue("@cod", txtCodDonante.Text.Trim());

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        txtNombre.Text = dr["NombreCompleto"].ToString();
                        txtDireccion.Text = dr["Direccion"].ToString();
                        txtTelefono.Text = dr["Telefono"].ToString();
                        tipoSangreDonante = dr["TipoSangre"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Donante no encontrado o no apto.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ConfigurarFormulario()
        {
            // Deshabilitar todo al inicio
            DeshabilitarControles();

            // Configuración inicial
            txtMedico.Text = "Dr. Argueta";
            txtMedico.ReadOnly = true;
            cboTipoDonacion.Items.AddRange(new[] { "Voluntaria", "Dirigida", "Reposición" });
            cboComponente.Items.AddRange(new[] { "Sangre Total", "Plasma", "Plaquetas", "Crioprecipitado" });
            dgvBolsas.AllowUserToAddRows = false;
            dgvBolsas.AllowUserToDeleteRows = false;
            dgvBolsas.ReadOnly = true;
            dgvBolsas.RowHeadersVisible = false;
            dgvBolsas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void DeshabilitarControles()
        {
            txtCodDonante.Enabled = false;
            txtNombre.Enabled = false;
            txtDireccion.Enabled = false;
            txtTelefono.Enabled = false;
            txtObservaciones.Enabled = false;
            dtpFecha.Enabled = false;
            cboTipoDonacion.Enabled = false;
            cboComponente.Enabled = false;
            btnBuscarDonante.Enabled = false;
            Agregar.Enabled = false;

            // Estado botones iniciales
            tsbNuevo.Enabled = true;
            tsbGuardar.Enabled = false;
            tsbModificar.Enabled = false;
            tsbEliminar.Enabled = false;
        }

        private void HabilitarControles()
        {
            txtCodDonante.Enabled = true;
            txtNombre.Enabled = false; // Se llena automáticamente
            txtDireccion.Enabled = false; // Se llena automáticamente
            txtTelefono.Enabled = false; // Se llena automáticamente
            txtObservaciones.Enabled = true;
            dtpFecha.Enabled = true;
            cboTipoDonacion.Enabled = true;
            cboComponente.Enabled = true;
            btnBuscarDonante.Enabled = true;
            Agregar.Enabled = true;
            tsbGuardar.Enabled = true;
            tsbEliminar.Enabled = false; // Solo habilita después de buscar
        }

        private void Nuevo()
        {
            HabilitarControles(); // habilita todos los campos y botones necesarios
            GenerarNumeroDonacion();

            dgvBolsas.Rows.Clear();
            txtCodDonante.Clear();
            txtNombre.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            txtObservaciones.Clear();
            tipoSangreDonante = "";

            tsbNuevo.Enabled = false;
            tsbGuardar.Enabled = true;
            tsbModificar.Enabled = false;
            tsbEliminar.Enabled = false;

            btnBuscarDonante.Enabled = true;
            txtCodDonante.Focus();
        }

        private void GenerarNumeroDonacion()
        {
            try
            {
                using (SqlConnection con = conexionDB.ConexionServer())
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(CAST(SUBSTRING(IdDonacion,6,10) AS INT)),0)+1 FROM Donacion", con);
                    int n = Convert.ToInt32(cmd.ExecuteScalar());
                    txtNoDonacion.Text = "DONA-" + n.ToString("000");
                }
            }
            catch { txtNoDonacion.Text = "DONA-001"; }
        }

        

        private void btnBuscarDonante_Click(object sender, EventArgs e)
        {
            using (frmBuscarDonacion frm = new frmBuscarDonacion())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    txtCodDonante.Text = frm.IdDonanteSeleccionado;
                    CargarDatosDonante();
                }
            }
        }



        private void Guardar()
        {
            if (dgvBolsas.Rows.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos una bolsa.");
                return;
            }

            try
            {
                using (SqlConnection con = conexionDB.ConexionServer())
                {
                    con.Open();
                    using (SqlTransaction tran = con.BeginTransaction())
                    {
                        try
                        {
                            if (esModoEdicion)
                            {
                                SqlCommand cmdDel = new SqlCommand("DELETE FROM BolsaSangre WHERE IdDonacion = @id", con, tran);
                                cmdDel.Parameters.AddWithValue("@id", txtNoDonacion.Text);
                                cmdDel.ExecuteNonQuery();

                                SqlCommand cmdUpd = new SqlCommand(@"
                                    UPDATE Donacion 
                                    SET FechaDonacion = @f, TipoDonacion = @t, Componente = @c, Observaciones = @o 
                                    WHERE IdDonacion = @id", con, tran);
                                cmdUpd.Parameters.AddWithValue("@id", txtNoDonacion.Text);
                                cmdUpd.Parameters.AddWithValue("@f", dtpFecha.Value);
                                cmdUpd.Parameters.AddWithValue("@t", cboTipoDonacion.Text);
                                cmdUpd.Parameters.AddWithValue("@c", cboComponente.Text);
                                cmdUpd.Parameters.AddWithValue("@o", txtObservaciones.Text ?? "");
                                cmdUpd.ExecuteNonQuery();
                            }

                            if (!esModoEdicion)
                            {
                                SqlCommand cmdIns = new SqlCommand(@"
        INSERT INTO Donacion
        (IdDonacion, IdDonante, FechaDonacion, TipoDonacion, Componente, Observaciones)
        VALUES
        (@id, @donante, @fecha, @tipo, @comp, @obs)", con, tran);

                                cmdIns.Parameters.AddWithValue("@id", txtNoDonacion.Text);
                                cmdIns.Parameters.AddWithValue("@donante", txtCodDonante.Text);
                                cmdIns.Parameters.AddWithValue("@fecha", dtpFecha.Value);
                                cmdIns.Parameters.AddWithValue("@tipo", cboTipoDonacion.Text);
                                cmdIns.Parameters.AddWithValue("@comp", cboComponente.Text);
                                cmdIns.Parameters.AddWithValue("@obs", txtObservaciones.Text ?? "");

                                cmdIns.ExecuteNonQuery();
                            }




                            foreach (DataGridViewRow row in dgvBolsas.Rows)
                            {
                                SqlCommand cmdBolsa = new SqlCommand(@"
    INSERT INTO BolsaSangre
    (CodigoBolsa, IdDonacion, IdTipoSangre, FechaExtraccion, FechaVencimiento, VolumenML, Estado, Ubicacion)
    VALUES
    ('BOL-' + RIGHT('000000' + CAST(NEXT VALUE FOR SeqCodigoBolsa AS VARCHAR(6)), 6),
     @don, (SELECT IdTipoSangre FROM TipoSangre WHERE Grupo + Rh = @tipo), @fext, @fvenc,
     500, 'Aprobada', 'San Salvador - Nevera A-01')", con, tran);
                                cmdBolsa.Parameters.AddWithValue("@don", txtNoDonacion.Text);
                                cmdBolsa.Parameters.AddWithValue("@tipo", row.Cells[1].Value);
                                cmdBolsa.Parameters.AddWithValue("@fext", DateTime.Today);
                                cmdBolsa.Parameters.AddWithValue("@fvenc", Convert.ToDateTime(row.Cells[4].Value));
                                cmdBolsa.ExecuteNonQuery();
                            }

                            tran.Commit();
                            MessageBox.Show(esModoEdicion ? "Donación modificada con éxito." : "Donación guardada con éxito.");
                            DeshabilitarControles();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            MessageBox.Show("Error al guardar: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error de conexión: " + ex.Message); }
        }

        private void Buscar()
        {
            frmBuscarDonaciones1 buscar = new frmBuscarDonaciones1();

            if (buscar.ShowDialog() == DialogResult.OK)
            {
                txtNoDonacion.Text = buscar.IdDonacionSeleccionada;
                CargarDatosDonacion();
                CargarBolsas();
                
            }
        }

        private void CargarDonacion(string id)
        {
            try
            {
                using (SqlConnection con = conexionDB.ConexionServer())
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT FechaDonacion, TipoDonacion, Componente, Observaciones, IdDonante FROM Donacion WHERE IdDonacion = @id", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        dr.Close();
                        txtNoDonacion.Text = id;
                        dtpFecha.Value = Convert.ToDateTime(dr["FechaDonacion"]);
                        cboTipoDonacion.Text = dr["TipoDonacion"].ToString();
                        cboComponente.Text = dr["Componente"].ToString();
                        txtObservaciones.Text = dr["Observaciones"]?.ToString() ?? "";
                        txtCodDonante.Text = dr["IdDonante"].ToString();
                        btnBuscarDonante_Click(null, null);

                        dgvBolsas.Rows.Clear();
                        SqlCommand cmdBolsas = new SqlCommand("SELECT CodigoBolsa, VolumenML, FechaExtraccion, FechaVencimiento, Estado, Ubicacion FROM BolsaSangre WHERE IdDonacion = @id", con);
                        cmdBolsas.Parameters.AddWithValue("@id", id);
                        SqlDataReader drBolsas = cmdBolsas.ExecuteReader();

                        while (drBolsas.Read())
                        {
                            dgvBolsas.Rows.Add(
                                drBolsas["CodigoBolsa"].ToString(),
                                tipoSangreDonante,
                                drBolsas["VolumenML"],
                                Convert.ToDateTime(drBolsas["FechaExtraccion"]).ToString("dd/MM/yyyy"),
                                Convert.ToDateTime(drBolsas["FechaVencimiento"]).ToString("dd/MM/yyyy"),
                                drBolsas["Estado"].ToString(),
                                drBolsas["Ubicacion"].ToString()
                            );
                        }
                        drBolsas.Close();

                        esModoEdicion = true;
                        idDonacionActual = id;
                        DeshabilitarControles();

                        tsbModificar.Enabled = true;
                        tsbEliminar.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("No se encontró la donación: " + id);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error al cargar: " + ex.Message); }
        }

        private void Modificar()
        {
            if (string.IsNullOrWhiteSpace(idDonacionActual))
            {
                MessageBox.Show("Primero busca una donación.");
                return;
            }

            HabilitarControles();
            tsbEliminar.Enabled = false;
        }

        private void Eliminar()
        {
            if (string.IsNullOrWhiteSpace(idDonacionActual))
            {
                MessageBox.Show("Primero busca una donación.");
                return;
            }

            if (MessageBox.Show($"¿Eliminar permanentemente la donación {txtNoDonacion.Text}?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection con = conexionDB.ConexionServer())
                    {
                        con.Open();
                        using (SqlTransaction tran = con.BeginTransaction())
                        {
                            SqlCommand cmd1 = new SqlCommand("DELETE FROM BolsaSangre WHERE IdDonacion = @id", con, tran);
                            cmd1.Parameters.AddWithValue("@id", txtNoDonacion.Text);
                            cmd1.ExecuteNonQuery();

                            SqlCommand cmd2 = new SqlCommand("DELETE FROM Donacion WHERE IdDonacion = @id", con, tran);
                            cmd2.Parameters.AddWithValue("@id", txtNoDonacion.Text);
                            cmd2.ExecuteNonQuery();

                            tran.Commit();
                        }
                    }
                    MessageBox.Show("Donación eliminada correctamente.");
                    Nuevo();
                }
                catch (Exception ex) { MessageBox.Show("Error al eliminar: " + ex.Message); }
            }
        }

        private void tsbGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
        }
        private void tsbNuevo_Click_1(object sender, EventArgs e)
        {
            Nuevo();
        }

        private void tsbModificar_Click_1(object sender, EventArgs e)
        {
            Modificar();
        }

        private void tsbEliminar_Click_1(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void tsbBuscar_Click_1(object sender, EventArgs e)
        {
            Buscar();
        }

        private void tsbSalir_Click_1(object sender, EventArgs e)
        {
            this.Close();

        }

        private void Agregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tipoSangreDonante))
            {
                MessageBox.Show("Primero busca un donante APTO.");
                return;
            }


            DateTime vencimiento = cboComponente.Text.Contains("Sangre Total")
                ? DateTime.Today.AddDays(42)
                : DateTime.Today.AddYears(5);

            dgvBolsas.Rows.Add(
                "[AUTO]",
                tipoSangreDonante,
                500,
                DateTime.Today.ToString("dd/MM/yyyy"),
                vencimiento.ToString("dd/MM/yyyy"),
                "Aprobada",
                "San Salvador - Nevera A-01"
            );
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Donaciones_Load(object sender, EventArgs e)
        {

        }
    }
}