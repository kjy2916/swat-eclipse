using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Specialized;

namespace SWAT_SQLite_Result.ArcSWAT
{
    /// <summary>
    /// The base class for a unit in SWAT, which could be HRU, Subbasin, Reach, Reservoir or Watershed.
    /// One unit could have different outputs/results.
    /// </summary>
    public abstract class SWATUnit
    {
        protected int _id = ScenarioResultStructure.UNKONWN_ID;
        protected ScenarioResult _scenario = null;
        protected Dictionary<string, SWATUnitResult> _results = null;

        public SWATUnit(DataRow unitInfoRow, ScenarioResult scenario)
        {
            _scenario = scenario;
        }

        /// <summary>
        /// ID
        /// </summary>
        public int ID { get { return _id; } }

        /// <summary>
        /// SWAT Unit Type`
        /// </summary>
        public abstract SWATUnitType Type { get; }

        /// <summary>
        /// If the outputs are in multi tables
        /// </summary>
        public abstract bool UseMultiOutputTable { get; }

        /// <summary>
        /// The format string to construct the name of output table using unit id
        /// </summary>
        public abstract string OutputTableFormatString { get; }

        /// <summary>
        /// get table names
        /// </summary>
        /// <param name="normalTableName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private string getTableName(string normalTableName)
        {
            if (UseMultiOutputTable && OutputTableFormatString.Length > 0)
            {
                if (_id == ScenarioResultStructure.UNKONWN_ID)
                    throw new Exception("Unset unit id!");
                return string.Format(OutputTableFormatString, normalTableName, ID);
            }
            return normalTableName;
        }

        /// <summary>
        /// Parent scenario
        /// </summary>
        public ScenarioResult Scenario { get { return _scenario; } }

        /// <summary>
        /// The table name for basic info
        /// </summary>
        public abstract string BasicInfoTableName{get;}

        public virtual string ToStringBasicInfo() { return ""; }

        /// <summary>
        /// Names of all result tables corresponding to this SWAT unit
        /// </summary>
        public string[] ResultTableNames { get { return ScenarioResultStructure.getResultTableNames(Type); } }

        public Dictionary<string, SWATUnitResult> Results 
        { 
            get 
            {
                if (_results == null)
                {
                    _results = new Dictionary<string, SWATUnitResult>();
                    loadResults();
                }
                return _results; 
            } 
        }

        public SWATUnitResult getResult(string tableName)
        {
            tableName = tableName.ToLower();
            if (Results.ContainsKey(tableName)) return Results[tableName];
            return null;
        }

        /// <summary>
        /// Needs to be called after the id is retrieved
        /// </summary>
        private void loadResults()
        {
            _results.Clear();
            foreach (string t in ResultTableNames)
                loadResults(t);
        }

        /// <summary>
        /// Need to move to scenario result structure 
        /// </summary>
        /// <param name="tableName"></param>
        private void loadResults(string tableName)
        {
            tableName = tableName.ToLower();
            if (_results.ContainsKey(tableName)) return;

            string multiTableName = getTableName(tableName);
            if (_scenario.Structure.isTableHasData(multiTableName))
                _results.Add(tableName, new SWATUnitResult(multiTableName, this));
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("{0} : {1}", Type,ID));
            sb.AppendLine(ToStringBasicInfo());
            sb.AppendLine("Results");
            foreach (string s in _results.Keys)
                sb.AppendLine(s);

            return sb.ToString();
        }
    }
}
