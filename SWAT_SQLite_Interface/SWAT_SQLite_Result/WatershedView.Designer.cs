namespace SWAT_SQLite_Result
{
    partial class WatershedView
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
            this.resultColumnTree1 = new SWAT_SQLite_Result.ResultColumnTree();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tableView1 = new SWAT_SQLite_Result.TableView();
            this.outputDisplayChart1 = new SWAT_SQLite_Result.OutputDisplayChart();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
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
            // resultColumnTree1
            // 
            this.resultColumnTree1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultColumnTree1.Location = new System.Drawing.Point(0, 0);
            this.resultColumnTree1.Name = "resultColumnTree1";
            this.resultColumnTree1.Size = new System.Drawing.Size(168, 874);
            this.resultColumnTree1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.outputDisplayChart1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tableView1);
            this.splitContainer2.Size = new System.Drawing.Size(862, 874);
            this.splitContainer2.SplitterDistance = 705;
            this.splitContainer2.TabIndex = 0;
            // 
            // tableView1
            // 
            this.tableView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableView1.Location = new System.Drawing.Point(0, 0);
            this.tableView1.Name = "tableView1";
            this.tableView1.ReadOnly = true;
            this.tableView1.RowHeadersVisible = false;
            this.tableView1.Size = new System.Drawing.Size(153, 874);
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
            this.outputDisplayChart1.Size = new System.Drawing.Size(705, 874);
            this.outputDisplayChart1.TabIndex = 0;
            this.outputDisplayChart1.Text = "outputDisplayChart1";
            // 
            // WatershedView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "WatershedView";
            this.Size = new System.Drawing.Size(1034, 874);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tableView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.outputDisplayChart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private ResultColumnTree resultColumnTree1;
        private OutputDisplayChart outputDisplayChart1;
        private TableView tableView1;
    }
}
