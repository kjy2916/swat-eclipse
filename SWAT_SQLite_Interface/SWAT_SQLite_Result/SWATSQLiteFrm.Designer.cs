namespace SWAT_SQLite_Result
{
    partial class SWATSQLiteFrm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.bOpen = new System.Windows.Forms.ToolStripButton();
            this.cmbProjects = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bPan = new System.Windows.Forms.ToolStripButton();
            this.bZoomIn = new System.Windows.Forms.ToolStripButton();
            this.bZoomOut = new System.Windows.Forms.ToolStripButton();
            this.bZoomExtent = new System.Windows.Forms.ToolStripButton();
            this.bSelect = new System.Windows.Forms.ToolStripButton();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.lblSelectionInformation = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblMapTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatistics = new System.Windows.Forms.ToolStripStatusLabel();
            this.projectTree1 = new SWAT_SQLite_Result.ProjectTree();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1043, 679);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(1043, 742);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMapTime,
            this.lblSelectionInformation,
            this.lblStatistics});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1043, 24);
            this.statusStrip1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.projectTree1);
            this.splitContainer1.Size = new System.Drawing.Size(1043, 679);
            this.splitContainer1.SplitterDistance = 150;
            this.splitContainer1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bOpen,
            this.cmbProjects,
            this.toolStripSeparator1,
            this.bPan,
            this.bZoomIn,
            this.bZoomOut,
            this.bZoomExtent,
            this.bSelect});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(636, 39);
            this.toolStrip1.TabIndex = 0;
            // 
            // bOpen
            // 
            this.bOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bOpen.Image = global::SWAT_SQLite_Result.Properties.Resources.folder;
            this.bOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bOpen.Name = "bOpen";
            this.bOpen.Size = new System.Drawing.Size(36, 36);
            this.bOpen.Text = "Open ArcSWAT Project";
            this.bOpen.Click += new System.EventHandler(this.bOpen_Click);
            // 
            // cmbProjects
            // 
            this.cmbProjects.Name = "cmbProjects";
            this.cmbProjects.Size = new System.Drawing.Size(400, 39);
            this.cmbProjects.ToolTipText = "Recently Opened Project";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // bPan
            // 
            this.bPan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bPan.Image = global::SWAT_SQLite_Result.Properties.Resources.hand;
            this.bPan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bPan.Name = "bPan";
            this.bPan.Size = new System.Drawing.Size(36, 36);
            this.bPan.Text = "toolStripButton1";
            this.bPan.ToolTipText = "Pan";
            this.bPan.Click += new System.EventHandler(this.bPan_Click);
            // 
            // bZoomIn
            // 
            this.bZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bZoomIn.Image = global::SWAT_SQLite_Result.Properties.Resources.zoom_in;
            this.bZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bZoomIn.Name = "bZoomIn";
            this.bZoomIn.Size = new System.Drawing.Size(36, 36);
            this.bZoomIn.Text = "toolStripButton2";
            this.bZoomIn.ToolTipText = "Zoom In";
            this.bZoomIn.Click += new System.EventHandler(this.bZoomIn_Click);
            // 
            // bZoomOut
            // 
            this.bZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bZoomOut.Image = global::SWAT_SQLite_Result.Properties.Resources.zoom_out;
            this.bZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bZoomOut.Name = "bZoomOut";
            this.bZoomOut.Size = new System.Drawing.Size(36, 36);
            this.bZoomOut.Text = "toolStripButton3";
            this.bZoomOut.ToolTipText = "Zoom Out";
            this.bZoomOut.Click += new System.EventHandler(this.bZoomOut_Click);
            // 
            // bZoomExtent
            // 
            this.bZoomExtent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bZoomExtent.Image = global::SWAT_SQLite_Result.Properties.Resources.zoom_extend;
            this.bZoomExtent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bZoomExtent.Name = "bZoomExtent";
            this.bZoomExtent.Size = new System.Drawing.Size(36, 36);
            this.bZoomExtent.Text = "toolStripButton4";
            this.bZoomExtent.ToolTipText = "Zoom Extent";
            this.bZoomExtent.Click += new System.EventHandler(this.bZoomExtent_Click);
            // 
            // bSelect
            // 
            this.bSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bSelect.Image = global::SWAT_SQLite_Result.Properties.Resources.select;
            this.bSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bSelect.Name = "bSelect";
            this.bSelect.Size = new System.Drawing.Size(36, 36);
            this.bSelect.Text = "toolStripButton5";
            this.bSelect.ToolTipText = "Select";
            this.bSelect.Click += new System.EventHandler(this.bSelect_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "Please Locate the ArcSWAT Project Folder";
            // 
            // lblSelectionInformation
            // 
            this.lblSelectionInformation.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lblSelectionInformation.Name = "lblSelectionInformation";
            this.lblSelectionInformation.Size = new System.Drawing.Size(78, 19);
            this.lblSelectionInformation.Text = "No Selection";
            this.lblSelectionInformation.ToolTipText = "Selection Information";
            // 
            // lblMapTime
            // 
            this.lblMapTime.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lblMapTime.Name = "lblMapTime";
            this.lblMapTime.Size = new System.Drawing.Size(4, 19);
            this.lblMapTime.ToolTipText = "Map Display Time";
            // 
            // lblStatistics
            // 
            this.lblStatistics.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lblStatistics.Name = "lblStatistics";
            this.lblStatistics.Size = new System.Drawing.Size(915, 19);
            this.lblStatistics.Spring = true;
            this.lblStatistics.Text = "No Statistics Data Available";
            this.lblStatistics.ToolTipText = "Statistic Information";
            // 
            // projectTree1
            // 
            this.projectTree1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.projectTree1.Location = new System.Drawing.Point(0, 0);
            this.projectTree1.Name = "projectTree1";
            this.projectTree1.Project = null;
            this.projectTree1.Size = new System.Drawing.Size(150, 679);
            this.projectTree1.TabIndex = 0;
            // 
            // SWATSQLiteFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1043, 742);
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "SWATSQLiteFrm";
            this.Text = "SWATSQLiteFrm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SWATSQLiteFrm_Load);
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton bOpen;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private ProjectTree projectTree1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ToolStripComboBox cmbProjects;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton bPan;
        private System.Windows.Forms.ToolStripButton bZoomIn;
        private System.Windows.Forms.ToolStripButton bZoomOut;
        private System.Windows.Forms.ToolStripButton bZoomExtent;
        private System.Windows.Forms.ToolStripButton bSelect;
        private System.Windows.Forms.ToolStripStatusLabel lblMapTime;
        private System.Windows.Forms.ToolStripStatusLabel lblSelectionInformation;
        private System.Windows.Forms.ToolStripStatusLabel lblStatistics;

    }
}