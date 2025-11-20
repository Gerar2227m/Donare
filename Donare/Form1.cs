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
    public partial class FrmLogin : Form


    {
        cConexion conexion = new cConexion();

        public FrmLogin()
        {
            InitializeComponent();
            txtContrasena.UseSystemPasswordChar = true;

        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)

        {
            string usuario = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Text.Trim();
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contrasena))
            {
                MessageBox.Show("Por favor ingresa usuario y contraseña.");
                return;
            }

            try
            {
                using (SqlConnection conn = conexion.ConexionServer())
                {
                    conn.Open();
                    string query = "SELECT tipo_usuario FROM Usuarios  WHERE nombre_usuario = @usuario AND contraseña = @contrasena";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@usuario", usuario);
                        cmd.Parameters.AddWithValue("@contrasena", contrasena);

                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            int tipoUsuario = Convert.ToInt32(result);

                            //  Pasamos el tipo de usuario al formulario Inicio
                            Inicio inicio = new Inicio(tipoUsuario);
                            inicio.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Usuario o contraseña incorrectos ❌");
                        }

                    }
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar: " + ex.Message);
            }
        }

        private void CrearUsuario_Click(object sender, EventArgs e)
        {
            
        }
    }
}
 
