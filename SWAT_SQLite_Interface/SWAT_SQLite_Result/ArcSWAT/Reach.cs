using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SWAT_SQLite_Result.ArcSWAT
{
    class Reach : SWATUnit
    {
        public Reach(DataRow rchInfoRow, ScenarioResult scenario)
            : base(rchInfoRow, scenario)

        {
            RowItem item = new RowItem(rchInfoRow);
            _id = item.getColumnValue_Int(ScenarioResult.COLUMN_NAME_RCH);
            _area = item.getColumnValue_Double(ScenarioResult.COLUMN_NAME_AREA_KM2);

            if (scenario.Subbasins.ContainsKey(_id))
                _sub = scenario.Subbasins[_id] as Subbasin;
        }

        private double _area = ScenarioResult.EMPTY_VALUE;
        private Subbasin _sub = null;

        public override string BasicInfoTableName
        {
            get { return ScenarioResult.INFO_TABLE_NAME_RCH; }
        }

        public override System.Collections.Specialized.StringCollection ResultTableNames
        {
            get
            {
                return new System.Collections.Specialized.StringCollection() { ScenarioResult.TABLE_NAME_REACH, ScenarioResult.TABLE_NAME_REACH_SEDIMENT };
            }
        }

        public override SWATUnitType Type
        {
            get { return SWATUnitType.RCH; }
        }

        public override string ToString()
        {
            return base.ToString() +
                string.Format("Contribution Area : {0:F4} km2",_area);
        }
    }
}
