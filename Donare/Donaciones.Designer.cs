namespace Donare
{
    partial class Donaciones
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Donaciones));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbNuevo = new System.Windows.Forms.ToolStripButton();
            this.tsbGuardar = new System.Windows.Forms.ToolStripButton();
            this.tsbModificar = new System.Windows.Forms.ToolStripButton();
            this.tsbEliminar = new System.Windows.Forms.ToolStripButton();
            this.tsbBuscar = new System.Windows.Forms.ToolStripButton();
            this.tsbSalir = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNoDonacion = new System.Windows.Forms.TextBox();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCodDonante = new System.Windows.Forms.TextBox();
            this.btnBuscarDonante = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cboComponente = new System.Windows.Forms.ComboBox();
            this.cboTipoDonacion = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtMedico = new System.Windows.Forms.TextBox();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.dgvBolsas = new System.Windows.Forms.DataGridView();
            this.colCodigoBolsa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTipoSangre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVolumen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFExtraccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFVencimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUbicacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Agregar = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBolsas)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNuevo,
            this.tsbGuardar,
            this.tsbModificar,
            this.tsbEliminar,
            this.tsbBuscar,
            this.tsbSalir});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1183, 33);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // tsbNuevo
            // 
            this.tsbNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNuevo.Image = ((System.Drawing.Image)(resources.GetObject("tsbNuevo.Image")));
            this.tsbNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNuevo.Name = "tsbNuevo";
            this.tsbNuevo.Size = new System.Drawing.Size(34, 28);
            this.tsbNuevo.Text = "toolStripButton1";
            this.tsbNuevo.Click += new System.EventHandler(this.tsbNuevo_Click_1);
            // 
            // tsbGuardar
            // 
            this.tsbGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbGuardar.Image = ((System.Drawing.Image)(resources.GetObject("tsbGuardar.Image")));
            this.tsbGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbGuardar.Name = "tsbGuardar";
            this.tsbGuardar.Size = new System.Drawing.Size(34, 28);
            this.tsbGuardar.Text = "toolStripButton2";
            this.tsbGuardar.Click += new System.EventHandler(this.tsbGuardar_Click);
            // 
            // tsbModificar
            // 
            this.tsbModificar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbModificar.Image = ((System.Drawing.Image)(resources.GetObject("tsbModificar.Image")));
            this.tsbModificar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbModificar.Name = "tsbModificar";
            this.tsbModificar.Size = new System.Drawing.Size(34, 28);
            this.tsbModificar.Text = "toolStripButton3";
            this.tsbModificar.Visible = false;
            this.tsbModificar.Click += new System.EventHandler(this.tsbModificar_Click_1);
            // 
            // tsbEliminar
            // 
            this.tsbEliminar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbEliminar.Image = ((System.Drawing.Image)(resources.GetObject("tsbEliminar.Image")));
            this.tsbEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEliminar.Name = "tsbEliminar";
            this.tsbEliminar.Size = new System.Drawing.Size(34, 28);
            this.tsbEliminar.Text = "toolStripButton4";
            this.tsbEliminar.Click += new System.EventHandler(this.tsbEliminar_Click_1);
            // 
            // tsbBuscar
            // 
            this.tsbBuscar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbBuscar.Image = ((System.Drawing.Image)(resources.GetObject("tsbBuscar.Image")));
            this.tsbBuscar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBuscar.Name = "tsbBuscar";
            this.tsbBuscar.Size = new System.Drawing.Size(34, 28);
            this.tsbBuscar.Text = "toolStripButton5";
            this.tsbBuscar.Click += new System.EventHandler(this.tsbBuscar_Click_1);
            // 
            // tsbSalir
            // 
            this.tsbSalir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSalir.Image = ((System.Drawing.Image)(resources.GetObject("tsbSalir.Image")));
            this.tsbSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSalir.Name = "tsbSalir";
            this.tsbSalir.Size = new System.Drawing.Size(34, 28);
            this.tsbSalir.Text = "toolStripButton6";
            this.tsbSalir.Click += new System.EventHandler(this.tsbSalir_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "No. Donación:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(469, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fecha:";
            // 
            // txtNoDonacion
            // 
            this.txtNoDonacion.Location = new System.Drawing.Point(145, 53);
            this.txtNoDonacion.Name = "txtNoDonacion";
            this.txtNoDonacion.Size = new System.Drawing.Size(190, 26);
            this.txtNoDonacion.TabIndex = 3;
            // 
            // dtpFecha
            // 
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(544, 53);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(200, 26);
            this.dtpFecha.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Cód. Donante:";
            // 
            // txtCodDonante
            // 
            this.txtCodDonante.Location = new System.Drawing.Point(145, 103);
            this.txtCodDonante.Name = "txtCodDonante";
            this.txtCodDonante.Size = new System.Drawing.Size(190, 26);
            this.txtCodDonante.TabIndex = 6;
            // 
            // btnBuscarDonante
            // 
            this.btnBuscarDonante.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarDonante.Image")));
            this.btnBuscarDonante.Location = new System.Drawing.Point(344, 103);
            this.btnBuscarDonante.Name = "btnBuscarDonante";
            this.btnBuscarDonante.Size = new System.Drawing.Size(34, 26);
            this.btnBuscarDonante.TabIndex = 7;
            this.btnBuscarDonante.UseVisualStyleBackColor = true;
            this.btnBuscarDonante.Click += new System.EventHandler(this.btnBuscarDonante_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(415, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Nombre:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 156);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Dirección:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 208);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "Télefono:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(533, 97);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(437, 26);
            this.txtNombre.TabIndex = 11;
            // 
            // txtDireccion
            // 
            this.txtDireccion.Location = new System.Drawing.Point(145, 150);
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(825, 26);
            this.txtDireccion.TabIndex = 12;
            // 
            // txtTelefono
            // 
            this.txtTelefono.Location = new System.Drawing.Point(148, 202);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(190, 26);
            this.txtTelefono.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(32, 252);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 20);
            this.label7.TabIndex = 14;
            this.label7.Text = "Tipo Donacion:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(349, 252);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(105, 20);
            this.label9.TabIndex = 16;
            this.label9.Text = "Componente:";
            // 
            // cboComponente
            // 
            this.cboComponente.FormattingEnabled = true;
            this.cboComponente.Location = new System.Drawing.Point(460, 244);
            this.cboComponente.Name = "cboComponente";
            this.cboComponente.Size = new System.Drawing.Size(151, 28);
            this.cboComponente.TabIndex = 17;
            // 
            // cboTipoDonacion
            // 
            this.cboTipoDonacion.FormattingEnabled = true;
            this.cboTipoDonacion.Location = new System.Drawing.Point(151, 244);
            this.cboTipoDonacion.Name = "cboTipoDonacion";
            this.cboTipoDonacion.Size = new System.Drawing.Size(167, 28);
            this.cboTipoDonacion.TabIndex = 18;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(34, 294);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(159, 20);
            this.label10.TabIndex = 20;
            this.label10.Text = "Doctor Responsable:";
            // 
            // txtMedico
            // 
            this.txtMedico.Location = new System.Drawing.Point(199, 288);
            this.txtMedico.Name = "txtMedico";
            this.txtMedico.Size = new System.Drawing.Size(190, 26);
            this.txtMedico.TabIndex = 21;
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Location = new System.Drawing.Point(533, 294);
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(190, 55);
            this.txtObservaciones.TabIndex = 22;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(415, 294);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(118, 20);
            this.label11.TabIndex = 23;
            this.label11.Text = "Observaciones:";
            // 
            // dgvBolsas
            // 
            this.dgvBolsas.AllowUserToAddRows = false;
            this.dgvBolsas.AllowUserToDeleteRows = false;
            this.dgvBolsas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBolsas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCodigoBolsa,
            this.colTipoSangre,
            this.colVolumen,
            this.colFExtraccion,
            this.colFVencimiento,
            this.colEstado,
            this.colUbicacion});
            this.dgvBolsas.Location = new System.Drawing.Point(56, 417);
            this.dgvBolsas.Name = "dgvBolsas";
            this.dgvBolsas.ReadOnly = true;
            this.dgvBolsas.RowHeadersWidth = 62;
            this.dgvBolsas.RowTemplate.Height = 28;
            this.dgvBolsas.Size = new System.Drawing.Size(1055, 300);
            this.dgvBolsas.TabIndex = 24;
            // 
            // colCodigoBolsa
            // 
            this.colCodigoBolsa.HeaderText = "Cód.Bolsa";
            this.colCodigoBolsa.MinimumWidth = 8;
            this.colCodigoBolsa.Name = "colCodigoBolsa";
            this.colCodigoBolsa.ReadOnly = true;
            this.colCodigoBolsa.Width = 150;
            // 
            // colTipoSangre
            // 
            this.colTipoSangre.HeaderText = "Tipo S.";
            this.colTipoSangre.MinimumWidth = 8;
            this.colTipoSangre.Name = "colTipoSangre";
            this.colTipoSangre.ReadOnly = true;
            this.colTipoSangre.Width = 150;
            // 
            // colVolumen
            // 
            this.colVolumen.HeaderText = "Volumen";
            this.colVolumen.MinimumWidth = 8;
            this.colVolumen.Name = "colVolumen";
            this.colVolumen.ReadOnly = true;
            this.colVolumen.Width = 150;
            // 
            // colFExtraccion
            // 
            this.colFExtraccion.HeaderText = "F. Extracción";
            this.colFExtraccion.MinimumWidth = 8;
            this.colFExtraccion.Name = "colFExtraccion";
            this.colFExtraccion.ReadOnly = true;
            this.colFExtraccion.Width = 150;
            // 
            // colFVencimiento
            // 
            this.colFVencimiento.HeaderText = "F. Vencimiento";
            this.colFVencimiento.MinimumWidth = 8;
            this.colFVencimiento.Name = "colFVencimiento";
            this.colFVencimiento.ReadOnly = true;
            this.colFVencimiento.Width = 150;
            // 
            // colEstado
            // 
            this.colEstado.HeaderText = "Estado";
            this.colEstado.MinimumWidth = 8;
            this.colEstado.Name = "colEstado";
            this.colEstado.ReadOnly = true;
            this.colEstado.Width = 150;
            // 
            // colUbicacion
            // 
            this.colUbicacion.HeaderText = "Ubicacion";
            this.colUbicacion.MinimumWidth = 8;
            this.colUbicacion.Name = "colUbicacion";
            this.colUbicacion.ReadOnly = true;
            this.colUbicacion.Width = 150;
            // 
            // Agregar
            // 
            this.Agregar.Location = new System.Drawing.Point(65, 381);
            this.Agregar.Name = "Agregar";
            this.Agregar.Size = new System.Drawing.Size(156, 29);
            this.Agregar.TabIndex = 25;
            this.Agregar.Text = "Agregar Bolsa";
            this.Agregar.UseVisualStyleBackColor = true;
            this.Agregar.Click += new System.EventHandler(this.Agregar_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(868, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(254, 20);
            this.label8.TabIndex = 26;
            this.label8.Text = "*Todos los campos son necesarios";
            // 
            // Donaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1183, 775);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.Agregar);
            this.Controls.Add(this.dgvBolsas);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtObservaciones);
            this.Controls.Add(this.txtMedico);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cboTipoDonacion);
            this.Controls.Add(this.cboComponente);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtTelefono);
            this.Controls.Add(this.txtDireccion);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnBuscarDonante);
            this.Controls.Add(this.txtCodDonante);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.txtNoDonacion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Donaciones";
            this.Text = "Donaciones";
            this.Load += new System.EventHandler(this.Donaciones_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBolsas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbNuevo;
        private System.Windows.Forms.ToolStripButton tsbGuardar;
        private System.Windows.Forms.ToolStripButton tsbModificar;
        private System.Windows.Forms.ToolStripButton tsbEliminar;
        private System.Windows.Forms.ToolStripButton tsbBuscar;
        private System.Windows.Forms.ToolStripButton tsbSalir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNoDonacion;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCodDonante;
        private System.Windows.Forms.Button btnBuscarDonante;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cboComponente;
        private System.Windows.Forms.ComboBox cboTipoDonacion;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtMedico;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView dgvBolsas;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCodigoBolsa;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTipoSangre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVolumen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFExtraccion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFVencimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUbicacion;
        private System.Windows.Forms.Button Agregar;
        private System.Windows.Forms.Label label8;
    }
}