using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SWAT_SQLite_Result.ArcSWAT
{
    /// <summary>
    /// Observation data for specific unit and data type
    /// </summary>
    public class SWATUnitObservationData
    {
        private SWATUnitType _unitType = SWATUnitType.UNKNOWN;
        private int _id = ScenarioResultStructure.UNKONWN_ID;
        private string _col = null;
        private Dictionary<int, SWATUnitColumnYearObservationData> _datas = new Dictionary<int, SWATUnitColumnYearObservationData>();
        private ObservationData _parentData = null;
        private int _startYear = ScenarioResultStructure.UNKONWN_ID;
        private int _endYear = ScenarioResultStructure.UNKONWN_ID;

        public SWATUnitObservationData(SWATUnitColumnYearResult result, ObservationData parent)
        {
            _parentData = parent;

            _id = result.UnitResult.Unit.ID;
            _unitType = result.UnitResult.Unit.Type;
            _col = result.Column;
            _startYear = result.UnitResult.Unit.Scenario.StartYear;
            _endYear = result.UnitResult.Unit.Scenario.EndYear;
        }

        public SWATUnitObservationData(int id, SWATUnitType unitType, 
            string dataType,
            int startYear, int endYear,
            ObservationData parent)
        {
            _parentData = parent;
            _id = id;
            _unitType = unitType;
            _col = dataType;
            _startYear = startYear;
            _endYear = endYear;
        }

        public SWATUnitColumnYearObservationData getObservedData(int year)
        {
            if (!_datas.ContainsKey(year))
                _datas.Add(year, new SWATUnitColumnYearObservationData(_col, year, this));
            return _datas[year];
        }

        public SWATUnitType UnitType { get { return _unitType; } }
        public string Column { get { return _col; } }
        public int ID { get { return _id; } }
        public ObservationData Observation { get { return _parentData; } }
        public int StartYear { get { return _startYear; } }
        public int EndYear { get { return _endYear; } }
    }
}
