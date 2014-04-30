using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SWAT_SQLite_Result.ArcSWAT
{
    public class SWATUnitColumnYearResult
    {
        private string _col = null;
        private string _colCompare = null;
        private int _year = -1;
        private string _id = null;
        private SWATUnitResult _result = null;
        private Statistics _stat = null;
        private int _dateIndex = -1;
        private int _colIndex = -1;
        private Dictionary<string, SWATUnitColumnYearCompareResult> _compares = new Dictionary<string, SWATUnitColumnYearCompareResult>();
        private Dictionary<string, DataTable> _compareCombineTable = new Dictionary<string, DataTable>();

        public SWATUnitColumnYearResult(string col, int year, SWATUnitResult result)
        {            
            _col = col;
            _year = year;
            _id = getUniqueResultID(col, year);
            _result = result;
            _colCompare = string.Format("{0}_{1}", _col, _result.Unit.Scenario.ModelType);
        }

        private DataTable _table = null;

        public DataTable Table
        {
            get
            {
                read();
                return _table;
            }
        }
        public SWATUnitResult UnitResult { get { return _result; } }
        public string Column { get { return _col; } }
        public string ColumnCompare { get { return _colCompare; } }
        public int Year { get { return _year; } }

        public Statistics Statistics { get { if (_stat == null) _stat = new Statistics(Table, _col); return _stat; } }

        #region Compare Table

        private SWATUnitColumnYearCompareResult Compare(Scenario scenario, SWATModelType modelType)
        {
            string tableID = string.Format("{0}_{1}", scenario.Name, modelType);
            if (!_compares.ContainsKey(tableID))
                  _compares.Add(tableID,new SWATUnitColumnYearCompareResult(this, this.getCompareResult(scenario,modelType)));

            return _compares[tableID];      
        }

        public SWATUnitColumnYearCompareResult Compare(ScenarioResult result)
        {
            return Compare(result.Scenario, result.ModelType);
        }

        public SWATUnitColumnYearCompareResult Compare(string scenarioName)
        {
            if (_result.Unit.Scenario.Scenario.Name.Equals(scenarioName)) throw new Exception("Scenario " + scenarioName + " is same with current scenario.");

            Scenario scenario = _result.Unit.Scenario.Scenario.Project.getScenario(scenarioName);
            if (scenario == null) throw new Exception("Can't find scenario " + scenarioName + " in current project.");

            return Compare(scenario, _result.Unit.Scenario.ModelType);
        }

        public SWATUnitColumnYearCompareResult Compare(SWATModelType modelType)
        {
            if (modelType == SWATModelType.UNKNOWN) throw new Exception("Unknown model type!");
            if (_result.Unit.Scenario.ModelType == modelType)
                throw new Exception("Compared model type is same with current scenario.");

            return Compare(_result.Unit.Scenario.Scenario, modelType);
        }

        public DataTable getComparedCombineTable(string scenarioName)
        {
            if (_result.Unit.Scenario.Scenario.Name.Equals(scenarioName)) throw new Exception("Scenario " + scenarioName + " is same with current scenario.");

            Scenario scenario = _result.Unit.Scenario.Scenario.Project.getScenario(scenarioName);
            if (scenario == null) throw new Exception("Can't find scenario " + scenarioName + " in current project.");

            return getComparedCombineTable(scenario, _result.Unit.Scenario.ModelType);
        }

        public DataTable getComparedCombineTable(SWATModelType modelType)
        {
            if (modelType == SWATModelType.UNKNOWN) throw new Exception("Unknown model type!");
            if (_result.Unit.Scenario.ModelType == modelType)
                throw new Exception("Compared model type is same with current scenario.");

            return getComparedCombineTable(_result.Unit.Scenario.Scenario, modelType);
        }

        private DataTable getComparedCombineTable(Scenario scenario, SWATModelType modelType)
        {
            string tableID = string.Format("{0}_{1}", scenario.Name, modelType);
            if (!_compareCombineTable.ContainsKey(tableID))
            {
                if (Table.Rows.Count == 0)
                    throw new Exception("There is no result.");

                DataTable comparaTable = getCompareTable(scenario, modelType);
                if (comparaTable.Rows.Count != Table.Rows.Count)
                    throw new Exception("The number of results are different.");

                DataTable dt = Table.Copy();
                dt.Columns[_colIndex].ColumnName =
                    string.Format("{0}_{1}", _col, _result.Unit.Scenario.ModelType);
                
                dt.Columns.Add(string.Format("{0}_{1}", _col, modelType), typeof(double));
                int newColIndex = dt.Columns.Count - 1;
                int colIndex2 = comparaTable.Columns.IndexOf(_col);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ArcSWAT.RowItem item = new ArcSWAT.RowItem(comparaTable.Rows[i]);
                    dt.Rows[i][newColIndex] = item.getColumnValue_Double(colIndex2);
                }
                _compareCombineTable.Add(tableID, dt);
            }
            return _compareCombineTable[tableID];            
        }

        /// <summary>
        /// get compare data table for given scenario with same model type
        /// </summary>
        /// <param name="scenario"></param>
        /// <returns></returns>
        /// <remarks>To compare between scenarios</remarks>
        public DataTable getCompareTable(string scenarioName)
        {
            if (_result.Unit.Scenario.Scenario.Name.Equals(scenarioName)) throw new Exception("Scenario " + scenarioName + " is same with current scenario.");

            Scenario scenario = _result.Unit.Scenario.Scenario.Project.getScenario(scenarioName);
            if (scenario == null) throw new Exception("Can't find scenario " + scenarioName + " in current project.");

            return getCompareTable(scenario, _result.Unit.Scenario.ModelType);
        }

        /// <summary>
        /// get compare data table for given model type within same scenario
        /// </summary>
        /// <param name="modelType"></param>
        /// <returns></returns>
        /// <remarks>To compare between models</remarks>
        public DataTable getCompareTable(SWATModelType modelType)
        {
            if (modelType == SWATModelType.UNKNOWN) throw new Exception("Unknown model type!");
            if (_result.Unit.Scenario.ModelType == modelType)
                throw new Exception("Compared model type is same with current scenario.");

            return getCompareTable(_result.Unit.Scenario.Scenario, modelType);
        }

        /// <summary>
        /// get compare table for given scenario and model type 
        /// </summary>
        /// <param name="scenario"></param>
        /// <param name="modelType"></param>
        /// <returns></returns>
        private SWATUnitColumnYearResult getCompareResult(Scenario scenario, SWATModelType modelType)
        {
            //get scenario result
            ScenarioResult compareResult = scenario.getModelResult(modelType);
            if (compareResult == null) throw new Exception("Can't find model " + modelType.ToString() + " in scenario " + scenario.Name);
            if (compareResult.Status == ScenarioResultStatus.UNKNOWN)
                throw new Exception("The status of model " + modelType.ToString() + " in scenario " + scenario.Name + " is unknown.");
            if (compareResult.Status == ScenarioResultStatus.UNSUCCESS)
                throw new Exception("The simulation of " + modelType.ToString() + " in scenario " + scenario.Name + " is not successful. Please check the model first.");
            if (compareResult.Status == ScenarioResultStatus.NO_EXIST)
                throw new Exception("The simulation result of " + modelType.ToString() + " in scenario " + scenario.Name + " doesn't exist. Please run the model first.");


            //get unit
            SWATUnit unit = compareResult.getSWATUnit(_result.Unit.Type, _result.Unit.ID);
            if (unit == null)
                throw new Exception("Can't find " + _result.Unit.Type + " " + _result.Unit.ID.ToString() + " in scenario " + scenario.Name + ",model " + modelType.ToString());

            SWATUnitResult unitResult = unit.getResult(_result.Name);
            if (unitResult == null)
                throw new Exception("Can't find result  " + _result.Name + " for " + _result.Unit.Type + " " + _result.Unit.ID.ToString() + " in scenario " + scenario.Name + ",model " + modelType.ToString());
            if (unitResult.Interval != _result.Interval)
                throw new Exception("The interval for " + _result.Name + " for " + _result.Unit.Type + " " + _result.Unit.ID.ToString() + " in scenario " + scenario.Name + ",model " + modelType.ToString() + " is different from current result.");

            return unitResult.getResult(_col, _year);
        }

        /// <summary>
        /// get compare table for given scenario and model type 
        /// </summary>
        /// <param name="scenario"></param>
        /// <param name="modelType"></param>
        /// <returns></returns>
        private DataTable getCompareTable(Scenario scenario, SWATModelType modelType)
        {
            return getCompareResult(scenario, modelType).Table;
        }


        #endregion


        public string ID { get { return _id; } }

        private void read()
        {
            if (_table != null) return;

            //check the column name
            if (!_result.Columns.Contains(_col)) _table = new DataTable();

            //only use year parameter for daily
            int year = _year;
            if (_result.Interval != SWATResultIntervalType.DAILY) year = -1;

            //get return columns based on interval
            string cols = ScenarioResultStructure.getDateColumns(_result.Interval);
            if (cols.Length > 0) cols += ",";
            cols += _col;

            //get year condition
            string yearCondition = "";
            if (year >= _result.Unit.Scenario.StartYear && year <= _result.Unit.Scenario.EndYear)
                yearCondition = string.Format("{0}={1}", ScenarioResultStructure.COLUMN_NAME_YEAR, year);

            //get id condition
            string idCondition = "";
            if (_result.Unit.Type != SWATUnitType.WSHD)
                idCondition = string.Format("{0}={1}", _result.Unit.Type.ToString(), _result.Unit.ID);

            string condition = idCondition;
            if (condition.Length > 0 && yearCondition.Length > 0)
                condition += " and " + yearCondition;
            if (condition.Length == 0 && yearCondition.Length > 0)
                condition = yearCondition;

            if (condition.Length > 0) condition = " where " + condition;

            DataTable dt = _result.Unit.Scenario.GetDataTable(
                string.Format("select {2} from {0} {1}",
                _result.Name, condition, cols));
            _colIndex = dt.Columns.IndexOf(_col);

            //add datetime column and calculate the date
            if (dt.Rows.Count > 0 && _result.Interval != SWATResultIntervalType.UNKNOWN)
            {
                dt.Columns.Add(SWATUnitResult.COLUMN_NAME_DATE, typeof(DateTime));
                _dateIndex = dt.Columns.Count - 1;
                foreach (DataRow r in dt.Rows)
                    calculateDate(r);
            }

            _table = dt;
        }

        private void calculateDate(DataRow r)
        {
            DateTime d = DateTime.Now;
            RowItem item = new RowItem(r);
            int year = item.getColumnValue_Int(ScenarioResultStructure.COLUMN_NAME_YEAR);
            int month = 1;
            int day = 1;
            if (_result.Interval == SWATResultIntervalType.MONTHLY || _result.Interval == SWATResultIntervalType.DAILY)
                month = item.getColumnValue_Int(ScenarioResultStructure.COLUMN_NAME_MONTH);
            if (_result.Interval == SWATResultIntervalType.DAILY)
                day = item.getColumnValue_Int(ScenarioResultStructure.COLUMN_NAME_DAY);

            r[SWATUnitResult.COLUMN_NAME_DATE] = new DateTime(year, month, day);
        }

        public static string getUniqueResultID(string col, int year)
        {
            string combineCol = col.Trim();
            if (year > 0) combineCol += "_" + year.ToString();
            return combineCol;
        }
    }
}
