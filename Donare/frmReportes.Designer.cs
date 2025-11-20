namespace Donare
{
    partial class frmReportes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvReportes = new System.Windows.Forms.DataGridView();
            this.btnInventario = new System.Windows.Forms.Button();
            this.btnDonaciones = new System.Windows.Forms.Button();
            this.btnSolicitados = new System.Windows.Forms.Button();
            this.btnVencimiento = new System.Windows.Forms.Button();
            this.generarExcel = new System.Windows.Forms.Button();
            this.panelFiltros = new System.Windows.Forms.Panel();
            this.panelFiltrosInventario = new System.Windows.Forms.Panel();
            this.panelFiltrosDonaciones = new System.Windows.Forms.Panel();
            this.panelFiltrosSolicitudes = new System.Windows.Forms.Panel();
            this.panelFiltrosVencimiento = new System.Windows.Forms.Panel();
            this.btnFiltrarVencimiento = new System.Windows.Forms.Button();
            this.numDiasVencimiento = new System.Windows.Forms.NumericUpDown();
            this.btnFiltrarSolicitudes = new System.Windows.Forms.Button();
            this.cmbEstadoSolicitud = new System.Windows.Forms.ComboBox();
            this.btnDonacionesDonante = new System.Windows.Forms.Button();
            this.btnDonacionesFecha = new System.Windows.Forms.Button();
            this.txtDonante = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtDonFin = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dtDonInicio = new System.Windows.Forms.Label();
            this.btnFiltrarInventario = new System.Windows.Forms.Button();
            this.cmbTipoInventario = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportes)).BeginInit();
            this.panelFiltros.SuspendLayout();
            this.panelFiltrosInventario.SuspendLayout();
            this.panelFiltrosDonaciones.SuspendLayout();
            this.panelFiltrosSolicitudes.SuspendLayout();
            this.panelFiltrosVencimiento.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDiasVencimiento)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvReportes
            // 
            this.dgvReportes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReportes.Location = new System.Drawing.Point(116, 94);
            this.dgvReportes.Name = "dgvReportes";
            this.dgvReportes.Size = new System.Drawing.Size(729, 373);
            this.dgvReportes.TabIndex = 0;
            // 
            // btnInventario
            // 
            this.btnInventario.Location = new System.Drawing.Point(3, 6);
            this.btnInventario.Name = "btnInventario";
            this.btnInventario.Size = new System.Drawing.Size(103, 34);
            this.btnInventario.TabIndex = 1;
            this.btnInventario.Text = "Inventario";
            this.btnInventario.UseVisualStyleBackColor = true;
            this.btnInventario.Click += new System.EventHandler(this.btnInventario_Click_1);
            // 
            // btnDonaciones
            // 
            this.btnDonaciones.Location = new System.Drawing.Point(3, 46);
            this.btnDonaciones.Name = "btnDonaciones";
            this.btnDonaciones.Size = new System.Drawing.Size(103, 34);
            this.btnDonaciones.TabIndex = 2;
            this.btnDonaciones.Text = "Donaciones";
            this.btnDonaciones.UseVisualStyleBackColor = true;
            this.btnDonaciones.Click += new System.EventHandler(this.btnDonaciones_Click_1);
            // 
            // btnSolicitados
            // 
            this.btnSolicitados.Location = new System.Drawing.Point(3, 86);
            this.btnSolicitados.Name = "btnSolicitados";
            this.btnSolicitados.Size = new System.Drawing.Size(103, 34);
            this.btnSolicitados.TabIndex = 3;
            this.btnSolicitados.Text = "Solicitudes";
            this.btnSolicitados.UseVisualStyleBackColor = true;
            this.btnSolicitados.Click += new System.EventHandler(this.btnSolicitados_Click);
            // 
            // btnVencimiento
            // 
            this.btnVencimiento.Location = new System.Drawing.Point(3, 126);
            this.btnVencimiento.Name = "btnVencimiento";
            this.btnVencimiento.Size = new System.Drawing.Size(103, 34);
            this.btnVencimiento.TabIndex = 4;
            this.btnVencimiento.Text = "Proximas a vencer";
            this.btnVencimiento.UseVisualStyleBackColor = true;
            this.btnVencimiento.Click += new System.EventHandler(this.btnVencimiento_Click_1);
            // 
            // generarExcel
            // 
            this.generarExcel.Location = new System.Drawing.Point(3, 259);
            this.generarExcel.Name = "generarExcel";
            this.generarExcel.Size = new System.Drawing.Size(103, 34);
            this.generarExcel.TabIndex = 6;
            this.generarExcel.Text = "Generar Excel";
            this.generarExcel.UseVisualStyleBackColor = true;
            this.generarExcel.Click += new System.EventHandler(this.generarExcel_Click);
            // 
            // panelFiltros
            // 
            this.panelFiltros.Controls.Add(this.panelFiltrosVencimiento);
            this.panelFiltros.Controls.Add(this.panelFiltrosSolicitudes);
            this.panelFiltros.Controls.Add(this.panelFiltrosDonaciones);
            this.panelFiltros.Controls.Add(this.panelFiltrosInventario);
            this.panelFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFiltros.Location = new System.Drawing.Point(0, 0);
            this.panelFiltros.Name = "panelFiltros";
            this.panelFiltros.Size = new System.Drawing.Size(845, 88);
            this.panelFiltros.TabIndex = 7;
            // 
            // panelFiltrosInventario
            // 
            this.panelFiltrosInventario.Controls.Add(this.btnFiltrarInventario);
            this.panelFiltrosInventario.Controls.Add(this.cmbTipoInventario);
            this.panelFiltrosInventario.Controls.Add(this.label1);
            this.panelFiltrosInventario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFiltrosInventario.Location = new System.Drawing.Point(0, 0);
            this.panelFiltrosInventario.Name = "panelFiltrosInventario";
            this.panelFiltrosInventario.Size = new System.Drawing.Size(845, 88);
            this.panelFiltrosInventario.TabIndex = 0;
            // 
            // panelFiltrosDonaciones
            // 
            this.panelFiltrosDonaciones.Controls.Add(this.btnDonacionesDonante);
            this.panelFiltrosDonaciones.Controls.Add(this.btnDonacionesFecha);
            this.panelFiltrosDonaciones.Controls.Add(this.txtDonante);
            this.panelFiltrosDonaciones.Controls.Add(this.label3);
            this.panelFiltrosDonaciones.Controls.Add(this.dtDonFin);
            this.panelFiltrosDonaciones.Controls.Add(this.label2);
            this.panelFiltrosDonaciones.Controls.Add(this.dateTimePicker1);
            this.panelFiltrosDonaciones.Controls.Add(this.dtDonInicio);
            this.panelFiltrosDonaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFiltrosDonaciones.Location = new System.Drawing.Point(0, 0);
            this.panelFiltrosDonaciones.Name = "panelFiltrosDonaciones";
            this.panelFiltrosDonaciones.Size = new System.Drawing.Size(845, 88);
            this.panelFiltrosDonaciones.TabIndex = 3;
            this.panelFiltrosDonaciones.Visible = false;
            // 
            // panelFiltrosSolicitudes
            // 
            this.panelFiltrosSolicitudes.Controls.Add(this.btnFiltrarSolicitudes);
            this.panelFiltrosSolicitudes.Controls.Add(this.cmbEstadoSolicitud);
            this.panelFiltrosSolicitudes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFiltrosSolicitudes.Location = new System.Drawing.Point(0, 0);
            this.panelFiltrosSolicitudes.Name = "panelFiltrosSolicitudes";
            this.panelFiltrosSolicitudes.Size = new System.Drawing.Size(845, 88);
            this.panelFiltrosSolicitudes.TabIndex = 8;
            this.panelFiltrosSolicitudes.Visible = false;
            // 
            // panelFiltrosVencimiento
            // 
            this.panelFiltrosVencimiento.Controls.Add(this.btnFiltrarVencimiento);
            this.panelFiltrosVencimiento.Controls.Add(this.numDiasVencimiento);
            this.panelFiltrosVencimiento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFiltrosVencimiento.Location = new System.Drawing.Point(0, 0);
            this.panelFiltrosVencimiento.Name = "panelFiltrosVencimiento";
            this.panelFiltrosVencimiento.Size = new System.Drawing.Size(845, 88);
            this.panelFiltrosVencimiento.TabIndex = 2;
            this.panelFiltrosVencimiento.Visible = false;
            this.panelFiltrosVencimiento.Paint += new System.Windows.Forms.PaintEventHandler(this.panelFiltrosVencimiento_Paint);
            // 
            // btnFiltrarVencimiento
            // 
            this.btnFiltrarVencimiento.Location = new System.Drawing.Point(324, 54);
            this.btnFiltrarVencimiento.Name = "btnFiltrarVencimiento";
            this.btnFiltrarVencimiento.Size = new System.Drawing.Size(98, 23);
            this.btnFiltrarVencimiento.TabIndex = 1;
            this.btnFiltrarVencimiento.Text = "Aplicar";
            this.btnFiltrarVencimiento.UseVisualStyleBackColor = true;
            this.btnFiltrarVencimiento.Click += new System.EventHandler(this.btnFiltrarVencimiento_Click);
            // 
            // numDiasVencimiento
            // 
            this.numDiasVencimiento.Location = new System.Drawing.Point(125, 26);
            this.numDiasVencimiento.Name = "numDiasVencimiento";
            this.numDiasVencimiento.Size = new System.Drawing.Size(297, 20);
            this.numDiasVencimiento.TabIndex = 0;
            this.numDiasVencimiento.ValueChanged += new System.EventHandler(this.numDiasVencimiento_ValueChanged);
            // 
            // btnFiltrarSolicitudes
            // 
            this.btnFiltrarSolicitudes.Location = new System.Drawing.Point(407, 54);
            this.btnFiltrarSolicitudes.Name = "btnFiltrarSolicitudes";
            this.btnFiltrarSolicitudes.Size = new System.Drawing.Size(75, 23);
            this.btnFiltrarSolicitudes.TabIndex = 1;
            this.btnFiltrarSolicitudes.Text = "Aplicar";
            this.btnFiltrarSolicitudes.UseVisualStyleBackColor = true;
            this.btnFiltrarSolicitudes.Click += new System.EventHandler(this.btnFiltrarSolicitudes_Click_1);
            // 
            // cmbEstadoSolicitud
            // 
            this.cmbEstadoSolicitud.FormattingEnabled = true;
            this.cmbEstadoSolicitud.Location = new System.Drawing.Point(114, 25);
            this.cmbEstadoSolicitud.Name = "cmbEstadoSolicitud";
            this.cmbEstadoSolicitud.Size = new System.Drawing.Size(368, 21);
            this.cmbEstadoSolicitud.TabIndex = 0;
            // 
            // btnDonacionesDonante
            // 
            this.btnDonacionesDonante.Location = new System.Drawing.Point(644, 54);
            this.btnDonacionesDonante.Name = "btnDonacionesDonante";
            this.btnDonacionesDonante.Size = new System.Drawing.Size(75, 23);
            this.btnDonacionesDonante.TabIndex = 7;
            this.btnDonacionesDonante.Text = "Aplicar";
            this.btnDonacionesDonante.UseVisualStyleBackColor = true;
            this.btnDonacionesDonante.Click += new System.EventHandler(this.btnDonacionesDonante_Click_1);
            // 
            // btnDonacionesFecha
            // 
            this.btnDonacionesFecha.Location = new System.Drawing.Point(441, 54);
            this.btnDonacionesFecha.Name = "btnDonacionesFecha";
            this.btnDonacionesFecha.Size = new System.Drawing.Size(75, 23);
            this.btnDonacionesFecha.TabIndex = 6;
            this.btnDonacionesFecha.Text = "Aplicar";
            this.btnDonacionesFecha.UseVisualStyleBackColor = true;
            this.btnDonacionesFecha.Click += new System.EventHandler(this.btnDonacionesFecha_Click_1);
            // 
            // txtDonante
            // 
            this.txtDonante.Location = new System.Drawing.Point(526, 28);
            this.txtDonante.Name = "txtDonante";
            this.txtDonante.Size = new System.Drawing.Size(193, 20);
            this.txtDonante.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(523, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Donante";
            // 
            // dtDonFin
            // 
            this.dtDonFin.Location = new System.Drawing.Point(320, 28);
            this.dtDonFin.Name = "dtDonFin";
            this.dtDonFin.Size = new System.Drawing.Size(200, 20);
            this.dtDonFin.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(317, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fecha fin";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(114, 28);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // dtDonInicio
            // 
            this.dtDonInicio.AutoSize = true;
            this.dtDonInicio.Location = new System.Drawing.Point(111, 9);
            this.dtDonInicio.Name = "dtDonInicio";
            this.dtDonInicio.Size = new System.Drawing.Size(79, 13);
            this.dtDonInicio.TabIndex = 0;
            this.dtDonInicio.Text = "Fecha de inicio";
            this.dtDonInicio.Click += new System.EventHandler(this.dtDonInicio_Click);
            // 
            // btnFiltrarInventario
            // 
            this.btnFiltrarInventario.Location = new System.Drawing.Point(110, 37);
            this.btnFiltrarInventario.Name = "btnFiltrarInventario";
            this.btnFiltrarInventario.Size = new System.Drawing.Size(93, 40);
            this.btnFiltrarInventario.TabIndex = 2;
            this.btnFiltrarInventario.Text = "Filtrar";
            this.btnFiltrarInventario.UseVisualStyleBackColor = true;
            this.btnFiltrarInventario.Click += new System.EventHandler(this.btnFiltrarInventario_Click_1);
            // 
            // cmbTipoInventario
            // 
            this.cmbTipoInventario.FormattingEnabled = true;
            this.cmbTipoInventario.Location = new System.Drawing.Point(209, 48);
            this.cmbTipoInventario.Name = "cmbTipoInventario";
            this.cmbTipoInventario.Size = new System.Drawing.Size(226, 21);
            this.cmbTipoInventario.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(107, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tipos de sangre";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnInventario);
            this.panel1.Controls.Add(this.btnDonaciones);
            this.panel1.Controls.Add(this.generarExcel);
            this.panel1.Controls.Add(this.btnSolicitados);
            this.panel1.Controls.Add(this.btnVencimiento);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 88);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(110, 391);
            this.panel1.TabIndex = 8;
            // 
            // frmReportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 479);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelFiltros);
            this.Controls.Add(this.dgvReportes);
            this.Name = "frmReportes";
            this.Text = "Reportes";
            this.Load += new System.EventHandler(this.frmReportes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportes)).EndInit();
            this.panelFiltros.ResumeLayout(false);
            this.panelFiltrosInventario.ResumeLayout(false);
            this.panelFiltrosInventario.PerformLayout();
            this.panelFiltrosDonaciones.ResumeLayout(false);
            this.panelFiltrosDonaciones.PerformLayout();
            this.panelFiltrosSolicitudes.ResumeLayout(false);
            this.panelFiltrosVencimiento.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numDiasVencimiento)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvReportes;
        private System.Windows.Forms.Button btnInventario;
        private System.Windows.Forms.Button btnDonaciones;
        private System.Windows.Forms.Button btnSolicitados;
        private System.Windows.Forms.Button btnVencimiento;
        private System.Windows.Forms.Button generarExcel;
        private System.Windows.Forms.Panel panelFiltros;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelFiltrosInventario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbTipoInventario;
        private System.Windows.Forms.Button btnFiltrarInventario;
        private System.Windows.Forms.Panel panelFiltrosDonaciones;
        private System.Windows.Forms.DateTimePicker dtDonFin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label dtDonInicio;
        private System.Windows.Forms.Button btnDonacionesFecha;
        private System.Windows.Forms.TextBox txtDonante;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelFiltrosSolicitudes;
        private System.Windows.Forms.Button btnDonacionesDonante;
        private System.Windows.Forms.ComboBox cmbEstadoSolicitud;
        private System.Windows.Forms.Button btnFiltrarSolicitudes;
        private System.Windows.Forms.Panel panelFiltrosVencimiento;
        private System.Windows.Forms.Button btnFiltrarVencimiento;
        private System.Windows.Forms.NumericUpDown numDiasVencimiento;
    }
}