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
    class Scenario : FolderBase
    {
        private static string DEFAULT_TXTINOUT_NAME = @"\TxtInOut";

        public Scenario(string f)
            : base(f)
        {
            if (IsValid)
            {
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
                _result_normal = new ScenarioResult(_modelfolder + @"\" + ScenarioResultStructure.DATABASE_NAME_NORMAL);
                _result_canswat = new ScenarioResult(_modelfolder + @"\" + ScenarioResultStructure.DATABASE_NAME_CANSWAT);
            }
        }

        public override string ToString()
        {
            if(!IsValid) return "";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("Model Folder : {0}", _modelfolder));
            sb.AppendLine("***\nNormal Result\n***");
            sb.AppendLine(_result_normal.ToString());
            sb.AppendLine("***\nCanSWAT Result\n***");
            sb.AppendLine(_result_canswat.ToString());
            return sb.ToString();
        }

        private string _name = null;
        private string _modelfolder = null;
        private ScenarioResult _result_normal = null;
        private ScenarioResult _result_canswat = null;

        public ScenarioResult ResultNormal { get { return _result_normal; } }
        public ScenarioResult ResultCanSWAT { get { return _result_canswat; } }
        public string Name { get { return _name; } } 

        public static Dictionary<string, Scenario> FromProjectFolder(string f)
        {
            Dictionary<string, Scenario> scenarios = new Dictionary<string, Scenario>();

            if (!Directory.Exists(f)) return scenarios;

            DirectoryInfo dir = new DirectoryInfo(f);
            DirectoryInfo[] subdirs = dir.GetDirectories();
            foreach (DirectoryInfo info in subdirs)
            {
                Scenario s = new Scenario(info.FullName);
                if (s.IsValid) scenarios.Add(s.Name,s);
            }
            return scenarios;
        }

        
    }
}
