namespace Donare
{
    partial class Solicitudes
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtIdSolicitud = new System.Windows.Forms.TextBox();
            this.txtMedico = new System.Windows.Forms.TextBox();
            this.cmbUrgencia = new System.Windows.Forms.ComboBox();
            this.cmbTipoSangre = new System.Windows.Forms.ComboBox();
            this.cmbCentroMedico = new System.Windows.Forms.ComboBox();
            this.numCantidad = new System.Windows.Forms.NumericUpDown();
            this.txtMotivo = new System.Windows.Forms.TextBox();
            this.Buscar = new System.Windows.Forms.Button();
            this.Aprobar = new System.Windows.Forms.Button();
            this.Rechazar = new System.Windows.Forms.Button();
            this.Salir = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCantidad)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(51, 317);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(1000, 329);
            this.dataGridView1.TabIndex = 0;
            // 
            // txtIdSolicitud
            // 
            this.txtIdSolicitud.Location = new System.Drawing.Point(146, 52);
            this.txtIdSolicitud.Name = "txtIdSolicitud";
            this.txtIdSolicitud.Size = new System.Drawing.Size(204, 26);
            this.txtIdSolicitud.TabIndex = 1;
            // 
            // txtMedico
            // 
            this.txtMedico.Location = new System.Drawing.Point(515, 52);
            this.txtMedico.Name = "txtMedico";
            this.txtMedico.Size = new System.Drawing.Size(161, 26);
            this.txtMedico.TabIndex = 2;
            // 
            // cmbUrgencia
            // 
            this.cmbUrgencia.FormattingEnabled = true;
            this.cmbUrgencia.Location = new System.Drawing.Point(146, 110);
            this.cmbUrgencia.Name = "cmbUrgencia";
            this.cmbUrgencia.Size = new System.Drawing.Size(121, 28);
            this.cmbUrgencia.TabIndex = 3;
            // 
            // cmbTipoSangre
            // 
            this.cmbTipoSangre.FormattingEnabled = true;
            this.cmbTipoSangre.Location = new System.Drawing.Point(146, 162);
            this.cmbTipoSangre.Name = "cmbTipoSangre";
            this.cmbTipoSangre.Size = new System.Drawing.Size(121, 28);
            this.cmbTipoSangre.TabIndex = 4;
            // 
            // cmbCentroMedico
            // 
            this.cmbCentroMedico.FormattingEnabled = true;
            this.cmbCentroMedico.Location = new System.Drawing.Point(515, 110);
            this.cmbCentroMedico.Name = "cmbCentroMedico";
            this.cmbCentroMedico.Size = new System.Drawing.Size(121, 28);
            this.cmbCentroMedico.TabIndex = 5;
            // 
            // numCantidad
            // 
            this.numCantidad.Location = new System.Drawing.Point(515, 163);
            this.numCantidad.Name = "numCantidad";
            this.numCantidad.Size = new System.Drawing.Size(120, 26);
            this.numCantidad.TabIndex = 6;
            // 
            // txtMotivo
            // 
            this.txtMotivo.Location = new System.Drawing.Point(822, 110);
            this.txtMotivo.Multiline = true;
            this.txtMotivo.Name = "txtMotivo";
            this.txtMotivo.Size = new System.Drawing.Size(159, 54);
            this.txtMotivo.TabIndex = 7;
            // 
            // Buscar
            // 
            this.Buscar.Location = new System.Drawing.Point(105, 265);
            this.Buscar.Name = "Buscar";
            this.Buscar.Size = new System.Drawing.Size(154, 29);
            this.Buscar.TabIndex = 8;
            this.Buscar.Text = "Buscar";
            this.Buscar.UseVisualStyleBackColor = true;
            this.Buscar.Click += new System.EventHandler(this.button1_Click);
            // 
            // Aprobar
            // 
            this.Aprobar.Location = new System.Drawing.Point(294, 265);
            this.Aprobar.Name = "Aprobar";
            this.Aprobar.Size = new System.Drawing.Size(120, 28);
            this.Aprobar.TabIndex = 9;
            this.Aprobar.Text = "Aprobar";
            this.Aprobar.UseVisualStyleBackColor = true;
            this.Aprobar.Click += new System.EventHandler(this.button2_Click);
            // 
            // Rechazar
            // 
            this.Rechazar.Location = new System.Drawing.Point(540, 265);
            this.Rechazar.Name = "Rechazar";
            this.Rechazar.Size = new System.Drawing.Size(107, 30);
            this.Rechazar.TabIndex = 10;
            this.Rechazar.Text = "Rechazar";
            this.Rechazar.UseVisualStyleBackColor = true;
            this.Rechazar.Click += new System.EventHandler(this.button3_Click);
            // 
            // Salir
            // 
            this.Salir.Location = new System.Drawing.Point(768, 265);
            this.Salir.Name = "Salir";
            this.Salir.Size = new System.Drawing.Size(112, 27);
            this.Salir.TabIndex = 11;
            this.Salir.Text = "Salir";
            this.Salir.UseVisualStyleBackColor = true;
            this.Salir.Click += new System.EventHandler(this.button4_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "Td Solicitud";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 20);
            this.label2.TabIndex = 13;
            this.label2.Text = "Urgencia:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 20);
            this.label3.TabIndex = 14;
            this.label3.Text = "Tipo Sangre:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(356, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(142, 20);
            this.label4.TabIndex = 15;
            this.label4.Text = "Medico Solicitante:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(370, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 20);
            this.label5.TabIndex = 16;
            this.label5.Text = "Centro Medico";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(409, 169);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 20);
            this.label6.TabIndex = 17;
            this.label6.Text = "Cantidad:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(728, 118);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 20);
            this.label7.TabIndex = 18;
            this.label7.Text = "Motivo:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(806, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(254, 20);
            this.label9.TabIndex = 19;
            this.label9.Text = "*Todos los campos son necesarios";
            // 
            // Solicitudes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1092, 603);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Salir);
            this.Controls.Add(this.Rechazar);
            this.Controls.Add(this.Aprobar);
            this.Controls.Add(this.Buscar);
            this.Controls.Add(this.txtMotivo);
            this.Controls.Add(this.numCantidad);
            this.Controls.Add(this.cmbCentroMedico);
            this.Controls.Add(this.cmbTipoSangre);
            this.Controls.Add(this.cmbUrgencia);
            this.Controls.Add(this.txtMedico);
            this.Controls.Add(this.txtIdSolicitud);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Solicitudes";
            this.Text = "Solicitudes";
            this.Load += new System.EventHandler(this.Solicitudes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCantidad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtIdSolicitud;
        private System.Windows.Forms.TextBox txtMedico;
        private System.Windows.Forms.ComboBox cmbUrgencia;
        private System.Windows.Forms.ComboBox cmbTipoSangre;
        private System.Windows.Forms.ComboBox cmbCentroMedico;
        private System.Windows.Forms.NumericUpDown numCantidad;
        private System.Windows.Forms.TextBox txtMotivo;
        private System.Windows.Forms.Button Buscar;
        private System.Windows.Forms.Button Aprobar;
        private System.Windows.Forms.Button Rechazar;
        private System.Windows.Forms.Button Salir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
    }
}