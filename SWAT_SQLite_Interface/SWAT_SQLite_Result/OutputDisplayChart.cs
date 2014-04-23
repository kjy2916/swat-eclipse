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
        private ChartArea _chartArea;   //used to change Y title

        public OutputDisplayChart()
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

        private void setChartArea(DataRowCollection rows, ArcSWAT.SWATResultIntervalType interval)
        {
            if (interval == ArcSWAT.SWATResultIntervalType.MONTHLY) //monthly
            {
                _chartArea.AxisX.Title = "Time (monthly)";
                if (rows.Count == 12) //for one year
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
                _chartArea.AxisX.LabelStyle.Angle = -45;

                if (rows.Count == 365 || rows.Count == 366) //for one year
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
        }

        public void DrawGraph(DataRowCollection rows, string xColName, StringCollection yColNames, ArcSWAT.SWATResultIntervalType interval)
        {
            foreach (Series line in this.Series)
                line.Points.Clear();
            _chartArea.AxisY.Title = "";

            if (rows == null || rows.Count == 0) return;
            if(yColNames == null || yColNames.Count == 0) return;
            if (interval == ArcSWAT.SWATResultIntervalType.UNKNOWN) return;

            this.DataSource = rows;
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
                    line.ToolTip = "#VALY(#VALX{yyyy/MM})";
                    //if (rows.Length == 12) //for one year
                    //    line.IsValueShownAsLabel = true;                    
                    //else
                    //     line.IsValueShownAsLabel = false;
                }
                else if (interval == ArcSWAT.SWATResultIntervalType.DAILY) //daily
                {
                    line.ToolTip = "#VALY(#VALX{yyyy-MM-dd})"; 
                    //line.IsValueShownAsLabel = false;                             
                }
                index++;
            }
            setChartArea(rows, interval);

            this.DataBind();
        }
    }
}
