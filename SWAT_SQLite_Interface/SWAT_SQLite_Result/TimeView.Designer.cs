namespace SWAT_SQLite_Result
{
    partial class TimeView
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
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.bRewind = new System.Windows.Forms.Button();
            this.bBack = new System.Windows.Forms.Button();
            this.bStop = new System.Windows.Forms.Button();
            this.bForward = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(4, 4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 0;
            // 
            // bRewind
            // 
            this.bRewind.Location = new System.Drawing.Point(4, 30);
            this.bRewind.Name = "bRewind";
            this.bRewind.Size = new System.Drawing.Size(41, 23);
            this.bRewind.TabIndex = 1;
            this.bRewind.Text = "<<";
            this.bRewind.UseVisualStyleBackColor = true;
            // 
            // bBack
            // 
            this.bBack.Location = new System.Drawing.Point(51, 30);
            this.bBack.Name = "bBack";
            this.bBack.Size = new System.Drawing.Size(41, 23);
            this.bBack.TabIndex = 1;
            this.bBack.Text = "<";
            this.bBack.UseVisualStyleBackColor = true;
            // 
            // bStop
            // 
            this.bStop.Location = new System.Drawing.Point(98, 29);
            this.bStop.Name = "bStop";
            this.bStop.Size = new System.Drawing.Size(41, 23);
            this.bStop.TabIndex = 1;
            this.bStop.Text = "[]";
            this.bStop.UseVisualStyleBackColor = true;
            // 
            // bForward
            // 
            this.bForward.Location = new System.Drawing.Point(145, 29);
            this.bForward.Name = "bForward";
            this.bForward.Size = new System.Drawing.Size(41, 23);
            this.bForward.TabIndex = 1;
            this.bForward.Text = ">";
            this.bForward.UseVisualStyleBackColor = true;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(210, 3);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(104, 45);
            this.trackBar1.TabIndex = 2;
            // 
            // TimeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.bForward);
            this.Controls.Add(this.bStop);
            this.Controls.Add(this.bBack);
            this.Controls.Add(this.bRewind);
            this.Controls.Add(this.dateTimePicker1);
            this.Name = "TimeView";
            this.Size = new System.Drawing.Size(316, 60);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button bRewind;
        private System.Windows.Forms.Button bBack;
        private System.Windows.Forms.Button bStop;
        private System.Windows.Forms.Button bForward;
        private System.Windows.Forms.TrackBar trackBar1;
    }
}
