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
        public RegistrarUsuario()
        {
            InitializeComponent();
            dtpFechaNacimiento.Format = DateTimePickerFormat.Short;

            cmbGenero.Items.Add("Masculino");
            cmbGenero.Items.Add("Femenino");
            cmbGenero.Items.Add("Otro");
            cmbGenero.SelectedIndex = 0;

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



            // === VALIDAR MAYORÍA DE EDAD (18 AÑOS) ===
            DateTime fechaNac = dtpFechaNacimiento.Value;
            int edad = DateTime.Today.Year - fechaNac.Year;

            // Ajuste: si aún no cumplió años este año
            if (DateTime.Today < fechaNac.AddYears(edad))
            {
                edad--;
            }

            if (edad < 18)
            {
                MessageBox.Show("Debes tener al menos 18 años para registrarte.",
                                "Edad no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }

            string email = txtEmail.Text.Trim();
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Por favor, ingresa un correo electrónico válido.\nEjemplo: usuario@dominio.com",
                                "Correo inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombreUsuario.Text) ||
                string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtContraseña.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                MessageBox.Show("Ingresa un email válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            int genero = cmbGenero.SelectedIndex + 1; 


            string query = @"INSERT INTO Usuarios 
        (nombre_usuario, nombre, apellido, email, fechanacimiento, genero, contraseña) 
        VALUES 
        (@nombre_usuario, @nombre, @apellido, @email, @fechanacimiento, @genero, @contraseña)";

            try
            {
                using (SqlConnection conn = conexion.ConexionServer())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombre_usuario", txtNombreUsuario.Text.Trim());
                        cmd.Parameters.AddWithValue("@nombre", txtNombre.Text.Trim());
                        cmd.Parameters.AddWithValue("@apellido", txtApellido.Text.Trim());
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@fechanacimiento", fechaNac);
                        cmd.Parameters.AddWithValue("@genero", genero);
                        cmd.Parameters.AddWithValue("@contraseña", txtContraseña.Text); // Mejor: encriptar

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("¡Usuario registrado con éxito!", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error de base de datos",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
