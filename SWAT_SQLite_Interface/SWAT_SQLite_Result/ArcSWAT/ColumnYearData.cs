using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SWAT_SQLite_Result.ArcSWAT
{
    public abstract class ColumnYearData
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
        public int Year { get { return _year; } }
        public Statistics Statistics { get { if (_stat == null) _stat = new Statistics(Table, _col); return _stat; } }
        public DataTable Table { get { read(); return _table; } }

        protected abstract void read();        

        public static string getUniqueResultID(string col, int year)
        {
            string combineCol = col.Trim();
            if (year > 0) combineCol += "_" + year.ToString();
            return combineCol;
        }
    }
}
