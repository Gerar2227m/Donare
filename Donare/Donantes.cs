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
            numTalla.ValueChanged += (s, e) => { ActualizarIMC(); EvaluarSiEstaApto_SinMensaje(); };
            numPulso.ValueChanged += (s, e) => EvaluarSiEstaApto_SinMensaje();
            numTemperatura.ValueChanged += (s, e) => EvaluarSiEstaApto_SinMensaje();
            numHemoglobina.ValueChanged += (s, e) => EvaluarSiEstaApto_SinMensaje();
            dtpFechaNacimiento.ValueChanged += (s, e) => EvaluarSiEstaApto_SinMensaje();
            cmbSexo.SelectedIndexChanged += (s, e) => EvaluarSiEstaApto_SinMensaje();

            txtPresion.TextChanged += (s, e) => EvaluarSiEstaApto_SinMensaje();
            txtPresion.KeyPress += txtPresion_KeyPress;  

            chkVIH.CheckedChanged += (s, e) => EvaluarSiEstaApto_SinMensaje();
            chkHepB.CheckedChanged += (s, e) => EvaluarSiEstaApto_SinMensaje();
            chkHepB.CheckedChanged += (s, e) => EvaluarSiEstaApto_SinMensaje();
            chkHepC.CheckedChanged += (s, e) => EvaluarSiEstaApto_SinMensaje();
            chkSifilis.CheckedChanged += (s, e) => EvaluarSiEstaApto_SinMensaje();

            ControlarEstadoCampos();
        }

        private void ConfigurarControles()
        {
            txtIdDonante.ReadOnly = true;
            txtEdad.ReadOnly = true;
            dtpFechaRegistro.Enabled = false;

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
            cmbRh.Items.AddRange(new[] { "+", "-" });
        }

 
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarTodo();
            esNuevo = true;
            modoEdicion = false;
            idDonanteActual = GenerarIdDonante();
            txtIdDonante.Text = idDonanteActual;
            dtpFechaRegistro.Value = DateTime.Today;
            txtNombreCompleto.Focus();

            ControlarEstadoCampos();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (var buscar = new frmBuscarDonante())
            {
                if (buscar.ShowDialog() != DialogResult.OK || string.IsNullOrEmpty(buscar.IdDonanteSeleccionado))
                    return;

                idDonanteActual = buscar.IdDonanteSeleccionado;

                const string sql = @"
            SELECT 
                d.*, 
                em.Peso, em.Talla, em.PresionArterial, em.Pulso, em.Temperatura, em.Hemoglobina,
                em.VIH, em.HepatitisB, em.HepatitisC, em.Sifilis, em.Chagas,
                em.HistorialRelevante, em.Apto,
                ts.Grupo, ts.Rh
            FROM Donante d
            LEFT JOIN EvaluacionMedica em ON d.IdDonante = em.IdDonante
                AND em.FechaEvaluacion = (SELECT MAX(FechaEvaluacion) 
                                          FROM EvaluacionMedica 
                                          WHERE IdDonante = d.IdDonante)
            LEFT JOIN TipoSangre ts ON em.IdTipoSangre = ts.IdTipoSangre
            WHERE d.IdDonante = @id";

                using (var con = conexion.ConexionServer())
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id", idDonanteActual);

                    try
                    {
                        con.Open();
                        using (var dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                MessageBox.Show("Donante no encontrado.");
                                return;
                            }

                            // DATOS PERSONALES
                            txtIdDonante.Text = dr["IdDonante"].ToString();
                            txtNombreCompleto.Text = dr["NombreCompleto"].ToString();
                            dtpFechaNacimiento.Value = Convert.ToDateTime(dr["FechaNacimiento"]);
                            cmbSexo.Text = dr["SexoBiologico"].ToString();
                            cmbTipoDocumento.Text = dr["TipoDocumento"].ToString();
                            txtNumeroDocumento.Text = dr["NumeroDocumento"].ToString();
                            txtTelefono.Text = dr["Telefono"]?.ToString() ?? "";
                            txtDireccion.Text = dr["Direccion"]?.ToString() ?? "";
                            dtpFechaRegistro.Value = Convert.ToDateTime(dr["FechaRegistro"]);

                            // DATOS MÉDICOS 
                            if (!dr.IsDBNull(dr.GetOrdinal("Peso")))
                            {
                                numPeso.Value = Convert.ToDecimal(dr["Peso"]);
                                numTalla.Value = Convert.ToDecimal(dr["Talla"]);
                                txtPresion.Text = dr["PresionArterial"]?.ToString() ?? "";
                                numPulso.Value = Convert.ToInt32(dr["Pulso"]);
                                numTemperatura.Value = Convert.ToDecimal(dr["Temperatura"]);
                                numHemoglobina.Value = Convert.ToDecimal(dr["Hemoglobina"]);

                                cmbGrupoSanguineo.Text = dr["Grupo"]?.ToString() ?? "";
                                cmbRh.Text = dr["Rh"]?.ToString() ?? "";

                                chkVIH.Checked = (bool)dr["VIH"];
                                chkHepB.Checked = (bool)dr["HepatitisB"];
                                chkHepC.Checked = (bool)dr["HepatitisC"];
                                chkSifilis.Checked = (bool)dr["Sifilis"];
                                txtHistorialRelevante.Text = dr["HistoriaRelevante"]?.ToString() ?? "";
                                chkApto.Checked = (bool)dr["Apto"];
                            }
                            else
                            {
                                // limpia datos médicos si no hay evaluación
                                numPeso.Value = numTalla.Value = numPulso.Value = numTemperatura.Value = numHemoglobina.Value = 0;
                                txtPresion.Clear();
                                cmbGrupoSanguineo.Text = cmbRh.Text = "";
                                chkVIH.Checked = chkHepB.Checked = chkHepC.Checked = chkSifilis.Checked = chkApto.Checked = false;
                                txtHistorialRelevante.Clear();
                            }

                            ActualizarIMC(); //  función del IMC

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
            ControlarEstadoCampos();
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
                return; //  NO GUARDA si no está apto
            }

            SqlConnection con = conexion.ConexionServer();
            SqlTransaction tran = null;
            try
            {
                con.Open();
                tran = con.BeginTransaction();

                
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

                //  Nueva evaluación médica
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
                    cmd.Parameters.AddWithValue("@rh", cmbRh.Text);  
                    cmd.Parameters.AddWithValue("@vih", chkVIH.Checked);
                    cmd.Parameters.AddWithValue("@hepb", chkHepB.Checked);
                    cmd.Parameters.AddWithValue("@hepc", chkHepC.Checked);
                    cmd.Parameters.AddWithValue("@sifi", chkSifilis.Checked);
                    cmd.Parameters.AddWithValue("@chag", "");
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
            
        }

        // Función para generar ID 
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
            //  código de limpiar
            idDonanteActual = "";
            esNuevo = true;
            modoEdicion = false;
        }

        private void Donantes_Load(object sender, EventArgs e)
        {

        }

        private void ConfigurarNumericUpDowns()
        {
            // TALLA (altura en cm) 
            numTalla.DecimalPlaces = 0;    
            numTalla.Increment = 1;
            numTalla.Minimum = 100;
            numTalla.Maximum = 220;
            numTalla.Value = 170;

            // PESO  kilo
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

        private void ActualizarIMC()
        {
            if (numTalla.Value > 0 && numPeso.Value > 0)
            {
                double alturaMetros = (double)numTalla.Value / 100;
                double imc = (double)numPeso.Value / (alturaMetros * alturaMetros);

                string estado = imc < 18.5 ? "Bajo peso" :
                                imc < 25 ? "Peso normal" :
                                imc < 30 ? "Sobrepeso" :
                                imc < 35 ? "Obesidad grado I" :
                                imc < 40 ? "Obesidad grado II" : "Obesidad grado III";

                Color color = imc < 18.5 ? Color.Orange :
                              imc < 25 ? Color.Green :
                              imc < 30 ? Color.Orange : Color.Red;

                lblIMC.Text = $"IMC: {imc:F1} → {estado}";
                lblIMC.ForeColor = color;
                lblIMC.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            }
            else
            {
                lblIMC.Text = "IMC: -";
                lblIMC.ForeColor = Color.Black;
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
            chkApto.Refresh();  
            return apto;
        }

        private bool EvaluarSiEstaApto_ConMensaje()
        {
            bool apto = EvaluarSiEstaApto_SinMensaje();  

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

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            if (!ValidarDatos()) return;
            if (!esNuevo && !modoEdicion)
            {
                MessageBox.Show("Presiona MODIFICAR para editar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!EvaluarSiEstaApto_ConMensaje())
            {
                return; // NO GUARDA si no está apto
            }

            SqlConnection con = conexion.ConexionServer();
            SqlTransaction tran = null;
            try
            {
                con.Open();
                tran = con.BeginTransaction();

           
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
                    cmd.Parameters.AddWithValue("@rh", cmbRh.Text);
                    cmd.Parameters.AddWithValue("@vih", chkVIH.Checked);
                    cmd.Parameters.AddWithValue("@hepb", chkHepB.Checked);
                    cmd.Parameters.AddWithValue("@hepc", chkHepC.Checked);
                    cmd.Parameters.AddWithValue("@sifi", chkSifilis.Checked);
                    cmd.Parameters.AddWithValue("@chag", "");
                    cmd.Parameters.AddWithValue("@hist", txtHistorialRelevante.Text.Trim());
                    cmd.Parameters.AddWithValue("@apto", chkApto.Checked);
                    cmd.ExecuteNonQuery();
                }

                tran.Commit();
                MessageBox.Show(esNuevo ? "Donante registrado con éxito!" : "Datos actualizados correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                esNuevo = false;
                modoEdicion = false;
                ControlarEstadoCampos();

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

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            using (var buscar = new frmBuscarDonante())
            {
                if (buscar.ShowDialog() != DialogResult.OK || string.IsNullOrEmpty(buscar.IdDonanteSeleccionado))
                    return;

                idDonanteActual = buscar.IdDonanteSeleccionado;

               
                string sql = @"
            SELECT 
                d.*, 
                em.Peso, em.Talla, em.PresionArterial, em.Pulso, em.Temperatura,
                em.Hemoglobina, em.VIH, em.HepatitisB, em.HepatitisC, em.Sifilis,
                em.Chagas, em.HistorialRelevante, em.Apto,
                ts.Grupo, ts.Rh
            FROM Donante d
            LEFT JOIN EvaluacionMedica em ON d.IdDonante = em.IdDonante
                AND em.FechaEvaluacion = (
                    SELECT MAX(FechaEvaluacion) 
                    FROM EvaluacionMedica 
                    WHERE IdDonante = d.IdDonante
                )
            LEFT JOIN TipoSangre ts ON em.IdTipoSangre = ts.IdTipoSangre
            WHERE d.IdDonante = @id";

                using (var con = conexion.ConexionServer())
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id", idDonanteActual);

                    try
                    {
                        con.Open();
                        using (var dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                MessageBox.Show("Donante no encontrado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            // === DATOS PERSONALES ===
                            txtIdDonante.Text = dr["IdDonante"].ToString();
                            txtNombreCompleto.Text = dr["NombreCompleto"].ToString();
                            dtpFechaNacimiento.Value = Convert.ToDateTime(dr["FechaNacimiento"]);
                            cmbSexo.Text = dr["SexoBiologico"].ToString();
                            cmbTipoDocumento.Text = dr["TipoDocumento"].ToString();
                            txtNumeroDocumento.Text = dr["NumeroDocumento"].ToString();
                            txtTelefono.Text = dr["Telefono"]?.ToString() ?? "";
                            txtDireccion.Text = dr["Direccion"]?.ToString() ?? "";
                            dtpFechaRegistro.Value = Convert.ToDateTime(dr["FechaRegistro"]);

                            // === DATOS MÉDICOS (solo si tiene evaluación) ===
                            if (!dr.IsDBNull(dr.GetOrdinal("Peso")))
                            {
                                numPeso.Value = Convert.ToDecimal(dr["Peso"]);
                                numTalla.Value = Convert.ToDecimal(dr["Talla"]);
                                txtPresion.Text = dr["PresionArterial"]?.ToString() ?? "";
                                numPulso.Value = Convert.ToInt32(dr["Pulso"]);
                                numTemperatura.Value = Convert.ToDecimal(dr["Temperatura"]);
                                numHemoglobina.Value = Convert.ToDecimal(dr["Hemoglobina"]);

                                // TIPO DE SANGRE
                                cmbGrupoSanguineo.Text = dr["Grupo"]?.ToString() ?? "";
                                cmbRh.Text = dr["Rh"]?.ToString() ?? "";

                                chkVIH.Checked = (bool)dr["VIH"];
                                chkHepB.Checked = (bool)dr["HepatitisB"];
                                chkHepC.Checked = (bool)dr["HepatitisC"];
                                chkSifilis.Checked = (bool)dr["Sifilis"];
                                
                                txtHistorialRelevante.Text = dr["HistorialRelevante"]?.ToString() ?? "";
                                chkApto.Checked = (bool)dr["Apto"];
                            }
                            else
                            {
                                // Limpiar datos médicos si no hay evaluación
                                numPeso.Value = numTalla.Value = numPulso.Value = numTemperatura.Value = numHemoglobina.Value = 0;
                                txtPresion.Clear();
                                cmbGrupoSanguineo.Text = cmbRh.Text = "";
                                chkVIH.Checked = chkHepB.Checked = chkHepC.Checked = chkSifilis.Checked = chkApto.Checked = false;
                                txtHistorialRelevante.Clear();
                            }

                            // Actualizar IMC y pestaña
                            ActualizarIMC();
                            esNuevo = false;
                            modoEdicion = true;
                            tabControl1.SelectedTab = tabpersonal;

                            // === ACTIVAR BOTONES DE EDICIÓN ===
                            btnModificar.Enabled = true;
                            btnEliminar.Enabled = true;
                            btnGuardar.Enabled = false;

                            MessageBox.Show("Donante cargado correctamente.", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al cargar donante: " + ex.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
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

        private void ControlarEstadoCampos()
        {
            bool habilitado = esNuevo || modoEdicion;

            // Datos personales
            txtNombreCompleto.ReadOnly = !habilitado;
            dtpFechaNacimiento.Enabled = habilitado;
            cmbSexo.Enabled = habilitado;
            cmbTipoDocumento.Enabled = habilitado;
            txtNumeroDocumento.ReadOnly = !habilitado;
            txtTelefono.ReadOnly = !habilitado;
            txtDireccion.ReadOnly = !habilitado;

            // Evaluación médica
            numPeso.Enabled = habilitado;
            numTalla.Enabled = habilitado;
            txtPresion.ReadOnly = !habilitado;
            numPulso.Enabled = habilitado;
            numTemperatura.Enabled = habilitado;
            numHemoglobina.Enabled = habilitado;
            cmbGrupoSanguineo.Enabled = habilitado;
            cmbRh.Enabled = habilitado;
            chkVIH.Enabled = habilitado;
            chkHepB.Enabled = habilitado;
            chkHepC.Enabled = habilitado;
            chkSifilis.Enabled = habilitado;
            txtHistorialRelevante.ReadOnly = !habilitado;
            chkApto.Enabled = false;

            // Botones
            btnGuardar.Enabled = habilitado;
            btnModificar.Enabled = !esNuevo && !modoEdicion;
            btnEliminar.Enabled = !esNuevo;
            btnNuevo.Enabled = true;
        }

        private void numPeso_ValueChanged(object sender, EventArgs e)
        {
            ActualizarIMC();
        }

        private void numTalla_ValueChanged(object sender, EventArgs e)
        {
            ActualizarIMC();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

