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

        private DataTable _aveAnnualBasinTbl = null;

        public DataTable AverageAnnualBasinTable
        {
            get
            {
                if (_aveAnnualBasinTbl == null)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach(string n in ScenarioResultStructure.STATUS_NAMES)
                    {
                        if (sb.Length > 0) sb.Append(" and ");
                        sb.Append(string.Format("{0} not like '{1}'", 
                            ScenarioResultStructure.COLUMN_NAME_AVE_ANNUAL_BASIN_NAME,n));
                    }
                    
                    _aveAnnualBasinTbl = Query.GetDataTable(
                        "select * from " + ScenarioResultStructure.TABLE_NAME_WATERSHED_AVERAGE_ANNUAL + " where " + sb.ToString(), 
                        Scenario.DatabasePath);
                }
                return _aveAnnualBasinTbl;
            }
        }
    }
}
