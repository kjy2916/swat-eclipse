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
                DataTable dt = _result.getPerformanceTalbe(year);
                if (!_warningHasShown && dt.Rows.Count == 0)
                {
                    SWAT_SQLite.showInformationWindow("No performance data. Please make sure the observed data has been uploaded and the simulation results exists!");
                    _warningHasShown = true;
                }
                this.dataGridView1.DataSource = dt;
            };

            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeaderMouseClick += (s, e) =>
            {
                if (e.RowIndex < 0) return;
                
                //get selected unit type and id
                string unitType = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                
                //get the unit
                ArcSWAT.SWATUnit unit = null;
                if (unitType == ArcSWAT.SWATUnitType.RCH.ToString())
                    unit = _result.getSWATUnit(ArcSWAT.SWATUnitType.RCH, id);
                else if (unitType == ArcSWAT.SWATUnitType.RES.ToString())
                    unit = _result.getSWATUnit(ArcSWAT.SWATUnitType.RES, id);
                else
                    return;

                if (unit == null) return;

                //get unit results
                foreach (ArcSWAT.SWATUnitResult unitResult in unit.Results.Values)
                {
                    string col = ArcSWAT.ObservationData.getObservationSWATColumn(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                    ArcSWAT.SWATUnitColumnYearResult oneResult = unitResult.getResult(col, -1);
                    if (oneResult != null)
                    {
                        this.outputDisplayChart1.CompareResult = oneResult.CompareWithObserved;
                        return;
                    }
                }
            };
        }

        private ArcSWAT.ScenarioResult _result = null;
        private bool _warningHasShown = false;

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
