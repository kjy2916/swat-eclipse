using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SWAT_SQLite_Result
{
    /// <summary>
    /// List R2 and NSE for all HRUs, Subbasins and Reaches in table
    /// May take a little bit longer for big model.
    /// Try to give the difference between two scenarios
    /// </summary>
    public partial class ScenarioComparasionReportView : UserControl
    {
        public ScenarioComparasionReportView()
        {
            InitializeComponent();

            cmbHRU.CheckedChanged += (s, e) => { updateUnitType(); };
            cmbReach.CheckedChanged += (s, e) => { updateUnitType(); };
            cmbSubbasin.CheckedChanged += (s, e) => { updateUnitType(); };

            resultColumnTree1.onResultTypeAndColumnChanged += (resultType, col) => { _resultType = resultType; _col = col; this.backgroundWorker1.RunWorkerAsync(); };
            cmbCompareResults.SelectedIndexChanged += (s, e) => { _compareResult = CompareResult; this.backgroundWorker1.RunWorkerAsync(); };

            backgroundWorker1.DoWork += (s, e) => { updateTableAndChart(); };
            backgroundWorker1.ProgressChanged += (s, e) => { richTextBox1.AppendText(e.UserState.ToString() + "\n"); richTextBox1.SelectionStart = richTextBox1.Text.Length; richTextBox1.ScrollToCaret(); };
            backgroundWorker1.RunWorkerCompleted += (s, e) => 
            {
                tableView1.DataTable = _differentTable;
                chart1.DataSource = _differentTable;
                chart1.Invalidate();

                if (_differentTable == null) return;
                richTextBox1.AppendText("Finished.\n"); 
            };

            //default select reach
            cmbReach.Checked = true;
        }

        private ArcSWAT.SWATUnitType UnitType
        {
            get
            {
                if (cmbHRU.Checked) return ArcSWAT.SWATUnitType.HRU;
                if (cmbReach.Checked) return ArcSWAT.SWATUnitType.RCH;
                if (cmbSubbasin.Checked) return ArcSWAT.SWATUnitType.SUB;
                return ArcSWAT.SWATUnitType.UNKNOWN;
            }
        }

        private void updateUnitType()
        {
            _resultType = null;
            _col = null;
            resultColumnTree1.setScenarioAndUnit(_scenarioResult, UnitType);
        }

        private List<ArcSWAT.ScenarioResult> _comparableResult = new List<ArcSWAT.ScenarioResult>();

        private void addCompareResults()
        {
            cmbCompareResults.Items.Clear();
            _comparableResult.Clear();

            if (_scenarioResult == null) return;

            _comparableResult = _scenarioResult.ComparableScenarioResults;
            foreach (ArcSWAT.ScenarioResult r in _comparableResult)
                cmbCompareResults.Items.Add(string.Format("{0}.{1}", r.Scenario.Name, r.ModelType));

            if (cmbCompareResults.Items.Count > 0)
                cmbCompareResults.SelectedIndex = 0;
            else
                SWAT_SQLite.showInformationWindow("No comparable scenarios!");
        }

        private ArcSWAT.ScenarioResult CompareResult
        {
            get
            {
                if (cmbCompareResults.SelectedIndex < 0) return null;
                if (cmbCompareResults.SelectedIndex >= _comparableResult.Count) return null;

                return _comparableResult[cmbCompareResults.SelectedIndex];
            }
        }

        private ArcSWAT.ScenarioResult _scenarioResult = null;

        public ArcSWAT.ScenarioResult Result
        {
            set
            {                
                _scenarioResult = value;
                updateUnitType();
                addCompareResults();
            }            
        }

        private string _resultType = null;
        private string _col = null;
        private ArcSWAT.ScenarioResult _compareResult = null;
        private DataTable _differentTable = null;

        private void updateTableAndChart()
        {
            _differentTable = null;

            if (_scenarioResult == null) return;
            if (_resultType == null) return;
            if (_col == null) return;
            if (_compareResult == null) return;

            _differentTable = _scenarioResult.getDifference(
                UnitType, _resultType, _col, 
                _compareResult, backgroundWorker1);
        }
    }
}
