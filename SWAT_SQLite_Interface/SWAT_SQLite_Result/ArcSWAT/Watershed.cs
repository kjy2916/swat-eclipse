using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace SWAT_SQLite_Result.ArcSWAT
{
    public class Watershed : SWATUnit
    {
        public Watershed(ScenarioResult scenario)
            : base(null, scenario)
        {

        }

        public override string BasicInfoTableName
        {
            get { return ""; }
        }

        public override SWATUnitType Type
        {
            get { return SWATUnitType.WSHD; }
        }
    }
}
