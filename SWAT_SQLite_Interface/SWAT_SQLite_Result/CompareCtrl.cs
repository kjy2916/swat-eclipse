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
    public partial class CompareCtrl : UserControl
    {
        public CompareCtrl()
        {
            InitializeComponent();
        }

        private bool _hasObservedData = false;
        private List<ArcSWAT.ScenarioResult> _comparableResult = new List<ArcSWAT.ScenarioResult>();

        public bool HasObervedData
        {
            set
            {
                if (!_hasObservedData && value)
                {
                    cmbCompareResults.Items.Add("Observed Data");
                    _hasObservedData = value;

                    if (chbCompare.Checked && cmbCompareResults.Items.Count > 0 && cmbCompareResults.SelectedIndex == -1)
                        cmbCompareResults.SelectedIndex = 0;                                        
                }
                if (_hasObservedData && !value)
                {
                    int selectIndex = cmbCompareResults.SelectedIndex;
                    cmbCompareResults.Items.RemoveAt(cmbCompareResults.Items.Count - 1);
                    if (selectIndex == cmbCompareResults.Items.Count &&
                        cmbCompareResults.Items.Count > 0)
                        selectIndex = 0;
                    else
                        cmbCompareResults.Text = "";
                    _hasObservedData = value;
                }
                this.Enabled = cmbCompareResults.Items.Count > 0;
            }
            get
            {
                return _hasObservedData;
            }
        }

        public ArcSWAT.ScenarioResult ScenarioResult
        {
            set
            {
                cmbCompareResults.Items.Clear();
                cmbSplitYear.Items.Clear();
                _comparableResult.Clear();              

                if (value == null) return;

                _comparableResult = value.ComparableScenarioResults;
                foreach (ArcSWAT.ScenarioResult r in _comparableResult)
                    cmbCompareResults.Items.Add(string.Format("{0}.{1}", r.Scenario.Name, r.ModelType));

                for (int i = value.StartYear; i <= value.EndYear; i++)
                    cmbSplitYear.Items.Add(i);

                this.Enabled = cmbCompareResults.Items.Count > 0;
            }
        }

        public event EventHandler onCompareResultChanged = null;
        public event EventHandler onCompareStatisticSplitYearChanged = null;

        private void CompareCtrl_Load(object sender, EventArgs e)
        {
            cmbSplitYear.SelectedIndexChanged += (ss, ee) =>
                {
                    if (onCompareStatisticSplitYearChanged != null) onCompareStatisticSplitYearChanged(this, new EventArgs());
                };
            cmbCompareResults.SelectedIndexChanged += (ss, ee) =>
                {
                    if (onCompareResultChanged != null) onCompareResultChanged(this,new EventArgs());                    
                };
            chbCompare.CheckedChanged += (ss, ee) =>
                {
                    cmbCompareResults.Enabled = chbCompare.Checked;
                    cmbSplitYear.Enabled = chbCompare.Checked;
                    if (chbCompare.Checked && cmbCompareResults.Items.Count > 0 && cmbCompareResults.SelectedIndex == -1)
                        cmbCompareResults.SelectedIndex = 0;

                    if (onCompareResultChanged != null) onCompareResultChanged(this, new EventArgs()); 
                };
            cmbCompareResults.Enabled = chbCompare.Checked;
            cmbSplitYear.Enabled = chbCompare.Checked;
        }

        public bool IsObservedDataSelected
        {
            get 
            {
                if (!chbCompare.Checked) return false;
                if (cmbCompareResults.SelectedIndex < 0) return false;
                if (cmbCompareResults.SelectedIndex >= _comparableResult.Count) return true;
                return false;
            }
        }

        public ArcSWAT.ScenarioResult CompareResult
        {
            get
            {
                if (!chbCompare.Checked) return null;
                if (cmbCompareResults.SelectedIndex < 0) return null;
                if (cmbCompareResults.SelectedIndex >= _comparableResult.Count) return null;

                return _comparableResult[cmbCompareResults.SelectedIndex];
            }
        }
        
        /// <summary>
        /// This is mostly used when compared with observed data. Usually the simulation period 
        /// is split into two: one for calibration and another for validation. This year here is
        /// the first year of the second period.
        /// </summary>
        public int SplitYearForStatistics
        {
            get
            {
                if (!chbCompare.Checked) return -1;
                if (cmbSplitYear.SelectedIndex < 0) return -1;
                return Convert.ToInt32(cmbSplitYear.SelectedItem.ToString());
            }
        }
    }
}
