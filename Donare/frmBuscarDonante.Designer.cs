
namespace Donare
{
    partial class frmBuscarDonante
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
            this.txtValor = new System.Windows.Forms.TextBox();
            this.dgvResultados = new System.Windows.Forms.DataGridView();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.cmbCriterio = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).BeginInit();
            this.SuspendLayout();
            // 
            // txtValor
            // 
            this.txtValor.Location = new System.Drawing.Point(57, 109);
            this.txtValor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtValor.Name = "txtValor";
            this.txtValor.Size = new System.Drawing.Size(112, 26);
            this.txtValor.TabIndex = 0;
            // 
            // dgvResultados
            // 
            this.dgvResultados.AllowUserToAddRows = false;
            this.dgvResultados.AllowUserToDeleteRows = false;
            this.dgvResultados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResultados.Location = new System.Drawing.Point(57, 219);
            this.dgvResultados.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvResultados.Name = "dgvResultados";
            this.dgvResultados.ReadOnly = true;
            this.dgvResultados.RowHeadersWidth = 51;
            this.dgvResultados.RowTemplate.Height = 24;
            this.dgvResultados.Size = new System.Drawing.Size(781, 289);
            this.dgvResultados.TabIndex = 1;
            this.dgvResultados.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResultados_CellDoubleClick);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(616, 109);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(84, 29);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click_1);
            // 
            // cmbCriterio
            // 
            this.cmbCriterio.FormattingEnabled = true;
            this.cmbCriterio.Location = new System.Drawing.Point(57, 54);
            this.cmbCriterio.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbCriterio.Name = "cmbCriterio";
            this.cmbCriterio.Size = new System.Drawing.Size(136, 28);
            this.cmbCriterio.TabIndex = 3;
            // 
            // frmBuscarDonante
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 562);
            this.Controls.Add(this.cmbCriterio);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.dgvResultados);
            this.Controls.Add(this.txtValor);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmBuscarDonante";
            this.Text = "frmBuscarDonante";
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtValor;
        private System.Windows.Forms.DataGridView dgvResultados;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.ComboBox cmbCriterio;
    }
}