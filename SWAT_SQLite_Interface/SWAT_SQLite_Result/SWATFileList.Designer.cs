namespace SWAT_SQLite_Result
{
    partial class SWATFileList
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbExtension = new System.Windows.Forms.ComboBox();
            this.bOpen = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bOpen);
            this.groupBox1.Controls.Add(this.cmbExtension);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(122, 65);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SWAT Files";
            // 
            // cmbExtension
            // 
            this.cmbExtension.FormattingEnabled = true;
            this.cmbExtension.Location = new System.Drawing.Point(7, 25);
            this.cmbExtension.Name = "cmbExtension";
            this.cmbExtension.Size = new System.Drawing.Size(63, 21);
            this.cmbExtension.TabIndex = 0;
            this.cmbExtension.SelectedIndexChanged += new System.EventHandler(this.cmbExtension_SelectedIndexChanged);
            // 
            // bOpen
            // 
            this.bOpen.Location = new System.Drawing.Point(76, 25);
            this.bOpen.Name = "bOpen";
            this.bOpen.Size = new System.Drawing.Size(40, 23);
            this.bOpen.TabIndex = 1;
            this.bOpen.Text = "open";
            this.bOpen.UseVisualStyleBackColor = true;
            this.bOpen.Click += new System.EventHandler(this.bOpen_Click);
            // 
            // SWATFileList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "SWATFileList";
            this.Size = new System.Drawing.Size(122, 65);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbExtension;
        private System.Windows.Forms.Button bOpen;
    }
}
