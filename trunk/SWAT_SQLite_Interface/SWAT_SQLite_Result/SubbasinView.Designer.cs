namespace SWAT_SQLite_Result
{
    partial class SubbasinView
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
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.lblStatistics = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.lblDate = new System.Windows.Forms.Label();
            this.resultColumnTree1 = new SWAT_SQLite_Result.ResultColumnTree();
            this.hruList1 = new SWAT_SQLite_Result.HRUList();
            this.subbasinMap1 = new SWAT_SQLite_Result.SubbasinMap();
            this.tableView1 = new SWAT_SQLite_Result.TableView();
            this.outputDisplayChart1 = new SWAT_SQLite_Result.OutputDisplayChart();
            this.yearCtrl1 = new SWAT_SQLite_Result.YearCtrl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tableView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.outputDisplayChart1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.resultColumnTree1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1034, 874);
            this.splitContainer1.SplitterDistance = 168;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.outputDisplayChart1);
            this.splitContainer2.Size = new System.Drawing.Size(862, 874);
            this.splitContainer2.SplitterDistance = 705;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.yearCtrl1);
            this.splitContainer3.Panel1.Controls.Add(this.lblDate);
            this.splitContainer3.Panel1.Controls.Add(this.hruList1);
            this.splitContainer3.Panel1.Controls.Add(this.lblStatistics);
            this.splitContainer3.Panel1.Controls.Add(this.lblInfo);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer3.Size = new System.Drawing.Size(862, 705);
            this.splitContainer3.SplitterDistance = 76;
            this.splitContainer3.TabIndex = 0;
            // 
            // lblStatistics
            // 
            this.lblStatistics.AutoSize = true;
            this.lblStatistics.Location = new System.Drawing.Point(10, 24);
            this.lblStatistics.Name = "lblStatistics";
            this.lblStatistics.Size = new System.Drawing.Size(59, 13);
            this.lblStatistics.TabIndex = 1;
            this.lblStatistics.Text = "lblStatistics";
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(10, 4);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(35, 13);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "lblInfo";
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.subbasinMap1);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.tableView1);
            this.splitContainer4.Size = new System.Drawing.Size(862, 625);
            this.splitContainer4.SplitterDistance = 658;
            this.splitContainer4.TabIndex = 0;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(10, 46);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(40, 13);
            this.lblDate.TabIndex = 3;
            this.lblDate.Text = "lblDate";
            // 
            // resultColumnTree1
            // 
            this.resultColumnTree1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultColumnTree1.Location = new System.Drawing.Point(0, 0);
            this.resultColumnTree1.Name = "resultColumnTree1";
            this.resultColumnTree1.Size = new System.Drawing.Size(168, 874);
            this.resultColumnTree1.TabIndex = 0;
            // 
            // hruList1
            // 
            this.hruList1.Location = new System.Drawing.Point(103, 44);
            this.hruList1.Name = "hruList1";
            this.hruList1.Size = new System.Drawing.Size(252, 27);
            this.hruList1.TabIndex = 2;
            // 
            // subbasinMap1
            // 
            this.subbasinMap1.AllowDrop = true;
            this.subbasinMap1.BackColor = System.Drawing.Color.White;
            this.subbasinMap1.CollectAfterDraw = false;
            this.subbasinMap1.CollisionDetection = false;
            this.subbasinMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.subbasinMap1.ExtendBuffer = false;
            this.subbasinMap1.FunctionMode = DotSpatial.Controls.FunctionMode.None;
            this.subbasinMap1.IsBusy = false;
            this.subbasinMap1.IsZoomedToMaxExtent = false;
            this.subbasinMap1.Legend = null;
            this.subbasinMap1.Location = new System.Drawing.Point(0, 0);
            this.subbasinMap1.Name = "subbasinMap1";
            this.subbasinMap1.ProgressHandler = null;
            this.subbasinMap1.ProjectionModeDefine = DotSpatial.Controls.ActionMode.Prompt;
            this.subbasinMap1.ProjectionModeReproject = DotSpatial.Controls.ActionMode.Prompt;
            this.subbasinMap1.RedrawLayersWhileResizing = false;
            this.subbasinMap1.SelectionEnabled = true;
            this.subbasinMap1.Size = new System.Drawing.Size(658, 625);
            this.subbasinMap1.TabIndex = 0;
            // 
            // tableView1
            // 
            this.tableView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableView1.Location = new System.Drawing.Point(0, 0);
            this.tableView1.Name = "tableView1";
            this.tableView1.ReadOnly = true;
            this.tableView1.RowHeadersVisible = false;
            this.tableView1.Size = new System.Drawing.Size(200, 625);
            this.tableView1.TabIndex = 0;
            // 
            // outputDisplayChart1
            // 
            chartArea1.Name = "ChartArea1";
            this.outputDisplayChart1.ChartAreas.Add(chartArea1);
            this.outputDisplayChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.outputDisplayChart1.Legends.Add(legend1);
            this.outputDisplayChart1.Location = new System.Drawing.Point(0, 0);
            this.outputDisplayChart1.Name = "outputDisplayChart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.outputDisplayChart1.Series.Add(series1);
            this.outputDisplayChart1.Size = new System.Drawing.Size(862, 165);
            this.outputDisplayChart1.TabIndex = 0;
            this.outputDisplayChart1.Text = "outputDisplayChart1";
            // 
            // yearCtrl1
            // 
            this.yearCtrl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.yearCtrl1.Location = new System.Drawing.Point(666, 4);
            this.yearCtrl1.Name = "yearCtrl1";
            this.yearCtrl1.Size = new System.Drawing.Size(196, 73);
            this.yearCtrl1.TabIndex = 4;
            // 
            // SubbasinView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "SubbasinView";
            this.Size = new System.Drawing.Size(1034, 874);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tableView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.outputDisplayChart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private ResultColumnTree resultColumnTree1;
        private SubbasinMap subbasinMap1;
        private TableView tableView1;
        private OutputDisplayChart outputDisplayChart1;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblStatistics;
        private HRUList hruList1;
        private System.Windows.Forms.Label lblDate;
        private YearCtrl yearCtrl1;
    }
}
