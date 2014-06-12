using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Data;

namespace SWAT_SQLite_Result.ArcSWAT
{
    /// <summary>
    /// Compare result between two different model in same scenario or same model in different scenario
    /// todo: calculate the difference
    /// </summary>
    public class SWATUnitColumnYearCompareResult : SeasonData
    {
        private SWATUnitColumnYearResult _result1 = null;
        private ColumnYearData _data2 = null;
        private DataTable _combineCompareTable = null;
        private DataTable _combineCompareTableForStatistics = null;
        private StringCollection _tableColumns = new StringCollection();
        private StringCollection _chartColumns = new StringCollection();
        private SWATResultIntervalType _interval = SWATResultIntervalType.UNKNOWN;
        private StatisticCompare _statistic = null;

        /// <summary>
        /// Compare two results
        /// </summary>
        /// <param name="result1"></param>
        /// <param name="result2"></param>
        public SWATUnitColumnYearCompareResult(SWATUnitColumnYearResult result1, SWATUnitColumnYearResult result2)
        {
            if(result1 == null || result2 == null) return;

            if (result1.UnitResult.Unit.Scenario.Scenario.Name.Equals(result2.UnitResult.Unit.Scenario.Scenario.Name)) //same scenario
            {
                if (result1.UnitResult.Unit.Scenario.ModelType == result2.UnitResult.Unit.Scenario.ModelType) //same model type
                    return;
            }
            else //different scenario
            {
                if (result1.UnitResult.Unit.Scenario.ModelType != result2.UnitResult.Unit.Scenario.ModelType) //different model type
                    return;
            }

            if (result1.UnitResult.Unit.Type != result2.UnitResult.Unit.Type ||
                result1.UnitResult.Unit.ID != result2.UnitResult.Unit.ID) return; //differnt unit type or id

            if (!result1.UnitResult.Name.Equals(result2.UnitResult.Name) || 
                result1.UnitResult.Interval != result2.UnitResult.Interval) return; //differnt data table or interval

            if (!result1.Column.Equals(result2.Column) || result1.Year != result2.Year) return; //different data column or year

            _result1 = result1;
            _data2 = result2;

            _interval = _result1.UnitResult.Interval;
            _chartColumns.Add(_result1.ColumnCompare);
            _chartColumns.Add(_data2.ColumnCompare);

            _tableColumns.Add(_result1.ColumnCompare);
            _tableColumns.Add(_data2.ColumnCompare);
            //_tableColumns.Add("ABSOLUTE");
            //_tableColumns.Add("RELATIVE");

            _statistic = new StatisticCompare(this,SeasonType.WholeYear);
        }

        /// <summary>
        /// Compare result and observed data
        /// </summary>
        /// <param name="result"></param>
        public SWATUnitColumnYearCompareResult(SWATUnitColumnYearResult result)
        {
            if (result == null) return;
            SWATUnitColumnYearObservationData observed = result.ObservedData;
            if (observed == null) return;

            _result1 = result;
            _data2 = observed;
            _interval = _result1.UnitResult.Interval;
            _chartColumns.Add(_result1.ColumnCompare);
            _chartColumns.Add(_data2.ColumnCompare);

            _tableColumns.Add(_result1.ColumnCompare);
            _tableColumns.Add(_data2.ColumnCompare);
            //_tableColumns.Add("ABSOLUTE");
            //_tableColumns.Add("RELATIVE");

            _statistic = new StatisticCompare(this,SeasonType.WholeYear);
        }

        public override DateTime FirstDay
        {
            get
            {
                return _result1.FirstDay;
            }
        }

        public override DateTime LastDay
        {
            get
            {
                return _result1.LastDay;
            }
        }

        public SWATResultIntervalType Interval { get { return _interval; } }
        public StringCollection TableColumns { get { return _tableColumns; } }
        public StringCollection ChartColumns { get { return _chartColumns; } }
        public StatisticCompare Statistics { get { return _statistic; } }
        public override int Year { get { return _result1.Year; } }

        /// <summary>
        /// Data table only used to calculate NSE when compared with observed data. The missing data is not accounted in the calculation. 
        /// For sediment and nutrient, this is very important.
        /// </summary>
        public DataTable TableForStatistics
        {
            get
            {
                if (_data2 is SWATUnitColumnYearResult) return Table;

                //observed, remove missing data 
                if (_combineCompareTableForStatistics == null)
                {
                    DataView view = Table.DefaultView;
                    view.RowFilter = _data2.ColumnCompare + " >= 0";
                    _combineCompareTableForStatistics = view.ToTable();                    
                }
                return _combineCompareTableForStatistics;
            }
        }

