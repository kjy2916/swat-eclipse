using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SWAT_SQLite_Result.ArcSWAT
{
    public enum StatisticCompareType
    {
        NSE = 0,
        R2 = 1,
        Bias = 2,
        RMSE = 3,
        CVRMSE = 4,
        NRMSE = 5
    }

    public class StatisticCompare
    {
        private Dictionary<string, double> _r2 = new Dictionary<string, double>();
        private Dictionary<string, double> _nse = new Dictionary<string, double>();
        private Dictionary<string, double> _bias = new Dictionary<string, double>();
        private Dictionary<string, double> _rmse = new Dictionary<string, double>();
        private Dictionary<string, double> _cvrmse = new Dictionary<string, double>();
        private Dictionary<string, double> _nrmse = new Dictionary<string, double>();
        private SWATUnitColumnYearCompareResult _result = null;
        private SeasonType _season = SeasonType.WholeYear;

        public StatisticCompare(SWATUnitColumnYearCompareResult result,SeasonType season)
        {
            _result = result;
            _season = season;
        }

        public double Statistic(string filter, StatisticCompareType statisticType)
        {
            switch (statisticType)
            {
                case StatisticCompareType.NSE:
                    return NSE(filter);
                case StatisticCompareType.R2:
                    return R2(filter);
                case StatisticCompareType.Bias:
                    return BIAS(filter);
                case StatisticCompareType.RMSE:
                    return RMSE(filter);
                case StatisticCompareType.CVRMSE:
                    return CVRMSE(filter);
                case StatisticCompareType.NRMSE:
                    return NRMSE(filter);
                default:
                    return ScenarioResultStructure.EMPTY_VALUE;
            }
        }

        private double R2(string filter)
        {
            if (!_r2.ContainsKey(filter))
                _r2.Add(filter, CalculateR2(_result.SeasonTableForStatistics(_season),
                    _result.ChartColumns[1], _result.ChartColumns[0], filter));
            return _r2[filter];
        }

        private double NSE(string filter)
        {
            if (!_nse.ContainsKey(filter))
                _nse.Add(filter, CalculateNSE(_result.SeasonTableForStatistics(_season),
                    _result.ChartColumns[1], _result.ChartColumns[0], filter));
            return _nse[filter];
        }

        private double BIAS(string filter)
        {
            if (!_bias.ContainsKey(filter))
                _bias.Add(filter, CalculateBias(_result.SeasonTableForStatistics(_season),
                    _result.ChartColumns[1], _result.ChartColumns[0], filter));
            return _bias[filter];
        }

        private double RMSE(string filter)
        {
            if (!_rmse.ContainsKey(filter))
                _rmse.Add(filter, CalculateRMSE(_result.SeasonTableForStatistics(_season),
                    _result.ChartColumns[1], _result.ChartColumns[0], filter));
            return _rmse[filter];
        }

        private double CVRMSE(string filter)
        {
            if (!_cvrmse.ContainsKey(filter))
                _cvrmse.Add(filter, CalculateCVRMSE(_result.SeasonTableForStatistics(_season),
                    _result.ChartColumns[1], _result.ChartColumns[0], filter));
            return _cvrmse[filter];
        }

        private double NRMSE(string filter)
        {
            if (!_nrmse.ContainsKey(filter))
                _nrmse.Add(filter, CalculateNRMSE(_result.SeasonTableForStatistics(_season),
                    _result.ChartColumns[1], _result.ChartColumns[0], filter));
            return _nrmse[filter];
        }

        /// <summary>
        /// For used in performance view
        /// </summary>
        /// <param name="splitYear"></param>
        /// <param name="before"></param>
        /// <param name="after"></param>
        public void Statistic(int splitYear, StatisticCompareType statisticType, out double before, out double after)
        {
            if (_result.FirstDay.Year == _result.LastDay.Year) { before = NSE(""); after = ScenarioResultStructure.EMPTY_VALUE; return; }
            if (splitYear > _result.LastDay.Year || splitYear <= _result.FirstDay.Year) { before = NSE(""); after = ScenarioResultStructure.EMPTY_VALUE; return; }

            string filter1 = string.Format("{0} < '{1}-01-01'", SWATUnitResult.COLUMN_NAME_DATE, splitYear);
            string filter2 = string.Format("{0} >= '{1}-01-01'", SWATUnitResult.COLUMN_NAME_DATE, splitYear);

            before = Statistic(filter1,statisticType);
            after = Statistic(filter2,statisticType);            
        }

        private static double Compute(DataTable dt, string expression, string filter)
        {
            object result = dt.Compute(expression, filter);
            if (result is System.DBNull)
                return ScenarioResultStructure.EMPTY_VALUE;
            double value = ScenarioResultStructure.EMPTY_VALUE;
            double.TryParse(result.ToString(), out value);
            return value;
        }

        private static double Count(DataTable dt, string col, string filter)
        {
            return Compute(dt, string.Format("Count({0})", col), filter);
        }

        private static double Average(DataTable dt, string col,string filter)
        {
            return Compute(dt,string.Format("Avg({0})", col), filter);            
        }

        private static double Max(DataTable dt, string col, string filter)
        {
            return Compute(dt, string.Format("Max({0})", col), filter);
        }

        private static double Min(DataTable dt, string col, string filter)
        {
            return Compute(dt, string.Format("Min({0})", col), filter);
        }

        private static double Sum(DataTable dt, string col, string filter)
        {
            return Compute(dt,string.Format("Sum({0})", col), filter);
        }

        private static double Variance(DataTable dt, string col, string filter)
        {
            double var = Compute(dt, string.Format("Var({0})", col), filter);
            if (var == ScenarioResultStructure.EMPTY_VALUE) return var;

            return var * (dt.Select(filter).Length - 1);
        }

        private static double CalculateBias(DataTable dt, string col_observed, string col_simulated, string filter)
        {
            //consider missing value in observed data
            //some year just doesn't have data
            if (dt == null || dt.Rows.Count == 0)
                return ScenarioResultStructure.EMPTY_VALUE;

            double sum_observed = Sum(dt, col_observed,filter);
            double sum_simulated = Sum(dt, col_simulated,filter);
            double bias = ScenarioResultStructure.EMPTY_VALUE;
            if (sum_observed >= 0.000001 && sum_simulated >= 0.000001)
                bias = sum_simulated / sum_observed - 1;
            return bias;
        }

        private static double CalculateRMSE(DataTable dt, string col_observed, string col_simulated, string filter)
        {
            //consider missing value in observed data
            //some year just doesn't have data
            if (dt == null || dt.Rows.Count == 0)
                return ScenarioResultStructure.EMPTY_VALUE;

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
            double top = Sum(dt, col_top, filter);
            if (top == ScenarioResultStructure.EMPTY_VALUE)
                return ScenarioResultStructure.EMPTY_VALUE;

            double num = Count(dt,col_top,filter);
            double rmse = ScenarioResultStructure.EMPTY_VALUE;
            rmse = Math.Sqrt(top/num);
            return rmse;
        }

        private static double CalculateCVRMSE(DataTable dt, string col_observed, string col_simulated, string filter)
        {
            //consider missing value in observed data
            //some year just doesn't have data
            if (dt == null || dt.Rows.Count == 0)
                return ScenarioResultStructure.EMPTY_VALUE;

            double rmse = CalculateRMSE(dt, col_observed, col_simulated, filter);
            if (rmse == ScenarioResultStructure.EMPTY_VALUE)
                return ScenarioResultStructure.EMPTY_VALUE;

            double ave_observed = Average(dt, col_observed, filter);
            if (ave_observed == ScenarioResultStructure.EMPTY_VALUE)
                return ScenarioResultStructure.EMPTY_VALUE;

            double cvrmse = ScenarioResultStructure.EMPTY_VALUE;
            if (rmse >= 0.000001 && ave_observed >= 0.000001)
                cvrmse = rmse / ave_observed;
            return cvrmse;
        }

        private static double CalculateNRMSE(DataTable dt, string col_observed, string col_simulated, string filter)
        {
            //consider missing value in observed data
            //some year just doesn't have data
            if (dt == null || dt.Rows.Count == 0)
                return ScenarioResultStructure.EMPTY_VALUE;

            double rmse = CalculateRMSE(dt, col_observed, col_simulated, filter);
            if (rmse == ScenarioResultStructure.EMPTY_VALUE)
                return ScenarioResultStructure.EMPTY_VALUE;

            double max_observed = Max(dt, col_observed, filter);
            if (max_observed == ScenarioResultStructure.EMPTY_VALUE)
                return ScenarioResultStructure.EMPTY_VALUE;

            double min_observed = Min(dt, col_observed, filter);
            if (min_observed == ScenarioResultStructure.EMPTY_VALUE)
                return ScenarioResultStructure.EMPTY_VALUE;

            double nrmse = ScenarioResultStructure.EMPTY_VALUE;
            nrmse = rmse / (max_observed - min_observed);
            return nrmse;
        }

        private static double CalculateR2(DataTable dt, string col_observed, string col_simulated,string filter)
        {
            //consider missing value in observed data
            //some year just doesn't have data
            if (dt == null || dt.Rows.Count == 0) 
                return ScenarioResultStructure.EMPTY_VALUE;

            double ave_observed = Average(dt, col_observed,filter);
            double ave_simulated = Average(dt, col_simulated,filter);

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
            double top = Sum(dt, col_top,filter);
            top *= top;

            double var_observed = Variance(dt, col_observed,filter);
            double var_simulated = Variance(dt, col_simulated,filter);

            double r2 = ScenarioResultStructure.EMPTY_VALUE;
            if(var_observed >= 0.000001 && var_simulated >= 0.000001)
                r2 = top / var_observed / var_simulated;
            return r2;
        }

        private static double CalculateNSE(DataTable dt, string col_observed, string col_simulated, string filter)
        {
            //consider missing value in observed data
            //some year just doesn't have data
            if (dt == null || dt.Rows.Count == 0)
                return ScenarioResultStructure.EMPTY_VALUE;

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
            double top = Sum(dt, col_top,filter);
            if (top == ScenarioResultStructure.EMPTY_VALUE)
                return ScenarioResultStructure.EMPTY_VALUE;

            //bottome part [sum(Oi-Oave)^2]
            double bottom = Variance(dt, col_observed,filter);
            if (bottom == ScenarioResultStructure.EMPTY_VALUE)
                return ScenarioResultStructure.EMPTY_VALUE;

            //NSE
            double nse = ScenarioResultStructure.EMPTY_VALUE;
            if (bottom >= 0.000001)
                nse = 1.0 - top / bottom;
            return nse;
        }

        public override string ToString()
        {
            return string.Format("R2 = {0:F4}; NSE = {1:F4}",R2(""),NSE(""));
        }

        public string ToString(int splitYear)
        {
            if (_result.FirstDay.Year == _result.LastDay.Year) return ToString();
            if (splitYear > _result.LastDay.Year || splitYear <= _result.FirstDay.Year) return ToString();

            string filter1 = string.Format("{0} < '{1}-01-01'", SWATUnitResult.COLUMN_NAME_DATE, splitYear);
            string filter2 = string.Format("{0} >= '{1}-01-01'", SWATUnitResult.COLUMN_NAME_DATE, splitYear);
            return string.Format("{0}-{1}:R2 = {2:F4},NSE = {3:F4}; {4}-{5}:R2 = {6:F4},NSE = {7:F4}", 
                _result.FirstDay.Year,splitYear - 1,
                R2(filter1), NSE(filter1),
                splitYear,_result.LastDay.Year,
                R2(filter2), NSE(filter2));            
        }
    }
}
