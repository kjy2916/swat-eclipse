using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SWAT_SQLite_Result.ArcSWAT
{
    public abstract class SeasonData
    {
        public abstract DataTable Table {get;}
        public abstract int Year { get; }
        private Dictionary<SeasonType, DataTable> _seasonTables = new Dictionary<SeasonType, DataTable>();

        public abstract DateTime FirstDay { get; }
        public abstract DateTime LastDay { get; }

        private static string SEASON_SQL_FORMAT =
            "{0} >= '{1:yyyy-MM-dd}' and {0} <= '{2:yyyy-MM-dd}'";

        private string getSeasonSQL(int year, int startMonth, int endMonth)
        {
            if (startMonth > 12 || startMonth < 1) startMonth = 1;
            if (endMonth > 12 || endMonth < 1) endMonth = 12;

            DateTime startDate = new DateTime(year, startMonth, 1);
            DateTime endDate = new DateTime(year, endMonth, DateTime.DaysInMonth(year, endMonth));
            if (startMonth > endMonth)
                endDate = endDate.AddYears(1);

            return string.Format(SEASON_SQL_FORMAT,
                SWATUnitResult.COLUMN_NAME_DATE,
                startDate, endDate);
        }

        protected string getSeasonSQL(SeasonType season)
        {
            int startMonth = -1;
            int endMonth = -1;
            if (season == SeasonType.SnowMelt)
            {
                startMonth = 3;
                endMonth = 5;
            }
            else if (season == SeasonType.GrowingSeason)
            {
                startMonth = 5;
                endMonth = 10;
            }

            if (startMonth == -1 && endMonth == -1) return "";

            //get season sql
            string sql = "";
            if (Year > 0)
                sql = getSeasonSQL(Year, startMonth, endMonth);
            else
            {
                //get first year and end year
                for (int i = FirstDay.Year; i <= LastDay.Year; i++)
                {
                    if (sql.Length > 0) sql += " or ";
                    sql += "(" + getSeasonSQL(i, startMonth, endMonth) + ")";
                }
            }
            System.Diagnostics.Debug.WriteLine(sql);
            return sql;
        } 

        /// <summary>
        /// The data table for given column and given season in given year
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable SeasonTable(SeasonType season)
        {
            if (season == SeasonType.WholeYear) return Table;
            if (Table.Rows.Count == 0) return Table;

            if (!_seasonTables.ContainsKey(season))
            {
                string sql = getSeasonSQL(season);
                if (sql.Length == 0) return Table;

                //get the season data from data view
                DataView view = new DataView(Table);
                view.RowFilter = sql;
                _seasonTables.Add(season, view.ToTable());
            }
            return _seasonTables[season];
        }
    }
}
