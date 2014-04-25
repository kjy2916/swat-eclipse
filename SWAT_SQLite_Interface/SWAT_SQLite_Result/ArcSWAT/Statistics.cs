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
        private string _col = "";

        public Statistics(DataTable dt, string col)
        {
            if (dt.Rows.Count == 0) return;

            //overall statistic
            try
            {
                _sum = Convert.ToDouble(dt.Compute(string.Format("Sum({0})", col), ""));
                _avg = Convert.ToDouble(dt.Compute(string.Format("Avg({0})", col), ""));
                _min = Convert.ToDouble(dt.Compute(string.Format("Min({0})", col), ""));
                _max = Convert.ToDouble(dt.Compute(string.Format("Max({0})", col), ""));

                //get number of years           
                DateTime startDay = Convert.ToDateTime(dt.Compute(string.Format("Min({0})", SWATUnitResult.COLUMN_NAME_DATE), ""));
                DateTime endDay = Convert.ToDateTime(dt.Compute(string.Format("Max({0})", SWATUnitResult.COLUMN_NAME_DATE), ""));
                int years = endDay.Year - startDay.Year + 1;
                if (years > 0)
                    _annualAverage = _sum / years;
            }
            catch
            {
            }

            _col = col;
        }

        public override string ToString()
        {
            return string.Format("({5}) Sum {0:F4}, Average {1:F4}, Minimum {2:F4}, Maximum {3:F4}, Annual Average {4:F4}",
                _sum, _avg, _min, _max, _annualAverage,_col);  
        }
    }
}
