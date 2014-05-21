using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace SWAT_SQLite_Result.ArcSWAT
{
    public class ScenarioResult
    {  
        public ScenarioResult(string databasePath,Scenario scen, SWATModelType modelType)
        {
            _databasePath = databasePath;
            _parentScenario = scen;
            _modelType = modelType;
            checkStatus();
            if (Status == ScenarioResultStatus.NORMAL)
                 loadModelStructure();
          
        }

        private Scenario _parentScenario = null;

        public Scenario Scenario { get { return _parentScenario; } }

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
        private DateTime _generationTime = DateTime.Now;
        private SWATModelType _modelType = SWATModelType.UNKNOWN;


        public ScenarioResultStatus Status { get { return _status; } }
        public String DatabasePath {get { return _databasePath; }}
        public int StartYear { get { return _startYear; } }
        public int EndYear { get { return _endYear; } }
        public SWATResultIntervalType Interval { get { return _interval; } }
        public DateTime SimulationTime { get { return _generationTime; } }
        public SWATModelType ModelType { get { return _modelType; } }

        private void checkStatus()
        {
            if (DatabasePath == null || !File.Exists(DatabasePath)) { _status = ScenarioResultStatus.NO_EXIST; return; }

            DataTable dt = Query.GetDataTable("select * from " + ScenarioResultStructure.TABLE_NAME_WATERSHED_AVERAGE_ANNUAL, DatabasePath);
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

            _generationTime = (new System.IO.FileInfo(DatabasePath)).LastWriteTime;
        }

#endregion

        #region All Kinds of SWAT Units

        private Dictionary<int, SWATUnit> _hrus = new Dictionary<int, SWATUnit>();
        private Dictionary<int, SWATUnit> _subbasins = new Dictionary<int, SWATUnit>();
        private Dictionary<int, SWATUnit> _reaches = new Dictionary<int, SWATUnit>();
        private Dictionary<int, SWATUnit> _reservoirs = new Dictionary<int, SWATUnit>();
        private Dictionary<SWATUnitType, Dictionary<int, SWATUnit>> _units = new Dictionary<SWATUnitType, Dictionary<int, SWATUnit>>();
        private Dictionary<SWATUnitType, List<int>> _unitIds = new Dictionary<SWATUnitType, List<int>>();

        private Watershed _watershed = null;

        public Dictionary<int, SWATUnit> HRUs { get { return _hrus; } }
        public Dictionary<int, SWATUnit> Subbasins { get { return _subbasins; } }
        public Dictionary<int, SWATUnit> Reaches { get { return _reaches; } }
        public Dictionary<int, SWATUnit> Reservoirs { get { return _reservoirs; } }
        
        public Watershed Watershed { get { return _watershed; } }

        public List<int> getSWATUnitIDs(SWATUnitType type)
        {
            List<int> ids = new List<int>();
            if (type == SWATUnitType.UNKNOWN || type == SWATUnitType.WSHD) return ids;

            if (_unitIds == null || !_unitIds.ContainsKey(type)) return ids;

            return _unitIds[type];
        }

        public SWATUnit getSWATUnit(SWATUnitType type, int id)
        {
            if (type == SWATUnitType.WSHD) return _watershed;

            Dictionary<int, SWATUnit> units = _units[type];
            if (units.ContainsKey(id)) return units[id];
            return null;
        }

        private void loadModelStructure()
        {
            _structure = new ScenarioResultStructure(this);

            _units.Clear();
            _unitIds.Clear();

            //subbasin first and then HRUs to add hru to subbasin
            _subbasins = readUnitBasicInfo(SWATUnitType.SUB);
            _hrus = readUnitBasicInfo(SWATUnitType.HRU);
            _reaches = readUnitBasicInfo(SWATUnitType.RCH);
            _reservoirs = readUnitBasicInfo(SWATUnitType.RES);
            _watershed = new Watershed(this);
        }

        private Dictionary<int, SWATUnit> readUnitBasicInfo(SWATUnitType type)
        {
            Dictionary<int, SWATUnit> units = new Dictionary<int, SWATUnit>();
            List<int> ids = new List<int>();

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
                {
                    units.Add(unit.ID, unit);
                    ids.Add(unit.ID);
                }
            }

            _units.Add(type,units);
            _unitIds.Add(type, ids);

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

        #region Difference compared with other scenario

        /// <summary>
        /// Scenario results which is comparable with current result.
        /// </summary>
        /// <remarks>
        /// 1. Results of other model type in same scenario; 
        /// 2. Results of same model type in other scenarios;
        /// 3. Must have reasult database in place.
        /// </remarks>
        public List<ScenarioResult> ComparableScenarioResults
        {
            get
            {
                List<ScenarioResult> results = new List<ScenarioResult>();

                //look for results in same scenario
                for (int i = Convert.ToInt32(ArcSWAT.SWATModelType.SWAT_488); i <= Convert.ToInt32(ArcSWAT.SWATModelType.CanSWAT); i++)
                {
                    ArcSWAT.SWATModelType modelType = (ArcSWAT.SWATModelType)i;
                    if (modelType == ModelType) continue;

                    ScenarioResult r = Scenario.getModelResult(modelType);
                    if (r == null || r.Status != ScenarioResultStatus.NORMAL) continue;
                    results.Add(r);
                }

                //look for results in other scenario
                foreach (string scenName in Scenario.Project.Scenarios.Keys)
                {
                    ArcSWAT.Scenario compareScenario = Scenario.Project.Scenarios[scenName];
                    if (Scenario == compareScenario) continue;

                    ScenarioResult r = compareScenario.getModelResult(ModelType);
                    if (r == null || r.Status != ScenarioResultStatus.NORMAL || r.Interval != this.Interval) continue;
                    results.Add(r);
                }

                return results;
            }
        }

        private DataSet _differenceDataset = new System.Data.DataSet();

        /// <summary>
        /// Retrieve difference data table between two scenarios
        /// </summary>
        /// <param name="type"></param>
        /// <param name="resultType"></param>
        /// <param name="col"></param>
        /// <param name="compareScenario"></param>
        /// <returns></returns>
        public DataTable getDifference(ArcSWAT.SWATUnitType type, string resultType, string col,
            ArcSWAT.ScenarioResult compareScenario, System.ComponentModel.BackgroundWorker worker = null)
        {
            string tableId = string.Format("{0}_{1}_{2}_{3}_{4}", type, resultType, col,
                compareScenario.ModelType, compareScenario.Scenario.Name);

            if (!_differenceDataset.Tables.Contains(tableId))
            {
                List<int> ids = getSWATUnitIDs(type);

                DataTable dt = new System.Data.DataTable(tableId);
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("R2", typeof(double));
                foreach (int id in ids)
                {
                    if (worker != null)
                        worker.ReportProgress(0, string.Format("{0}:{1}",type,id));

                    ArcSWAT.SWATUnit unit = getSWATUnit(type, id);
                    if (unit == null) continue;

                    ArcSWAT.SWATUnitResult unitResult = unit.getResult(resultType);
                    if (unitResult == null) continue;

                    ArcSWAT.SWATUnitColumnYearResult oneUnitResult = unitResult.getResult(col, -1);
                    if (oneUnitResult == null) continue;

                    try
                    {
                        DataRow r = dt.NewRow();
                        r[0] = id;
                        r[1] = oneUnitResult.Compare(compareScenario).Statistics.R2;
                        dt.Rows.Add(r);
                    }
                    catch (System.Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine(e.Message);
                    }
                }
                _differenceDataset.Tables.Add(dt);
            }
            return _differenceDataset.Tables[tableId];
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
