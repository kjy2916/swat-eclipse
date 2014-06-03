using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SWAT_SQLite_Result.ArcSWAT
{
    /// <summary>
    /// Corresponding to one specific scenario folder
    /// </summary>
    public class Scenario : FolderBase
    {
        private static string DEFAULT_TXTINOUT_NAME = @"\TxtInOut";

        public Scenario(string f, Project prj)
            : base(f)
        {
            if (IsValid)
            {
                _prj = prj;
                _modelfolder = Folder + DEFAULT_TXTINOUT_NAME;
                if (!Directory.Exists(_modelfolder))
                {
                    _modelfolder = null;
                    _isValid = false;
                    _error = _modelfolder + " doesn't exist!";
                    return;
                }
                _name = (new DirectoryInfo(Folder)).Name;

                //Regular SWAT and CanSWAT could run one a same model 
                _hasResults = false;
                for (int i = Convert.ToInt32(ArcSWAT.SWATModelType.SWAT_488); i <= Convert.ToInt32(ArcSWAT.SWATModelType.CanSWAT); i++)
                {
                    SWATModelType modelType = (SWATModelType)i;
                    ScenarioResult result = new ScenarioResult(
                        _modelfolder + @"\" + ScenarioResultStructure.getDatabaseName(modelType), 
                        this,modelType);
                    if(result.Status == ScenarioResultStatus.NORMAL) _hasResults = true;
                    _results.Add(modelType,result);
                }
            }
        }

        public override string ToString()
        {
            if(!IsValid) return "";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("Model Folder : {0}", _modelfolder));
            //sb.AppendLine("***\nNormal Result\n***");
            //sb.AppendLine(_result_normal.ToString());
            //sb.AppendLine("***\nCanSWAT Result\n***");
            //sb.AppendLine(_result_canswat.ToString());
            return sb.ToString();
        }

        private Project _prj = null;
        private string _name = null;
        private string _modelfolder = null;
        private Dictionary<SWATModelType, ScenarioResult> _results = new Dictionary<SWATModelType, ScenarioResult>();
        private bool _hasResults = false;

        public bool hasResults{get{return _hasResults;}}
        public string Name { get { return _name; } }
        public string ModelFolder { get { return _modelfolder; } }
        public Project Project { get { return _prj; } }
        public ScenarioResult getModelResult(SWATModelType modelType)
        {
            if (_results.ContainsKey(modelType)) 
                return _results[modelType];
            return null;
        }

        /// <summary>
        /// Re-read results when it's simulated again.
        /// </summary>
        /// <param name="modelType"></param>
        public void reReadResults(SWATModelType modelType)
        {
            ScenarioResult result = getModelResult(modelType);
            if (result != null)
                _results[modelType] = 
                    new ScenarioResult(
                        _modelfolder + @"\" + ScenarioResultStructure.getDatabaseName(modelType), this,modelType);
        }

        public static Dictionary<string, Scenario> FromProjectFolder(string f,Project prj)
        {
            Dictionary<string, Scenario> scenarios = new Dictionary<string, Scenario>();

            if (!Directory.Exists(f)) return scenarios;

            DirectoryInfo dir = new DirectoryInfo(f);
            DirectoryInfo[] subdirs = dir.GetDirectories();
            foreach (DirectoryInfo info in subdirs)
            {
                Scenario s = new Scenario(info.FullName,prj);
                if (s.IsValid) scenarios.Add(s.Name,s);
            }
            return scenarios;
        }

        
    }
}
