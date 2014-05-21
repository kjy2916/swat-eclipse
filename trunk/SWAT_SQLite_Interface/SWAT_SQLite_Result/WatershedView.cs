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
    public partial class WatershedView : UserControl
    {
        public WatershedView()
        {
            InitializeComponent();
        }

        private string _resultType = null;
        private string _col = null;
        private DateTime _date = DateTime.Now;
        private ArcSWAT.ScenarioResult _compareResult = null;

        private ArcSWAT.Project _project = null;
        private ArcSWAT.ScenarioResult _scenario = null;

        /// <summary>
        /// Happens when statistic information is changed
        /// </summary>
        public event EventHandler onDataStatisticsChanged = null;

        private string _statistics = "No Statistics Data Available";
        public string Statistics { get { return _statistics; } }

        public void setProjectScenario(ArcSWAT.Project project, ArcSWAT.ScenarioResult scenario)
        {
            this.Resize += (ss, ee) => { this.splitContainer2.SplitterDistance = 72; };

            _project = project;
            _scenario = scenario;
            _date = new DateTime(scenario.StartYear, 1, 1);

            //year control
            yearCtrl1.Scenario = scenario;
            yearCtrl1.onYearChanged += (s, e) => { updateTableAndChart(); };
            yearCtrl1.onYearDisplayTypeChanged += (s, e) => { updateTableAndChart(); };

            //columns
            resultColumnTree1.onResultTypeAndColumnChanged += (resultType, col) =>
            {
                _resultType = resultType;
                _col = col;

                //only for daily and monthly
                this.yearCtrl1.Visible = _scenario.Structure.getInterval(_resultType) == ArcSWAT.SWATResultIntervalType.DAILY ||
                    _scenario.Structure.getInterval(_resultType) == ArcSWAT.SWATResultIntervalType.MONTHLY;

                updateTableAndChart();
            };
            resultColumnTree1.setScenarioAndUnit(scenario, ArcSWAT.SWATUnitType.WSHD);

            //chart export
            outputDisplayChart1.onExport += (s, e) =>
                {

                };

            //compare control
            compareCtrl1.ScenarioResult = scenario;
            compareCtrl1.onCompareResultChanged += (ss, ee) => { _compareResult = compareCtrl1.CompareResult; updateTableAndChart(); };


            //update
            updateTableAndChart();

            this.tableView2.DataTable = this._scenario.Watershed.AverageAnnualBasinTable;
        }

        private void updateTableAndChart()
        {
            _statistics = "No Statistics Data Available";
            if (onDataStatisticsChanged != null)
                onDataStatisticsChanged(this, new EventArgs());

            if (_resultType == null || _col == null) return;

            if (!this._scenario.Watershed.Results.ContainsKey(_resultType)) return;

            ArcSWAT.SWATUnitResult result = this._scenario.Watershed.Results[_resultType];
            if (!result.Columns.Contains(_col)) return;

            int year = -1;
            if ((result.Interval == ArcSWAT.SWATResultIntervalType.DAILY || result.Interval == ArcSWAT.SWATResultIntervalType.MONTHLY) && yearCtrl1.DisplayByYear)
                year = yearCtrl1.Year;

            if (_compareResult == null) //don't compare
            {
                ArcSWAT.SWATUnitColumnYearResult oneResult = result.getResult(_col, year);
 
                this.tableView1.Result = oneResult;
                this.outputDisplayChart1.Result = oneResult;
                _statistics = oneResult.Statistics.ToString();
                if (onDataStatisticsChanged != null)
                    onDataStatisticsChanged(this, new EventArgs());
            }
            else //compare
            {
                try
                {
                    ArcSWAT.SWATUnitColumnYearCompareResult compare =
                        result.getResult(_col, year).Compare(_compareResult);
                    this.tableView1.CompareResult = compare;
                    this.outputDisplayChart1.CompareResult = compare;
                    _statistics = compare.Statistics.ToString();
                    if (onDataStatisticsChanged != null)
                        onDataStatisticsChanged(this, new EventArgs());
                }
                catch (System.Exception e)
                {
                    SWAT_SQLite.showInformationWindow(e.ToString());
                }
            }
        }
    }
}
