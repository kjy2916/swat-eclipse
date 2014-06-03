using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWAT_SQLite_Result.ArcSWAT
{
    /// <summary>
    /// Corresponding to the watershed folder
    /// Used to get access to shapefiles
    /// </summary>
    public class Spatial : FolderBase
    {
        private static string DEFAULT_SUBBASIN_PATH = @"\Shapes\subs1.shp";

        //The monitoring_points1.shp in shapes folder doesn't include reservoirs. 
        //monitoring_points2.shp is exported from GeoDatabase.
        private static string DEFAULT_MONITORING_POINTS_PATH = @"\Shapes\monitoring_points2.shp"; 
        private static string DEFAULT_REACH_PATH = @"\Shapes\riv1.shp";

        private string _subbasinShapefile = null;
        private string _monitoringShapefile = null;
        private string _reachShapefile = null;

        public Spatial(string f)
            : base(f)
        {
            _subbasinShapefile = f + DEFAULT_SUBBASIN_PATH;
            if (!System.IO.File.Exists(_subbasinShapefile))
                _subbasinShapefile = null;

            _monitoringShapefile = f + DEFAULT_MONITORING_POINTS_PATH;
            if (!System.IO.File.Exists(_monitoringShapefile))
                _monitoringShapefile = null;

            _reachShapefile = f + DEFAULT_REACH_PATH;
            if (!System.IO.File.Exists(_reachShapefile))
                _reachShapefile = null;
        }

        public override string ToString()
        {
            return Folder;
        }

        public string SubbasinShapefile { get { return _subbasinShapefile; } }
        public string MonitoringShapefile { get { return _monitoringShapefile; } }
        public string ReachShapefile { get { return _reachShapefile; } }
    }
}
