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
        private ArcSWAT.SWATUnitColumnYearResult _currentResult = null;

        private ArcSWAT.SWATUnitType _unitType = ArcSWAT.SWATUnitType.UNKNOWN;
        private int _id = -1;
        private string _col = null;

        private int _year = -1;
        private ArcSWAT.StatisticCompareType _statisticType = ArcSWAT.StatisticCompareType.NSE;

        private DataTable _statisticTable = null;
        private DataTable _comparedStatisticTable = null;
        private DataTable _comparedSeasonTable = null;

        private ArcSWAT.SWATUnitColumnYearResult getResult(ArcSWAT.ScenarioResult result)
        {
            if (_unitType == ArcSWAT.SWATUnitType.UNKNOWN) return null;

            //get the unit
            ArcSWAT.SWATUnit unit = result.getSWATUnit(_unitType, _id);
            if (unit == null) return null;

            //get unit results
            foreach (ArcSWAT.SWATUnitResult unitResult in unit.Results.Values)
            {
                ArcSWAT.SWATUnitColumnYearResult r = unitResult.getResult(_col, -1);
                if (r != null) return r;                
            }
            return null;
        }

        private void updateComparedTable()
        {
            this.outputDisplayChart1.Season = seasonCtrl1.Season;

            if (_comparedSeasonTable == null)
            {
                _comparedSeasonTable = new DataTable();
                _comparedSeasonTable.Columns.Add("Year", typeof(Int32));
                _comparedSeasonTable.Columns.Add(_result.ID, typeof(double));
                _comparedSeasonTable.Columns.Add(compareCtrl1.CompareResult.ID, typeof(double));
            }
            _comparedSeasonTable.Rows.Clear();

            int index = (int)(seasonCtrl1.Season);
            for (int i = 0; i < _statisticTable.Rows.Count; i++)
            {
                DataRow newRow = _comparedSeasonTable.NewRow();
                newRow[0] = _statisticTable.Rows[i][0];
                newRow[1] = _statisticTable.Rows[i][index];
                newRow[2] = this._comparedStatisticTable.Rows[i][index];
                _comparedSeasonTable.Rows.Add(newRow);
            }
            dataGridView3.DataSource = _comparedSeasonTable;
        }

        public PerformanceView()
        {
            InitializeComponent();

            this.Resize += (s, e) => { this.splitContainer1.SplitterDistance = this.Height - 250; }; //always set the height of chart as 250

            cmbSplitYear.SelectedIndexChanged += (s, e) => {
                updatePerformanceTable();
            };

            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowEnter += (s, e) =>
            {
                if (e.RowIndex < 0) return;

                try
                {
                    //get selected unit type and id
                    string unitType = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    if (unitType == ArcSWAT.SWATUnitType.RCH.ToString())
                        _unitType = ArcSWAT.SWATUnitType.RCH;
                    else if (unitType == ArcSWAT.SWATUnitType.RES.ToString())
                        _unitType = ArcSWAT.SWATUnitType.RES;
                    else
                        return;

                    //get the id
                    _id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());

                    //get column
                    _col = ArcSWAT.ObservationData.getObservationSWATColumn(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());

                    //get current result
                    _currentResult = getResult(_result);

                    //get statistic info for each hydrological year
                    _statisticTable = _currentResult.UnitResult.getYearlyPerformanceTable(_col,_statisticType);
                    this.dataGridView2.DataSource = _statisticTable;
                }
                catch { }

            };

            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowEnter += (s, e) =>
                {
                    if (e.RowIndex < 0) return;
                    if (_currentResult == null) return;

                    try
                    {
                        //get year
                        _year = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString());

                        //show the compared data chart
                        this.outputDisplayChart1.Result = _currentResult.UnitResult.getResult(_col, _year);
                    }
                    catch { }

                };

            //change the season type of chart when choose different column
            this.dataGridView2.ColumnHeaderMouseClick += (s, e) =>
                {
                    if (e.ColumnIndex <= 0) return;
                    this.outputDisplayChart1.Season = (ArcSWAT.SeasonType)(e.ColumnIndex);
                    if(this.outputDisplayChart1.DataSource != null)
                        this.outputDisplayChart1.Result = _currentResult.UnitResult.getResult(_col, _year);
                };

            compareCtrl1.onCompareResultChanged += (s, e) =>
                {
                    if (compareCtrl1.CompareResult == null) return;

                    ArcSWAT.SWATUnitColumnYearResult r = getResult(compareCtrl1.CompareResult);
                    if(r == null) return;

                    _comparedStatisticTable = r.UnitResult.getYearlyPerformanceTable(_col,_statisticType);

                    updateComparedTable();
                };
            seasonCtrl1.onSeasonTypeChanged += (s, e) => { updateComparedTable(); };

            dataGridView3.RowEnter += (s, e) =>
                {
                    try
                    {
                        //get year
                        int year = Convert.ToInt32(dataGridView3.Rows[e.RowIndex].Cells[0].Value.ToString());

                        //show the compared data chart
                        this.outputDisplayChart1.CompareResult = _currentResult.UnitResult.getResult(_col, year).Compare(compareCtrl1.CompareResult);
                    }
                    catch { }
                };

            this.cmbStatisticTypes.SelectedIndexChanged += (s, e) =>
                {
                    if (cmbStatisticTypes.SelectedIndex == -1) return;
                    ArcSWAT.StatisticCompareType type = (ArcSWAT.StatisticCompareType)(cmbStatisticTypes.SelectedIndex);
                    if (type == _statisticType) return;
                    _statisticType = type;

                    updatePerformanceTable();
                };
        }

        private void updatePerformanceTable()
        {
            if (_result == null) return;
            int year = Convert.ToInt32(cmbSplitYear.SelectedItem.ToString());
            DataTable dt = _result.getPerformanceTable(year, _statisticType);
            if (!_warningHasShown && dt.Rows.Count == 0)
            {
                SWAT_SQLite.showInformationWindow("No performance data. Please make sure the observed data has been uploaded and the simulation results exists!");
                _warningHasShown = true;
            }
            this.dataGridView1.DataSource = null;
            this.dataGridView1.DataSource = dt;
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

                compareCtrl1.ScenarioResult = value;
            }
        }
    }
}
