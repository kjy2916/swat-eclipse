using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Data;
using LumenWorks.Framework.IO.Csv;
using System.IO;

namespace SWAT_SQLite_Result.ArcSWAT
{
    public class ObservationData
    {
        private string _databasePath = null;
        private Dictionary<string, SWATUnitObservationData> _allData = new Dictionary<string, SWATUnitObservationData>();
        private Dictionary<string, List<SWATUnitObservationData>> _allData_display = new Dictionary<string, List<SWATUnitObservationData>>();
        public static string OBSERVATION_COLUMN_DATE = "date";
        public static string OBSERVATION_COLUMN_VALUE = "value";

        private bool _exist = false;

        public ObservationData(string databasePath)
        {
            _databasePath = databasePath;
            _exist = System.IO.File.Exists(_databasePath);

            loadData();
        }

        #region Read Data

        public DataTable GetDataTable(string query)
        {
            return Query.GetDataTable(query, _databasePath);
        }

        /// <summary>
        /// Used to get observed data for display
        /// </summary>
        /// <param name="unitType"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<SWATUnitObservationData> getObservedData(SWATUnitType unitType, int id)
        {
            string display_id = string.Format("{0}_{1}", unitType, id);
            if (_allData_display.ContainsKey(display_id)) return _allData_display[display_id];
            return new List<SWATUnitObservationData>();
        }

        public SWATUnitObservationData getObservedData(SWATUnitType unitType, int id, string col)
        {
            string uniqueID = getUniqueId(unitType, id, col);
            if (_allData.ContainsKey(uniqueID)) return _allData[uniqueID];
            return null;
        }

        /// <summary>
        /// Used to get observed data corresponding to specific result
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public SWATUnitColumnYearObservationData getObservedData(SWATUnitColumnYearResult result)
        {
            //check if the database is there
            if (!_exist) return null;

            //see if the column has correponding observed data
            string[] columns = getObservationDataColumns(result.UnitResult.Unit.Type);
            if (columns == null) return null;
            if (System.Array.IndexOf(columns, result.Column) == -1) return null;
           
            //find the data            
            string uniqueID = getUniqueId(result.UnitResult.Unit.Type, result.UnitResult.Unit.ID,result.Column);
            if (_allData.ContainsKey(uniqueID))
            {
                uniqueID = getUniqueId(result);
                if(!_allData.ContainsKey(uniqueID))
                    _allData.Add(uniqueID, new SWATUnitObservationData(result, this));
                SWATUnitObservationData unitData = _allData[uniqueID];
                return unitData.getObservedData(result.Year);
            }
            return null;            
          }

        private string getUniqueId(SWATUnitType type, int id, string col)
        {
            return getUniqueId(type, id, col, -1, -1);
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
        /// Load data. Would be used to show all the observed data.
        /// </summary>
        /// <param name="startYear"></param>
        /// <param name="endYear"></param>
        /// <remarks>could add other types in the future</remarks>
        private void loadData()
        {
            _allData.Clear();
            _allData_display.Clear();
            loadData(SWATUnitType.RCH);
            loadData(SWATUnitType.RES);            
        }

        private void loadData(SWATUnitType type, int id, string col)
        {
            if (!_exist) return;

            string tableName = getTableName(type, id, col);
            DataTable dt = GetDataTable(
                string.Format("select name from sqlite_master where type = 'table' and name = '{0}'", tableName));
            if (dt.Rows.Count > 0)
            {
                //see if there are some data
                //don't consider empty tables
                dt = GetDataTable(
                    string.Format("select count(*) from [{0}]", tableName));
                if (dt.Rows.Count == 0) return;
                RowItem item = new RowItem(dt.Rows[0]);
                if (item.getColumnValue_Int(0) == 0) return;

                //read data                
                string dataUniqueId = getUniqueId(type, id, col);

                //remove previous one
                _allData.Remove(dataUniqueId);
                _allData_display.Remove(dataUniqueId);

                //add new one
                SWATUnitObservationData data =
                    new SWATUnitObservationData(
                        id, type, col,
                        -1, -1,
                        this);

                //add to all data
                _allData.Add(dataUniqueId, data);

                //add to display data
                string display_id = string.Format("{0}_{1}", type, id);
                if (!_allData_display.ContainsKey(display_id))
                    _allData_display.Add(display_id, new List<SWATUnitObservationData>());
                _allData_display[display_id].Add(data);
            }
        }

        private void loadData(SWATUnitType type)
        {
            if (!_exist) return;

            DataTable dt = GetDataTable("select name from sqlite_master where type = 'table'");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    //get table name
                    string tableName = r[0].ToString();
                    if(!tableName.Contains(type.ToString())) continue;

                    //see if there are some data
                    //don't consider empty tables
                    dt = GetDataTable(
                        string.Format("select count(*) from [{0}]", tableName));
                    RowItem item = new RowItem(dt.Rows[0]);
                    if (item.getColumnValue_Int(0) == 0) continue;

                    //extract the id from table name
                    string[] all_in_tableName = tableName.Split(OBSERVATION_TABLE_NAME_DELIMITER);
                    if (all_in_tableName.Length < 3) continue;

                    int id = -1;
                    if (int.TryParse(all_in_tableName[1], out id))
                    {
                        string col = all_in_tableName[2];

                        string dataUniqueId = getUniqueId(type, id, col);

                        if (!_allData.ContainsKey(dataUniqueId))
                        {
                            SWATUnitObservationData data =
                                new SWATUnitObservationData(
                                    id, type, col,
                                    -1, -1,
                                    this);

                            //add to all data
                            _allData.Add(dataUniqueId,data);

                            //add to display data
                            string display_id = string.Format("{0}_{1}",type,id);
                            if (!_allData_display.ContainsKey(display_id))
                                _allData_display.Add(display_id, new List<SWATUnitObservationData>());
                            _allData_display[display_id].Add(data);
                        }
                    }
                }
            }
        }

