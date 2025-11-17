using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Donare
{
    public partial class Donantes : Form
    {

        private string idDonanteActual = "";
        private bool esNuevo = true;
        private bool modoEdicion = false;
        private cConexion conexion = new cConexion();

        
        public Donantes()
        {
            InitializeComponent();
            ConfigurarControles();
            CargarCombos();
            txtPresion.KeyPress += txtPresion_KeyPress;

            numPeso.ValueChanged += (s, e) => EvaluarSiEstaApto_SinMensaje();
            numTalla.ValueChanged += (s, e) => { CalcularIMC(); EvaluarSiEstaApto_SinMensaje(); };
            numPulso.ValueChanged += (s, e) => EvaluarSiEstaApto_SinMensaje();
            numTemperatura.ValueChanged += (s, e) => EvaluarSiEstaApto_SinMensaje();
            numHemoglobina.ValueChanged += (s, e) => EvaluarSiEstaApto_SinMensaje();
            txtPresion.TextChanged += (s, e) => EvaluarSiEstaApto_SinMensaje();  // ← importante para presión
            dtpFechaNacimiento.ValueChanged += (s, e) => EvaluarSiEstaApto_SinMensaje();
            cmbSexo.SelectedIndexChanged += (s, e) => EvaluarSiEstaApto_SinMensaje();
        }

        private void ConfigurarControles()
        {
            txtIdDonante.ReadOnly = true;
            txtEdad.ReadOnly = true;
            dtpFechaRegistro.Enabled = false;

            // DUI automático
            txtNumeroDocumento.KeyPress += (s, e) =>
            {
                if (cmbTipoDocumento.Text == "DUI" && txtNumeroDocumento.Text.Length == 8 && e.KeyChar != (char)Keys.Back)
                {
                    txtNumeroDocumento.Text += "-";
                    txtNumeroDocumento.SelectionStart = txtNumeroDocumento.Text.Length;
                }
            };

            // Edad automática
            dtpFechaNacimiento.ValueChanged += (s, e) =>
            {
                int edad = DateTime.Today.Year - dtpFechaNacimiento.Value.Year;
                if (dtpFechaNacimiento.Value.Date > DateTime.Today.AddYears(-edad)) edad--;
                txtEdad.Text = edad.ToString();
            };
        }

        private void CargarCombos()
        {
            cmbSexo.Items.AddRange(new[] { "M", "F" });
            cmbTipoDocumento.Items.AddRange(new[] { "DUI", "Pasaporte", "Otro" });
            cmbGrupoSanguineo.Items.AddRange(new[] { "A", "B", "AB", "O" });
            cmbGrupoSanguineo.Items.AddRange(new[] { "+", "-" });
        }

        // BOTONES
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarTodo();
            esNuevo = true;
            modoEdicion = false;
            idDonanteActual = GenerarIdDonante();
            txtIdDonante.Text = idDonanteActual;
            dtpFechaRegistro.Value = DateTime.Today;
            txtNombreCompleto.Focus();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (var buscar = new frmBuscarDonante())
            {
                if (buscar.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(buscar.IdDonanteSeleccionado))
                {
                    idDonanteActual = buscar.IdDonanteSeleccionado;

                    // ===== AQUÍ EMPIEZA EL CÓDIGO QUE CARGA EL DONANTE (100% FUNCIONA) =====
                    SqlConnection con2 = conexion.ConexionServer();
                    try
                    {
                        con2.Open();
                        SqlCommand cmd = new SqlCommand(@"
                    SELECT d.*, em.*
                    FROM Donante d
                    LEFT JOIN EvaluacionMedica em ON d.IdDonante = em.IdDonante 
                        AND em.FechaEvaluacion = (
                            SELECT MAX(FechaEvaluacion) 
                            FROM EvaluacionMedica 
                            WHERE IdDonante = d.IdDonante
                        )
                    WHERE d.IdDonante = @id", con2);
                        cmd.Parameters.AddWithValue("@id", idDonanteActual);
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.Read())
                        {
                            txtIdDonante.Text = dr["IdDonante"].ToString();
                            txtNombreCompleto.Text = dr["NombreCompleto"].ToString();
                            dtpFechaNacimiento.Value = Convert.ToDateTime(dr["FechaNacimiento"]);
                            cmbSexo.Text = dr["SexoBiologico"].ToString();
                            cmbTipoDocumento.Text = dr["TipoDocumento"].ToString();
                            txtNumeroDocumento.Text = dr["NumeroDocumento"].ToString();
                            txtTelefono.Text = dr["Telefono"]?.ToString() ?? "";
                            txtDireccion.Text = dr["Direccion"]?.ToString() ?? "";
                            dtpFechaRegistro.Value = Convert.ToDateTime(dr["FechaRegistro"]);

                            // Si tiene evaluación médica
                            if (!dr.IsDBNull(dr.GetOrdinal("Peso")))
                            {
                                numPeso.Value = Convert.ToDecimal(dr["Peso"]);
                                numTalla.Value = Convert.ToDecimal(dr["Talla"]);
                                txtPresion.Text = dr["PresionArterial"]?.ToString() ?? "";
                                numPulso.Value = Convert.ToInt32(dr["Pulso"]);
                                numTemperatura.Value = Convert.ToDecimal(dr["Temperatura"]);
                                numHemoglobina.Value = Convert.ToDecimal(dr["Hemoglobina"]);

                                int idTipo = Convert.ToInt32(dr["IdTipoSangre"]);
                                SqlCommand cmdTipo = new SqlCommand("SELECT Grupo, Rh FROM TipoSangre WHERE IdTipoSangre = @t", con2);
                                cmdTipo.Parameters.AddWithValue("@t", idTipo);
                                SqlDataReader drTipo = cmdTipo.ExecuteReader();
                                if (drTipo.Read())
                                {
                                    cmbGrupoSanguineo.Text = drTipo["Grupo"].ToString();
                                    cmbRh.Text = drTipo["Rh"].ToString();
                                }
                                drTipo.Close();

                                chkVIH.Checked = (bool)dr["VIH"];
                                chkHepB.Checked = (bool)dr["HepatitisB"];
                                chkHepC.Checked = (bool)dr["HepatitisC"];
                                chkSifilis.Checked = (bool)dr["Sifilis"];
                                txtHistorialRelevante.Text = dr["HistorialRelevante"]?.ToString() ?? "";
                                chkApto.Checked = (bool)dr["Apto"];
                            }

                            esNuevo = false;
                            modoEdicion = false;
                            tabControl1.SelectedTab = tabpersonal;
                            MessageBox.Show("Donante cargado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al cargar: " + ex.Message);
                    }
                    finally
                    {
                        con2.Close();
                    }
                }
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (esNuevo || string.IsNullOrEmpty(idDonanteActual))
            {
                MessageBox.Show("Primero busca un donante existente.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            modoEdicion = true;
            MessageBox.Show("Puedes modificar los datos.\nAl terminar, presiona GUARDAR.", "Modo Edición", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarDatos()) return;
            if (!esNuevo && !modoEdicion)
            {
                MessageBox.Show("Presiona MODIFICAR para editar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!EvaluarSiEstaApto_ConMensaje())
            {
                return; // ← NO GUARDA si no está apto
            }

            SqlConnection con = conexion.ConexionServer();
            SqlTransaction tran = null;
            try
            {
                con.Open();
                tran = con.BeginTransaction();

                // 1. Donante: INSERT o UPDATE
                string sqlDonante = esNuevo ?
                    @"INSERT INTO Donante (IdDonante, NombreCompleto, FechaNacimiento, SexoBiologico, TipoDocumento, NumeroDocumento, Telefono, Direccion, FechaRegistro)
                  VALUES (@id, @nom, @fnac, @sex, @tipodoc, @numdoc, @tel, @dir, GETDATE())" :
                    @"UPDATE Donante SET NombreCompleto=@nom, FechaNacimiento=@fnac, SexoBiologico=@sex,
                  TipoDocumento=@tipodoc, NumeroDocumento=@numdoc, Telefono=@tel, Direccion=@dir
                  WHERE IdDonante=@id";

                using (SqlCommand cmd = new SqlCommand(sqlDonante, con, tran))
                {
                    cmd.Parameters.AddWithValue("@id", idDonanteActual);
                    cmd.Parameters.AddWithValue("@nom", txtNombreCompleto.Text.Trim());
                    cmd.Parameters.AddWithValue("@fnac", dtpFechaNacimiento.Value.Date);
                    cmd.Parameters.AddWithValue("@sex", cmbSexo.Text);
                    cmd.Parameters.AddWithValue("@tipodoc", cmbTipoDocumento.Text);
                    cmd.Parameters.AddWithValue("@numdoc", txtNumeroDocumento.Text.Trim());
                    cmd.Parameters.AddWithValue("@tel", txtTelefono.Text.Trim());
                    cmd.Parameters.AddWithValue("@dir", txtDireccion.Text.Trim());
                    cmd.ExecuteNonQuery();
                }

                // 2. Nueva evaluación médica
                string sqlEval = @"
                INSERT INTO EvaluacionMedica 
                (IdDonante, FechaEvaluacion, Peso, Talla, PresionArterial, Pulso, Temperatura, Hemoglobina,
                 IdTipoSangre, VIH, HepatitisB, HepatitisC, Sifilis, Chagas, HistorialRelevante, Apto)
                VALUES 
                (@id, GETDATE(), @peso, @talla, @pa, @pulso, @temp, @hb,
                 (SELECT IdTipoSangre FROM TipoSangre WHERE Grupo=@grupo AND Rh=@rh),
                 @vih, @hepb, @hepc, @sifi, @chag, @hist, @apto)";

                using (SqlCommand cmd = new SqlCommand(sqlEval, con, tran))
                {
                    cmd.Parameters.AddWithValue("@id", idDonanteActual);
                    cmd.Parameters.AddWithValue("@peso", numPeso.Value);
                    cmd.Parameters.AddWithValue("@talla", numTalla.Value);
                    cmd.Parameters.AddWithValue("@pa", txtPresion.Text.Trim());
                    cmd.Parameters.AddWithValue("@pulso", numPulso.Value);
                    cmd.Parameters.AddWithValue("@temp", numTemperatura.Value);
                    cmd.Parameters.AddWithValue("@hb", numHemoglobina.Value);
                    cmd.Parameters.AddWithValue("@grupo", cmbGrupoSanguineo.Text);
                    cmd.Parameters.AddWithValue("@rh", cmbGrupoSanguineo.Text);
                    cmd.Parameters.AddWithValue("@vih", chkVIH.Checked);
                    cmd.Parameters.AddWithValue("@hepb", chkHepB.Checked);
                    cmd.Parameters.AddWithValue("@hepc", chkHepC.Checked);
                    cmd.Parameters.AddWithValue("@sifi", chkSifilis.Checked);
                    cmd.Parameters.AddWithValue("@hist", txtHistorialRelevante.Text.Trim());
                    cmd.Parameters.AddWithValue("@apto", chkApto.Checked);
                    cmd.ExecuteNonQuery();
                }

                tran.Commit();
                MessageBox.Show(esNuevo ? "Donante registrado con éxito!" : "Datos actualizados correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                esNuevo = false;
                modoEdicion = false;
                
            }
            catch (Exception ex)
            {
                tran?.Rollback();
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (esNuevo || string.IsNullOrEmpty(idDonanteActual)) return;

            if (MessageBox.Show("¿Desactivar este donante?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                SqlConnection con = conexion.ConexionServer();
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Donante SET Activo=0 WHERE IdDonante=@id", con);
                    cmd.Parameters.AddWithValue("@id", idDonanteActual);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Donante desactivado.");
                    LimpiarTodo();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        // Función para generar ID (ajústala a tu lógica)
        private string GenerarIdDonante()
        {
            SqlConnection con = conexion.ConexionServer();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Donante", con);
                int count = (int)cmd.ExecuteScalar();
                return "DON-" + (count + 1).ToString("D5");
            }
            finally
            {
                con.Close();
            }
        }

        // Resto de métodos (CargarDonanteCompleto, CargarUltimaEvaluacion, CargarHistorialDonaciones, ValidarDatos, LimpiarTodo)
        // → te los paso si me dices que sí, o ya los tenías.

        private bool ValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(txtNombreCompleto.Text) ||
                cmbSexo.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(txtNumeroDocumento.Text))
            {
                MessageBox.Show("Completa los campos obligatorios.", "Validación");
                return false;
            }
            return true;
        }

        private void LimpiarTodo()
        {
            // tu código de limpiar
            idDonanteActual = "";
            esNuevo = true;
            modoEdicion = false;
        }

        private void Donantes_Load(object sender, EventArgs e)
        {

        }

        private void ConfigurarNumericUpDowns()
        {
            // TALLA (altura en cm) → permite decimales
            numTalla.DecimalPlaces = 1;
            numTalla.Increment = 0.1M;
            numTalla.Minimum = 100;
            numTalla.Maximum = 220;
            numTalla.Value = 170;

            // PESO → permite medio kilo
            numPeso.DecimalPlaces = 1;
            numPeso.Increment = 0.5M;
            numPeso.Minimum = 40;
            numPeso.Maximum = 200;
            numPeso.Value = 70;

            // PULSO
            numPulso.Minimum = 30;
            numPulso.Maximum = 200;
            numPulso.Value = 75;

            // TEMPERATURA
            numTemperatura.DecimalPlaces = 1;
            numTemperatura.Increment = 0.1M;
            numTemperatura.Minimum = 35;
            numTemperatura.Maximum = 42;
            numTemperatura.Value = 36.6M;

            // HEMOGLOBINA
            numHemoglobina.DecimalPlaces = 1;
            numHemoglobina.Increment = 0.1M;
            numHemoglobina.Minimum = 8;
            numHemoglobina.Maximum = 20;
            numHemoglobina.Value = 14;
        }

        private void CalcularIMC()
        {
            if (numTalla.Value > 0)
            {
                double alturaMetros = (double)numTalla.Value / 100;
                double imc = (double)numPeso.Value / (alturaMetros * alturaMetros);
                lblIMC.Text = $"IMC: {imc:F1}";  // ← asegúrate de tener un Label llamado lblIMC

                // Color según IMC
                if (imc < 18.5) lblIMC.ForeColor = Color.Orange;
                else if (imc > 30) lblIMC.ForeColor = Color.Red;
                else lblIMC.ForeColor = Color.Green;
            }
            else
            {
                lblIMC.Text = "IMC: -";
            }
        }

        private bool EvaluarSiEstaApto_SinMensaje()
        {
            bool apto = true;
            string motivo = "";

            // Edad
            int edad = DateTime.Today.Year - dtpFechaNacimiento.Value.Year;
            if (dtpFechaNacimiento.Value.Date > DateTime.Today.AddYears(-edad)) edad--;
            if (edad < 18 || edad > 65) { apto = false; motivo += "Edad no permitida. "; }

            // Peso
            if (numPeso.Value < 50) { apto = false; motivo += "Peso < 50 kg. "; }

            // Hemoglobina
            if (cmbSexo.Text == "F" && numHemoglobina.Value < 12.5M ||
                cmbSexo.Text == "M" && numHemoglobina.Value < 13.5M)
            { apto = false; motivo += "Hemoglobina baja. "; }

            // Pulso
            if (numPulso.Value < 60 || numPulso.Value > 100) { apto = false; motivo += "Pulso fuera de rango. "; }

            // Temperatura
            if (numTemperatura.Value > 37.5M) { apto = false; motivo += "Temperatura alta. "; }

            // Presión arterial
            if (!string.IsNullOrWhiteSpace(txtPresion.Text))
            {
                try
                {
                    string[] partes = txtPresion.Text.Trim().Split('/');
                    if (partes.Length == 2 && int.TryParse(partes[0], out int sist) && int.TryParse(partes[1], out int dias))
                    {
                        if (sist < 90 || sist > 140 || dias < 60 || dias > 90)
                        { apto = false; motivo += $"Presión fuera de rango ({sist}/{dias}). "; }
                    }
                    else { apto = false; motivo += "Formato presión incorrecto. "; }
                }
                catch { apto = false; motivo += "Presión inválida. "; }
            }
            else { apto = false; motivo += "Falta presión arterial. "; }

            // Pruebas
            if (chkVIH.Checked || chkHepB.Checked || chkHepC.Checked || chkSifilis.Checked)
            { apto = false; motivo += "Prueba reactiva. "; }

            chkApto.Checked = apto;
            return apto;  // devuelve true o false
        }

        private bool EvaluarSiEstaApto_ConMensaje()
        {
            bool apto = EvaluarSiEstaApto_SinMensaje();  // reutiliza la anterior

            if (!apto)
            {
                MessageBox.Show("DONANTE NO APTO\n\nNo se puede guardar hasta corregir los problemas.",
                                "Evaluación Médica", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return apto;
        }

        private void txtPresion_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPresion_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '/' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; 
                return;
            }

            //  Evita más de una barra
            if (e.KeyChar == '/' && txtPresion.Text.Contains("/"))
            {
                e.Handled = true;
                return;
            }

            //  Evita más de 3 dígitos antes de la barra
            if (char.IsDigit(e.KeyChar) && !txtPresion.Text.Contains("/") &&
                (txtPresion.Text.Length >= 3 && txtPresion.SelectionStart > 2))
            {
                e.Handled = true;
                return;
            }

            //  Evita más de 2 dígitos después de la barra
            if (char.IsDigit(e.KeyChar) && txtPresion.Text.Contains("/") &&
                txtPresion.Text.IndexOf('/') < txtPresion.SelectionStart - 1)
            {
                int posBarra = txtPresion.Text.IndexOf('/');
                if (txtPresion.Text.Length - posBarra - 1 >= 2)
                {
                    e.Handled = true;
                    return;
                }
            }

          
            if (char.IsDigit(e.KeyChar) && !txtPresion.Text.Contains("/") &&
                txtPresion.Text.Length == 2 && txtPresion.SelectionStart == 2)
            {
                txtPresion.Text += "/";
                txtPresion.SelectionStart = txtPresion.Text.Length;
            }
        }
}
}

