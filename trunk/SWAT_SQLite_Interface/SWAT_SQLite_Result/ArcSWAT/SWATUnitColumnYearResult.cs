using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SWAT_SQLite_Result.ArcSWAT
{
    public class SWATUnitColumnYearResult : ColumnYearData
    {
        
        private SWATUnitResult _result = null;
        private Dictionary<string, SWATUnitColumnYearCompareResult> _compares = new Dictionary<string, SWATUnitColumnYearCompareResult>();

        public SWATUnitColumnYearResult(string col, int year, SWATUnitResult result) : base(col,year)
        {            
            _result = result;
            _colCompare = string.Format("{0}_{1}", _col, _result.Unit.Scenario.ModelType);
        }

        public SWATUnitResult UnitResult { get { return _result; } }

        #region Compare with Observed Data


        private SWATUnitColumnYearCompareResult _compareWithObserved = null;

        public SWATUnitColumnYearCompareResult CompareWithObserved
        {
            get
            {
                if (ObservedData == null) return null;
                if (_compareWithObserved == null)
                    _compareWithObserved = new SWATUnitColumnYearCompareResult(this);
                return _compareWithObserved;
            }
        }

        /// <summary>
        /// Corresponding observed data
        /// </summary>
        public SWATUnitColumnYearObservationData ObservedData{get{return _result.Unit.Scenario.Scenario.Project.Observation.getObservedData(this);}}
        
        #endregion

        #region Compare With Other Scenario or Model

        private SWATUnitColumnYearCompareResult Compare(Scenario scenario, SWATModelType modelType)
        {
            string tableID = string.Format("{0}_{1}", scenario.Name, modelType);
            if (!_compares.ContainsKey(tableID))
                  _compares.Add(tableID,
                      new SWATUnitColumnYearCompareResult(this, this.getCompareResult(scenario,modelType)));

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

        #endregion

        protected override void read()
        {
            if (_table != null) return;

            //check the column name
            if (!_result.Columns.Contains(_col)) _table = new DataTable();

            //add codes to output reading time for test
            DateTime readStartTime = DateTime.Now;

            //only use year parameter for daily
            int year = _year;
            if (_result.Interval != SWATResultIntervalType.DAILY &&
                _result.Interval != SWATResultIntervalType.MONTHLY) year = -1;

            //get return columns based on interval
            string cols = ScenarioResultStructure.getDateColumns(_result.Interval);
            if (cols.Length > 0) cols += ",";
            cols += _col;
            if (HasLanduseColumn)
                cols += "," + ScenarioResultStructure.COLUMN_NAME_HRU_LANDUSE;
            if(HasMgtOperationColumn)
                cols += "," + ScenarioResultStructure.COLUMN_NAME_HRU_MGT_OPERATION + "," + 
                    ScenarioResultStructure.COLUMN_NAME_HRU_MGT_LANDUSE;

            //get year condition
            string yearCondition = "";
            if (year >= _result.Unit.Scenario.StartYear && year <= _result.Unit.Scenario.EndYear)
                yearCondition = string.Format("({0}={1} or {0}={2})", ScenarioResultStructure.COLUMN_NAME_YEAR, year-1,year); //read two years of data to consider hydrological year

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

            //output reading time for test
            System.Diagnostics.Debug.WriteLine(string.Format("Reading {0}_{1}_{2}: {3} ms",
                                UnitResult.Unit.Type,Column,Year, DateTime.Now.Subtract(readStartTime).TotalMilliseconds));

            //add datetime column and calculate the date
            if (dt.Rows.Count > 0 && _result.Interval != SWATResultIntervalType.UNKNOWN)
            {
                dt.Columns.Add(SWATUnitResult.COLUMN_NAME_DATE, typeof(DateTime));
                foreach (DataRow r in dt.Rows)
                    calculateDate(r);                                
            }

            _table = dt;
        }

        /// <summary>
        /// If has LULC column
        /// </summary>
        public bool HasLanduseColumn
        {
            get
            {
                return _result.Unit.Type == SWATUnitType.HRU && _result.Name.Equals(ScenarioResultStructure.TABLE_NAME_HRU);
            }
        }

        /// <summary>
        /// If has mgt operation column
        /// </summary>
        public bool HasMgtOperationColumn
        {
            get
            {
                return _result.Unit.Type == SWATUnitType.HRU && _result.Name.Equals(ScenarioResultStructure.TABLE_NAME_HRU_MGT);
            }
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
    }
}
