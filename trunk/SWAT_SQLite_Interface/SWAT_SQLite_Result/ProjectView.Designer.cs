namespace SWAT_SQLite_Result
{
    partial class ProjectView
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
            this.subbasinMap1 = new SWAT_SQLite_Result.SubbasinMap();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbObservedColumns = new System.Windows.Forms.ComboBox();
            this.tableView1 = new SWAT_SQLite_Result.TableView();
            this.bDeleteObservationData = new System.Windows.Forms.Button();
            this.bLoadObservationData = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.outputDisplayChart1 = new SWAT_SQLite_Result.OutputDisplayChart();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tableView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.subbasinMap1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(860, 533);
            this.splitContainer1.SplitterDistance = 650;
            this.splitContainer1.TabIndex = 0;
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
            this.subbasinMap1.Size = new System.Drawing.Size(650, 533);
            this.subbasinMap1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbObservedColumns);
            this.groupBox1.Controls.Add(this.tableView1);
            this.groupBox1.Controls.Add(this.bDeleteObservationData);
            this.groupBox1.Controls.Add(this.bLoadObservationData);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(206, 533);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Observation";
            // 
            // cmbObservedColumns
            // 
            this.cmbObservedColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbObservedColumns.FormattingEnabled = true;
            this.cmbObservedColumns.Location = new System.Drawing.Point(7, 20);
            this.cmbObservedColumns.Name = "cmbObservedColumns";
            this.cmbObservedColumns.Size = new System.Drawing.Size(193, 21);
            this.cmbObservedColumns.TabIndex = 2;
            // 
            // tableView1
            // 
            this.tableView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableView1.Location = new System.Drawing.Point(7, 76);
            this.tableView1.Name = "tableView1";
            this.tableView1.ReadOnly = true;
            this.tableView1.RowHeadersVisible = false;
            this.tableView1.Size = new System.Drawing.Size(193, 451);
            this.tableView1.TabIndex = 1;
            // 
            // bDeleteObservationData
            // 
            this.bDeleteObservationData.Location = new System.Drawing.Point(83, 47);
            this.bDeleteObservationData.Name = "bDeleteObservationData";
            this.bDeleteObservationData.Size = new System.Drawing.Size(70, 23);
            this.bDeleteObservationData.TabIndex = 0;
            this.bDeleteObservationData.Text = "Delete";
            this.bDeleteObservationData.UseVisualStyleBackColor = true;
            // 
            // bLoadObservationData
            // 
            this.bLoadObservationData.Location = new System.Drawing.Point(7, 47);
            this.bLoadObservationData.Name = "bLoadObservationData";
            this.bLoadObservationData.Size = new System.Drawing.Size(70, 23);
            this.bLoadObservationData.TabIndex = 0;
            this.bLoadObservationData.Text = "Load";
            this.bLoadObservationData.UseVisualStyleBackColor = true;
            this.bLoadObservationData.Click += new System.EventHandler(this.bLoadObservationData_Click);
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
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.outputDisplayChart1);
            this.splitContainer2.Size = new System.Drawing.Size(860, 665);
            this.splitContainer2.SplitterDistance = 533;
            this.splitContainer2.TabIndex = 1;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Observed files|*.csv";
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
            this.outputDisplayChart1.Size = new System.Drawing.Size(860, 128);
            this.outputDisplayChart1.TabIndex = 0;
            this.outputDisplayChart1.Text = "outputDisplayChart1";
            // 
            // ProjectView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer2);
            this.Name = "ProjectView";
            this.Size = new System.Drawing.Size(860, 665);
            this.Load += new System.EventHandler(this.ProjectView_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tableView1)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.outputDisplayChart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private SubbasinMap subbasinMap1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button bLoadObservationData;
        private System.Windows.Forms.Button bDeleteObservationData;
        private TableView tableView1;
        private System.Windows.Forms.ComboBox cmbObservedColumns;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private OutputDisplayChart outputDisplayChart1;

    }
}
