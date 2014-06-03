using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SWAT_SQLite_Result.ArcSWAT
{
    public class Subbasin : SWATUnit
    {
        public Subbasin(DataRow subInfoRow, ScenarioResult scenario)
            : base(subInfoRow, scenario)
        {
            RowItem item = new RowItem(subInfoRow);
            _id = item.getColumnValue_Int(ScenarioResultStructure.COLUMN_NAME_SUB);
            _area = item.getColumnValue_Double(ScenarioResultStructure.COLUMN_NAME_AREA_KM2);
            _area_fr_wshd = item.getColumnValue_Double(ScenarioResultStructure.COLUMN_NAME_AREA_FR_WSHD);
        }

        public override string BasicInfoTableName
        {
            get { return ScenarioResultStructure.INFO_TABLE_NAME_SUB; }
        }

        public override SWATUnitType Type
        {
            get { return SWATUnitType.SUB; }
        }

        public override bool UseMultiOutputTable
        {
            get { return true; }
        }

        public override string OutputTableFormatString
        {
            get { return ScenarioResultStructure.TABLE_NAME_FORMAT_RCH_SUB; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(base.ToString());
            sb.AppendLine(string.Format("{0} HRUs", _hrus.Count));
            foreach (int hruid in _hrus.Keys)
                sb.AppendLine(hruid.ToString());
            
            return sb.ToString();
        }

        public override string ToStringBasicInfo()
        {
            return string.Format("Subbasin: {2}, Area : {0:F4} km2, Area Fraction in Watershed : {1:P2}", _area, _area_fr_wshd,ID);
        }

        public void addHRU(HRU hru)
        {
            if (hru == null) return;
            if (_hrus.ContainsKey(hru.ID)) return;
            _hrus.Add(hru.ID, hru);
        }

        public Dictionary<int, HRU> HRUs { get { return _hrus; } }

        private Dictionary<int, HRU> _hrus = new Dictionary<int, HRU>(); //initial subbasin first and then add corresonding to subbasin
        private double _area = ScenarioResultStructure.EMPTY_VALUE;
        private double _area_fr_wshd = ScenarioResultStructure.EMPTY_VALUE;
    }
}
