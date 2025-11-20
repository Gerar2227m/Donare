using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Donare
{
    public partial class frmBuscarDonacion : Form
    {
        private readonly cConexion conexionDB = new cConexion();
        public string IdDonanteSeleccionado { get; private set; }

        public frmBuscarDonacion()
        {
            InitializeComponent();
            CargarTodosLosDonantes();
            txtFiltro.Focus();
        }

        private void CargarTodosLosDonantes()
        {
            try
            {
                using (SqlConnection con = conexionDB.ConexionServer())
                {
                    con.Open();
                    string sql = @"
                        SELECT d.IdDonante, d.NombreCompleto, d.Direccion, d.Telefono
                        FROM Donante d
                        INNER JOIN EvaluacionMedica em ON d.IdDonante = em.IdDonante AND em.Apto = 1
                        ORDER BY d.NombreCompleto";

                    SqlDataAdapter da = new SqlDataAdapter(sql, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvDonantes.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar donantes: " + ex.Message);
            }
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            if (dgvDonantes.DataSource is DataTable dt)
            {
                string filtro = txtFiltro.Text.Trim();
                if (string.IsNullOrEmpty(filtro))
                    dt.DefaultView.RowFilter = "";
                else
                    dt.DefaultView.RowFilter = $"NombreCompleto LIKE '%{filtro}%'";
            }
        }

        private void dgvDonantes_DoubleClick(object sender, EventArgs e)
        {
            if (dgvDonantes.CurrentRow != null)
            {
                IdDonanteSeleccionado = dgvDonantes.CurrentRow.Cells["IdDonante"].Value.ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void txtFiltro_TextChanged_1(object sender, EventArgs e)
        {
            if (dgvDonantes.DataSource is DataTable dt)
            {
                string filtro = txtFiltro.Text.Trim().Replace("'", "''");

                if (string.IsNullOrEmpty(filtro))
                {
                    dt.DefaultView.RowFilter = "";
                }
                else
                {
                    dt.DefaultView.RowFilter = $"NombreCompleto LIKE '%{filtro}%'";
                }
            }
        }

        private void dgvDonantes_DoubleClick_1(object sender, EventArgs e)
        {
            if (dgvDonantes.CurrentRow != null && dgvDonantes.CurrentRow.Index >= 0)
            {
                IdDonanteSeleccionado = dgvDonantes.CurrentRow.Cells["IdDonante"].Value.ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void frmBuscarDonacion_Load(object sender, EventArgs e)
        {

        }
    }
}