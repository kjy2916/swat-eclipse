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

        private ArcSWAT.Project _project = null;
        private ArcSWAT.ScenarioResult _scenario = null;
        private ArcSWAT.SWATUnitType _type = ArcSWAT.SWATUnitType.UNKNOWN;
        private Dictionary<int, ArcSWAT.SWATUnit> _unitList = null;
        public event SwitchFromSubbasin2HRUEventHandler onSwitch2HRU = null;
 
        public void setProjectScenario(ArcSWAT.Project project, ArcSWAT.ScenarioResult scenario,ArcSWAT.SWATUnitType type)
        {
            _project = project;
            _scenario = scenario;            
            _type = type;
            _date = new DateTime(scenario.StartYear, 1, 1);
            lblDate.Text = "Map:" + _date.ToString("yyyy-MM-dd");
            if (type == ArcSWAT.SWATUnitType.SUB)
                _unitList = _scenario.Subbasins;
            if (type == ArcSWAT.SWATUnitType.RCH)
                _unitList = _scenario.Reaches;
            if (type == ArcSWAT.SWATUnitType.HRU)
                _unitList = _scenario.HRUs;

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
                        this.lblInfo.Text = "Information: " + _unit.ToStringBasicInfo();

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

                //only for daily
                this.yearCtrl1.Visible = _scenario.Structure.getInterval(_resultType) == ArcSWAT.SWATResultIntervalType.DAILY;

                updateMap();
                updateTableAndChart();
            };
            resultColumnTree1.setScenarioAndUnit(scenario, type);

            //map            
            subbasinMap1.onLayerSelectionChanged += (id) =>
            {
                if (type != ArcSWAT.SWATUnitType.SUB && type != ArcSWAT.SWATUnitType.RCH && type != ArcSWAT.SWATUnitType.HRU && _unitList != null) return;

                if (type == ArcSWAT.SWATUnitType.HRU)
                    _unit = (_scenario.Subbasins[id] as ArcSWAT.Subbasin).HRUs.First().Value;
                else
                    _unit = _unitList[id];

                //show basic information
                this.lblInfo.Text = "Information: " + _unit.ToStringBasicInfo();

                //get hrus
                if(type == ArcSWAT.SWATUnitType.SUB)
                    hruList1.Subbasin = _unit as ArcSWAT.Subbasin;
                if(type == ArcSWAT.SWATUnitType.HRU)
                    hruList1.Subbasin = (_unit as ArcSWAT.HRU).Subbasin;

                updateTableAndChart();
            };
            subbasinMap1.setProjectScenario(project, scenario, type);

            //chart export
            outputDisplayChart1.onExport += (s, e) =>
                {

                };

            //table view
            tableView1.onDateChanged += (d) => { if (_type == ArcSWAT.SWATUnitType.HRU) return; _date = d; lblDate.Text = "Map:" + _date.ToString("yyyy-MM-dd"); updateMap(); };

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
            if (_resultType == null || _col == null || _unit == null) return;

            if (!_unit.Results.ContainsKey(_resultType)) return;

            ArcSWAT.SWATUnitResult result = _unit.Results[_resultType];
            if (!result.Columns.Contains(_col)) return;

            int year = -1;
            if (result.Interval == ArcSWAT.SWATResultIntervalType.DAILY && yearCtrl1.DisplayByYear)
                year = yearCtrl1.Year;
            //DataTable dt = result.getDataTable(_col, year);

            //if (dt.Rows.Count == 0 && _type == ArcSWAT.SWATUnitType.HRU)
            //    MessageBox.Show("No results for HRU " + _unit.ID.ToString() + ". For more results, please modify file.cio.");

            //this.tableView1.SWATResultTable = dt;            

            //StringCollection cols = new StringCollection() { _col };
            //this.outputDisplayChart1.DrawGraph(dt.Rows, ArcSWAT.SWATUnitResult.COLUMN_NAME_DATE, cols, result.Interval);

            //this.lblStatistics.Text = "Statistics :" + result.getStatistics(_col,year).ToString(); 
          
            //DataTable combineTable = combineComparedResults(dt,_scenario.ModelType,
            //    result.getDataTable_Compared(_col,year, ArcSWAT.SWATModelType.CanSWAT), ArcSWAT.SWATModelType.CanSWAT, _col);

            StringCollection cols = new StringCollection() { _col + "_" + ArcSWAT.SWATModelType.SWAT,_col + "_" + ArcSWAT.SWATModelType.CanSWAT };
            try
            {
                DataTable combineTable = result.getResult(_col, year).getComparedCombineTable(ArcSWAT.SWATModelType.CanSWAT);
                this.tableView1.setResultTable(combineTable, cols);

                this.outputDisplayChart1.DrawGraph(combineTable.Rows, ArcSWAT.SWATUnitResult.COLUMN_NAME_DATE, cols, result.Interval);

            }
            catch (System.Exception e)
            {
                SWAT_SQLite.showInformationWindow(e.ToString());
            }
            
        }
    }
}
