namespace SWAT_SQLite_Result
{
    partial class CompareCtrl
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
            this.cmbSplitYear = new System.Windows.Forms.ComboBox();
            this.cmbCompareResults = new System.Windows.Forms.ComboBox();
            this.chbCompare = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbSplitYear);
            this.groupBox1.Controls.Add(this.cmbCompareResults);
            this.groupBox1.Controls.Add(this.chbCompare);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(265, 65);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Compare";
            // 
            // cmbSplitYear
            // 
            this.cmbSplitYear.FormattingEnabled = true;
            this.cmbSplitYear.Location = new System.Drawing.Point(201, 25);
            this.cmbSplitYear.Name = "cmbSplitYear";
            this.cmbSplitYear.Size = new System.Drawing.Size(58, 21);
            this.cmbSplitYear.TabIndex = 2;
            // 
            // cmbCompareResults
            // 
            this.cmbCompareResults.FormattingEnabled = true;
            this.cmbCompareResults.Location = new System.Drawing.Point(102, 25);
            this.cmbCompareResults.Name = "cmbCompareResults";
            this.cmbCompareResults.Size = new System.Drawing.Size(91, 21);
            this.cmbCompareResults.TabIndex = 1;
            // 
            // chbCompare
            // 
            this.chbCompare.AutoSize = true;
            this.chbCompare.Location = new System.Drawing.Point(6, 27);
            this.chbCompare.Name = "chbCompare";
            this.chbCompare.Size = new System.Drawing.Size(90, 17);
            this.chbCompare.TabIndex = 0;
            this.chbCompare.Text = "Compare with";
            this.chbCompare.UseVisualStyleBackColor = true;
            // 
            // CompareCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "CompareCtrl";
            this.Size = new System.Drawing.Size(265, 65);
            this.Load += new System.EventHandler(this.CompareCtrl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chbCompare;
        private System.Windows.Forms.ComboBox cmbCompareResults;
        private System.Windows.Forms.ComboBox cmbSplitYear;
    }
}
