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
        public Spatial(string f)
            : base(f)
        {
        }

        public override string ToString()
        {
            return Folder;
        }
    }
}