        private static char OBSERVATION_TABLE_NAME_DELIMITER = '-';

        public static string getTableName(SWATUnitType type, int id, string col)
        {
            return string.Format("{0}{3}{1}{3}{2}", type, id, col, OBSERVATION_TABLE_NAME_DELIMITER);
        }

        private static string OBSERVATION_DATA_TYPE_FLOW = "FLOW(m3/s)";
        private static string OBSERVATION_DATA_TYPE_SEDIMENT = "TSS(ton)";
        private static string OBSERVATION_DATA_TYPE_ORGNIC_N = "Organic N(kg)";
        private static string OBSERVATION_DATA_TYPE_NO3 = "NO3(kg)";
        private static string OBSERVATION_DATA_TYPE_NO2 = "NO2(kg)";
        private static string OBSERVATION_DATA_TYPE_NH4 = "NH4(kg)";
        private static string OBSERVATION_DATA_TYPE_TN = "TN(kg)";
        private static string OBSERVATION_DATA_TYPE_ORGNIC_P = "Organic P(kg)";
        private static string OBSERVATION_DATA_TYPE_MINERAL_P = "Mineral P(kg)";
        private static string OBSERVATION_DATA_TYPE_TP = "TP(kg)";

        private static string OBSERVATION_DATA_TYPE_SWAT_FLOW = "FLOW_OUTcms";
        private static string OBSERVATION_DATA_TYPE_SWAT_SEDIMENT = "SED_OUTtons";
        private static string OBSERVATION_DATA_TYPE_SWAT_ORGNIC_N = "ORGN_OUTkg";
        private static string OBSERVATION_DATA_TYPE_SWAT_NO3 = "NO3_OUTkg";
        private static string OBSERVATION_DATA_TYPE_SWAT_NO2 = "NO2_OUTkg";
        private static string OBSERVATION_DATA_TYPE_SWAT_NH4 = "NH4_OUTkg";
        private static string OBSERVATION_DATA_TYPE_SWAT_TN = "TOT_Nkg";
        private static string OBSERVATION_DATA_TYPE_SWAT_ORGNIC_P = "ORGP_OUTkg";
        private static string OBSERVATION_DATA_TYPE_SWAT_MINERAL_P = "MINP_OUTkg";
        private static string OBSERVATION_DATA_TYPE_SWAT_TP = "TOT_Pkg";

        /// <summary>
        /// All SWAT columns which would have observation data
        /// </summary>
        private static string[] OBSERVATION_COLUMNS_SWAT = new string[]{
            OBSERVATION_DATA_TYPE_SWAT_FLOW,
            OBSERVATION_DATA_TYPE_SWAT_SEDIMENT,
            OBSERVATION_DATA_TYPE_SWAT_ORGNIC_N,
            OBSERVATION_DATA_TYPE_SWAT_NO3,
            OBSERVATION_DATA_TYPE_SWAT_NO2,
            OBSERVATION_DATA_TYPE_SWAT_NH4,
            OBSERVATION_DATA_TYPE_SWAT_TN,
            OBSERVATION_DATA_TYPE_SWAT_ORGNIC_P,
            OBSERVATION_DATA_TYPE_SWAT_MINERAL_P,
            OBSERVATION_DATA_TYPE_SWAT_TP};

