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

        private void addComparableResult(ArcSWAT.ScenarioResult result)
        {
            if (result == null) return;

            string id = string.Format("{0}.{1}", result.Scenario.Name, result.ModelType);
            cmbCompareResults.Items.Add(id);
            _comparableResult.Add(result);
        }

        public bool HasObervedData
        {
            set
            {
                if (!_hasObservedData && value)
                {
                    cmbCompareResults.Items.Add("Observed Data");
                    _hasObservedData = value;
                }
                if (_hasObservedData && !value)
                {
                    int selectIndex = cmbCompareResults.SelectedIndex;
                    cmbCompareResults.Items.RemoveAt(cmbCompareResults.Items.Count - 1);
                    if (selectIndex == cmbCompareResults.Items.Count && 
                        cmbCompareResults.Items.Count > 0)
                        selectIndex = 0;
                    _hasObservedData = value;
                }
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
                _comparableResult.Clear();              

                //look for results in same scenario
                for (int i = Convert.ToInt32(ArcSWAT.SWATModelType.SWAT); i <= Convert.ToInt32(ArcSWAT.SWATModelType.CanSWAT); i++)
                {
                    ArcSWAT.SWATModelType modelType = (ArcSWAT.SWATModelType)i;
                    if (modelType == value.ModelType) continue;

                    addComparableResult(value.Scenario.getModelResult(modelType));
                }

                //look for results in other scenario
                foreach (string scenName in value.Scenario.Project.Scenarios.Keys)
                {
                    ArcSWAT.Scenario compareScenario = value.Scenario.Project.Scenarios[scenName];
                    if (value.Scenario == compareScenario) continue;

                    addComparableResult(compareScenario.getModelResult(value.ModelType));
                }
            }
        }

        public event EventHandler onCompareResultChanged = null;

        private void CompareCtrl_Load(object sender, EventArgs e)
        {
            cmbCompareResults.SelectedIndexChanged += (ss, ee) =>
                {
                    if (onCompareResultChanged != null) onCompareResultChanged(this,new EventArgs());                    
                };
            chbCompare.CheckedChanged += (ss, ee) =>
                {
                    cmbCompareResults.Enabled = chbCompare.Checked;
                    if (onCompareResultChanged != null) onCompareResultChanged(this, new EventArgs()); 
                };
            cmbCompareResults.Enabled = chbCompare.Checked;
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
    }
}
