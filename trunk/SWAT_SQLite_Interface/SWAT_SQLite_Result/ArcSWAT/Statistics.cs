using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SWAT_SQLite_Result.ArcSWAT
{
    public class Statistics
    {
        private double _sum = ScenarioResultStructure.EMPTY_VALUE;
        private double _avg = ScenarioResultStructure.EMPTY_VALUE;
        private double _min = ScenarioResultStructure.EMPTY_VALUE;
        private double _max = ScenarioResultStructure.EMPTY_VALUE;
        private double _annualAverage = ScenarioResultStructure.EMPTY_VALUE;

        public Statistics(DataTable dt, string col, int years)
        {
            if (dt.Rows.Count == 0) return;

            //overall statistic
            _sum = Convert.ToDouble(dt.Compute(string.Format("Sum({0})", col), ""));
            _avg = Convert.ToDouble(dt.Compute(string.Format("Avg({0})", col), ""));
            _min = Convert.ToDouble(dt.Compute(string.Format("Min({0})", col), ""));
            _max = Convert.ToDouble(dt.Compute(string.Format("Max({0})", col), ""));
            if(years > 0)
                _annualAverage = _sum / years;   
        }

        public override string ToString()
        {
            return string.Format("Sum : {0:F4}, Average : {1:F4}, Minimum : {2:F4}, Maximum : {3:F4}, Annual Average : {4:F4}",
                _sum, _avg, _min, _max, _annualAverage);  
        }
    }
}
