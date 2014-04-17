using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace SWAT_SQLite_Result.ArcSWAT
{
    class ScenarioResult
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

        #endregion

        public ScenarioResult(string databasePath)
        {
            _databasePath = databasePath;
            checkStatus();
            loadModelStructure();
        }

        #region Helper

        private string getInfoTableFromType(SWATUnitType type)
        {
            switch (type)
            {
                case SWATUnitType.HRU: return INFO_TABLE_NAME_HRU;
                case SWATUnitType.SUB: return INFO_TABLE_NAME_SUB;
                case SWATUnitType.RCH: return INFO_TABLE_NAME_RCH;
                case SWATUnitType.RES: return INFO_TABLE_NAME_RSV;
                default: return "";
            }
        }

        #endregion

        #region Basic Information

        private string _databasePath = null;
        private int _startYear = UNKONWN_ID;
        private int _endYear = UNKONWN_ID;
        private SWATResultIntervalType _interval = SWATResultIntervalType.UNKNOWN;
        private ScenarioResultStatus _status = ScenarioResultStatus.UNKNOWN;

        public ScenarioResultStatus Status { get { return _status; } }
        public String DatabasePath {get { return _databasePath; }}
        public int StartYear { get { return _startYear; } }
        public int EndYear { get { return _endYear; } }
        public SWATResultIntervalType Interval { get { return _interval; } }
 
        private void checkStatus()
        {
            if (DatabasePath == null || !File.Exists(DatabasePath)) { _status = ScenarioResultStatus.NO_EXIST; return; }

            DataTable dt = Query.GetDataTable("select * from ave_annual_basin", DatabasePath);
            if (dt.Rows.Count == 0) { _status = ScenarioResultStatus.UNSUCCESS; return; }

            foreach (DataRow r in dt.Rows)
            {
                RowItem item = new RowItem(r);
                string name = item.getColumnValue_String("NAME");
                if (name.Equals("START_YEAR")) 
                    _startYear = item.getColumnValue_Int("VALUE");
                else if (name.Equals("END_YEAR")) 
                    _endYear = item.getColumnValue_Int("VALUE");
                else if (name.Equals("OUTPUT_INTERVAL")) 
                    _interval = (SWATResultIntervalType)(item.getColumnValue_Int("VALUE"));
                else if (name.Equals("SUCCESS")) 
                    _status = ScenarioResultStatus.NORMAL;                
            }             
        }

#endregion

        #region All Kinds of SWAT Units

        private Dictionary<int, SWATUnit> _hrus = new Dictionary<int, SWATUnit>();
        private Dictionary<int, SWATUnit> _subbasins = new Dictionary<int, SWATUnit>();

        public Dictionary<int, SWATUnit> Subbasins { get { return _subbasins; } }

        private void loadModelStructure()
        {
            //subbasin first and then HRUs to add hru to subbasin
            _subbasins = readUnitBasicInfo(SWATUnitType.SUB);
            _hrus = readUnitBasicInfo(SWATUnitType.HRU);            
        }

        private Dictionary<int, SWATUnit> readUnitBasicInfo(SWATUnitType type)
        {
            Dictionary<int, SWATUnit> units = new Dictionary<int, SWATUnit>();

            DataTable dt = GetDataTable("select * from " + getInfoTableFromType(type));
            foreach (DataRow r in dt.Rows)
            {
                SWATUnit unit = null;
                switch (type)
                {
                    case SWATUnitType.HRU: unit = new HRU(r, this); break;
                    case SWATUnitType.SUB: unit = new Subbasin(r, this); break;
                    case SWATUnitType.RCH: unit = new HRU(r, this); break;
                    case SWATUnitType.RES: unit = new HRU(r, this); break;
                }
                if (unit != null) units.Add(unit.ID, unit);
            }
            return units;
        }

        #endregion

        #region Results

        public DataTable GetDataTable(string query)
        {
            if (Status != ScenarioResultStatus.NORMAL) return new DataTable();
            return Query.GetDataTable(query, DatabasePath);
        }



#endregion

        public override string ToString()
        {
            if (Status != ScenarioResultStatus.NORMAL) return Status.ToString();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("Database File: {0}",DatabasePath));
            sb.AppendLine(string.Format("Start Year: {0}", StartYear));
            sb.AppendLine(string.Format("End Year: {0}", EndYear));
            sb.AppendLine(string.Format("Interval : {0}", Interval));

            return sb.ToString();
        }
    }
}
