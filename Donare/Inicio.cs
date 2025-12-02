using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Donare
{
    public partial class Inicio : Form
    {
        int tipoUsuario;
        bool panelVisible = false;
        int panelWidth = 300;
        public Inicio(int tipoUsuario)
        {
            InitializeComponent();
            this.tipoUsuario = tipoUsuario;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RegistrarUsuario registrarUsuario = new RegistrarUsuario();
            registrarUsuario.ShowDialog();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            if (tipoUsuario == 2)
            {
                button1.Enabled = false;
                button1.Visible = false;
                button1.Hide();
                button3.Visible = false;
                button3.Hide();
                button3.Enabled = false;

            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Donantes donantes = new Donantes();
            donantes.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
                    }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e) //Botón para iniciar pagina de reportes
        {
            frmReportes frm = new frmReportes();
            frm.ShowDialog();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Donaciones donaciones = new Donaciones();
            donaciones.ShowDialog();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            Donaciones donaciones = new Donaciones();
            donaciones.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Solicitudes solicitudes = new Solicitudes();
            solicitudes.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmLogin login = new FrmLogin();   
            login.ShowDialog();                      
            this.Hide();
        }

        
        

        private void flowNotificaciones_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
