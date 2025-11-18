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
    public partial class frmBuscarDonante : Form
    {
        private cConexion conexion = new cConexion();
        public string IdDonanteSeleccionado { get; private set; } = "";

        public frmBuscarDonante()
        {
            InitializeComponent();
            Configurar();
        }

        private void Configurar()
        {
            // Opciones del ComboBox
            cmbCriterio.Items.AddRange(new object[]
            {
                "DUI",
                "Nombre del donante",
                "Teléfono",
                "Código de donante (DON-00001)",
                "Tipo de sangre (A+, O-, etc.)"
            });
            cmbCriterio.SelectedIndex = 0;
            txtValor.Focus();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            
        }

        // Doble clic → selecciona y cierra
        private void dgvResultados_DoubleClick(object sender, EventArgs e)
        {
            if (dgvResultados.CurrentRow != null && dgvResultados.CurrentRow.Cells["IdDonante"].Value != null)
            {
                IdDonanteSeleccionado = dgvResultados.CurrentRow.Cells["IdDonante"].Value.ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        // Enter en el TextBox también busca
        private void txtValor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBuscar.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtValor.Text))
            {
                MessageBox.Show("Escribe algo para buscar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string valor = $"%{txtValor.Text.Trim()}%";
            string sql = "";

            switch (cmbCriterio.SelectedIndex)
            {
                case 0: // DUI
                    sql = "SELECT IdDonante, NombreCompleto, NumeroDocumento AS DUI, Telefono FROM Donante WHERE NumeroDocumento LIKE @val";
                    break;
                case 1: // Nombre
                    sql = "SELECT IdDonante, NombreCompleto, NumeroDocumento AS DUI, Telefono FROM Donante WHERE NombreCompleto LIKE @val";
                    break;
                case 2: // Teléfono
                    sql = "SELECT IdDonante, NombreCompleto, NumeroDocumento AS DUI, Telefono FROM Donante WHERE Telefono LIKE @val";
                    break;
                case 3: // Código de donante
                    sql = "SELECT IdDonante, NombreCompleto, NumeroDocumento AS DUI, Telefono FROM Donante WHERE IdDonante LIKE @val";
                    break;
                case 4: // Tipo de sangre (última donación)
                    sql = @"SELECT DISTINCT d.IdDonante, d.NombreCompleto, d.NumeroDocumento AS DUI, ts.Grupo + ts.Rh AS TipoSangre
                            FROM Donante d
                            INNER JOIN Donacion don ON d.IdDonante = don.IdDonante
                            INNER JOIN BolsaSangre b ON don.IdDonacion = b.IdDonacion
                            INNER JOIN TipoSangre ts ON b.IdTipoSangre = ts.IdTipoSangre
                            WHERE ts.Grupo + ts.Rh LIKE @val";
                    break;
            }

            SqlConnection con = conexion.ConexionServer();
            try
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@val", valor);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvResultados.DataSource = dt;

                    if (dt.Rows.Count == 0)
                        MessageBox.Show("No se encontraron resultados.", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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
}

