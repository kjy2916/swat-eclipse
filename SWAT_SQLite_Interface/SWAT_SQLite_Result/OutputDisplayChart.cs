using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data;
using System.Drawing;

namespace SWAT_SQLite_Result
{
    class OutputDisplayChart : Chart
    {
        private ChartArea _chartArea = null;   //used to change Y title

        private Series getLine(int index)
        {
            string seriesName = string.Format("data_{0}", index);
            if (this.Series.IndexOf(seriesName) == -1)
            {
                Series line = this.Series.Add(seriesName);
                line.ChartType = SeriesChartType.Line;
                line.ChartArea = "chart_area";
                line.MarkerStyle = MarkerStyle.Diamond;
                line.MarkerSize = 10;
                line.Color = Color.Blue;
                if (index == 0)
                    line.Color = Color.Red;
                line.BorderWidth = 2;
                line.IsValueShownAsLabel = false;
                line.IsVisibleInLegend = false;
                line.XValueType = ChartValueType.DateTime;
            }
            return this.Series[seriesName];
        }

        public event EventHandler onExport;

        private void setChartArea(DataTable dt, ArcSWAT.SWATResultIntervalType interval)
        {
            if (interval == ArcSWAT.SWATResultIntervalType.MONTHLY) //monthly
            {
                _chartArea.AxisX.Title = "Time (monthly)";
                if (dt.Rows.Count == 12) //for one year
                {
                    _chartArea.AxisX.LabelStyle.Format = "yyyy/MM";
                    _chartArea.AxisX.LabelStyle.Angle = 0;

                    _chartArea.AxisX.MajorTickMark.Interval = 1;
                    _chartArea.AxisX.MajorTickMark.IntervalType = DateTimeIntervalType.Months;
                }
                else
                {
                    _chartArea.AxisX.LabelStyle.Format = "yyyy";
                    _chartArea.AxisX.LabelStyle.Angle = 0;

                    _chartArea.AxisX.MajorTickMark.Interval = 1; //half a year
                    _chartArea.AxisX.MajorTickMark.IntervalType = DateTimeIntervalType.Years;
                }
            }
            else if (interval == ArcSWAT.SWATResultIntervalType.DAILY) //daily
            {
                _chartArea.AxisX.Title = "Time (daily)";
                _chartArea.AxisX.LabelStyle.Format = "yyyy-MM-dd";
                //_chartArea.AxisX.LabelStyle.Angle = -45;

                if (dt.Rows.Count == 365 || dt.Rows.Count == 366) //for one year
                {
                    _chartArea.AxisX.MajorTickMark.Interval = 1;
                    _chartArea.AxisX.MajorTickMark.IntervalType = DateTimeIntervalType.Months;
                }
                else//all year
                {
                    _chartArea.AxisX.MajorTickMark.Interval = 1; //half a year
                    _chartArea.AxisX.MajorTickMark.IntervalType = DateTimeIntervalType.Years;
                }
            }
            else //yearly
            {
                _chartArea.AxisX.Title = "Year";
                _chartArea.AxisX.LabelStyle.Format = "yyyy";
                _chartArea.AxisX.LabelStyle.Angle = 0;

                _chartArea.AxisX.MajorTickMark.Interval = 1; //half a year
                _chartArea.AxisX.MajorTickMark.IntervalType = DateTimeIntervalType.Years;
            }
        }

        public ArcSWAT.SWATUnitColumnYearCompareResult CompareResult
        {
            set
            {
                if (value == null) { clear(); return; }
                DrawGraph(value.SeasonTable(Season), ArcSWAT.SWATUnitResult.COLUMN_NAME_DATE,
                    value.ChartColumns, value.Interval);
            }

        }

        public ArcSWAT.SWATUnitColumnYearResult Result
        {
            set
            {
                if (value == null) { clear(); return; }
                StringCollection cols = new StringCollection() { value.Column };
                DrawGraph(value.SeasonTable(Season), ArcSWAT.SWATUnitResult.COLUMN_NAME_DATE,
                    cols, value.UnitResult.Interval);
            }
        }

        public ArcSWAT.SWATUnitColumnYearObservationData ObservedData
        {
            set
            {
                if (value == null) { clear(); return; }
                StringCollection cols = new StringCollection() { value.Column };
                DrawGraph(value.Table, ArcSWAT.SWATUnitResult.COLUMN_NAME_DATE,
                    cols, ArcSWAT.SWATResultIntervalType.DAILY);
            }
        }

        private ArcSWAT.SeasonType _season = ArcSWAT.SeasonType.WholeYear;

        /// <summary>
        /// set the season type
        /// </summary>
        public ArcSWAT.SeasonType Season { set { _season = value; } get { return _season; } }

        public void clear()
        {
            foreach (Series line in this.Series)
            {
                line.Points.Clear();
                line.XValueMember = "";
                line.YValueMembers = "";
            }
            if(_chartArea != null)
                _chartArea.AxisY.Title = "";

            this.DataSource = null;
        }

        private void DrawGraph(DataTable dt, string xColName, StringCollection yColNames, ArcSWAT.SWATResultIntervalType interval)
        {
            if (_chartArea == null)
            {
                this.ChartAreas.Clear();
                this.Series.Clear();
                this.Titles.Clear();

                _chartArea = this.ChartAreas.Add("chart_area");
                _chartArea.AxisY.Title = "y";
                _chartArea.AxisX.MajorGrid.Enabled = false;
                _chartArea.AxisY.MajorGrid.Enabled = false;
                _chartArea.AxisX.MajorTickMark.TickMarkStyle = TickMarkStyle.AcrossAxis;

                //context menu
                System.Windows.Forms.ToolStripMenuItem exportMenu =
                    new System.Windows.Forms.ToolStripMenuItem("Export current results to CSV");
                exportMenu.Click +=
                    (ss, _e) =>
                    {
                        if (onExport != null)
                            onExport(this, new EventArgs());
                    };

                this.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
                this.ContextMenuStrip.Items.Add(exportMenu);
            }

            clear();

            if (dt.Rows == null || dt.Rows.Count == 0) return;
            if(yColNames == null || yColNames.Count == 0) return;
            if (interval == ArcSWAT.SWATResultIntervalType.UNKNOWN) return;

            this.DataSource = dt.Rows;
            if(yColNames.Count == 1)
                _chartArea.AxisY.Title = yColNames[0];

            int index = 0;
            foreach (string yColName in yColNames)
            {
                Series line = getLine(index);
                line.XValueMember = xColName;
                line.YValueMembers = yColName;
                line.LegendText = yColName;

                if (interval == ArcSWAT.SWATResultIntervalType.MONTHLY) //monthly
                {
                    line.ToolTip = "#VALY{F4}(#VALX{yyyy/MM})";
                    //if (rows.Length == 12) //for one year
                    //    line.IsValueShownAsLabel = true;                    
                    //else
                    //     line.IsValueShownAsLabel = false;
                }
                else if (interval == ArcSWAT.SWATResultIntervalType.DAILY) //daily
                {
                    line.ToolTip = "#VALY{F4}(#VALX{yyyy-MM-dd})";
                    //line.IsValueShownAsLabel = false;                             
                }
                else //yearly
                {
                    line.ToolTip = "#VALY{F4}(#VALX{yyyy})";
                }
                if (yColNames.Count > 1)
                    line.ToolTip = yColName + ":" + line.ToolTip;

                if (index == 0)
                    line.Color = System.Drawing.Color.Red;
                else
                    line.Color = System.Drawing.Color.Green;

                index++;
            }
            setChartArea(dt, interval);

            this.DataBind();
        }
    }
}
