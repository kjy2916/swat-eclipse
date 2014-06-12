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
        #region SWAT Input File Extension for Different Unit
        private static string SWAT_EXTENSION_HRU = "hru";
        private static string SWAT_EXTENSION_HRU_MGT = "mgt";
        private static string SWAT_EXTENSION_HRU_SOL = "sol";
        private static string SWAT_EXTENSION_HRU_CHM = "chm";
        private static string SWAT_EXTENSION_HRU_GW = "gw";
        private static string SWAT_EXTENSION_HRU_SEP = "sep";

        private static string SWAT_EXTENSION_SUB = "sub";
        private static string SWAT_EXTENSION_SUB_PND = "pnd";
        private static string SWAT_EXTENSION_SUB_WUS = "wus";

        private static string SWAT_EXTENSION_RCH = "rte";

        private static string SWAT_EXTENSION_RES = "res";

        private static string[] SWAT_UNIT_EXTENSIONS_HRU = new string[] 
            {SWAT_EXTENSION_HRU,
            SWAT_EXTENSION_HRU_MGT,
            SWAT_EXTENSION_HRU_SOL,
            SWAT_EXTENSION_HRU_CHM,
            SWAT_EXTENSION_HRU_GW,
            SWAT_EXTENSION_HRU_SEP};

        private static string[] SWAT_UNIT_EXTENSIONS_SUB = new string[] 
            {SWAT_EXTENSION_SUB,
            SWAT_EXTENSION_SUB_PND,
            SWAT_EXTENSION_SUB_WUS};

        private static string[] SWAT_UNIT_EXTENSIONS_RCH = new string[] { SWAT_EXTENSION_RCH };

        private static string[] SWAT_UNIT_EXTENSIONS_RES = new string[] { SWAT_EXTENSION_RES };

        public static string[] getSWATFileExtentions(SWATUnitType type)
        {
            switch (type)
            {
                case SWATUnitType.HRU: return SWAT_UNIT_EXTENSIONS_HRU;
                case SWATUnitType.SUB: return SWAT_UNIT_EXTENSIONS_SUB;
                case SWATUnitType.RCH: return SWAT_UNIT_EXTENSIONS_RCH;
                case SWATUnitType.RES: return SWAT_UNIT_EXTENSIONS_RES;
                default: return new string[]{};
            }
        }
        #endregion

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

        private void loadResults()
        {
            if (_results == null) _results = new Dictionary<string, SWATUnitResult>();
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

        /// <summary>
        /// File name for the unit
        /// </summary>
        public virtual string FileName
        {
            get
            {
                return string.Format("{0:00000}0000",ID);
            }
        }

        /// <summary>
        /// Get the input file name based on given extension
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public string getInputFileName(string extension)
        {
            return _scenario.Scenario.ModelFolder + @"\" + FileName + "." + extension;            
        }
    }
}
