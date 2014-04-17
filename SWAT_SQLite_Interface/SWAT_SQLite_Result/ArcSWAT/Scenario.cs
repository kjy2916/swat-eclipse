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
            }
        }

        public override string ToString()
        {
            return Folder;
        }

        private string _name = null;
        private string _modelfolder = null;
        private string _resultDatabase = null;



        public string Name
        {
            get { return _name; }
        }

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