        private Dictionary<SeasonType, DataTable> _seasonTableForStatistics = new Dictionary<SeasonType, DataTable>();
        public DataTable SeasonTableForStatistics(SeasonType season)
        {
            if (season == SeasonType.WholeYear) return TableForStatistics;

            if (!_seasonTableForStatistics.ContainsKey(season))
            {
                DataView view = new DataView(TableForStatistics);
                view.RowFilter = getSeasonSQL(season);
                _seasonTableForStatistics.Add(season,view.ToTable());
            }
                
            return _seasonTableForStatistics[season];
        }

        public override DataTable Table
        {
            get
            {
                if (_combineCompareTable == null)
                {
                    if (_result1 == null || _data2 == null)
                        throw new Exception("The two results don't have base to compare.");

                    if (_result1.Table.Rows.Count == 0)
                        throw new Exception("There is no result in result 1.");

                    if (_result1.Table.Rows.Count != _data2.Table.Rows.Count &&
                        _data2 is SWATUnitColumnYearResult) //only applied when two results are compared
                                                            //Oberserved data may has some missing data, so this should not apply.
                        throw new Exception("The number of results are different.");

                    System.Diagnostics.Debug.WriteLine("{0:yyyy-MM-dd hh:mm:ss FFF} Start to generate compare table... ", DateTime.Now);
                    DataTable dt = _result1.Table.Copy();
                    dt.Columns[_result1.Column].ColumnName = _result1.ColumnCompare;

                    //add new column for result2
                    dt.Columns.Add(_data2.ColumnCompare, typeof(double));

                    //copy data from result2 to the combine table
                    int newColIndex = dt.Columns.Count - 1;                
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (_data2 is SWATUnitColumnYearResult)
                        {
                            ArcSWAT.RowItem item = new ArcSWAT.RowItem(_data2.Table.Rows[i]);
                            dt.Rows[i][newColIndex] = item.getColumnValue_Double(_data2.Column);
                        }
                        else if (_data2 is SWATUnitColumnYearObservationData)
                        {
                            //see if the oberved data is missing
                            DateTime d = DateTime.Parse(dt.Rows[i][SWATUnitResult.COLUMN_NAME_DATE].ToString());
                            
                            DataRow[] rs = null;
                            if(_result1.UnitResult.Interval == SWATResultIntervalType.MONTHLY)
                                //in phase I, the monthly data is using day 15 for each month, and I would use day 1 for each month, so here for monthly observed data, use both conditions
                                rs = _data2.Table.Select(string.Format("{0}='{1:yyyy-MM-01}' or {0}='{1:yyyy-MM-15}'", SWATUnitResult.COLUMN_NAME_DATE, d));
                            else if (_result1.UnitResult.Interval == SWATResultIntervalType.DAILY)
                                rs = _data2.Table.Select(string.Format("{0}='{1:yyyy-MM-dd}'", SWATUnitResult.COLUMN_NAME_DATE, d));

                            if (rs != null && rs.Length > 0)
                                dt.Rows[i][newColIndex] = double.Parse(rs[0][_data2.Column].ToString());
                            else
                                dt.Rows[i][newColIndex] = ScenarioResultStructure.EMPTY_OBSERVED_VALUE; //missing observed data, shouldn't use 0 as for daily observed data they may be 0
                                                                                                        //changed to -0.000001 to make the chart better
                        }                        
                    }
                    System.Diagnostics.Debug.WriteLine("{0:yyyy-MM-dd hh:mm:ss FFF} End to generate compare table... ", DateTime.Now);

                    //add two column for absolute change and relative change
                    //don't calculate relative change when first result is 0
                    //dt.Columns.Add("ABSOLUTE", typeof(double)).Expression = 
                    //    string.Format("{0} - {1}", _result1.ColumnCompare, _data2.ColumnCompare);
                    //dt.Columns.Add("RELATIVE", typeof(double)).Expression =
                    //    string.Format("IIF({0} = 0, 0.0,ABSOLUTE/{0})", _result1.ColumnCompare);

                    _combineCompareTable = dt;
                }
                return _combineCompareTable;
            }
        }

        private Dictionary<SeasonType, StatisticCompare> _seasonStat = new Dictionary<SeasonType, StatisticCompare>();
        public StatisticCompare SeasonStatistics(SeasonType season)
        {
            if (season == SeasonType.WholeYear) return Statistics;

            if (!_seasonStat.ContainsKey(season))
                _seasonStat.Add(season, new StatisticCompare(this,season));
            return _seasonStat[season];
        }
    }
}
