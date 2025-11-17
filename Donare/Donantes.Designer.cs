
namespace Donare
{
    partial class Donantes
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabpersonal = new System.Windows.Forms.TabPage();
            this.tabmedicos = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.tabControl1.SuspendLayout();
            this.tabpersonal.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabpersonal);
            this.tabControl1.Controls.Add(this.tabmedicos);
            this.tabControl1.Location = new System.Drawing.Point(30, 40);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1091, 610);
            this.tabControl1.TabIndex = 0;
            // 
            // tabpersonal
            // 
            this.tabpersonal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tabpersonal.Controls.Add(this.comboBox2);
            this.tabpersonal.Controls.Add(this.comboBox1);
            this.tabpersonal.Controls.Add(this.textBox4);
            this.tabpersonal.Controls.Add(this.dateTimePicker1);
            this.tabpersonal.Controls.Add(this.textBox3);
            this.tabpersonal.Controls.Add(this.textBox2);
            this.tabpersonal.Controls.Add(this.textBox1);
            this.tabpersonal.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tabpersonal.Location = new System.Drawing.Point(4, 29);
            this.tabpersonal.Name = "tabpersonal";
            this.tabpersonal.Padding = new System.Windows.Forms.Padding(3);
            this.tabpersonal.Size = new System.Drawing.Size(1083, 577);
            this.tabpersonal.TabIndex = 0;
            this.tabpersonal.Text = "DATOS PERSONALES DEL DONANTE";
            // 
            // tabmedicos
            // 
            this.tabmedicos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tabmedicos.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tabmedicos.Location = new System.Drawing.Point(4, 29);
            this.tabmedicos.Name = "tabmedicos";
            this.tabmedicos.Padding = new System.Windows.Forms.Padding(3);
            this.tabmedicos.Size = new System.Drawing.Size(1083, 577);
            this.tabmedicos.TabIndex = 1;
            this.tabmedicos.Text = "DATOS MEDICOS DEL DONANTE";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(65, 55);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(174, 26);
            this.textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(65, 117);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(311, 26);
            this.textBox2.TabIndex = 1;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(65, 180);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(311, 26);
            this.textBox3.TabIndex = 2;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(65, 235);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 26);
            this.dateTimePicker1.TabIndex = 3;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(65, 295);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 26);
            this.textBox4.TabIndex = 4;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(65, 351);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 28);
            this.comboBox1.TabIndex = 5;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(65, 413);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 28);
            this.comboBox2.TabIndex = 6;
            // 
            // Donantes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1152, 662);
            this.Controls.Add(this.tabControl1);
            this.Name = "Donantes";
            this.Text = "Donantes";
            this.tabControl1.ResumeLayout(false);
            this.tabpersonal.ResumeLayout(false);
            this.tabpersonal.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabpersonal;
        private System.Windows.Forms.TabPage tabmedicos;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}