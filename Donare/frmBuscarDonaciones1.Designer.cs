
namespace Donare
{
    partial class frmBuscarDonaciones1
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
            this.label1 = new System.Windows.Forms.Label();
            this.dgvDonaciones = new System.Windows.Forms.DataGridView();
            this.txtFiltro = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonaciones)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Nombre:";
            // 
            // dgvDonaciones
            // 
            this.dgvDonaciones.AllowUserToAddRows = false;
            this.dgvDonaciones.AllowUserToDeleteRows = false;
            this.dgvDonaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDonaciones.Location = new System.Drawing.Point(67, 121);
            this.dgvDonaciones.Name = "dgvDonaciones";
            this.dgvDonaciones.ReadOnly = true;
            this.dgvDonaciones.RowHeadersWidth = 62;
            this.dgvDonaciones.RowTemplate.Height = 28;
            this.dgvDonaciones.Size = new System.Drawing.Size(667, 276);
            this.dgvDonaciones.TabIndex = 4;
            // 
            // txtFiltro
            // 
            this.txtFiltro.Location = new System.Drawing.Point(142, 54);
            this.txtFiltro.Name = "txtFiltro";
            this.txtFiltro.Size = new System.Drawing.Size(100, 26);
            this.txtFiltro.TabIndex = 3;
            // 
            // frmBuscarDonaciones1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvDonaciones);
            this.Controls.Add(this.txtFiltro);
            this.Name = "frmBuscarDonaciones1";
            this.Text = "frmBuscarDonaciones1";
            this.Load += new System.EventHandler(this.frmBuscarDonaciones1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonaciones)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvDonaciones;
        private System.Windows.Forms.TextBox txtFiltro;
    }
}