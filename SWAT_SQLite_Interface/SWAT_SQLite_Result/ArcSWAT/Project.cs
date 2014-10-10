using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace SWAT_SQLite_Result.ArcSWAT
{
    /// <summary>
    /// Similar with class Project in CanSWAT interface
    /// As this would suppose to be a separate program and doesn't utilize any 
    /// ESRI functions, class Project is modified.
    /// </summary>
    public class Project : FolderBase
    {
        private static string DEFAULT_WATERSHED_FOLDER = @"\Watershed";
        private static string DEFAULT_SCENARIOS_FOLDER = @"\Scenarios";
        private static string DEFAULT_OBSERVATION_DATA_FILE_DAILY = DEFAULT_SCENARIOS_FOLDER + @"\observation_daily.db3";
        private static string DEFAULT_OBSERVATION_DATA_FILE_MONTHLY = DEFAULT_SCENARIOS_FOLDER + @"\observation_monthly.db3";
        private static string DEFAULT_OBSERVATION_DATA_FILE_YEARLY = DEFAULT_SCENARIOS_FOLDER + @"\observation_yearly.db3";

        private Dictionary<string, Scenario> _scenarios = null;
        private Spatial _spatial = null;
        private ObservationData _observation_daily = null;
        private ObservationData _observation_monthly = null;
        private ObservationData _observation_yearly = null;

        public Project(string prj) : base(prj)
        {
            Debug.WriteLine("Information: Intialize ArcSWAT project using " + prj);

            if (!IsValid) return;

            _spatial = new Spatial(Folder + DEFAULT_WATERSHED_FOLDER);
            if (!_spatial.IsValid) { _isValid = false; _error = _spatial.Error; return; }

            _scenarios = Scenario.FromProjectFolder(Folder + DEFAULT_SCENARIOS_FOLDER,this);
            if (_scenarios.Count == 0) { _isValid = false; _error = "No Scenarios found!"; return; }

            _observation_daily = new ObservationData(Folder + DEFAULT_OBSERVATION_DATA_FILE_DAILY);
            _observation_monthly = new ObservationData(Folder + DEFAULT_OBSERVATION_DATA_FILE_MONTHLY);
            _observation_yearly = new ObservationData(Folder + DEFAULT_OBSERVATION_DATA_FILE_YEARLY);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Folder);
            if (!IsValid) sb.AppendLine(Error);
            else
            {
                sb.AppendLine(_spatial.ToString());
                sb.AppendLine(string.Format("{0} scenarios", _scenarios.Count));
                foreach (string s in _scenarios.Keys)
                {
                    sb.AppendLine("-----------------------------------------");
                    sb.AppendLine(string.Format("Scenario : {0}", s));
                    sb.AppendLine(_scenarios[s].ToString());
                }
            }
            return sb.ToString();            
        }

        public Dictionary<string, Scenario> Scenarios
        {
            get { return _scenarios; }
        }

        public Scenario getScenario(string scenarioName)
        {
            if (_scenarios.ContainsKey(scenarioName)) return _scenarios[scenarioName];
            return null;
        }

        public Spatial Spatial { get { return _spatial; } }

        public ObservationData Observation(SWATResultIntervalType interval) 
        {
            switch (interval)
            {
                case SWATResultIntervalType.DAILY: return _observation_daily;
                case SWATResultIntervalType.MONTHLY: return _observation_monthly;
                case SWATResultIntervalType.YEARLY: return _observation_yearly;
                default: return null;
            }
        }
    }
}
