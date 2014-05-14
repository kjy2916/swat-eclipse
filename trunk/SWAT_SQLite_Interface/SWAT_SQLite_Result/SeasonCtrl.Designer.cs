namespace SWAT_SQLite_Result
{
    partial class SeasonCtrl
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
            this.rdbGrowingSeason = new System.Windows.Forms.RadioButton();
            this.rdbSnowMelt = new System.Windows.Forms.RadioButton();
            this.rdbWholeYear = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbGrowingSeason);
            this.groupBox1.Controls.Add(this.rdbSnowMelt);
            this.groupBox1.Controls.Add(this.rdbWholeYear);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(178, 65);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Season";
            // 
            // rdbGrowingSeason
            // 
            this.rdbGrowingSeason.AutoSize = true;
            this.rdbGrowingSeason.Location = new System.Drawing.Point(7, 39);
            this.rdbGrowingSeason.Name = "rdbGrowingSeason";
            this.rdbGrowingSeason.Size = new System.Drawing.Size(103, 17);
            this.rdbGrowingSeason.TabIndex = 0;
            this.rdbGrowingSeason.TabStop = true;
            this.rdbGrowingSeason.Text = "Growing Season";
            this.rdbGrowingSeason.UseVisualStyleBackColor = true;
            // 
            // rdbSnowMelt
            // 
            this.rdbSnowMelt.AutoSize = true;
            this.rdbSnowMelt.Location = new System.Drawing.Point(94, 19);
            this.rdbSnowMelt.Name = "rdbSnowMelt";
            this.rdbSnowMelt.Size = new System.Drawing.Size(75, 17);
            this.rdbSnowMelt.TabIndex = 0;
            this.rdbSnowMelt.TabStop = true;
            this.rdbSnowMelt.Text = "Snow Melt";
            this.rdbSnowMelt.UseVisualStyleBackColor = true;
            // 
            // rdbWholeYear
            // 
            this.rdbWholeYear.AutoSize = true;
            this.rdbWholeYear.Location = new System.Drawing.Point(7, 20);
            this.rdbWholeYear.Name = "rdbWholeYear";
            this.rdbWholeYear.Size = new System.Drawing.Size(81, 17);
            this.rdbWholeYear.TabIndex = 0;
            this.rdbWholeYear.TabStop = true;
            this.rdbWholeYear.Text = "Whole Year";
            this.rdbWholeYear.UseVisualStyleBackColor = true;
            // 
            // SeasonCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "SeasonCtrl";
            this.Size = new System.Drawing.Size(178, 65);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbGrowingSeason;
        private System.Windows.Forms.RadioButton rdbSnowMelt;
        private System.Windows.Forms.RadioButton rdbWholeYear;
    }
}
