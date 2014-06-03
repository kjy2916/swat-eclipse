namespace SWAT_SQLite_Result
{
    partial class ScenarioView
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
            this.bOpenModelFolder = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cmbModelType = new System.Windows.Forms.ComboBox();
            this.bRun = new System.Windows.Forms.Button();
            this.lblSimulationTime = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bOpenModelFolder
            // 
            this.bOpenModelFolder.Location = new System.Drawing.Point(627, 4);
            this.bOpenModelFolder.Name = "bOpenModelFolder";
            this.bOpenModelFolder.Size = new System.Drawing.Size(130, 23);
            this.bOpenModelFolder.TabIndex = 2;
            this.bOpenModelFolder.Text = "Open Model Folder";
            this.bOpenModelFolder.UseVisualStyleBackColor = true;
            this.bOpenModelFolder.Click += new System.EventHandler(this.bOpenModelFolder_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(7, 33);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(926, 363);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(763, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Backup";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // cmbModelType
            // 
            this.cmbModelType.FormattingEnabled = true;
            this.cmbModelType.Location = new System.Drawing.Point(7, 4);
            this.cmbModelType.Name = "cmbModelType";
            this.cmbModelType.Size = new System.Drawing.Size(121, 21);
            this.cmbModelType.TabIndex = 6;
            // 
            // bRun
            // 
            this.bRun.Location = new System.Drawing.Point(135, 1);
            this.bRun.Name = "bRun";
            this.bRun.Size = new System.Drawing.Size(75, 23);
            this.bRun.TabIndex = 7;
            this.bRun.Text = "Run";
            this.bRun.UseVisualStyleBackColor = true;
            this.bRun.Click += new System.EventHandler(this.bRun_Click);
            // 
            // lblSimulationTime
            // 
            this.lblSimulationTime.AutoSize = true;
            this.lblSimulationTime.Location = new System.Drawing.Point(217, 4);
            this.lblSimulationTime.Name = "lblSimulationTime";
            this.lblSimulationTime.Size = new System.Drawing.Size(35, 13);
            this.lblSimulationTime.TabIndex = 8;
            this.lblSimulationTime.Text = "label1";
            // 
            // ScenarioView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblSimulationTime);
            this.Controls.Add(this.bRun);
            this.Controls.Add(this.cmbModelType);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.bOpenModelFolder);
            this.Name = "ScenarioView";
            this.Size = new System.Drawing.Size(942, 399);
            this.Load += new System.EventHandler(this.ScenarioView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bOpenModelFolder;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cmbModelType;
        private System.Windows.Forms.Button bRun;
        private System.Windows.Forms.Label lblSimulationTime;
    }
}
