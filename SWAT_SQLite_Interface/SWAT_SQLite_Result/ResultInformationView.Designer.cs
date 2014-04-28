namespace SWAT_SQLite_Result
{
    partial class ResultInformationView
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
            this.lblResultGenerationTime = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblResultGenerationTime
            // 
            this.lblResultGenerationTime.AutoSize = true;
            this.lblResultGenerationTime.Location = new System.Drawing.Point(4, 4);
            this.lblResultGenerationTime.Name = "lblResultGenerationTime";
            this.lblResultGenerationTime.Size = new System.Drawing.Size(35, 13);
            this.lblResultGenerationTime.TabIndex = 0;
            this.lblResultGenerationTime.Text = "label1";
            // 
            // ResultInformationView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblResultGenerationTime);
            this.Name = "ResultInformationView";
            this.Size = new System.Drawing.Size(734, 391);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblResultGenerationTime;
    }
}
