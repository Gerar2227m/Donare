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
        public string IdDonanteSeleccionado { get; private set; }

        public frmBuscarDonante()
        {
            InitializeComponent();
            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            // Configurar el ComboBox (lo que ya tienes arriba)
            cmbCriterio.Items.AddRange(new[] { "Nombre Completo", "DUI", "Código" });
            cmbCriterio.SelectedIndex = 0; // por defecto busca por nombre

            // Doble clic en el grid selecciona y cierra
            dgvResultados.DoubleClick += (s, e) => Seleccionar();
            dgvResultados.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) Seleccionar(); };

            // Enter en el textbox hace búsqueda automática
            txtValor.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) btnBuscar.PerformClick(); };
        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            string texto = txtValor.Text.Trim();
            string opcion = cmbCriterio.Text;

            if (string.IsNullOrEmpty(texto))
            {
                CargarTodos();
                return;
            }

            string sql = "";
            switch (opcion)
            {
                case "Nombre Completo":
                    sql = "SELECT IdDonante, NombreCompleto, NumeroDocumento AS DUI, Telefono FROM Donante WHERE NombreCompleto LIKE @texto ORDER BY NombreCompleto";
                    break;
                case "DUI":
                    sql = "SELECT IdDonante, NombreCompleto, NumeroDocumento AS DUI, Telefono FROM Donante WHERE NumeroDocumento LIKE @texto ORDER BY NombreCompleto";
                    break;
                case "Código":
                    sql = "SELECT IdDonante, NombreCompleto, NumeroDocumento AS DUI, Telefono FROM Donante WHERE IdDonante LIKE @texto ORDER BY NombreCompleto";
                    break;
            }

            using (var con = conexion.ConexionServer())
            using (var cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@texto", "%" + texto + "%");
                var da = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);
                dgvResultados.DataSource = dt;
            }
        }

        private void CargarTodos()
        {
            using (var con = conexion.ConexionServer())
            {
                string sql = "SELECT IdDonante, NombreCompleto, NumeroDocumento AS DUI, Telefono FROM Donante ORDER BY NombreCompleto";
                var da = new SqlDataAdapter(sql, con);
                var dt = new DataTable();
                da.Fill(dt);
                dgvResultados.DataSource = dt;
            }
        }

        private void Seleccionar()
        {
            if (dgvResultados.CurrentRow != null)
            {
                IdDonanteSeleccionado = dgvResultados.CurrentRow.Cells["IdDonante"].Value.ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        // Opcional: cargar todos al abrir
        private void frmBuscarDonante_Load(object sender, EventArgs e)
        {
            CargarTodos();
        }

        private void dgvResultados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvResultados.CurrentRow != null)
            {
                // Aquí guardamos el ID del donante seleccionado
                IdDonanteSeleccionado = dgvResultados.CurrentRow.Cells["IdDonante"].Value.ToString();

                // Esto hace que tu código actual en frmDonantes siga funcionando
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}

