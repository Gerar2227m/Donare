
namespace Donare
{
    partial class frmBuscarDonacion
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
            this.txtFiltro = new System.Windows.Forms.TextBox();
            this.dgvDonantes = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonantes)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFiltro
            // 
            this.txtFiltro.Location = new System.Drawing.Point(130, 34);
            this.txtFiltro.Name = "txtFiltro";
            this.txtFiltro.Size = new System.Drawing.Size(100, 26);
            this.txtFiltro.TabIndex = 0;
            this.txtFiltro.TextChanged += new System.EventHandler(this.txtFiltro_TextChanged_1);
            // 
            // dgvDonantes
            // 
            this.dgvDonantes.AllowUserToAddRows = false;
            this.dgvDonantes.AllowUserToDeleteRows = false;
            this.dgvDonantes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDonantes.Location = new System.Drawing.Point(55, 101);
            this.dgvDonantes.Name = "dgvDonantes";
            this.dgvDonantes.ReadOnly = true;
            this.dgvDonantes.RowHeadersWidth = 62;
            this.dgvDonantes.RowTemplate.Height = 28;
            this.dgvDonantes.Size = new System.Drawing.Size(667, 276);
            this.dgvDonantes.TabIndex = 1;
            this.dgvDonantes.DoubleClick += new System.EventHandler(this.dgvDonantes_DoubleClick_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nombre:";
            // 
            // frmBuscarDonacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvDonantes);
            this.Controls.Add(this.txtFiltro);
            this.Name = "frmBuscarDonacion";
            this.Text = "frmBuscarDonacion";
            this.Load += new System.EventHandler(this.frmBuscarDonacion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonantes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFiltro;
        private System.Windows.Forms.DataGridView dgvDonantes;
        private System.Windows.Forms.Label label1;
    }
}