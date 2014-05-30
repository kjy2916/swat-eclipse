using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SWAT_SQLite_Result
{
    /// <summary>
    /// Show performance of all SWAT units (with observed data) compared to observed data in one interface
    /// </summary>
    public partial class PerformanceView : UserControl
    {
        public PerformanceView()
        {
            InitializeComponent();

            this.Resize += (s, e) => { this.splitContainer1.SplitterDistance = this.Height - 250; }; //always set the height of chart as 250

            cmbSplitYear.SelectedIndexChanged += (s, e) => {
                if (_result == null) return;
                int year = Convert.ToInt32(cmbSplitYear.SelectedItem.ToString());
                this.dataGridView1.DataSource = _result.getPerformanceTalbe(year);
            };
        }

        private ArcSWAT.ScenarioResult _result = null;

        public ArcSWAT.ScenarioResult Result
        {
            set
            {
                _result = value;

                cmbSplitYear.Items.Clear();
                cmbSplitYear.Items.Add(-1);
                for (int i = value.StartYear; i <= value.EndYear; i++)
                    cmbSplitYear.Items.Add(i);
                cmbSplitYear.SelectedIndex = 0;
            }
        }
    }
}
