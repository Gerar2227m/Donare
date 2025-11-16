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
    public partial class RegistrarUsuario : Form
    {

        cConexion conexion = new cConexion();

        private int usuarioActualId = 0;
        private bool modoEdicion = false;
        private DataTable dtUsuarios;
        private int indiceActual = 0;
        public RegistrarUsuario()
        {
            InitializeComponent();
            ConfigurarControles();
            CargarTodosLosUsuarios();
            MostrarPrimerUsuario();
            dtpFechaNacimiento.Format = DateTimePickerFormat.Short;

            cmbGenero.Items.Add("Masculino");
            cmbGenero.Items.Add("Femenino");
            cmbGenero.Items.Add("Otro");
            cmbGenero.SelectedIndex = 0;

            cmbtipo.Items.Add("Administrador");
            cmbtipo.Items.Add("Operador");
            cmbtipo.SelectedIndex = 0;

        }
        private void ConfigurarControles()
        {
            dtpFechaNacimiento.Format = DateTimePickerFormat.Short;
            cmbGenero.Items.AddRange(new string[] { "Masculino", "Femenino", "Otro" });
            cmbtipo.Items.AddRange(new string[] { "Administrador", "Operador" });
            cmbGenero.SelectedIndex = 0;
            cmbtipo.SelectedIndex = 0;

            // Al inicio solo navegación, no edición
            DeshabilitarEdicion();
        }

        private void CargarTodosLosUsuarios()
        {
            string query = @"SELECT id_usuario, nombre_usuario, nombre, apellido, email, 
                            fechanacimiento, genero, tipo_usuario 
                            FROM Usuarios ORDER BY id_usuario";

            try
            {
                using (SqlConnection conn = conexion.ConexionServer())
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    dtUsuarios = new DataTable();
                    da.Fill(dtUsuarios);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message);
                dtUsuarios = new DataTable();
            }
        }
        private void MostrarPrimerUsuario()
        {
            if (dtUsuarios.Rows.Count > 0)
            {
                indiceActual = 0;
                MostrarUsuario(indiceActual);
            }
            else
            {
                LimpiarCampos();
                DeshabilitarEdicion();
            }
            ActualizarEstadoNavegacion();
        }

        private void MostrarUsuario(int indice)
        {
            if (indice < 0 || indice >= dtUsuarios.Rows.Count) return;

            DataRow row = dtUsuarios.Rows[indice];
            usuarioActualId = Convert.ToInt32(row["id_usuario"]);
            txtNombreUsuario.Text = row["nombre_usuario"].ToString();
            txtNombre.Text = row["nombre"].ToString();
            txtApellido.Text = row["apellido"].ToString();
            txtEmail.Text = row["email"].ToString();
            txtContraseña.Clear(); // Nunca mostramos la contraseña
            dtpFechaNacimiento.Value = Convert.ToDateTime(row["fechanacimiento"]);
            cmbGenero.SelectedIndex = Convert.ToInt32(row["genero"]) - 1;
            cmbtipo.SelectedIndex = Convert.ToInt32(row["tipo_usuario"]) - 1;

            modoEdicion = false;
            DeshabilitarEdicion();
        }


        // 4. LIMPIAR CAMPOS

        private void LimpiarCampos()
        {
            txtNombreUsuario.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtEmail.Clear();
            txtContraseña.Clear();
            dtpFechaNacimiento.Value = DateTime.Today;
            cmbGenero.SelectedIndex = 0;
            cmbtipo.SelectedIndex = 0;
            usuarioActualId = 0;
        }


        // 5. HABILITAR / DESHABILITAR EDICIÓN

        private void HabilitarEdicion()
        {
            txtNombreUsuario.Enabled = txtNombre.Enabled = txtApellido.Enabled =
            txtEmail.Enabled = txtContraseña.Enabled = dtpFechaNacimiento.Enabled =
            cmbGenero.Enabled = cmbtipo.Enabled = true;
            btnGuardar_Click.Enabled = true;
        }

        private void DeshabilitarEdicion()
        {
            txtNombreUsuario.Enabled = txtNombre.Enabled = txtApellido.Enabled =
            txtEmail.Enabled = txtContraseña.Enabled = dtpFechaNacimiento.Enabled =
            cmbGenero.Enabled = cmbtipo.Enabled = false;
            btnGuardar_Click.Enabled = false;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime fechaNac = dtpFechaNacimiento.Value;
            int edad = DateTime.Today.Year - fechaNac.Year;
            if (DateTime.Today < fechaNac.AddYears(edad)) edad--;
            if (edad < 18)
            {
                MessageBox.Show("Debes tener al menos 18 años.", "Edad no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsValidEmail(txtEmail.Text.Trim()))
            {
                MessageBox.Show("Correo electrónico inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombreUsuario.Text) ||
                string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                (!modoEdicion && string.IsNullOrWhiteSpace(txtContraseña.Text))) // solo obligatorio al crear
            {
                MessageBox.Show("Todos los campos obligatorios deben estar llenos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = conexion.ConexionServer())
                {
                    conn.Open();

                    // ==== VERIFICAR NOMBRE DE USUARIO DUPLICADO (excepto el registro actual) ====
                    string checkQuery = "SELECT COUNT(*) FROM Usuarios WHERE nombre_usuario = @nombre_usuario AND id_usuario != @id";
                    using (SqlCommand cmdCheck = new SqlCommand(checkQuery, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@nombre_usuario", txtNombreUsuario.Text.Trim());
                        cmdCheck.Parameters.AddWithValue("@id", usuarioActualId); // si es nuevo, id=0 → no afecta
                        int existe = (int)cmdCheck.ExecuteScalar();
                        if (existe > 0)
                        {
                            MessageBox.Show("Este nombre de usuario ya está en uso por otro registro.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtNombreUsuario.Focus();
                            return;
                        }
                    }

                    SqlCommand cmd;

                    if (modoEdicion && usuarioActualId > 0)
                    {
                        // ——— ACTUALIZAR (UPDATE) ———
                        string updateQuery = @"UPDATE Usuarios SET 
                    nombre_usuario = @nombre_usuario,
                    nombre = @nombre,
                    apellido = @apellido,
                    email = @email,
                    fechanacimiento = @fechanacimiento,
                    genero = @genero,
                    tipo_usuario = @tipo_usuario";

                        // Solo actualiza contraseña si el usuario escribió algo
                        if (!string.IsNullOrWhiteSpace(txtContraseña.Text))
                            updateQuery += ", contraseña = @contraseña";

                        updateQuery += " WHERE id_usuario = @id";

                        cmd = new SqlCommand(updateQuery, conn);
                        cmd.Parameters.AddWithValue("@id", usuarioActualId);
                    }
                    else
                    {
                        // ——— CREAR NUEVO (INSERT) ———
                        cmd = new SqlCommand(@"INSERT INTO Usuarios 
                    (nombre_usuario, nombre, apellido, email, fechanacimiento, genero, contraseña, tipo_usuario)
                    VALUES (@nombre_usuario, @nombre, @apellido, @email, @fechanacimiento, @genero, @contraseña, @tipo_usuario)", conn);
                    }

                    // Parámetros comunes
                    cmd.Parameters.AddWithValue("@nombre_usuario", txtNombreUsuario.Text.Trim());
                    cmd.Parameters.AddWithValue("@nombre", txtNombre.Text.Trim());
                    cmd.Parameters.AddWithValue("@apellido", txtApellido.Text.Trim());
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@fechanacimiento", fechaNac);
                    cmd.Parameters.AddWithValue("@genero", cmbGenero.SelectedIndex + 1);
                    cmd.Parameters.AddWithValue("@tipo_usuario", cmbtipo.SelectedIndex + 1);

                    // Solo agrega contraseña si es nuevo o si la escribió en edición
                    if (!modoEdicion || !string.IsNullOrWhiteSpace(txtContraseña.Text))
                        cmd.Parameters.AddWithValue("@contraseña", txtContraseña.Text);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show(modoEdicion ? "¡Usuario actualizado con éxito!" : "¡Usuario registrado con éxito!",
                                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refrescar todo y volver al modo lectura
                CargarTodosLosUsuarios();

                if (!modoEdicion)
                    indiceActual = dtUsuarios.Rows.Count - 1; // el nuevo queda al final

                MostrarUsuario(indiceActual);
                DeshabilitarEdicion();
                modoEdicion = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevo_Click_Click(object sender, EventArgs e)
        {
            LimpiarCampos();           
            HabilitarEdicion();      
            modoEdicion = false;       
            usuarioActualId = 0;      
            txtNombreUsuario.Focus();  
        }

        private void btnGuardar_Click_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void btnSalir_Click_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrimero_Click(object sender, EventArgs e)
        {
            indiceActual = 0;
            MostrarUsuario(indiceActual);
            ActualizarEstadoNavegacion();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (indiceActual > 0)
            {
                indiceActual--;
                MostrarUsuario(indiceActual);
                ActualizarEstadoNavegacion();
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (indiceActual < dtUsuarios.Rows.Count - 1)
            {
                indiceActual++;
                MostrarUsuario(indiceActual);
                ActualizarEstadoNavegacion();
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            indiceActual = dtUsuarios.Rows.Count - 1;
            MostrarUsuario(indiceActual);
            ActualizarEstadoNavegacion();
        }

        private void ActualizarEstadoNavegacion()
        {
            btnPrimero.Enabled = btnAnterior.Enabled = (indiceActual > 0);
            btnSiguiente.Enabled = btnUltimo.Enabled = (indiceActual < dtUsuarios.Rows.Count - 1);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (usuarioActualId == 0)
            {
                MessageBox.Show("No hay usuario seleccionado para editar.");
                return;
            }
            modoEdicion = true;
            HabilitarEdicion();
            txtNombre.Focus();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (usuarioActualId == 0) return;

            if (MessageBox.Show($"¿Seguro que deseas eliminar al usuario {txtNombreUsuario.Text}?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = conexion.ConexionServer())
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM Usuarios WHERE id_usuario=@id", conn);
                        cmd.Parameters.AddWithValue("@id", usuarioActualId);
                        cmd.ExecuteNonQuery();
                    }

                    CargarTodosLosUsuarios();
                    if (indiceActual >= dtUsuarios.Rows.Count && indiceActual > 0) indiceActual--;
                    MostrarUsuario(indiceActual);
                    MessageBox.Show("Usuario eliminado.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message);
                }
            }
        }
    }
}