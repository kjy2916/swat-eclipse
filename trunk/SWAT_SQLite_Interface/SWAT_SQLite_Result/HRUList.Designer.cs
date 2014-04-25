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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbHRUs = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // bGoHRU
            // 
            this.bGoHRU.Location = new System.Drawing.Point(172, 4);
            this.bGoHRU.Name = "bGoHRU";
            this.bGoHRU.Size = new System.Drawing.Size(75, 23);
            this.bGoHRU.TabIndex = 7;
            this.bGoHRU.Text = "Go";
            this.bGoHRU.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "HRUs";
            // 
            // cmbHRUs
            // 
            this.cmbHRUs.FormattingEnabled = true;
            this.cmbHRUs.Location = new System.Drawing.Point(45, 3);
            this.cmbHRUs.Name = "cmbHRUs";
            this.cmbHRUs.Size = new System.Drawing.Size(121, 21);
            this.cmbHRUs.TabIndex = 5;
            // 
            // HRUList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bGoHRU);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbHRUs);
            this.Name = "HRUList";
            this.Size = new System.Drawing.Size(252, 27);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bGoHRU;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbHRUs;
    }
}
