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
    public class SWATUnitColumnYearCompareResult
    {
        private SWATUnitColumnYearResult _result1 = null;
        private ColumnYearData _data2 = null;
        private DataTable _combineCompareTable = null;
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
            _tableColumns.Add("ABSOLUTE");
            _tableColumns.Add("RELATIVE");

            _statistic = new StatisticCompare(this);
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
            _tableColumns.Add("ABSOLUTE");
            _tableColumns.Add("RELATIVE");

            _statistic = new StatisticCompare(this);
        }

        public SWATResultIntervalType Interval { get { return _interval; } }
        public StringCollection TableColumns { get { return _tableColumns; } }
        public StringCollection ChartColumns { get { return _chartColumns; } }
        public StatisticCompare Statistics { get { return _statistic; } }
        public DataTable Table
        {
            get
            {
                if (_combineCompareTable == null)
                {
                    if (_result1 == null || _data2 == null)
                        throw new Exception("The two results don't have base to compare.");

                    if (_result1.Table.Rows.Count == 0)
                        throw new Exception("There is no result in result 1.");

                    if (_result1.Table.Rows.Count != _data2.Table.Rows.Count)
                        throw new Exception("The number of results are different.");

                    DataTable dt = _result1.Table.Copy();
                    dt.Columns[_result1.Column].ColumnName = _result1.ColumnCompare;

                    //add new column for result2
                    dt.Columns.Add(_data2.ColumnCompare, typeof(double));

                    //copy data from result2 to the combine table
                    int newColIndex = dt.Columns.Count - 1;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ArcSWAT.RowItem item = new ArcSWAT.RowItem(_data2.Table.Rows[i]);
                        dt.Rows[i][newColIndex] = item.getColumnValue_Double(_data2.Column);
                    }

                    //add two column for absolute change and relative change
                    dt.Columns.Add("ABSOLUTE", typeof(double)).Expression = string.Format("{0} - {1}", _result1.ColumnCompare, _data2.ColumnCompare);
                    dt.Columns.Add("RELATIVE", typeof(double)).Expression = string.Format("ABSOLUTE/{0}", _result1.ColumnCompare);

                    _combineCompareTable = dt;
                }
                return _combineCompareTable;
            }
        }
    }
}
