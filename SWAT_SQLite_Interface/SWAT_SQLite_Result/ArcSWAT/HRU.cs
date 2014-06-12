using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;

namespace SWAT_SQLite_Result.ArcSWAT
{
    public class HRU : SWATUnit
    {
        public HRU(DataRow hruInfoRow, ScenarioResult scenario) : base(hruInfoRow,scenario)
        {
            RowItem item = new RowItem(hruInfoRow);
            _id = item.getColumnValue_Int(ScenarioResultStructure.COLUMN_NAME_HRU);
            _seqIdInSubbasin = item.getColumnValue_Int(ScenarioResultStructure.COLUMN_NAME_HRU_SEQ);
            _area = item.getColumnValue_Double(ScenarioResultStructure.COLUMN_NAME_AREA_KM2);
            _area_fr_sub = item.getColumnValue_Double(ScenarioResultStructure.COLUMN_NAME_AREA_FR_SUB);
            _area_fr_wshd = item.getColumnValue_Double(ScenarioResultStructure.COLUMN_NAME_AREA_FR_WSHD);

            //connect hru and subbasin
            int subid = item.getColumnValue_Int(ScenarioResultStructure.COLUMN_NAME_SUB);
            if (scenario.Subbasins.ContainsKey(subid))
            {
                _sub = scenario.Subbasins[subid] as Subbasin;
                if (_sub != null) _sub.addHRU(this);                    
            }
        }

        public override string FileName
        {
            get
            {
                return string.Format("{0:00000}{1:0000}", _sub.ID, _seqIdInSubbasin);
            }
        }

        public override string BasicInfoTableName
        {
            get { return ScenarioResultStructure.INFO_TABLE_NAME_HRU; }
        }

        public override SWATUnitType Type
        {
            get { return SWATUnitType.HRU; }
        }

        public override string ToStringBasicInfo()
        {
            return string.Format("HRU: {4}, Subbasin: {0}, Seq: {5}, Area : {1:F4} km2, Area Fraction in Subbasin : {2:P2}, Area Fraction in Watershed : {3:P2}",
            _sub == null ? -1 : _sub.ID, _area, _area_fr_sub, _area_fr_wshd, ID,_seqIdInSubbasin);
        }

        public double AreaFractionSub { get { return _area_fr_sub; } }
        public Subbasin Subbasin { get { return _sub; } }

        private Subbasin _sub = null;
        private int _seqIdInSubbasin = -1;        
        private double _area = ScenarioResultStructure.EMPTY_VALUE;
        private double _area_fr_sub = ScenarioResultStructure.EMPTY_VALUE;
        private double _area_fr_wshd = ScenarioResultStructure.EMPTY_VALUE;           

    }
}
