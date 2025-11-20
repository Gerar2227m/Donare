using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Donare
{
    public partial class frmBuscarDonaciones1 : Form
    {
        private readonly cConexion conexionDB = new cConexion();
        public string IdDonacionSeleccionada { get; private set; }

        public frmBuscarDonaciones1()
        {
            InitializeComponent();
            CargarDonaciones();
            txtFiltro.Focus();
            dgvDonaciones.DoubleClick += dgvDonaciones_DoubleClick;
            txtFiltro.TextChanged += txtFiltro_TextChanged;
        }

        private void CargarDonaciones()
        {
            try
            {
                using (SqlConnection con = conexionDB.ConexionServer())
                {
                    con.Open();

                    string sql = @"
                        SELECT d.IdDonacion, d.FechaDonacion, don.NombreCompleto AS Donante, d.TipoDonacion
                        FROM Donacion d
                        INNER JOIN Donante don ON d.IdDonante = don.IdDonante
                        ORDER BY d.IdDonacion DESC";

                    SqlDataAdapter da = new SqlDataAdapter(sql, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvDonaciones.AutoGenerateColumns = true;
                    dgvDonaciones.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar donaciones: " + ex.Message);
            }
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            if (dgvDonaciones.DataSource is DataTable dt)
            {
                string filtro = txtFiltro.Text.Trim().Replace("'", "''");

                if (string.IsNullOrEmpty(filtro))
                    dt.DefaultView.RowFilter = "";
                else
                    dt.DefaultView.RowFilter = $"Donante LIKE '%{filtro}%'";
            }
        }

        private void dgvDonaciones_DoubleClick(object sender, EventArgs e)
        {
            if (dgvDonaciones.CurrentRow != null && dgvDonaciones.CurrentRow.Index >= 0)
            {
                IdDonacionSeleccionada = dgvDonaciones.CurrentRow.Cells["IdDonacion"].Value.ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void frmBuscarDonaciones1_Load(object sender, EventArgs e)
        {

        }
    }
}
