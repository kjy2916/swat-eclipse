using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace SWAT_SQLite_Result.ArcSWAT
{
    class Watershed : SWATUnit
    {
        public Watershed(ScenarioResult scenario)
            : base(null, scenario)
        {

        }

        public override string BasicInfoTableName
        {
            get { return ""; }
        }

        public override System.Collections.Specialized.StringCollection ResultTableNames
        {
            get
            {
                return new System.Collections.Specialized.StringCollection()
            {ScenarioResult.TABLE_NAME_WATERSHED_DAILY,
            ScenarioResult.TABLE_NAME_WATERSHED_MONTHLY,
            ScenarioResult.TABLE_NAME_WATERSHED_YEARLY};
            }
        }

        public override SWATUnitType Type
        {
            get { return SWATUnitType.WSHD; }
        }
    }
}
