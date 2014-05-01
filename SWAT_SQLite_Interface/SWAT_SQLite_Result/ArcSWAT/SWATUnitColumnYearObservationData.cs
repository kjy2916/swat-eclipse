using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SWAT_SQLite_Result.ArcSWAT
{
    public class SWATUnitColumnYearObservationData : ColumnYearData
    {
        private SWATUnitObservationData _parentData = null;

        public SWATUnitColumnYearObservationData(string col, int year,SWATUnitObservationData data) : base(col,year)
        {
            _parentData = data;
            _colCompare = string.Format("{0}_Observed", _col);
        }

        protected override void read()
        {
            if (_table != null) return;
            if(_parentData == null) return;
            if(_parentData.Observation == null) return;

            //construct SQL
            string tableName = _parentData.UnitType.ToString();
            string filter = string.Format("{0}={1}",ObservationData.OBSERVATION_COLUMN_ID,_parentData.ID);

            if (_year > 0) //specific year
            {
                filter += " and ";
                filter += string.Format("{0} >= '{1}-01-01' and {0} <= '{1}-12-31'", ObservationData.OBSERVATION_COLUMN_DATE, _year);
            }
            else //all years in between start and end year
            {
                if (_parentData.StartYear > 0)
                {
                    filter += " and ";
                    filter += string.Format("{0} >= '{1}-01-01'", ObservationData.OBSERVATION_COLUMN_DATE, _parentData.StartYear);
                }
                if (_parentData.EndYear > 0)
                {
                    filter += " and ";
                    filter += string.Format("{0} <= '{1}-01-01'", ObservationData.OBSERVATION_COLUMN_DATE, _parentData.EndYear);
                }
            }

            _table = _parentData.Observation.GetDataTable(
                string.Format("select {0} from {1} where {2}",
                _col,tableName,filter));
        }
    }
}
