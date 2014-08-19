namespace SWAT_SQLite_Result
{
    partial class YearCtrl
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
            this.components = new System.ComponentModel.Container();
            this.rdbEachYear = new System.Windows.Forms.RadioButton();
            this.rdbAllYears = new System.Windows.Forms.RadioButton();
            this.tbYear = new System.Windows.Forms.TrackBar();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bPlay = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tbYear)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdbEachYear
            // 
            this.rdbEachYear.AutoSize = true;
            this.rdbEachYear.Location = new System.Drawing.Point(6, 19);
            this.rdbEachYear.Name = "rdbEachYear";
            this.rdbEachYear.Size = new System.Drawing.Size(75, 17);
            this.rdbEachYear.TabIndex = 0;
            this.rdbEachYear.Text = "Each Year";
            this.rdbEachYear.UseVisualStyleBackColor = true;
            // 
            // rdbAllYears
            // 
            this.rdbAllYears.AutoSize = true;
            this.rdbAllYears.Location = new System.Drawing.Point(6, 39);
            this.rdbAllYears.Name = "rdbAllYears";
            this.rdbAllYears.Size = new System.Drawing.Size(66, 17);
            this.rdbAllYears.TabIndex = 0;
            this.rdbAllYears.Text = "All Years";
            this.rdbAllYears.UseVisualStyleBackColor = true;
            // 
            // tbYear
            // 
            this.tbYear.LargeChange = 1;
            this.tbYear.Location = new System.Drawing.Point(77, 17);
            this.tbYear.Name = "tbYear";
            this.tbYear.Size = new System.Drawing.Size(93, 45);
            this.tbYear.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bPlay);
            this.groupBox1.Controls.Add(this.rdbEachYear);
            this.groupBox1.Controls.Add(this.tbYear);
            this.groupBox1.Controls.Add(this.rdbAllYears);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(207, 65);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Year";
            // 
            // bPlay
            // 
            this.bPlay.Location = new System.Drawing.Point(166, 17);
            this.bPlay.Name = "bPlay";
            this.bPlay.Size = new System.Drawing.Size(35, 23);
            this.bPlay.TabIndex = 2;
            this.bPlay.Text = "start";
            this.bPlay.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            this.timer1.Interval = 1500;
            // 
            // YearCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "YearCtrl";
            this.Size = new System.Drawing.Size(207, 65);
            ((System.ComponentModel.ISupportInitialize)(this.tbYear)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rdbEachYear;
        private System.Windows.Forms.RadioButton rdbAllYears;
        private System.Windows.Forms.TrackBar tbYear;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bPlay;
        private System.Windows.Forms.Timer timer1;
    }
}
