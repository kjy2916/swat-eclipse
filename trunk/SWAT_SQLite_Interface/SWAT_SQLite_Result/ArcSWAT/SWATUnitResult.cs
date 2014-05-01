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
        private Dictionary<string, SWATUnitColumnYearResult> _results = new Dictionary<string, SWATUnitColumnYearResult>();
        
        private StringCollection _columns = null;

        public SWATUnitResult(string tableName, SWATUnit parentUnit)
        {
            _tableName = tableName;
            _unit = parentUnit;
        }

        /// <summary>
        /// read result for given column and date
        /// </summary>
        /// <param name="col"></param>
        /// <param name="date"></param>
        /// <returns></returns>
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

        /// <summary>
        /// read result for given column and year
        /// </summary>
        /// <param name="col"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public DataTable getDataTable(string col,int year)
        {
            return getResult(col, year).Table;
        }

        /// <summary>
        /// Read result for given column
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public DataTable getDataTable(string col)
        {
            return getDataTable(col, -1);          
        }

        public Statistics getStatistics(string col, int year)
        {
            return getResult(col, year).Statistics;
        }

        public SWATUnitColumnYearResult getResult(string col, int year)
        {
            //see if the result is already there
            string id = SWATUnitColumnYearResult.getUniqueResultID(col, year);
            if (!_results.ContainsKey(id)) _results.Add(id, new SWATUnitColumnYearResult(col, year, this));
            return _results[id];
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
