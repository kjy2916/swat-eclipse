namespace SWAT_SQLite_Result
{
    partial class ScenarioComparasionReportView
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cmbCompareResults = new System.Windows.Forms.ComboBox();
            this.cmbReach = new System.Windows.Forms.RadioButton();
            this.cmbSubbasin = new System.Windows.Forms.RadioButton();
            this.cmbHRU = new System.Windows.Forms.RadioButton();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.resultColumnTree1 = new SWAT_SQLite_Result.ResultColumnTree();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableView1 = new SWAT_SQLite_Result.TableView();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.cmbReservoir = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableView1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cmbCompareResults);
            this.splitContainer1.Panel1.Controls.Add(this.cmbReservoir);
            this.splitContainer1.Panel1.Controls.Add(this.cmbReach);
            this.splitContainer1.Panel1.Controls.Add(this.cmbSubbasin);
            this.splitContainer1.Panel1.Controls.Add(this.cmbHRU);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(843, 814);
            this.splitContainer1.SplitterDistance = 26;
            this.splitContainer1.TabIndex = 0;
            // 
            // cmbCompareResults
            // 
            this.cmbCompareResults.FormattingEnabled = true;
            this.cmbCompareResults.Location = new System.Drawing.Point(279, 2);
            this.cmbCompareResults.Name = "cmbCompareResults";
            this.cmbCompareResults.Size = new System.Drawing.Size(121, 21);
            this.cmbCompareResults.TabIndex = 1;
            // 
            // cmbReach
            // 
            this.cmbReach.AutoSize = true;
            this.cmbReach.Location = new System.Drawing.Point(134, 4);
            this.cmbReach.Name = "cmbReach";
            this.cmbReach.Size = new System.Drawing.Size(57, 17);
            this.cmbReach.TabIndex = 0;
            this.cmbReach.TabStop = true;
            this.cmbReach.Text = "Reach";
            this.cmbReach.UseVisualStyleBackColor = true;
            // 
            // cmbSubbasin
            // 
            this.cmbSubbasin.AutoSize = true;
            this.cmbSubbasin.Location = new System.Drawing.Point(59, 4);
            this.cmbSubbasin.Name = "cmbSubbasin";
            this.cmbSubbasin.Size = new System.Drawing.Size(69, 17);
            this.cmbSubbasin.TabIndex = 0;
            this.cmbSubbasin.TabStop = true;
            this.cmbSubbasin.Text = "Subbasin";
            this.cmbSubbasin.UseVisualStyleBackColor = true;
            // 
            // cmbHRU
            // 
            this.cmbHRU.AutoSize = true;
            this.cmbHRU.Location = new System.Drawing.Point(4, 4);
            this.cmbHRU.Name = "cmbHRU";
            this.cmbHRU.Size = new System.Drawing.Size(49, 17);
            this.cmbHRU.TabIndex = 0;
            this.cmbHRU.TabStop = true;
            this.cmbHRU.Text = "HRU";
            this.cmbHRU.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(843, 784);
            this.splitContainer2.SplitterDistance = 161;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.resultColumnTree1);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.richTextBox1);
            this.splitContainer4.Size = new System.Drawing.Size(161, 784);
            this.splitContainer4.SplitterDistance = 537;
            this.splitContainer4.TabIndex = 1;
            // 
            // resultColumnTree1
            // 
            this.resultColumnTree1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultColumnTree1.Location = new System.Drawing.Point(0, 0);
            this.resultColumnTree1.Name = "resultColumnTree1";
            this.resultColumnTree1.Size = new System.Drawing.Size(161, 537);
            this.resultColumnTree1.TabIndex = 0;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(161, 243);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.chart1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.tableView1);
            this.splitContainer3.Size = new System.Drawing.Size(678, 784);
            this.splitContainer3.SplitterDistance = 469;
            this.splitContainer3.TabIndex = 0;
            // 
            // chart1
            // 
            chartArea1.AxisY.Maximum = 1D;
            chartArea1.AxisY.Minimum = 0D;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series1.ToolTip = "#VALX : #VALY{F4} ";
            series1.XValueMember = "ID";
            series1.YValueMembers = "R2";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(469, 784);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // tableView1
            // 
            this.tableView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableView1.Location = new System.Drawing.Point(0, 0);
            this.tableView1.Name = "tableView1";
            this.tableView1.ReadOnly = true;
            this.tableView1.RowHeadersVisible = false;
            this.tableView1.Season = SWAT_SQLite_Result.ArcSWAT.SeasonType.WholeYear;
            this.tableView1.Size = new System.Drawing.Size(205, 784);
            this.tableView1.TabIndex = 0;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            // 
            // cmbReservoir
            // 
            this.cmbReservoir.AutoSize = true;
            this.cmbReservoir.Location = new System.Drawing.Point(197, 4);
            this.cmbReservoir.Name = "cmbReservoir";
            this.cmbReservoir.Size = new System.Drawing.Size(70, 17);
            this.cmbReservoir.TabIndex = 0;
            this.cmbReservoir.TabStop = true;
            this.cmbReservoir.Text = "Reservoir";
            this.cmbReservoir.UseVisualStyleBackColor = true;
            // 
            // ScenarioComparasionReportView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ScenarioComparasionReportView";
            this.Size = new System.Drawing.Size(843, 814);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RadioButton cmbReach;
        private System.Windows.Forms.RadioButton cmbSubbasin;
        private System.Windows.Forms.RadioButton cmbHRU;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private ResultColumnTree resultColumnTree1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private TableView tableView1;
        private System.Windows.Forms.ComboBox cmbCompareResults;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.RadioButton cmbReservoir;

    }
}
