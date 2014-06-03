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
        protected Dictionary<string, SWATUnitResult> _results = new Dictionary<string, SWATUnitResult>();

        public SWATUnit(DataRow unitInfoRow, ScenarioResult scenario)
        {
            _scenario = scenario;
            loadResults(); //don't load all datas
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

        public Dictionary<string, SWATUnitResult> Results { get { return _results; } }

        public SWATUnitResult getResult(string tableName)
        {
            tableName = tableName.ToLower();
            if (Results.ContainsKey(tableName)) return Results[tableName];
            return null;
        }

        private void loadResults()
        {            
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

            if(_scenario.Structure.isTableHasData(tableName))
                _results.Add(tableName, new SWATUnitResult(tableName, this));
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
