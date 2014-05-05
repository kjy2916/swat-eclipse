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

            resultColumnTree1.onResultTypeAndColumnChanged += (resultType, col) => { _resultType = resultType; _col = col; updateTableAndChart(); };
            cmbCompareResults.SelectedIndexChanged += (s, e) => { updateTableAndChart(); };
            
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

            //look for results in same scenario
            for (int i = Convert.ToInt32(ArcSWAT.SWATModelType.SWAT); i <= Convert.ToInt32(ArcSWAT.SWATModelType.CanSWAT); i++)
            {
                ArcSWAT.SWATModelType modelType = (ArcSWAT.SWATModelType)i;
                if (modelType == _scenarioResult.ModelType) continue;

                addComparableResult(_scenarioResult.Scenario.getModelResult(modelType));
            }

            //look for results in other scenario
            foreach (string scenName in _scenarioResult.Scenario.Project.Scenarios.Keys)
            {
                ArcSWAT.Scenario compareScenario = _scenarioResult.Scenario.Project.Scenarios[scenName];
                if (_scenarioResult.Scenario == compareScenario) continue;

                addComparableResult(compareScenario.getModelResult(_scenarioResult.ModelType));
            }

            if (cmbCompareResults.Items.Count > 0)
                cmbCompareResults.SelectedIndex = 0;
        }

        private void addComparableResult(ArcSWAT.ScenarioResult result)
        {
            if (result == null) return;

            string id = string.Format("{0}.{1}", result.Scenario.Name, result.ModelType);
            cmbCompareResults.Items.Add(id);
            _comparableResult.Add(result);
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

        private void updateTableAndChart()
        {
            if (_scenarioResult == null) return;
            if (_resultType == null) return;
            if (_col == null) return;

            ArcSWAT.ScenarioResult compare = CompareResult;
            if (compare == null) return;

            DataTable dt = calculateDifference(UnitType, _resultType, _col, compare);
            tableView1.DataTable = dt;
            chart1.DataSource = dt;
            chart1.Invalidate();
        }

        private DataSet _differenceDataset = new System.Data.DataSet();

        private DataTable calculateDifference(ArcSWAT.SWATUnitType type,string resultType, string col, 
            ArcSWAT.ScenarioResult compareScenario)
        {            
            string tableId = string.Format("{0}_{1}_{2}_{3}_{4}",type,resultType,col,
                compareScenario.ModelType, compareScenario.Scenario.Name);
            if (!_differenceDataset.Tables.Contains(tableId))
            {
                List<int> ids = _scenarioResult.getSWATUnitIDs(type);

                DataTable dt = createDifferentDataTable(tableId);
                foreach (int id in ids)
                {
                    ArcSWAT.SWATUnit unit = _scenarioResult.getSWATUnit(type, id);
                    if (unit == null) continue;

                    ArcSWAT.SWATUnitResult unitResult = unit.getResult(resultType);
                    if (unitResult == null) continue;

                    ArcSWAT.SWATUnitColumnYearResult oneUnitResult = unitResult.getResult(col, -1);
                    if (oneUnitResult == null) continue;

                    try
                    {
                        DataRow r = dt.NewRow();
                        r[0] = id;
                        r[1] = oneUnitResult.Compare(compareScenario).Statistics.R2;
                        dt.Rows.Add(r);
                    }
                    catch (System.Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine(e.Message);
                    }
                }
                _differenceDataset.Tables.Add(dt);
            }
            return _differenceDataset.Tables[tableId];
        }

       

        private DataTable createDifferentDataTable(string tableName)
        {
            DataTable dt = new System.Data.DataTable(tableName);
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("R2", typeof(double));

            return dt;
        }
    }
}
