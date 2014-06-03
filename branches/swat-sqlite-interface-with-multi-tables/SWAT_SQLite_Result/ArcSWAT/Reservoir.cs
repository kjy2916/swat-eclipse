using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SWAT_SQLite_Result.ArcSWAT
{
    public class Reservoir : SWATUnit
    {
        public Reservoir(DataRow resInfoRow, ScenarioResult scenario)
            : base(resInfoRow, scenario)
        {
            RowItem item = new RowItem(resInfoRow);
            _id = item.getColumnValue_Int(ScenarioResultStructure.COLUMN_NAME_RES); 
        }

        public override string BasicInfoTableName
        {
            get { return ScenarioResultStructure.INFO_TABLE_NAME_RSV; }
        }

        public override SWATUnitType Type
        {
            get { return SWATUnitType.RES; }
        }

        public override bool UseMultiOutputTable
        {
            get { return false; }
        }

        public override string OutputTableFormatString
        {
            get { return ScenarioResultStructure.TABLE_NAME_FORMAT_RSV; }
        }
    }
}
