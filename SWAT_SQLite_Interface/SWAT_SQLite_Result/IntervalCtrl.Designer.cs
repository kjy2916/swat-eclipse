namespace SWAT_SQLite_Result
{
    partial class IntervalCtrl
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
            this.rdbYearly = new System.Windows.Forms.RadioButton();
            this.rdbMonthly = new System.Windows.Forms.RadioButton();
            this.rdbDaily = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbYearly);
            this.groupBox1.Controls.Add(this.rdbMonthly);
            this.groupBox1.Controls.Add(this.rdbDaily);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(207, 51);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Interval";
            // 
            // rdbYearly
            // 
            this.rdbYearly.AutoSize = true;
            this.rdbYearly.Location = new System.Drawing.Point(129, 20);
            this.rdbYearly.Name = "rdbYearly";
            this.rdbYearly.Size = new System.Drawing.Size(54, 17);
            this.rdbYearly.TabIndex = 0;
            this.rdbYearly.TabStop = true;
            this.rdbYearly.Text = "Yearly";
            this.rdbYearly.UseVisualStyleBackColor = true;
            // 
            // rdbMonthly
            // 
            this.rdbMonthly.AutoSize = true;
            this.rdbMonthly.Location = new System.Drawing.Point(61, 20);
            this.rdbMonthly.Name = "rdbMonthly";
            this.rdbMonthly.Size = new System.Drawing.Size(62, 17);
            this.rdbMonthly.TabIndex = 0;
            this.rdbMonthly.TabStop = true;
            this.rdbMonthly.Text = "Monthly";
            this.rdbMonthly.UseVisualStyleBackColor = true;
            // 
            // rdbDaily
            // 
            this.rdbDaily.AutoSize = true;
            this.rdbDaily.Location = new System.Drawing.Point(7, 20);
            this.rdbDaily.Name = "rdbDaily";
            this.rdbDaily.Size = new System.Drawing.Size(48, 17);
            this.rdbDaily.TabIndex = 0;
            this.rdbDaily.TabStop = true;
            this.rdbDaily.Text = "Daily";
            this.rdbDaily.UseVisualStyleBackColor = true;
            // 
            // IntervalCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "IntervalCtrl";
            this.Size = new System.Drawing.Size(207, 51);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbYearly;
        private System.Windows.Forms.RadioButton rdbMonthly;
        private System.Windows.Forms.RadioButton rdbDaily;
    }
}
