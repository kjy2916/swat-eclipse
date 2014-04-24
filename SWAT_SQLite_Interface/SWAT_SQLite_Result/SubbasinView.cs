using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SWAT_SQLite_Result
{
    public partial class SubbasinView : UserControl
    {
        public SubbasinView()
        {
            InitializeComponent();
        }

        private string _resultType = null;
        private string _col = null;
        private int _id = -1;
        private DateTime _date = DateTime.Now;

        private ArcSWAT.Project _project = null;
        private ArcSWAT.ScenarioResult _scenario = null;

        public void setProjectScenario(ArcSWAT.Project project, ArcSWAT.ScenarioResult scenario)
        {
            _project = project;
            _scenario = scenario;
            _date = new DateTime(scenario.StartYear, 1, 1);

            //columns
            resultColumnTree1.onResultTypeAndColumnChanged += (resultType, col) =>
            {
                _resultType = resultType;
                _col = col;
                updateMap();
                updateTableAndChart();
            };
            resultColumnTree1.setScenarioAndUnit(scenario, ArcSWAT.SWATUnitType.SUB);

            //map
            subbasinMap1.onLayerSelectionChanged += (id) =>
            {
                _id = id;
                updateTableAndChart();
            };
            subbasinMap1.setProjectScenario(project, scenario, ArcSWAT.SWATUnitType.SUB);

            //table view
            tableView1.onDateChanged += (d) => { _date = d; updateMap(); };

            //update
            updateMap();
            updateTableAndChart();
        }

        private void updateMap()
        {
            if (_resultType == null || _col == null) return;
            subbasinMap1.drawLayer(_resultType, _col, _date);
        }

        private void updateTableAndChart()
        {
            if (_resultType == null || _col == null || _id <= 0) return;

            if (!_scenario.Subbasins.ContainsKey(_id)) return;

            ArcSWAT.SWATUnit unit = _scenario.Subbasins[_id];
            if (!unit.Results.ContainsKey(_resultType)) return;

            ArcSWAT.SWATUnitResult result = unit.Results[_resultType];
            if (!result.Columns.Contains(_col)) return;

            DataTable dt = result.getDataTable(_col);
            this.tableView1.SWATResultTable = dt;

            StringCollection cols = new StringCollection() { _col };
            this.outputDisplayChart1.DrawGraph(dt.Rows, ArcSWAT.SWATUnitResult.COLUMN_NAME_DATE, cols, result.Interval);

            this.lblStatistics.Text = result.getStatistics(_col).ToString();
            this.lblInfo.Text = unit.ToStringBasicInfo();
        }
    }
}
