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
    public delegate void SwitchFromSubbasin2HRUEventHandler(ArcSWAT.HRU hru);

    public partial class SubbasinView : UserControl
    {
        public SubbasinView()
        {
            InitializeComponent();
        }

        private string _resultType = null;
        private string _col = null;
        private ArcSWAT.SWATUnit _unit = null;
        private DateTime _date = DateTime.Now;
        private string _statistics = "No Statistics Data Available";

        private ArcSWAT.Project _project = null;
        private ArcSWAT.ScenarioResult _scenario = null;
        private ArcSWAT.SWATUnitType _type = ArcSWAT.SWATUnitType.UNKNOWN;
        private Dictionary<int, ArcSWAT.SWATUnit> _unitList = null;

        /// <summary>
        /// Happens when go button is clicked
        /// </summary>
        public event SwitchFromSubbasin2HRUEventHandler onSwitch2HRU = null;

        /// <summary>
        /// Happens when new feature is selected
        /// </summary>
        public event EventHandler onMapSelectionChanged = null;

        /// <summary>
        /// Happens when time is changed
        /// </summary>
        public event EventHandler onMapTimeChanged = null;

        /// <summary>
        /// Happens when statistic information is changed
        /// </summary>
        public event EventHandler onDataStatisticsChanged = null;

        public DateTime MapTime { get { return _date; } }
        public ArcSWAT.SWATUnit MapSelection { get { return _unit; } }
        public string Statistics { get { return _statistics; } }

        public void setProjectScenario(ArcSWAT.Project project, ArcSWAT.ScenarioResult scenario,ArcSWAT.SWATUnitType type)
        {
            _project = project;
            _scenario = scenario;            
            _type = type;
            _date = new DateTime(scenario.StartYear, 1, 1);
            if (onMapTimeChanged != null)
                onMapTimeChanged(this, new EventArgs());
            
            if (type == ArcSWAT.SWATUnitType.SUB)
                _unitList = _scenario.Subbasins;
            else if (type == ArcSWAT.SWATUnitType.RCH)
                _unitList = _scenario.Reaches;
            else if (type == ArcSWAT.SWATUnitType.HRU)
                _unitList = _scenario.HRUs;
            else if (type == ArcSWAT.SWATUnitType.RES)
                _unitList = _scenario.Reservoirs;

            this.Resize += (ss, ee) => { splitContainer3.SplitterDistance = 72; };

            //season control
            seasonCtrl1.onSeasonTypeChanged += (s, e) => { tableView1.Season = seasonCtrl1.Season; outputDisplayChart1.Season = seasonCtrl1.Season; updateTableAndChart(); };

            //year control
            yearCtrl1.Scenario = scenario;
            yearCtrl1.onYearChanged += (s, e) => { updateTableAndChart(); };
            yearCtrl1.onYearDisplayTypeChanged += (s, e) => { updateTableAndChart(); };

            //only for subbasin to show hru list
            hruList1.Visible = (type == ArcSWAT.SWATUnitType.SUB || type == ArcSWAT.SWATUnitType.HRU);
            hruList1.onSwitch2HRU += (hru) =>
                {
                    if (_type == ArcSWAT.SWATUnitType.HRU) 
                    {
                        if (_unit != null && _unit.ID == hruList1.HRU.ID) return;

                        _unit = hruList1.HRU;

                        //show basic information
                        if (onMapSelectionChanged != null)
                            onMapSelectionChanged(this, new EventArgs());

                        //update table and chart
                        updateTableAndChart(); 
                    }
                    if (_type == ArcSWAT.SWATUnitType.SUB)
                    {
                        if (onSwitch2HRU != null) onSwitch2HRU(hru);
                    }
                };

            //columns
            resultColumnTree1.onResultTypeAndColumnChanged += (resultType, col) =>
            {
                _resultType = resultType;
                _col = col;

                //only for daily and monthly
                this.yearCtrl1.Visible = _scenario.Structure.getInterval(_resultType) == ArcSWAT.SWATResultIntervalType.DAILY ||
                    _scenario.Structure.getInterval(_resultType) == ArcSWAT.SWATResultIntervalType.MONTHLY;

                updateMap();
                updateTableAndChart();
            };
            resultColumnTree1.setScenarioAndUnit(scenario, type);

            //map            
            subbasinMap1.onLayerSelectionChanged += (unitType, id) =>
            {                
                if (type != ArcSWAT.SWATUnitType.SUB && type != ArcSWAT.SWATUnitType.RCH && type != ArcSWAT.SWATUnitType.HRU && type != ArcSWAT.SWATUnitType.RES && _unitList != null) return;
                if (id <= 0)
                    _unit = null;
                else
                {
                    if (type == ArcSWAT.SWATUnitType.HRU)
                        _unit = (_scenario.Subbasins[id] as ArcSWAT.Subbasin).HRUs.First().Value;
                    else
                        _unit = _unitList[id];
                }

                //show basic information
                if (onMapSelectionChanged != null)
                    onMapSelectionChanged(this, new EventArgs());

                if (_unit != null)
                {
                    //get hrus
                    if(type == ArcSWAT.SWATUnitType.SUB)
                        hruList1.Subbasin = _unit as ArcSWAT.Subbasin;
                    if(type == ArcSWAT.SWATUnitType.HRU)
                        hruList1.Subbasin = (_unit as ArcSWAT.HRU).Subbasin;
                }

                updateTableAndChart();
            };
            subbasinMap1.setProjectScenario(project, scenario, type);

            //chart export
            outputDisplayChart1.onExport += (s, e) =>
                {

                };

            //table view
            tableView1.onDateChanged += (d) => 
            { 
                if (_type == ArcSWAT.SWATUnitType.HRU) return; 
                _date = d;
                if (onMapTimeChanged != null)
                    onMapTimeChanged(this, new EventArgs());
                updateMap(); 
            };

            //compare control
            compareCtrl1.ScenarioResult = scenario;
            compareCtrl1.onCompareResultChanged += (ss, ee) => 
            {
                updateTableAndChart(); 
            };

            //update
            updateMap();
            updateTableAndChart();
        }

        public ArcSWAT.HRU HRU
        {
            set
            {
                if (_type == ArcSWAT.SWATUnitType.HRU)
                    subbasinMap1.HRU = value;
            }
        }

        private void updateMap()
        {
            if (_type == ArcSWAT.SWATUnitType.HRU) return;
            if (_resultType == null || _col == null) return;
            subbasinMap1.drawLayer(_resultType, _col, _date);
        }

        private void updateTableAndChart()
        {
            tableView1.DataTable = null;
            outputDisplayChart1.clear();
            _statistics = "No Statistics Data Available";
            if (onDataStatisticsChanged != null)
                onDataStatisticsChanged(this, new EventArgs());

            if (_resultType == null || _col == null || _unit == null) return;

            if (!_unit.Results.ContainsKey(_resultType)) return;

            ArcSWAT.SWATUnitResult result = _unit.Results[_resultType];
            if (!result.Columns.Contains(_col)) return;

            //consider year selection
            int year = -1;
            if ((result.Interval == ArcSWAT.SWATResultIntervalType.DAILY || result.Interval == ArcSWAT.SWATResultIntervalType.MONTHLY) && yearCtrl1.DisplayByYear)
                year = yearCtrl1.Year;

            //current working result
            ArcSWAT.SWATUnitColumnYearResult oneResult = result.getResult(_col, year);

            //set compare control
            compareCtrl1.HasObervedData = (oneResult.ObservedData != null);

            //do the update
            if (compareCtrl1.CompareResult == null && !compareCtrl1.IsObservedDataSelected) //don't compare
            {              
                if (oneResult.Table.Rows.Count == 0 && _type == ArcSWAT.SWATUnitType.HRU)
                    MessageBox.Show("No results for HRU " + _unit.ID.ToString() + ". For more results, please modify file.cio.");

                this.tableView1.Result = oneResult;
                this.outputDisplayChart1.Result = oneResult;
                this._statistics = oneResult.SeasonStatistics(seasonCtrl1.Season).ToString();
                if (onDataStatisticsChanged != null)
                    onDataStatisticsChanged(this, new EventArgs());
            }
            else //compare
            {
                try
                {
                    ArcSWAT.SWATUnitColumnYearCompareResult compare = null;
                    if (compareCtrl1.CompareResult != null)
                        compare = oneResult.Compare(compareCtrl1.CompareResult);
                    else
                        compare = oneResult.CompareWithObserved;
                    this.tableView1.CompareResult = compare;
                    this.outputDisplayChart1.CompareResult = compare;
                    this._statistics = compare.SeasonStatistics(seasonCtrl1.Season).ToString();
                    if (onDataStatisticsChanged != null)
                        onDataStatisticsChanged(this, new EventArgs());
                }
                catch (System.Exception e)
                {
                    SWAT_SQLite.showInformationWindow(e.ToString());
                }
            }

            
        }

        public DotSpatial.Controls.Map Map { get { return subbasinMap1; } }
    }
}
