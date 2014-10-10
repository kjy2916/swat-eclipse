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
                    for(int j = Convert.ToInt32(SWATResultIntervalType.MONTHLY);j<= Convert.ToInt32(SWATResultIntervalType.YEARLY);j++)
                    {
                        SWATResultIntervalType interval = (SWATResultIntervalType)j;
                        ScenarioResult result = new ScenarioResult(
                            _modelfolder + @"\" + ScenarioResultStructure.getDatabaseName(modelType,interval), 
                            this,modelType,interval);
                        if(result.Status == ScenarioResultStatus.NORMAL) _hasResults = true;
                        _results.Add(getResultID(modelType,interval),result);
                    }
                }
            }
        }

        public string getResultStatus(SWATModelType modelType)
        {
            StringBuilder status = new StringBuilder();
            for (int j = Convert.ToInt32(SWATResultIntervalType.MONTHLY); j <= Convert.ToInt32(SWATResultIntervalType.YEARLY); j++)
            {
                SWATResultIntervalType interval = (SWATResultIntervalType)j;
                ScenarioResult result = getModelResult(modelType, interval);
                if(result == null) continue;

                if(status.Length > 0) status.Append(";");
                status.Append(interval);
                status.Append(":");
                if (result.Status != ScenarioResultStatus.NORMAL)
                    status.Append(result.Status);
                else
                    status.Append(string.Format("{0:yyyy-MM-dd hh:mm:ss}", result.SimulationTime));
            }
            return status.ToString();
        }

        private string getResultID(SWATModelType modelType, SWATResultIntervalType interval)
        {
            return modelType.ToString() + "_" + interval.ToString();
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
        private Dictionary<string, ScenarioResult> _results = new Dictionary<string, ScenarioResult>();
        private bool _hasResults = false;

        public bool hasResults{get{return _hasResults;}}
        public string Name { get { return _name; } }
        public string ModelFolder { get { return _modelfolder; } }
        public Project Project { get { return _prj; } }
        public ScenarioResult getModelResult(SWATModelType modelType, SWATResultIntervalType interval)
        {
            string id = getResultID(modelType, interval);
            if (_results.ContainsKey(id)) 
                return _results[id];
            return null;
        }

        /// <summary>
        /// Re-read results when it's simulated again.
        /// </summary>
        /// <param name="modelType"></param>
        public void reReadResults(SWATModelType modelType,SWATResultIntervalType interval)
        {
            ScenarioResult result = getModelResult(modelType,interval);
            if (result != null)
                _results[getResultID(modelType, interval)] = 
                    new ScenarioResult(
                        _modelfolder + @"\" + ScenarioResultStructure.getDatabaseName(modelType,interval), this,modelType,interval);
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

        /// <summary>
        /// Modify output interval in file.cio to run model in different mode 
        /// </summary>
        /// <param name="interval"></param>
        public void modifyOutputInterval(SWATResultIntervalType interval)
        {
            if(interval == SWATResultIntervalType.UNKNOWN) return;

            //find file.cio
            string cioFile = _modelfolder + @"\file.cio";
            if (!System.IO.File.Exists(cioFile))
                throw new Exception("Couldn't find " + cioFile);

            //modify file.cio with given output interval, which is located in line 59
            string cio = null;
            using (System.IO.StreamReader reader = new StreamReader(cioFile))
            {
                cio = reader.ReadToEnd();
            }
            using (System.IO.StreamWriter writer = new StreamWriter(cioFile))
            {
                using (System.IO.StringReader reader = new StringReader(cio))
                {
                    string oneline = reader.ReadLine();
                    while (oneline != null)
                    {
                        if (oneline.Contains("IPRINT"))
                            oneline = string.Format("{0}    | IPRINT: print code (month, day, year)",Convert.ToInt32(interval).ToString().PadLeft(16));
                        writer.WriteLine(oneline);
                        oneline = reader.ReadLine();
                    }
                }
            }
        }        
    }
}
