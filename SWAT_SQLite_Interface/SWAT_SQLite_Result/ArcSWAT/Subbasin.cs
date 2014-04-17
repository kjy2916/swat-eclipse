using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SWAT_SQLite_Result.ArcSWAT
{
    class Subbasin : SWATUnit
    {
        public Subbasin(int id, ScenarioResult scenario)
            : base(id, SWATUnitType.SUB, scenario)
        {

        }

        public Subbasin(DataRow subInfoRow, ScenarioResult scenario)
            : base(subInfoRow, scenario)
        {
            if (!subInfoRow.Table.TableName.Equals(BasicInfoTableName)) return;

            _type = SWATUnitType.SUB;

            RowItem item = new RowItem(subInfoRow);
            _id = item.getColumnValue_Int(ScenarioResult.COLUMN_NAME_SUB);
            _area = item.getColumnValue_Double(ScenarioResult.COLUMN_NAME_AREA_KM2);
            _area_fr_wshd = item.getColumnValue_Double(ScenarioResult.COLUMN_NAME_AREA_FR_WSHD);
        }

        public override string BasicInfoTableName
        {
            get { return ScenarioResult.INFO_TABLE_NAME_SUB; }
        }

        public override System.Collections.Specialized.StringCollection ResultTableNames
        {
            get
            {
                return new System.Collections.Specialized.StringCollection() { 
                    ScenarioResult.TABLE_NAME_SUB
                };
            }
        }

        public void addHRU(HRU hru)
        {
            if (hru == null) return;
            if (_hrus.ContainsKey(hru.ID)) return;
            _hrus.Add(hru.ID, hru);
        }

        private Dictionary<int, HRU> _hrus = new Dictionary<int, HRU>(); //initial subbasin first and then add corresonding to subbasin
        private double _area = ScenarioResult.EMPTY_VALUE;
        private double _area_fr_wshd = ScenarioResult.EMPTY_VALUE;
    }
}
