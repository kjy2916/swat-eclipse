namespace SWAT_SQLite_Result
{
    partial class TableResultsCtrl
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
            this.cmbColumns = new System.Windows.Forms.ComboBox();
            this.cmbResultTypes = new System.Windows.Forms.ComboBox();
            this.cmbIDs = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblStatistics = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.bGoSubbasin = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbColumns
            // 
            this.cmbColumns.FormattingEnabled = true;
            this.cmbColumns.Location = new System.Drawing.Point(215, 5);
            this.cmbColumns.Name = "cmbColumns";
            this.cmbColumns.Size = new System.Drawing.Size(117, 21);
            this.cmbColumns.TabIndex = 0;
            // 
            // cmbResultTypes
            // 
            this.cmbResultTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbResultTypes.FormattingEnabled = true;
            this.cmbResultTypes.Location = new System.Drawing.Point(106, 5);
            this.cmbResultTypes.Name = "cmbResultTypes";
            this.cmbResultTypes.Size = new System.Drawing.Size(103, 21);
            this.cmbResultTypes.TabIndex = 2;
            // 
            // cmbIDs
            // 
            this.cmbIDs.FormattingEnabled = true;
            this.cmbIDs.Location = new System.Drawing.Point(4, 4);
            this.cmbIDs.Name = "cmbIDs";
            this.cmbIDs.Size = new System.Drawing.Size(96, 21);
            this.cmbIDs.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 29);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1007, 752);
            this.dataGridView1.TabIndex = 4;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(4, 91);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(790, 535);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(782, 509);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Table View";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(782, 509);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Chart View";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(4, 32);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(35, 13);
            this.lblInfo.TabIndex = 6;
            this.lblInfo.Text = "label1";
            // 
            // lblStatistics
            // 
            this.lblStatistics.AutoSize = true;
            this.lblStatistics.Location = new System.Drawing.Point(5, 54);
            this.lblStatistics.Name = "lblStatistics";
            this.lblStatistics.Size = new System.Drawing.Size(35, 13);
            this.lblStatistics.TabIndex = 7;
            this.lblStatistics.Text = "label1";
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(782, 509);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Map View";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // bGoSubbasin
            // 
            this.bGoSubbasin.Location = new System.Drawing.Point(338, 5);
            this.bGoSubbasin.Name = "bGoSubbasin";
            this.bGoSubbasin.Size = new System.Drawing.Size(99, 23);
            this.bGoSubbasin.TabIndex = 8;
            this.bGoSubbasin.Text = "See Subbasin";
            this.bGoSubbasin.UseVisualStyleBackColor = true;
            // 
            // TableResultsCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bGoSubbasin);
            this.Controls.Add(this.lblStatistics);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.cmbIDs);
            this.Controls.Add(this.cmbResultTypes);
            this.Controls.Add(this.cmbColumns);
            this.Name = "TableResultsCtrl";
            this.Size = new System.Drawing.Size(797, 626);
            this.Load += new System.EventHandler(this.TableResultsCtrl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbColumns;
        private System.Windows.Forms.ComboBox cmbResultTypes;
        private System.Windows.Forms.ComboBox cmbIDs;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblStatistics;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button bGoSubbasin;
    }
}
