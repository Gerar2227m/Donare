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
        public Inicio()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RegistrarUsuario registrarUsuario = new RegistrarUsuario();
            registrarUsuario.ShowDialog();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {

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
    }
}
