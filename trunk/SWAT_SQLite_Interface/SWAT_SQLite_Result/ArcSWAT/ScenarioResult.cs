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

        public ScenarioResult(string databasePath)
        {
            _databasePath = databasePath;
            checkStatus();
            loadModelStructure();
        }

        private ScenarioResultStructure _structure = null;

        public ScenarioResultStructure Structure { get { return _structure; } }

        #region Helper

        private string getInfoTableFromType(SWATUnitType type)
        {
            switch (type)
            {
                case SWATUnitType.HRU: return ScenarioResultStructure.INFO_TABLE_NAME_HRU;
                case SWATUnitType.SUB: return ScenarioResultStructure.INFO_TABLE_NAME_SUB;
                case SWATUnitType.RCH: return ScenarioResultStructure.INFO_TABLE_NAME_RCH;
                case SWATUnitType.RES: return ScenarioResultStructure.INFO_TABLE_NAME_RSV;
                default: return "";
            }
        }

        #endregion

        #region Basic Information

        private string _databasePath = null;
        private int _startYear = ScenarioResultStructure.UNKONWN_ID;
        private int _endYear = ScenarioResultStructure.UNKONWN_ID;
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
        private Dictionary<int, SWATUnit> _reaches = new Dictionary<int, SWATUnit>();
        private Dictionary<int, SWATUnit> _reservoirs = new Dictionary<int, SWATUnit>();
        private Watershed _watershed = null;

        public Dictionary<int, SWATUnit> Subbasins { get { return _subbasins; } }

        private void loadModelStructure()
        {
            //subbasin first and then HRUs to add hru to subbasin
            _subbasins = readUnitBasicInfo(SWATUnitType.SUB);
            _hrus = readUnitBasicInfo(SWATUnitType.HRU);
            _reaches = readUnitBasicInfo(SWATUnitType.RCH);
            _reservoirs = readUnitBasicInfo(SWATUnitType.RES);
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
                    case SWATUnitType.RCH: unit = new Reach(r, this); break;
                    case SWATUnitType.RES: unit = new Reservoir(r, this); break;
                }
                if (unit != null && unit.ID != ScenarioResultStructure.UNKONWN_ID && !units.ContainsKey(unit.ID)) 
                    units.Add(unit.ID, unit);
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

            foreach (SWATUnit u in _hrus.Values)
                sb.AppendLine(u.ToString());

            foreach (SWATUnit u in _subbasins.Values)
                sb.AppendLine(u.ToString());

            foreach (SWATUnit u in _reaches.Values)
                sb.AppendLine(u.ToString());

            if (_reservoirs.Count == 0)
                sb.AppendLine("No Reservoirs!");
            else
            {
                foreach (SWATUnit u in this._reservoirs.Values)
                    sb.AppendLine(u.ToString());
            }

            return sb.ToString();
        }
    }
}
