namespace SWAT_SQLite_Result
{
    partial class HRUList
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bGoHRU = new System.Windows.Forms.Button();
            this.cmbHRUs = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bGoHRU
            // 
            this.bGoHRU.Location = new System.Drawing.Point(98, 24);
            this.bGoHRU.Name = "bGoHRU";
            this.bGoHRU.Size = new System.Drawing.Size(49, 23);
            this.bGoHRU.TabIndex = 7;
            this.bGoHRU.Text = "Go";
            this.bGoHRU.UseVisualStyleBackColor = true;
            // 
            // cmbHRUs
            // 
            this.cmbHRUs.FormattingEnabled = true;
            this.cmbHRUs.Location = new System.Drawing.Point(6, 24);
            this.cmbHRUs.Name = "cmbHRUs";
            this.cmbHRUs.Size = new System.Drawing.Size(86, 21);
            this.cmbHRUs.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbHRUs);
            this.groupBox1.Controls.Add(this.bGoHRU);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(155, 65);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "HRUs";
            // 
            // HRUList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "HRUList";
            this.Size = new System.Drawing.Size(155, 65);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bGoHRU;
        private System.Windows.Forms.ComboBox cmbHRUs;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
