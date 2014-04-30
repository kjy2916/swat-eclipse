using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SWAT_SQLite_Result.ArcSWAT
{
    public class StatisticCompare
    {
        private double _r2 = ScenarioResultStructure.EMPTY_VALUE;
        private double _nse = ScenarioResultStructure.EMPTY_VALUE;
        private SWATUnitColumnYearCompareResult _result = null;

        public StatisticCompare(SWATUnitColumnYearCompareResult result)
        {
            _result = result;
        }

        public double R2 
        { 
            get 
            {
                if (_r2 == ScenarioResultStructure.EMPTY_VALUE)
                    _r2 = CalculateR2(_result.Table, _result.ChartColumns[0], _result.ChartColumns[1]);
                return _r2;
            } 
        }

        public double NSE
        {
            get
            {
                if (_nse == ScenarioResultStructure.EMPTY_VALUE)
                    _nse = CalculateNSE(_result.Table, _result.ChartColumns[0], _result.ChartColumns[1]);
                return _nse;
            }
        }

        private static double Average(DataTable dt, string col)
        {
            return Convert.ToDouble(dt.Compute(string.Format("Avg({0})", col), ""));
        }

        private static double Sum(DataTable dt, string col)
        {
            return Convert.ToDouble(dt.Compute(string.Format("Sum({0})", col), ""));
        }

        private static double Variance(DataTable dt, string col)
        {
            return Convert.ToDouble(dt.Compute(string.Format("Var({0})", col), "")) * (dt.Rows.Count - 1);
        }

        private static double CalculateR2(DataTable dt, string col_observed, string col_simulated)
        {
            double ave_observed = Average(dt, col_observed);
            double ave_simulated = Average(dt, col_simulated);

            //add a new colum R2_TOP for [(Oi-Oave) * (Pi-Pave)]
            string col_top = "R2_TOP";
            if (!dt.Columns.Contains(col_top))
            {
                DataColumn col = new DataColumn(col_top, typeof(double));
                col.Expression = string.Format("({0} - {1}) * ({2} - {3})",
                    col_observed, ave_observed, col_simulated, ave_simulated);
                dt.Columns.Add(col);
            }

            //get top value
            double top = Sum(dt, col_top);
            top *= top;

            double var_observed = Variance(dt, col_observed);
            double var_simulated = Variance(dt, col_simulated);

            double r2 = -1.0;
            if(var_observed >= 0.000001 && var_simulated >= 0.000001)
                r2 = top / var_observed / var_simulated;
            return r2;
        }

        private static double CalculateNSE(DataTable dt, string col_observed, string col_simulated)
        {
            //top part [sum(Oi-Pi)^2)]
            //add a new colum NSE_TOP for [(Oi-Pi)^2]
            string col_top = "NSE_TOP";
            if (!dt.Columns.Contains(col_top))
            {
                DataColumn col = new DataColumn(col_top, typeof(double));
                col.Expression = string.Format("({0} - {1}) * ({0} - {1})",
                col_observed, col_simulated);
                dt.Columns.Add(col);
            }

            //get top value
            double top = Sum(dt, col_top);

            //bottome part [sum(Oi-Oave)^2]
            double bottom = Variance(dt, col_observed);

            //NSE
            double nse = -1.0f;
            if (bottom >= 0.000001)
                nse = 1.0 - top / bottom;
            return nse;
        }

        public override string ToString()
        {
            return string.Format("R2 = {0:F4}; NSE = {1:F4}",R2,NSE);
        }
    }
}
