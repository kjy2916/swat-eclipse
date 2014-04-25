using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace SWAT_SQLite_Result.ArcSWAT
{
    /// <summary>
    /// One type result of a SWAT Unit
    /// todo 
    /// 1. add statistics information
    /// 2. add year control for daily result to speed up
    /// 3. add results from table ave_annual_basin. Maybe add to other view.
    /// </summary>
    public class SWATUnitResult
    {
        public static string COLUMN_NAME_DATE = "DATE1";

        private SWATUnit _unit = null;
        private string _tableName = null;
        private SWATResultIntervalType _interval = SWATResultIntervalType.UNKNOWN;
        private Dictionary<string, DataTable> _results = new Dictionary<string, DataTable>();
        private Dictionary<string, Statistics> _statistics = new Dictionary<string, Statistics>();
        private StringCollection _columns = null;

        public SWATUnitResult(string tableName, SWATUnit parentUnit)
        {
            _tableName = tableName;
            _unit = parentUnit;
        }

        public double getData(string col, DateTime date)
        {
            DataTable dt = getDataTable(col,date.Year);
            if (dt.Rows.Count == 0) return ScenarioResultStructure.EMPTY_VALUE;

            string filter = string.Format("{0}='{1:yyyy-MM-dd}'",COLUMN_NAME_DATE,date);
            DataRow[] rows = dt.Select(filter);
            if (rows.Length == 0) return ScenarioResultStructure.EMPTY_VALUE;

            RowItem item = new RowItem(rows[0]);
            return item.getColumnValue_Double(col);
        }

        private string getIndexString(string col, int year)
        {
            string combineCol = col;
            if (year >= _unit.Scenario.StartYear && year <= _unit.Scenario.EndYear)
                combineCol += "_" + year.ToString();
            return combineCol;
        }

        public DataTable getDataTable(string col,int year)
        {
            col = col.Trim();

            //check the column name
            if (!Columns.Contains(col)) return new DataTable();

            //only use year parameter for daily
            if (Interval != SWATResultIntervalType.DAILY) year = -1;

            //see if the result is already there
            string combineCol = getIndexString(col, year);
            if (_results.ContainsKey(combineCol)) return _results[combineCol];

            //get return columns based on interval
            string cols = ScenarioResultStructure.getDateColumns(Interval);
            if (cols.Length > 0) cols += ",";
            cols += col;

            //get year condition
            string yearCondition = "";
            if (year >= _unit.Scenario.StartYear && year <= _unit.Scenario.EndYear)
                yearCondition = string.Format("{0}={1}", ScenarioResultStructure.COLUMN_NAME_YEAR,year);

            //get id condition
            string idCondition = "";
            if (_unit.Type != SWATUnitType.WSHD)
                idCondition = string.Format("{0}={1}", _unit.Type.ToString(), _unit.ID);

            string condition = idCondition;
            if (condition.Length > 0 && yearCondition.Length > 0)
                condition += " and " + yearCondition;

            if (condition.Length > 0) condition = " where " + condition;

            DataTable dt = _unit.Scenario.GetDataTable(
                string.Format("select {2} from {0} {1}",
                Name, condition, cols));

            _results.Add(combineCol, dt);

            //add datetime column and calculate the date
            if (dt.Rows.Count > 0 && Interval != SWATResultIntervalType.UNKNOWN)
            {
                dt.Columns.Add(COLUMN_NAME_DATE, typeof(DateTime));
                foreach (DataRow r in dt.Rows)
                    calculateDate(r);
            }

            return dt; 
        }

        /// <summary>
        /// Read data for given column
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public DataTable getDataTable(string col)
        {
            return getDataTable(col, -1);          
        }

        public Statistics getStatistics(string col,int year)
        {
            col = col.Trim();
            if (!_statistics.ContainsKey(col))
            {
                DataTable dt = getDataTable(col,year);
                _statistics.Add(col, new Statistics(dt, col));
            }

            return _statistics[col];
        }

        private void calculateDate(DataRow r)
        {
            DateTime d = DateTime.Now;
            RowItem item = new RowItem(r);
            int year = item.getColumnValue_Int(ScenarioResultStructure.COLUMN_NAME_YEAR);
            int month = 1;
            int day = 1;
            if (Interval == SWATResultIntervalType.MONTHLY || Interval == SWATResultIntervalType.DAILY)
                month = item.getColumnValue_Int(ScenarioResultStructure.COLUMN_NAME_MONTH);
            if (Interval == SWATResultIntervalType.DAILY)
                day = item.getColumnValue_Int(ScenarioResultStructure.COLUMN_NAME_DAY);

            r[COLUMN_NAME_DATE] = new DateTime(year,month,day);
        }

        /// <summary>
        /// Name of the result, also the table name
        /// </summary>
        public string Name { get { return _tableName; } }

        /// <summary>
        /// Result interval
        /// </summary>
        public SWATResultIntervalType Interval
        {
            get
            {
                if(_interval == SWATResultIntervalType.UNKNOWN)
                    _interval = _unit.Scenario.Structure.getInterval(Name);
                return _interval;
            }
        }

        /// <summary>
        /// Data Columns
        /// </summary>
        public StringCollection Columns { get { if (_columns == null) _columns = _unit.Scenario.Structure.getDataColumns(Name); return _columns; } }


        public SWATUnit Unit { get { return _unit; } }
    }
}
