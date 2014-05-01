using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Data;

namespace SWAT_SQLite_Result.ArcSWAT
{
    public class ObservationData
    {
        private string _databasePath = null;
        private Dictionary<string, SWATUnitObservationData> _allData = new Dictionary<string, SWATUnitObservationData>();
        public static string OBSERVATION_COLUMN_DATE = "date";
        public static string OBSERVATION_COLUMN_ID = "id";
        private bool _exist = false;

        public ObservationData(string databasePath)
        {
            _databasePath = databasePath;
            _exist = System.IO.File.Exists(_databasePath);
        }

        #region Read Data

        public DataTable GetDataTable(string query)
        {
            return Query.GetDataTable(query, _databasePath);
        }

        public SWATUnitColumnYearObservationData getObservedData(SWATUnitColumnYearResult result)
        {
            //check if the database is there
            if (!_exist) return null;

            //see if the column has correponding observed data
            if (System.Array.IndexOf(OBSERVATION_COLUMNS, result.Column) == -1) return null;

            //see if the id is in the list
            List<int> ids = getIDs(result.UnitResult.Unit.Type);
            if(ids.Count == 0) return null;
            if (!ids.Contains(result.UnitResult.Unit.ID)) return null;
            
            //find the data
            string uniqueID = getUniqueId(result);
            if (!_allData.ContainsKey(uniqueID))
                _allData.Add(uniqueID, new SWATUnitObservationData(result, this));
            SWATUnitObservationData unitData = _allData[uniqueID];

            return unitData.getObservedData(result.Year);
          }

        private string getUniqueId(SWATUnitType type, int id, string col, int startYear, int endYear)
        {
            return string.Format("{0}_{1}_{2}_{3}_{4}",
                type, id, col, startYear, endYear);
        }

        private string getUniqueId(SWATUnitColumnYearResult result)
        {
            int startYear = result.UnitResult.Unit.Scenario.StartYear;
            int endYear = result.UnitResult.Unit.Scenario.EndYear;
            SWATUnitType unitType = result.UnitResult.Unit.Type;
            int id = result.UnitResult.Unit.ID;
            string col = result.Column;

            return getUniqueId(unitType, id, col, startYear, endYear);
        }

        /// <summary>
        /// Load data
        /// </summary>
        /// <param name="startYear"></param>
        /// <param name="endYear"></param>
        /// <remarks>could add other types in the future</remarks>
        private void loadData(int startYear, int endYear)
        {
            LoadData(SWATUnitType.RCH,startYear,endYear);
            LoadData(SWATUnitType.RES,startYear,endYear);            
        }

        Dictionary<SWATUnitType, List<int>> _ids = new Dictionary<SWATUnitType, List<int>>();

        private List<int> getIDs(SWATUnitType type)
        {
            List<int> ids = new List<int>();
            if (type == SWATUnitType.UNKNOWN) return ids;
            if (!_ids.ContainsKey(type))
            {
                DataTable dt = GetDataTable("select distinct " + OBSERVATION_COLUMN_ID + " from " + type.ToString());
                if (dt.Rows.Count == 0) return ids;

                foreach (DataRow r in dt.Rows)
                {
                    RowItem item = new RowItem(r);
                    int id = item.getColumnValue_Int(0);

                    if (id <= 0) continue;

                    ids.Add(id);
                }
                _ids.Add(type, ids);
            }
            return _ids[type];
        }

        private void LoadData(SWATUnitType type, int startYear, int endYear)
        {
            //get ids
            List<int> ids = getIDs(type);
            if (ids.Count == 0) return;

            //table name
            string tableName = type.ToString();

            //get columns
            StringCollection cols = Query.GetDataColumns(_databasePath, tableName);
            if (cols.Count == 0) return;

            //get types corresponding to column
            List<ObservationDataType> dataTypes = new List<ObservationDataType>();
            foreach (string col in cols) dataTypes.Add(Column2DataType(col));            

            //read all data

            foreach (int id in ids)
            {
                foreach (ObservationDataType dataType in dataTypes)
                {
                    if (dataType == ObservationDataType.UNKNOWN) return;

                    string dataUniqueId = getUniqueId(type, id, dataType.ToString(),startYear,endYear);
                    _allData.Add(dataUniqueId,
                        new SWATUnitObservationData(
                            id,type,dataType,
                            startYear,endYear,
                            this));
                }                
            }
        }

        private static string[] OBSERVATION_COLUMNS = new string[] {
            ObservationDataType.FLOW_OUTcms.ToString(),
            ObservationDataType.SED_OUTtons.ToString(),
            ObservationDataType.ORGN_OUTkg.ToString(),
            ObservationDataType.SOL_N.ToString(),
            ObservationDataType.TOT_Nkg.ToString(),
            ObservationDataType.ORGP_OUTkg.ToString(),
            ObservationDataType.MINP_OUTkg.ToString(),
            ObservationDataType.TOT_Pkg.ToString()};

        private static ObservationDataType Column2DataType(string col)
        {
            int index = Array.IndexOf(OBSERVATION_COLUMNS, col);
            if (index > -1) return (ObservationDataType)index;
            return ObservationDataType.UNKNOWN;
        }

        #endregion

        //#region Save Data from CSV



        //#region
    }
}
