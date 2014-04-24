using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SWAT_SQLite_Result
{
    public partial class TimeView : UserControl
    {
        public TimeView()
        {
            InitializeComponent();
        }

        private ArcSWAT.SWATUnitResult _result = null;
        private int _startYear = -1;

        private ArcSWAT.SWATUnitResult Result
        {
            set
            {
                _result = value;
                _startYear = _result.Unit.Scenario.StartYear;

                //date time picker
                dateTimePicker1.MinDate = new DateTime(_result.Unit.Scenario.StartYear, 1, 1);
                dateTimePicker1.MaxDate = new DateTime(_result.Unit.Scenario.EndYear, 12, 31);

                if (dateTimePicker1.Value < dateTimePicker1.MinDate || dateTimePicker1.Value > dateTimePicker1.MaxDate)
                    dateTimePicker1.Value = dateTimePicker1.MinDate;

                //track bar
                trackBar1.Minimum = 0;
                TimeSpan span = dateTimePicker1.MaxDate.Subtract(dateTimePicker1.MinDate);
                if (_result.Interval == ArcSWAT.SWATResultIntervalType.DAILY)
                    trackBar1.Maximum = Convert.ToInt32(span.TotalDays);
                else if (_result.Interval == ArcSWAT.SWATResultIntervalType.MONTHLY)
                    trackBar1.Maximum = (_result.Unit.Scenario.EndYear - _result.Unit.Scenario.StartYear + 1) * 12;
                else if (_result.Interval == ArcSWAT.SWATResultIntervalType.YEARLY)
                    trackBar1.Maximum = (_result.Unit.Scenario.EndYear - _result.Unit.Scenario.StartYear + 1);

            }
        }

        private DateTime Date
        {
            get
            {
                return dateTimePicker1.Value;
            }
            set
            {
                dateTimePicker1.Value = value;

                if (_result.Interval == ArcSWAT.SWATResultIntervalType.MONTHLY && value.Day != 1)
                    dateTimePicker1.Value = new DateTime(value.Year, value.Month, 1);
                if (_result.Interval == ArcSWAT.SWATResultIntervalType.YEARLY && (value.Day != 1 || value.Month != 1))
                    dateTimePicker1.Value = new DateTime(value.Year, 1, 1);

                TimeSpan span = dateTimePicker1.Value.Subtract(dateTimePicker1.MinDate);
                if (_result.Interval == ArcSWAT.SWATResultIntervalType.DAILY)
                    trackBar1.Value = Convert.ToInt32(span.TotalDays);
                else if (_result.Interval == ArcSWAT.SWATResultIntervalType.MONTHLY)
                    trackBar1.Value = (dateTimePicker1.Value.Year - _startYear) * 12 + dateTimePicker1.Value.Month;
                else if (_result.Interval == ArcSWAT.SWATResultIntervalType.YEARLY)
                    trackBar1.Value = dateTimePicker1.Value.Year - _startYear + 1;

            }
        }
    }
}
