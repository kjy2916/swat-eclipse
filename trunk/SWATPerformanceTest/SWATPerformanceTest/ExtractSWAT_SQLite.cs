using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;

namespace SWATPerformanceTest
{
    public enum SWATResultIntervalType
    {
        UNKNOWN = -1,
        DAILY = 1,
        MONTHLY = 0,
        YEARLY = 2
    }

    public class RowItem
    {
        protected System.Data.DataRow _row;

        public RowItem(System.Data.DataRow row)
        {
            _row = row;
        }

        private bool isColumnValueInvalid(int columnIndex)
        {
            if (_row == null) return false;
            if (columnIndex < 0) return false;
            if (_row.Table.Columns.Count <= columnIndex) return false;
            if (_row[columnIndex] is System.DBNull) return false;

            return true;
        }

        public string getColumnValue_String(int columnIndex)
        {
            if (isColumnValueInvalid(columnIndex)) return _row[columnIndex].ToString();
            return "";
        }

        public int getColumnValue_Int(int columnIndex)
        {
            string v = getColumnValue_String(columnIndex);

            int v_int = -1;
            if (int.TryParse(v, out v_int)) return v_int;
            return -1;
        }

        public double getColumnValue_Double(int columnIndex)
        {
            string v = getColumnValue_String(columnIndex);

            double v_double = -99.0;
            if (double.TryParse(v, out v_double)) return v_double;
            return -99.0;
        }

        private bool isColumnValueInvalid(string columnName)
        {
            if (_row == null) return false;
            if (_row.Table.Columns[columnName] == null) return false;
            if (_row[columnName] is System.DBNull) return false;

            return true;
        }

        public string getColumnValue_String(string columnName)
        {
            if (isColumnValueInvalid(columnName)) return _row[columnName].ToString();
            return "";
        }

        public int getColumnValue_Int(string columnName)
        {
            string v = getColumnValue_String(columnName);

            int v_int = -1;
            if (int.TryParse(v, out v_int)) return v_int;
            return -1;
        }

        public double getColumnValue_Double(string columnName)
        {
            string v = getColumnValue_String(columnName);

            double v_double = -99.0;
            if (double.TryParse(v, out v_double)) return v_double;
            return -99.0;
        }
    }


    class ExtractSWAT_SQLite : System.IDisposable
    {
        public ExtractSWAT_SQLite(string db3Path)
        {
            _db3Path = db3Path;
        }

        public void Dispose()
        {
            if (_connection != null && _connection.State != ConnectionState.Closed)
                _connection.Close();
        }

        private string _db3Path = "";
        private SQLiteConnection _connection = null;
        private int _startYear = -1;
        private int _endYear = -1;
        private SWATResultIntervalType _interval =  SWATResultIntervalType.UNKNOWN;

        private void getBasicInformation()
        {
            DataTable dt = Extract("select * from ave_annual_basin");
            if (dt.Rows.Count == 0) return;

            foreach (DataRow r in dt.Rows)
            {
                RowItem item = new RowItem(r);
                string name = item.getColumnValue_String("NAME");
                if (name.Equals("START_YEAR_OUTPUT"))
                    _startYear = item.getColumnValue_Int("VALUE");
                else if (name.Equals("END_YEAR"))
                    _endYear = item.getColumnValue_Int("VALUE");
                else if (name.Equals("OUTPUT_INTERVAL"))
                    _interval = (SWATResultIntervalType)(item.getColumnValue_Int("VALUE"));
            }
        }

        private int EndYear
        {
            get
            {
                if (_endYear == -1) getBasicInformation();
                return _endYear;
            }
        }

        private SWATResultIntervalType Interval
        {
            get
            {
                if (_interval == SWATResultIntervalType.UNKNOWN) getBasicInformation();
                return _interval;
            }
        }

        private int StartYear
        {
            get
            {
                if (_startYear == -1) getBasicInformation();
                return _startYear;
            }
        }

        private SQLiteConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    //Build the connection string;
                    SQLiteConnectionStringBuilder s = new SQLiteConnectionStringBuilder();
                    s.DataSource = _db3Path;
                    s.Version = 3;
                    s.FailIfMissing = false;

