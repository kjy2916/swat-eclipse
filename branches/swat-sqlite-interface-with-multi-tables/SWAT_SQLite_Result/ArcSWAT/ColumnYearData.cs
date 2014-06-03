using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SWAT_SQLite_Result.ArcSWAT
{
    public abstract class ColumnYearData : SeasonData
    {
        public ColumnYearData(string col, int year)
        {            
            _col = col;
            _year = year;
            _id = getUniqueResultID(col, year);
        }

        protected string _col = null;
        protected int _year = -1;
        protected string _id = null;
        protected DataTable _table = null;
        protected Statistics _stat = null;
        protected string _colCompare = null;

        public string ColumnCompare { get { return _colCompare; } }
        public string Column { get { return _col; } }
        public string ID { get { return _id; } }
        public override int Year { get { return _year; } }
        public Statistics Statistics { get { if (_stat == null) _stat = new Statistics(Table, _col); return _stat; } }

        private Dictionary<SeasonType, Statistics> _seasonStat = new Dictionary<SeasonType, Statistics>();
        public Statistics SeasonStatistics(SeasonType season)
        {
            if (season == SeasonType.WholeYear) return Statistics;

            if (!_seasonStat.ContainsKey(season))
                _seasonStat.Add(season, new Statistics(SeasonTable(season), _col));
            return _seasonStat[season];
        }
        

        /// <summary>
        /// The data table for given column and year
        /// </summary>
        public override DataTable Table 
        { 
            get 
            { 
                read();
                return _table; 
            } 
        }

        public override DateTime FirstDay
        {
            get 
            { 
                if(Table.Rows.Count > 0)
                    return DateTime.Parse(Table.Rows[0][SWATUnitResult.COLUMN_NAME_DATE].ToString());
                return new DateTime(1900, 1, 1);
            }
        }

        public override DateTime LastDay
        {
            get 
            {
                if (Table.Rows.Count > 0)
                    return DateTime.Parse(Table.Rows[Table.Rows.Count - 1][SWATUnitResult.COLUMN_NAME_DATE].ToString());
                return new DateTime(1900, 1, 1);
            }
        }

        protected abstract void read();        

        public static string getUniqueResultID(string col, int year)
        {
            string combineCol = col.Trim();
            if (year > 0) combineCol += "_" + year.ToString();
            return combineCol;
        }
    }
}