        /// <summary>
        /// All variables which would have observation data. Here the name is a user-friendly name and 
        /// is corresponding to SWAT columns.
        /// </summary>
        private static string[] OBSERVATION_COLUMNS = new string[] {
            OBSERVATION_DATA_TYPE_FLOW,
            OBSERVATION_DATA_TYPE_SEDIMENT,
            OBSERVATION_DATA_TYPE_ORGNIC_N,
            OBSERVATION_DATA_TYPE_NO3,
            OBSERVATION_DATA_TYPE_NO2,
            OBSERVATION_DATA_TYPE_NH4,
            OBSERVATION_DATA_TYPE_TN,
            OBSERVATION_DATA_TYPE_ORGNIC_P,
            OBSERVATION_DATA_TYPE_MINERAL_P,
            OBSERVATION_DATA_TYPE_TP};

        private static string[] OBSERVATION_REACH_COLUMNS = OBSERVATION_COLUMNS;

        private static string[] OBSERVATION_RESERVOIR_COLUMNS = new string[] {
            OBSERVATION_DATA_TYPE_FLOW,
            OBSERVATION_DATA_TYPE_SEDIMENT,
            OBSERVATION_DATA_TYPE_ORGNIC_N,
            OBSERVATION_DATA_TYPE_ORGNIC_P,
            OBSERVATION_DATA_TYPE_MINERAL_P};

        public static string[] getObservationDataColumns(SWATUnitType unitType)
        {
            if (unitType == SWATUnitType.RCH) return OBSERVATION_REACH_COLUMNS;
            if (unitType == SWATUnitType.RES) return OBSERVATION_RESERVOIR_COLUMNS;
            return null;
        }

        /// <summary>
        /// Translate from the user friendly name to SWAT column name
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public static string getObservationSWATColumn(string col)
        {
            int index = Array.IndexOf(OBSERVATION_COLUMNS, col);
            if (index == -1) return "";
            return OBSERVATION_COLUMNS_SWAT[index];
        }

        #endregion

        #region Save Data from CSV

        private static AccessColumn OBSERVED_TABLE_DATE_COLUMN =
            new AccessColumn() { ColumnName = OBSERVATION_COLUMN_DATE, Type = typeof(DateTime) };

        private static AccessColumn OBSERVED_TABLE_VALUE_COLUMN =
            new AccessColumn() { ColumnName = OBSERVATION_COLUMN_VALUE, Type = typeof(double) };

        private string INSERT_SQL_FORMAT = "insert into {0} (" +
            OBSERVATION_COLUMN_DATE + "," + OBSERVATION_COLUMN_VALUE + ") values ('{1:yyyy-MM-dd}',{2});";

        public bool delete(SWATUnitType unitType, int id, string col)
        {
            if (getObservedData(unitType, id, col) == null) return true;

            //empty the table, but not drop the table
            string tableName = "[" + getTableName(unitType, id, col) + "]";
            SQLite.PrepareTable(_databasePath, tableName,
                OBSERVED_TABLE_DATE_COLUMN, OBSERVED_TABLE_VALUE_COLUMN);

            //reload all observed data
            loadData();

            return true;
        }

        /// <summary>
        /// Load given csv file into the database
        /// </summary>
        /// <param name="csvFile"></param>
        /// <param name="unitType"></param>
        /// <param name="id"></param>
        /// <param name="col"></param>
        public bool loadCSV(string csvFile, SWATUnitType unitType, int id, string col)
        {
            if(!System.IO.File.Exists(csvFile)) 
                throw new Exception(csvFile + " doesn't exist.");
            
            if(getObservedData(unitType,id,col) != null)
                if(System.Windows.Forms.MessageBox.Show(
                    string.Format("Observed data for {0},{1},{2} already exist. Do you want to overwrite?",unitType,id,col), 
                    SWAT_SQLite.NAME, System.Windows.Forms.MessageBoxButtons.YesNo) 
                    == System.Windows.Forms.DialogResult.No) 
                    return false;

            //save data
            using (CsvReader csv = new CsvReader(new StreamReader(csvFile),true))
            {                
                if (csv.FieldCount < 2)
                    throw new Exception("There should be at least two data columns in the csv file!");
 
                //create the table
                //this would also delete existing data
                string tableName = "[" + getTableName(unitType, id, col) + "]";
                SQLite.PrepareTable(_databasePath, tableName,
                    OBSERVED_TABLE_DATE_COLUMN, OBSERVED_TABLE_VALUE_COLUMN);

                StringBuilder sb = new StringBuilder();
                DateTime date = DateTime.Now;
                double value = -99.0;
                while (csv.ReadNextRecord())
                {
                    if (DateTime.TryParse(csv[0], out date) &&
                        double.TryParse(csv[1],out value))
                        sb.Append(string.Format(INSERT_SQL_FORMAT, tableName, date, value));                   
                }
                if (sb.Length == 0)
                    throw new Exception("The given file is empty!");

                SQLite.insert(_databasePath,sb.ToString());

                //update the file status. It may not exist before the data is loaded.
                _exist = System.IO.File.Exists(_databasePath);

                //reload the data
                loadData(unitType, id, col);

                return true;
            }
        }

        #endregion
    }
}