                    //Open the connection;
                    _connection = new SQLiteConnection(s.ConnectionString);
                    _connection.Open();
                 }
                return _connection;
            }
        }

        public static string COLUMN_NAME_YEAR = "YR";
        public static string COLUMN_NAME_MONTH = "MO";
        public static string COLUMN_NAME_DAY = "DA";

        public static string DATE_COLUMNS_YEARLY = COLUMN_NAME_YEAR;
        public static string DATE_COLUMNS_MONTHLY = DATE_COLUMNS_YEARLY + "," + COLUMN_NAME_MONTH;
        public static string DATE_COLUMNS_DAILY = DATE_COLUMNS_MONTHLY + "," + COLUMN_NAME_DAY;

        /// <summary>
        /// Retrieve data columns for given result interval
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string getDateColumns(SWATResultIntervalType type)
        {
            switch (type)
            {
                case SWATResultIntervalType.DAILY: return DATE_COLUMNS_DAILY;
                case SWATResultIntervalType.MONTHLY: return DATE_COLUMNS_MONTHLY;
                case SWATResultIntervalType.YEARLY: return DATE_COLUMNS_YEARLY;
                default: return "";
            }
        }

        public static string getTableName(SourceType source)
        {
            switch (source)
            {
                case SourceType.HRU: return "hru";
                case SourceType.REACH: return "rch";
                case SourceType.RESERVOIR: return "rsv";
                case SourceType.SUBBASIN: return "sub";
                case SourceType.WATER: return "wtr";
                default: return "";
            }
        }

        public DataTable Extract(SourceType source, int id, string var)
        {
            return Extract(StartYear, EndYear, source, id, var);
        }

        public DataTable Extract(int year, SourceType source, int id, string var)
        {
            return Extract(year,year, source, id, var);
        }

        private double _extractTime = 0.0;

        public double ExtractTime { get { return _extractTime; } }

        public DataTable Extract(
            int requestStartYear, int requestFinishYear,
            SourceType source, int id, string var)
        {
            //columns
            string cols = getDateColumns(Interval);
            cols += "," + var;

            //get table
            string table = getTableName(source);

            //id, for wtr is not correct
            string idCondition = string.Format("{0}={1}",table,id);

            //year condition
            if (requestStartYear < StartYear) requestStartYear = StartYear;
            if (requestFinishYear > EndYear) requestFinishYear = EndYear;
            string yearCondition = "";
            if (requestStartYear != StartYear || requestFinishYear != EndYear)
            {
                if (requestStartYear == requestFinishYear)
                    yearCondition = string.Format(" AND YR = {0}", requestStartYear);
                else
                    yearCondition = string.Format(" AND YR >= {0} AND YR <= {1}", requestStartYear, requestFinishYear);
            }            

            string query = string.Format("select {0} from {1} where {2} {3}", 
                cols,table,idCondition,yearCondition);

            //start time
            DateTime startTime = DateTime.Now;

            DataTable dt = Extract(query);
            
            //output time
            _extractTime = DateTime.Now.Subtract(startTime).TotalMilliseconds;

            //add datetime column and calculate the date
            dt.TableName = "SQLite";
            dt.Columns.Add("TIME", typeof(DateTime));
            foreach (DataRow r in dt.Rows)
                calculateDate(r);

            return dt;
        }

        private DataTable Extract(string query)
        {
            DataTable dt = new DataTable();
            using (SQLiteDataAdapter a = new SQLiteDataAdapter(query, Connection))
            using (SQLiteCommandBuilder b = new SQLiteCommandBuilder(a))
                try
                {
                    a.Fill(dt);
                }
                catch (System.Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Query: " + query);
                    System.Diagnostics.Debug.WriteLine(e.ToString());
                    dt = null;
                } 
            return dt;
        }

        private void calculateDate(DataRow r)
        {
            DateTime d = DateTime.Now;
            RowItem item = new RowItem(r);
            int year = item.getColumnValue_Int(COLUMN_NAME_YEAR);
            int month = 1;
            int day = 1;
            if (Interval == SWATResultIntervalType.MONTHLY || Interval == SWATResultIntervalType.DAILY)
                month = item.getColumnValue_Int(COLUMN_NAME_MONTH);
            if (Interval == SWATResultIntervalType.DAILY)
                day = item.getColumnValue_Int(COLUMN_NAME_DAY);

            r["TIME"] = new DateTime(year, month, day);
        }

    }
}
