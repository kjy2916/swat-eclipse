using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Data;

namespace SWAT_SQLite_Result.ArcSWAT
{
    /// <summary>
    /// The structure of the scenario result database
    /// Suppose to be a static structure, don't need to load twice
    /// </summary>
    public class ScenarioResultStructure
    {
        #region result SQLite database structure

        public static int UNKONWN_ID = -1;
        public static double EMPTY_VALUE = -99.0;

        private static string EXE_NAME_SWAT_445 = "swat_sqlite_445.exe";
        private static string EXE_NAME_SWAT_488 = "swat_sqlite_488.exe";
        private static string EXE_NAME_SWAT_622 = "swat_sqlite_622.exe";
        private static string EXE_NAME_CANSWAT = "canswat_sqlite.exe";

        private static string[] EXE_NAMES = new string[] { EXE_NAME_SWAT_488,EXE_NAME_SWAT_445, EXE_NAME_SWAT_622, EXE_NAME_CANSWAT};

        private static string DATABASE_NAME_NORMAL_445 = "result_445.db3";
        private static string DATABASE_NAME_NORMAL_488 = "result_488.db3";
        private static string DATABASE_NAME_NORMAL_622 = "result_622.db3";
        private static string DATABASE_NAME_CANSWAT = "result_canswat.db3";

        private static string[] DATABASE_NAMES = new string[] { DATABASE_NAME_NORMAL_488,DATABASE_NAME_NORMAL_445, DATABASE_NAME_NORMAL_622, DATABASE_NAME_CANSWAT };

        public static string INFO_TABLE_NAME_HRU = "hru_info";
        public static string INFO_TABLE_NAME_SUB = "sub_info";
        public static string INFO_TABLE_NAME_RCH = "rch_info";
        public static string INFO_TABLE_NAME_RSV = "rsv_info";

        public static string COLUMN_NAME_HRU = "HRU";
        public static string COLUMN_NAME_SUB = "SUB";
        public static string COLUMN_NAME_RCH = "RCH";
        public static string COLUMN_NAME_RES = "RES";
        public static string COLUMN_NAME_YEAR = "YR";
        public static string COLUMN_NAME_MONTH = "MO";
        public static string COLUMN_NAME_DAY = "DA";
        public static string COLUMN_NAME_AREA_KM2 = "AREA_KM2";
        public static string COLUMN_NAME_AREA_FR_SUB = "AREA_FR_SUB";
        public static string COLUMN_NAME_AREA_FR_WSHD = "AREA_FR_WSHD";
        public static string COLUMN_NAME_AVE_ANNUAL_BASIN_NAME = "NAME";

        public static string NAME_STATUS_START_YEAR = "START_YEAR";
        public static string NAME_STATUS_END_YEAR = "END_YEAR";
        public static string NAME_STATUS_OUTPUT_INTERVAL = "OUTPUT_INTERVAL";
        public static string NAME_STATUS_SUCCESS = "SUCCESS";

        public static string[] STATUS_NAMES = new string[] { NAME_STATUS_START_YEAR, NAME_STATUS_END_YEAR, NAME_STATUS_OUTPUT_INTERVAL, NAME_STATUS_SUCCESS };

        public static string TABLE_NAME_HRU = "hru";
        public static string TABLE_NAME_HRU_DEPRESSION = "wtr";
        public static string TABLE_NAME_HRU_POTHOLE = "pot";
        public static string TABLE_NAME_HRU_MGT = "mgt";
        public static string TABLE_NAME_HRU_SOIL_NUTRIENT = "snu";
        public static string TABLE_NAME_HRU_SOIL_WATER = "swr";

        public static string TABLE_NAME_SUB = "sub";

        public static string TABLE_NAME_RESERVOIR = "rsv";

        public static string TABLE_NAME_REACH = "rch";
        public static string TABLE_NAME_REACH_SEDIMENT = "sed";

        public static string TABLE_NAME_WATERSHED_DAILY = "watershed_daily";
        public static string TABLE_NAME_WATERSHED_MONTHLY = "watershed_monthly";
        public static string TABLE_NAME_WATERSHED_YEARLY = "watershed_yearly";
        
        public static string TABLE_NAME_WATERSHED_AVERAGE_ANNUAL = "ave_annual_basin";

        public static string[] DAILY_TABLES = new string[] { TABLE_NAME_WATERSHED_DAILY, TABLE_NAME_HRU_MGT, TABLE_NAME_HRU_SOIL_NUTRIENT, TABLE_NAME_HRU_SOIL_WATER };
        public static string[] MONTHLY_TABLES = new string[] { TABLE_NAME_WATERSHED_MONTHLY };
        public static string[] YEARLY_TABLES = new string[] { TABLE_NAME_WATERSHED_YEARLY };

        public static string[] HRU_TABLES = new string[] {
                    TABLE_NAME_HRU, 
                    TABLE_NAME_HRU_DEPRESSION,
                    TABLE_NAME_HRU_MGT,
                    TABLE_NAME_HRU_POTHOLE,
                    TABLE_NAME_HRU_SOIL_NUTRIENT,
                    TABLE_NAME_HRU_SOIL_WATER};

