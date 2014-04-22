using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SWAT_SQLite_Result.ArcSWAT
{
    class Reservoir : SWATUnit
    {
        public Reservoir(DataRow resInfoRow, ScenarioResult scenario)
            : base(resInfoRow, scenario)
        {
            RowItem item = new RowItem(resInfoRow);
            _id = item.getColumnValue_Int(ScenarioResult.COLUMN_NAME_RES); 
        }

        public override string BasicInfoTableName
        {
            get { return ScenarioResult.INFO_TABLE_NAME_RSV; }
        }

        public override System.Collections.Specialized.StringCollection ResultTableNames
        {
            get { return new System.Collections.Specialized.StringCollection() { ScenarioResult.TABLE_NAME_RESERVOIR }; }
        }

        public override SWATUnitType Type
        {
            get { return SWATUnitType.RES; }
        }
    }
}
