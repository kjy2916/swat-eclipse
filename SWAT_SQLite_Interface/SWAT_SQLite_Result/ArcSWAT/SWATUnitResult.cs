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

        /// <summary>
        /// Read data for given column
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public DataTable getDataTable(string col)
        {
            col = col.Trim();
            if (_results.ContainsKey(col)) return _results[col];

            if (!Columns.Contains(col)) return new DataTable();

            string cols = ScenarioResultStructure.getDateColumns(Interval);
            if (cols.Length > 0) cols += ",";
            cols += col;

            DataTable dt = _unit.Scenario.GetDataTable(
                string.Format("select {3} from {0} where {1}={2}",
                Name, _unit.Type.ToString(), _unit.ID, cols));
            _results.Add(col, dt);

            //add datetime column and calculate the date
            if (dt.Rows.Count > 0 && Interval != SWATResultIntervalType.UNKNOWN)
            {
                dt.Columns.Add(COLUMN_NAME_DATE, typeof(DateTime));
                foreach (DataRow r in dt.Rows)
                    calculateDate(r);
            }

            return dt;            
        }

        public Statistics getStatistics(string col)
        {
            col = col.Trim();
            if (!_statistics.ContainsKey(col))
            {
                DataTable dt = getDataTable(col);
                _statistics.Add(col, new Statistics(dt, col, _unit.Scenario.EndYear - _unit.Scenario.StartYear + 1));
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

    }
}