        public static string[] REACH_TABLES = new string[] {
                    TABLE_NAME_REACH, 
                    TABLE_NAME_REACH_SEDIMENT};

        public static string[] SUBBASIN_TABLES = new string[] {
                    TABLE_NAME_SUB};

        public static string[] RESERVOIR_TABLES = new string[] {
                    TABLE_NAME_RESERVOIR};

        public static string[] WATERSHED_TABLES = new string[] {
                    TABLE_NAME_WATERSHED_DAILY,
                    TABLE_NAME_WATERSHED_MONTHLY,
                    TABLE_NAME_WATERSHED_YEARLY};


        public static string DATE_COLUMNS_YEARLY = COLUMN_NAME_YEAR;
        public static string DATE_COLUMNS_MONTHLY = DATE_COLUMNS_YEARLY + "," + COLUMN_NAME_MONTH;
        public static string DATE_COLUMNS_DAILY = DATE_COLUMNS_MONTHLY + "," + COLUMN_NAME_DAY;

        #endregion

        public ScenarioResultStructure(ScenarioResult scenario)
        {
            _scenario = scenario;
        }

        private ScenarioResult _scenario = null;

        private Dictionary<string, StringCollection> _columns = new Dictionary<string, StringCollection>();

        public static string getDatabaseName(SWATModelType modelType)
        {
            int index = Convert.ToInt32(modelType);
            if (index >= 0 && index < DATABASE_NAMES.Length)
                return DATABASE_NAMES[index];
            return "";
        }

        public static string getSWATExecutableName(SWATModelType modelType)
        {
            int index = Convert.ToInt32(modelType);
            if (index >= 0 && index < EXE_NAMES.Length)
                return EXE_NAMES[index];
            return "";
        }

        /// <summary>
        /// Retrieve data columns from table name
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        /// <remarks>Only float columns are return</remarks>
        public StringCollection getDataColumns(string tableName)
        {
            StringCollection cols = new StringCollection();
            if (tableName == null) return cols;

            tableName = tableName.Trim();
            if (tableName.Length == 0) return cols;

            if (!_columns.ContainsKey(tableName))
            {
                if (_scenario == null) return cols;                

                DataTable dt = _scenario.GetDataTable(
                    string.Format("PRAGMA table_info({0})", tableName));
                foreach (DataRow r in dt.Rows)
                {
                    RowItem item = new RowItem(r);
                    if (item.getColumnValue_String("type").ToLower().Equals("float"))
                        cols.Add(item.getColumnValue_String("name"));
                }
                if (cols.Count > 0)
                    _columns.Add(tableName, cols);
                else
                    return new StringCollection();
            }
            return _columns[tableName];
        }

        /// <summary>
        /// Retrieve data interval from table name
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        /// <remarks>The interval of some outputs keeps same regardless of system setting (iprint)</remarks>
        public SWATResultIntervalType getInterval(string tableName)
        {
            if (System.Array.IndexOf(DAILY_TABLES, tableName) > -1) return SWATResultIntervalType.DAILY;
            if (System.Array.IndexOf(MONTHLY_TABLES, tableName) > -1) return SWATResultIntervalType.MONTHLY;
            if (System.Array.IndexOf(YEARLY_TABLES, tableName) > -1) return SWATResultIntervalType.YEARLY;
            if (_scenario != null) return _scenario.Interval;
            return SWATResultIntervalType.UNKNOWN;            
        }

        private Dictionary<SWATUnitType, StringCollection> _resultTableNames = new Dictionary<SWATUnitType, StringCollection>();

        public static string[] getResultTableNames(SWATUnitType type)
        {
            switch (type)
            {
                case SWATUnitType.HRU: return HRU_TABLES;
                case SWATUnitType.RCH: return REACH_TABLES;
                case SWATUnitType.SUB: return SUBBASIN_TABLES;
                case SWATUnitType.RES: return RESERVOIR_TABLES;
                case SWATUnitType.WSHD: return WATERSHED_TABLES;
                default: return new string[] { };
            }
        }       

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

        private Dictionary<string, bool> _tableStatus = new Dictionary<string, bool>();

        public bool isTableHasData(string tableName)
        {
            tableName = tableName.Trim().ToLower();
            if (!_tableStatus.ContainsKey(tableName))
            {
                bool hasData = false;
                DataTable dt = _scenario.GetDataTable(
                    string.Format("select * from sqlite_master where type = 'table' and name ='{0}'", tableName));
                if (dt.Rows.Count > 0)
                {
                    dt = _scenario.GetDataTable(
                        string.Format("select count(*) from {0}", tableName));

                    RowItem item = new RowItem(dt.Rows[0]);
                    if (item.getColumnValue_Int(0) > 0) hasData = true;
                }
                _tableStatus[tableName] = hasData;
            }
            return _tableStatus[tableName];
        }

        /// <summary>
        /// Get tables with data in it
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public StringCollection getResultTablesWithData(SWATUnitType type)
        {
            string[] tbls_fromType = getResultTableNames(type);
            StringCollection tbls = new StringCollection();
            foreach(string tbl in tbls_fromType)
                if(isTableHasData(tbl))
                    tbls.Add(tbl);
            return tbls;
        }
    }
}
