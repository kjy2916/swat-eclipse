using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SWAT_SQLite_Result.ArcSWAT
{
    public class Reach : SWATUnit
    {
        public Reach(DataRow rchInfoRow, ScenarioResult scenario)
            : base(rchInfoRow, scenario)

        {
            RowItem item = new RowItem(rchInfoRow);
            _id = item.getColumnValue_Int(ScenarioResultStructure.COLUMN_NAME_RCH);
            _area = item.getColumnValue_Double(ScenarioResultStructure.COLUMN_NAME_AREA_KM2);

            if (scenario.Subbasins.ContainsKey(_id))
                _sub = scenario.Subbasins[_id] as Subbasin;
        }

        private double _area = ScenarioResultStructure.EMPTY_VALUE;
        private Subbasin _sub = null;

        public override string BasicInfoTableName
        {
            get { return ScenarioResultStructure.INFO_TABLE_NAME_RCH; }
        }

        public override SWATUnitType Type
        {
            get { return SWATUnitType.RCH; }
        }

        public override string ToStringBasicInfo()
        {
            return string.Format("Reach: {1}, Contribution Area : {0:F4} km2", _area,ID);
        }
    }
}
