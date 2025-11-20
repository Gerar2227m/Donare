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
            this.btnVoverl = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.cmbTipoInventario = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportes)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvReportes
            // 
            this.dgvReportes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReportes.Location = new System.Drawing.Point(143, 60);
            this.dgvReportes.Name = "dgvReportes";
            this.dgvReportes.Size = new System.Drawing.Size(531, 378);
            this.dgvReportes.TabIndex = 0;
            // 
            // btnInventario
            // 
            this.btnInventario.Location = new System.Drawing.Point(12, 60);
            this.btnInventario.Name = "btnInventario";
            this.btnInventario.Size = new System.Drawing.Size(103, 34);
            this.btnInventario.TabIndex = 1;
            this.btnInventario.Text = "Inventario";
            this.btnInventario.UseVisualStyleBackColor = true;
            this.btnInventario.Click += new System.EventHandler(this.btnInventario_Click_1);
            // 
            // btnDonaciones
            // 
            this.btnDonaciones.Location = new System.Drawing.Point(12, 122);
            this.btnDonaciones.Name = "btnDonaciones";
            this.btnDonaciones.Size = new System.Drawing.Size(103, 34);
            this.btnDonaciones.TabIndex = 2;
            this.btnDonaciones.Text = "Donaciones";
            this.btnDonaciones.UseVisualStyleBackColor = true;
            this.btnDonaciones.Click += new System.EventHandler(this.btnDonaciones_Click_1);
            // 
            // btnSolicitados
            // 
            this.btnSolicitados.Location = new System.Drawing.Point(12, 193);
            this.btnSolicitados.Name = "btnSolicitados";
            this.btnSolicitados.Size = new System.Drawing.Size(103, 34);
            this.btnSolicitados.TabIndex = 3;
            this.btnSolicitados.Text = "Solicitudes";
            this.btnSolicitados.UseVisualStyleBackColor = true;
            this.btnSolicitados.Click += new System.EventHandler(this.btnSolicitados_Click);
            // 
            // btnVencimiento
            // 
            this.btnVencimiento.Location = new System.Drawing.Point(12, 267);
            this.btnVencimiento.Name = "btnVencimiento";
            this.btnVencimiento.Size = new System.Drawing.Size(103, 34);
            this.btnVencimiento.TabIndex = 4;
            this.btnVencimiento.Text = "Proximas a vencer";
            this.btnVencimiento.UseVisualStyleBackColor = true;
            this.btnVencimiento.Click += new System.EventHandler(this.btnVencimiento_Click_1);
            // 
            // btnVoverl
            // 
            this.btnVoverl.Location = new System.Drawing.Point(12, 5);
            this.btnVoverl.Name = "btnVoverl";
            this.btnVoverl.Size = new System.Drawing.Size(103, 34);
            this.btnVoverl.TabIndex = 5;
            this.btnVoverl.Text = "Inicio";
            this.btnVoverl.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(12, 404);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(103, 34);
            this.button6.TabIndex = 6;
            this.button6.Text = "Generar PDF";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // cmbTipoInventario
            // 
            this.cmbTipoInventario.FormattingEnabled = true;
            this.cmbTipoInventario.Location = new System.Drawing.Point(143, 33);
            this.cmbTipoInventario.Name = "cmbTipoInventario";
            this.cmbTipoInventario.Size = new System.Drawing.Size(204, 21);
            this.cmbTipoInventario.TabIndex = 7;
            // 
            // frmReportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cmbTipoInventario);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.btnVoverl);
            this.Controls.Add(this.btnVencimiento);
            this.Controls.Add(this.btnSolicitados);
            this.Controls.Add(this.btnDonaciones);
            this.Controls.Add(this.btnInventario);
            this.Controls.Add(this.dgvReportes);
            this.Name = "frmReportes";
            this.Text = "Reportes";
            this.Load += new System.EventHandler(this.frmReportes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvReportes;
        private System.Windows.Forms.Button btnInventario;
        private System.Windows.Forms.Button btnDonaciones;
        private System.Windows.Forms.Button btnSolicitados;
        private System.Windows.Forms.Button btnVencimiento;
        private System.Windows.Forms.Button btnVoverl;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.ComboBox cmbTipoInventario;
    }
}