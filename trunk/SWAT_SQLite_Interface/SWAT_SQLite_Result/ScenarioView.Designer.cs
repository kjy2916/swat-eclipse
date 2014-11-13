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
            this.bRunDaily = new System.Windows.Forms.Button();
            this.lblSimulationTime = new System.Windows.Forms.Label();
            this.bRunMonthly = new System.Windows.Forms.Button();
            this.bRunYearly = new System.Windows.Forms.Button();
            this.bFileCIO = new System.Windows.Forms.Button();
            this.bBasinBsn = new System.Windows.Forms.Button();
            this.bFigFig = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bOpenModelFolder
            // 
            this.bOpenModelFolder.Location = new System.Drawing.Point(389, 3);
            this.bOpenModelFolder.Name = "bOpenModelFolder";
            this.bOpenModelFolder.Size = new System.Drawing.Size(110, 23);
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
            this.button1.Location = new System.Drawing.Point(691, 2);
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
            // bRunDaily
            // 
            this.bRunDaily.Location = new System.Drawing.Point(134, 4);
            this.bRunDaily.Name = "bRunDaily";
            this.bRunDaily.Size = new System.Drawing.Size(79, 23);
            this.bRunDaily.TabIndex = 7;
            this.bRunDaily.Text = "Run Daily";
            this.bRunDaily.UseVisualStyleBackColor = true;
            this.bRunDaily.Click += new System.EventHandler(this.bRun_Click);
            // 
            // lblSimulationTime
            // 
            this.lblSimulationTime.AutoSize = true;
            this.lblSimulationTime.Location = new System.Drawing.Point(772, 7);
            this.lblSimulationTime.Name = "lblSimulationTime";
            this.lblSimulationTime.Size = new System.Drawing.Size(35, 13);
            this.lblSimulationTime.TabIndex = 8;
            this.lblSimulationTime.Text = "label1";
            // 
            // bRunMonthly
            // 
            this.bRunMonthly.Location = new System.Drawing.Point(219, 4);
            this.bRunMonthly.Name = "bRunMonthly";
            this.bRunMonthly.Size = new System.Drawing.Size(79, 23);
            this.bRunMonthly.TabIndex = 9;
            this.bRunMonthly.Text = "Run Monthly";
            this.bRunMonthly.UseVisualStyleBackColor = true;
            this.bRunMonthly.Click += new System.EventHandler(this.bRunMonthly_Click);
            // 
            // bRunYearly
            // 
            this.bRunYearly.Location = new System.Drawing.Point(304, 4);
            this.bRunYearly.Name = "bRunYearly";
            this.bRunYearly.Size = new System.Drawing.Size(79, 23);
            this.bRunYearly.TabIndex = 10;
            this.bRunYearly.Text = "Run Yearly";
            this.bRunYearly.UseVisualStyleBackColor = true;
            this.bRunYearly.Click += new System.EventHandler(this.bRunYearly_Click);
            // 
            // bFileCIO
            // 
            this.bFileCIO.Location = new System.Drawing.Point(502, 3);
            this.bFileCIO.Name = "bFileCIO";
            this.bFileCIO.Size = new System.Drawing.Size(49, 23);
            this.bFileCIO.TabIndex = 11;
            this.bFileCIO.Text = "file.cio";
            this.bFileCIO.UseVisualStyleBackColor = true;
            this.bFileCIO.Click += new System.EventHandler(this.bFileCIO_Click);
            // 
            // bBasinBsn
            // 
            this.bBasinBsn.Location = new System.Drawing.Point(557, 3);
            this.bBasinBsn.Name = "bBasinBsn";
            this.bBasinBsn.Size = new System.Drawing.Size(66, 23);
            this.bBasinBsn.TabIndex = 12;
            this.bBasinBsn.Text = "basin.bsn";
            this.bBasinBsn.UseVisualStyleBackColor = true;
            this.bBasinBsn.Click += new System.EventHandler(this.bBasinBsn_Click);
            // 
            // bFigFig
            // 
            this.bFigFig.Location = new System.Drawing.Point(629, 2);
            this.bFigFig.Name = "bFigFig";
            this.bFigFig.Size = new System.Drawing.Size(57, 23);
            this.bFigFig.TabIndex = 13;
            this.bFigFig.Text = "fig.fig";
            this.bFigFig.UseVisualStyleBackColor = true;
            this.bFigFig.Click += new System.EventHandler(this.bFigFig_Click);
            // 
            // ScenarioView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bFigFig);
            this.Controls.Add(this.bBasinBsn);
            this.Controls.Add(this.bFileCIO);
            this.Controls.Add(this.bRunYearly);
            this.Controls.Add(this.bRunMonthly);
            this.Controls.Add(this.lblSimulationTime);
            this.Controls.Add(this.bRunDaily);
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
        private System.Windows.Forms.Button bRunDaily;
        private System.Windows.Forms.Label lblSimulationTime;
        private System.Windows.Forms.Button bRunMonthly;
        private System.Windows.Forms.Button bRunYearly;
        private System.Windows.Forms.Button bFileCIO;
        private System.Windows.Forms.Button bBasinBsn;
        private System.Windows.Forms.Button bFigFig;
    }
}
