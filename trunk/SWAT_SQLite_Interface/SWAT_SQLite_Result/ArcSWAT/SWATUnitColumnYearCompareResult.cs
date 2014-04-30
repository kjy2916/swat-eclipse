using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Data;

namespace SWAT_SQLite_Result.ArcSWAT
{
    public class SWATUnitColumnYearCompareResult
    {
        private SWATUnitColumnYearResult _result1 = null;
        private SWATUnitColumnYearResult _result2 = null;
        private DataTable _combineCompareTable = null;
        private StringCollection _dataColumns = new StringCollection();
        private SWATResultIntervalType _interval = SWATResultIntervalType.UNKNOWN;

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
            _result2 = result2;

            _interval = _result1.UnitResult.Interval;
            _dataColumns.Add(_result1.ColumnCompare);
            _dataColumns.Add(_result2.ColumnCompare);
        }

        public SWATResultIntervalType Interval { get { return _interval; } }
        public StringCollection Columns { get { return _dataColumns; } }

        public DataTable Table
        {
            get
            {
                if (_combineCompareTable == null)
                {
                    if (_result1 == null || _result2 == null)
                        throw new Exception("The two results don't have base to compare.");

                    if (_result1.Table.Rows.Count == 0)
                        throw new Exception("There is no result in result 1.");

                    if (_result1.Table.Rows.Count != _result2.Table.Rows.Count)
                        throw new Exception("The number of results are different.");

                    DataTable dt = _result1.Table.Copy();
                    dt.Columns[_result1.Column].ColumnName = _result1.ColumnCompare;

                    //add new column for result2
                    dt.Columns.Add(_result2.ColumnCompare, typeof(double));

                    //copy data from result2 to the combine table
                    int newColIndex = dt.Columns.Count - 1;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ArcSWAT.RowItem item = new ArcSWAT.RowItem(_result2.Table.Rows[i]);
                        dt.Rows[i][newColIndex] = item.getColumnValue_Double(_result2.Column);
                    }
                    _combineCompareTable = dt;
                }
                return _combineCompareTable;
            }
        }
    }
}
