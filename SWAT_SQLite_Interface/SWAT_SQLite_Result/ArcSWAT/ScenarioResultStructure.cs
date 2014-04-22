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
    class ScenarioResultStructure
    {
        #region result SQLite database structure

        public static int UNKONWN_ID = -1;
        public static double EMPTY_VALUE = -99.0;

        public static string DATABASE_NAME_NORMAL = "result.db3";
        public static string DATABASE_NAME_CANSWAT = "result_can.db3";

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
        public static string COLUMN_NAME_AREA_KM2 = "AREA_KM2";
        public static string COLUMN_NAME_AREA_FR_SUB = "AREA_FR_SUB";
        public static string COLUMN_NAME_AREA_FR_WSHD = "AREA_FR_WSHD";

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
        //public static string TABLE_NAME_REACH_SEDIMENT = "ave_annual_basin";

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

        #endregion

        private ScenarioResult _scenario = null;

        private Dictionary<string, StringCollection> _columns = new Dictionary<string, StringCollection>();

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

            tableName = tableName.Trim().ToLower();
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
                if(cols.Count > 0)
                    _columns.Add(tableName, cols);
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
    }
}
