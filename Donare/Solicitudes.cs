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
    public partial class Solicitudes : Form
    {
        public Solicitudes()
        {
            InitializeComponent();
            CargarTiposSangre();
            CargarCentros();
            GenerarIdAlIniciar();
            CargarUrgencias();
        }

        private void CargarUrgencias()
        {
            cmbUrgencia.Items.Clear();

            cmbUrgencia.Items.Add("Baja");
            cmbUrgencia.Items.Add("Normal");
            cmbUrgencia.Items.Add("Alta");
            cmbUrgencia.Items.Add("Emergencia");

            cmbUrgencia.SelectedIndex = 0; // Selecciona la primera por defecto
        }
        private void GenerarIdAlIniciar()
        {
            txtIdSolicitud.Text = GenerarIdSolicitud();
        }

        private string GenerarIdSolicitud()
        {
            string año = DateTime.Now.Year.ToString();
            string random = new Random().Next(1, 99999).ToString("D5");
            return $"SOL-{año}-{random}";
        }

        private void CargarTiposSangre()
        {
            cConexion con = new cConexion();
            using (SqlConnection cn = con.ConexionServer())
            {
                string query = "SELECT IdTipoSangre, Grupo + Rh AS Tipo FROM TipoSangre";

                SqlDataAdapter da = new SqlDataAdapter(query, cn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbTipoSangre.DataSource = dt;
                cmbTipoSangre.DisplayMember = "Tipo";
                cmbTipoSangre.ValueMember = "IdTipoSangre";
            }
        }

        private void CargarCentros()
        {
            cmbCentroMedico.Items.Clear();
            cmbCentroMedico.Items.Add("Emergencias Generales");
            cmbCentroMedico.Items.Add("Hospital General");
            cmbCentroMedico.Items.Add("Centro Médico Privado");
            cmbCentroMedico.Items.Add("Clínica Familiar");

            cmbCentroMedico.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbTipoSangre.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un tipo de sangre.");
                return;
            }

            cConexion con = new cConexion();
            using (SqlConnection cn = con.ConexionServer())
            {
                string query = @"SELECT CodigoBolsa, FechaExtraccion, FechaVencimiento, 
                                        VolumenML, Estado, Ubicacion
                                 FROM BolsaSangre
                                 WHERE IdTipoSangre = @tipo 
                                 AND Estado = 'Aprobada'";

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@tipo", Convert.ToInt32(cmbTipoSangre.SelectedValue));

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;

                if (dt.Rows.Count == 0)
                    MessageBox.Show("No hay bolsas disponibles para este tipo de sangre.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string idSolicitud = txtIdSolicitud.Text;  

            string centro = cmbCentroMedico.Text;
            string medico = txtMedico.Text;
            string motivo = txtMotivo.Text == "" ? "Sin motivo especificado" : txtMotivo.Text;
            string prioridad = cmbUrgencia.Text;

            if (medico.Trim() == "")
            {
                MessageBox.Show("Ingrese el nombre del médico solicitante.");
                return;
            }

            if (prioridad == "")
            {
                MessageBox.Show("Seleccione una prioridad.");
                return;
            }

            int tipo = Convert.ToInt32(cmbTipoSangre.SelectedValue);
            int cantidad = Convert.ToInt32(numCantidad.Value);

            cConexion con = new cConexion();
            using (SqlConnection cn = con.ConexionServer())
            {
                string query = @"INSERT INTO SolicitudMedica
                (IdSolicitud, CentroSolicitante, MedicoSolicitante, TipoSangreRequerida,
                 CantidadUnidades, Prioridad, Motivo, Estado)
                VALUES
                (@Id, @Centro, @Medico, @Tipo, @Cantidad, @Prioridad, @Motivo, 'Aprobada')";

                SqlCommand cmd = new SqlCommand(query, cn);

                cmd.Parameters.AddWithValue("@Id", idSolicitud);
                cmd.Parameters.AddWithValue("@Centro", centro);
                cmd.Parameters.AddWithValue("@Medico", medico);
                cmd.Parameters.AddWithValue("@Tipo", tipo);
                cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                cmd.Parameters.AddWithValue("@Prioridad", prioridad);
                cmd.Parameters.AddWithValue("@Motivo", motivo);

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }

            MessageBox.Show("Solicitud aprobada correctamente.\nCódigo: " + idSolicitud);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtIdSolicitud.Text == "")
            {
                MessageBox.Show("Ingrese el ID de la solicitud a rechazar.");
                return;
            }

            cConexion con = new cConexion();
            using (SqlConnection cn = con.ConexionServer())
            {
                string query = "UPDATE SolicitudMedica SET Estado='Rechazada' WHERE IdSolicitud=@Id";

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@Id", txtIdSolicitud.Text);

                cn.Open();
                int filas = cmd.ExecuteNonQuery();
                cn.Close();

                if (filas > 0)
                    MessageBox.Show("Solicitud rechazada correctamente.");
                else
                    MessageBox.Show("No se encontró la solicitud indicada.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Solicitudes_Load(object sender, EventArgs e)
        {

        }
    }
}
