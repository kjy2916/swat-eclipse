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

        public SWATUnitType UnitType { get { return _parentData.UnitType; } }
        public int UnitID { get { return _parentData.ID; } }

        protected override void read()
        {
            if (_table != null) return;
            if(_parentData == null) return;
            if(_parentData.Observation == null) return;

            //get table name
            //each observed data for one corresponding table in the database
            string tableName = 
                ObservationData.getTableName(_parentData.UnitType,_parentData.ID,_col);

            //get filter based on given year
            string filter = "";
            if (_year > 0) //specific year
            {
                 filter += string.Format("{0} >= '{1}-01-01' and {0} <= '{2}-12-31'", ObservationData.OBSERVATION_COLUMN_DATE, _year-1,_year); //read two years of data to consider hydrological year
            }
            else //all years in between start and end year
            {
                if (_parentData.StartYear > 0)
                {
                    filter += string.Format("{0} >= '{1}-01-01'", ObservationData.OBSERVATION_COLUMN_DATE, _parentData.StartYear);
                }
                if (_parentData.EndYear > 0)
                {
                    if(filter.Length > 0) filter += " and ";
                    filter += string.Format("{0} <= '{1}-01-01'", ObservationData.OBSERVATION_COLUMN_DATE, _parentData.EndYear);
                }
            }
            if (filter.Length > 0)
                filter = " where " + filter;

            DataTable dt = _parentData.Observation.GetDataTable(
                string.Format("select * from [{0}] {1}",tableName,filter));

            if (dt.Columns.Count == 2)
            {
                //add a date time column and convert time from text to date
                dt.Columns.Add(SWATUnitResult.COLUMN_NAME_DATE, typeof(DateTime));
                DateTime d = DateTime.Now;
                foreach (DataRow r in dt.Rows)
                {
                    RowItem item = new RowItem(r);
                    if (DateTime.TryParse(item.getColumnValue_String(0), out d))
                        r[2] = d;
                }

                dt.Columns[1].ColumnName = _col;
                _table = dt;
            }
        }
    }
}
