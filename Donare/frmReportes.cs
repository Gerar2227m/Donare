using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Donare
{
    public partial class frmReportes : Form
    {
        public frmReportes()
        {
            InitializeComponent();
        }
        private void btnVolver_Click(object sender, EventArgs e)//Evento que al darle click a Inicio regresa a la pantalla de inicio
        {
            Inicio frm = new Inicio();
            frm.Show();
            this.Hide();
        }
        private void btnInventario_Click(object sender, EventArgs e)
        {
            
        }
        private void btnDonaciones_Click(object sender, EventArgs e)
        {

        }






        private void frmReportes_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new cConexion().ConexionServer();
            SqlDataAdapter da = new SqlDataAdapter("SELECT IdTipoSangre, Grupo + Rh AS Tipo FROM TipoSangre", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cmbTipoInventario.DataSource = dt;
            cmbTipoInventario.DisplayMember = "Tipo";
            cmbTipoInventario.ValueMember = "IdTipoSangre";
        }

        private void btnInventario_Click_1(object sender, EventArgs e)
        {
            string query = @"
             SELECT 
             TS.Grupo + TS.Rh AS TipoSangre,
             BS.CodigoBolsa,
             BS.FechaExtraccion,
             BS.FechaVencimiento,
             BS.VolumenML,
             BS.Estado,
             BS.Ubicacion
              FROM dbo.BolsaSangre BS
             INNER JOIN dbo.TipoSangre TS
             ON BS.IdTipoSangre = TS.IdTipoSangre
             ORDER BY TS.Grupo, TS.Rh, BS.FechaVencimiento";

            SqlConnection conn = new cConexion().ConexionServer();
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvReportes.DataSource = dt;
        }
       
        

        private void btnVencimiento_Click(object sender, EventArgs e)
        {
            
        }

        private void btnDonaciones_Click_1(object sender, EventArgs e)
        {
            string query = @"
    SELECT 
        D.IdDonacion,
        Don.NombreCompleto AS Donante,
        D.FechaDonacion,
        D.TipoDonacion,
        D.Componente,
        SUM(BS.VolumenML) AS VolumenTotal
    FROM dbo.Donacion D
    INNER JOIN dbo.Donante Don
        ON D.IdDonante = Don.IdDonante
    LEFT JOIN dbo.BolsaSangre BS
        ON BS.IdDonacion = D.IdDonacion
    GROUP BY 
        D.IdDonacion,
        Don.NombreCompleto,
        D.FechaDonacion,
        D.TipoDonacion,
        D.Componente
    ORDER BY D.FechaDonacion DESC;";

            SqlConnection conn = new cConexion().ConexionServer();
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvReportes.DataSource = dt;
        }

        private void btnSolicitados_Click(object sender, EventArgs e)
        {
            string query = @"
    SELECT 
        SM.IdSolicitud,
        SM.CentroSolicitante,
        SM.MedicoSolicitante,
        TS.Grupo + TS.Rh AS TipoSangre,
        SM.CantidadUnidades,
        SM.Prioridad,
        SM.Estado,
        SB.CodigoBolsa,
        SB.FechaAsignacion
    FROM dbo.SolicitudMedica SM
    LEFT JOIN dbo.TipoSangre TS
        ON SM.TipoSangreRequerida = TS.IdTipoSangre
    LEFT JOIN dbo.SolicitudBolsa SB
        ON SM.IdSolicitud = SB.IdSolicitud
    ORDER BY SM.IdSolicitud, SB.FechaAsignacion;
    ";

            SqlConnection conn = new cConexion().ConexionServer();
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvReportes.DataSource = dt;
        }

        private void btnVencimiento_Click_1(object sender, EventArgs e)
        {
            string query = @"
    SELECT 
        BS.CodigoBolsa,
        TS.Grupo + TS.Rh AS TipoSangre,
        BS.FechaExtraccion,
        BS.FechaVencimiento,
        BS.Estado
    FROM dbo.BolsaSangre BS
    INNER JOIN dbo.TipoSangre TS
        ON BS.IdTipoSangre = TS.IdTipoSangre
    WHERE BS.FechaVencimiento <= DATEADD(day, 15, GETDATE())
    ORDER BY BS.FechaVencimiento";

            SqlConnection conn = new cConexion().ConexionServer();
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvReportes.DataSource = dt;
        }
    }
}
