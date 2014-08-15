using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SWATPerformanceTest
{
    class SQLiteTest
    {
        private ExtractSWAT_SQLite _extractSQLite;
        private ExtractSWAT_Text _extractText;
        private int _startYear;
        private int _endYear;

        public SQLiteTest(string scenariosDir, string scenarioName)
        {
            _extractSQLite = new ExtractSWAT_SQLite(scenariosDir + @"\" + scenarioName + @"\txtinout\result_627.db3");
            _extractText = new ExtractSWAT_Text(scenariosDir,scenarioName);

            _startYear = _extractText.StartYear;
            _endYear = _extractText.EndYear;
        }

        private int getNumberofUnit(SourceType source)
        {
            if (source == SourceType.HRU || source == SourceType.WATER) return _extractText.NumberofHRU;
            if (source == SourceType.REACH || source == SourceType.SUBBASIN) return _extractText.NumberofSubbasin;
            return -1;
        }

        public Dictionary<string,double> Compare(SourceType source, string var)
        {
            Dictionary<string, double> results = new Dictionary<string, double>();

            int num = getNumberofUnit(source);
            if (num == -1) return results;

            for (int i = 1; i <= num; i++)
                results.Add(string.Format("{0}-{1}-{2}",source,i,var),
                    Compare(_startYear, _endYear, source, i, var));
            return results;
        }

        private double Compare(int startYear, int endYear, SourceType source, int id, string var)
        {
            DataTable dtSQLite = _extractSQLite.Extract(startYear, endYear, source, id, var);
            DataTable dtText = _extractText.Extract(startYear, endYear, source, id, var);

            Console.WriteLine(string.Format("Extract time for {0}-{1}-{2}-{3}-{4}: SQLite = {5:F4} ms, Text = {6:F4} ms",
                startYear, endYear, source, id, var, _extractSQLite.ExtractTime,_extractText.ExtractTime));

            //the join table structure
            DataTable dt = new DataTable();
            dt.Columns.Add("TIME", typeof(DateTime));
            dt.Columns.Add("SQLite", typeof(double));
            dt.Columns.Add("Text", typeof(double));

            //join these two tables using Linq
            var results = from table1 in dtSQLite.AsEnumerable()
                          join table2 in dtText.AsEnumerable() on table1["TIME"] equals table2["TIME"]
                          select dt.LoadDataRow(new object[]
                              {
                                  table1["TIME"],
                                  table1[var],
                                  table2[var]
                              }, false);

            results.CopyToDataTable();

            return CalculateR2(dt, "SQLite", "Text", "");
        }

        #region R2 Calculation

        public static double EMPTY_VALUE = -99.0;

        private static double Compute(DataTable dt, string expression, string filter)
        {
            object result = dt.Compute(expression, filter);
            if (result is System.DBNull)
                return EMPTY_VALUE;
            double value = EMPTY_VALUE;
            double.TryParse(result.ToString(), out value);
            return value;
        }

        private static double Average(DataTable dt, string col, string filter)
        {
            return Compute(dt, string.Format("Avg({0})", col), filter);
        }

        private static double Sum(DataTable dt, string col, string filter)
        {
            return Compute(dt, string.Format("Sum({0})", col), filter);
        }

        private static double Variance(DataTable dt, string col, string filter)
        {
            double var = Compute(dt, string.Format("Var({0})", col), filter);
            if (var == EMPTY_VALUE) return var;

            return var * (dt.Select(filter).Length - 1);
        }

        static double CalculateR2(DataTable dt, string col_observed, string col_simulated, string filter)
        {
            //consider missing value in observed data
            //some year just doesn't have data
            if (dt == null || dt.Rows.Count == 0)
                return EMPTY_VALUE;

            double ave_observed = Average(dt, col_observed, filter);
            double ave_simulated = Average(dt, col_simulated, filter);

            //see if all values in the time series are 0
            if (ave_observed == 0 || ave_simulated == 0)
                return 0.0;

            //add a new colum R2_TOP for [(Oi-Oave) * (Pi-Pave)]
            string col_top = "R2_TOP";
            if (dt.Columns.Contains(col_top))
                dt.Columns.Remove(col_top);

            DataColumn col = new DataColumn(col_top, typeof(double));
            col.Expression = string.Format("({0} - {1}) * ({2} - {3})",
                col_observed, ave_observed, col_simulated, ave_simulated);
            dt.Columns.Add(col);

            //get top value
            double top = Sum(dt, col_top, filter);
            top *= top;

            double var_observed = Variance(dt, col_observed, filter);
            double var_simulated = Variance(dt, col_simulated, filter);

            double r2 = EMPTY_VALUE;
            if (var_observed >= 0.000001 && var_simulated >= 0.000001)
                r2 = top / var_observed / var_simulated;
            return r2;
        }

        #endregion
    }
}
