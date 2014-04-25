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
    public partial class YearCtrl : UserControl
    {
        public event EventHandler onYearChanged;
        public event EventHandler onYearDisplayTypeChanged;

        public YearCtrl()
        {
            InitializeComponent();

            tbYear.Scroll +=
            (ss, _e) =>
            {
                toolTip1.SetToolTip(tbYear, tbYear.Value.ToString());

                if (onYearChanged != null) onYearChanged(this, new EventArgs());
            };

            rdbEachYear.CheckedChanged += (ss, e) => { tbYear.Enabled = DisplayByYear; if (onYearDisplayTypeChanged != null) onYearDisplayTypeChanged(this, new EventArgs()); };
         }

        public ArcSWAT.ScenarioResult Scenario
        {
            set
            {
                tbYear.Minimum = value.StartYear;
                tbYear.Maximum = value.EndYear;
                tbYear.Value = value.StartYear;

                rdbEachYear.Checked = true;
            }
        }

        public bool DisplayByYear { get { return rdbEachYear.Checked; } }
        public int Year { get { return tbYear.Value; } }
    }
}
