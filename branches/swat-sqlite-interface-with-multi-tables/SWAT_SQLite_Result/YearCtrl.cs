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

        public YearCtrl()
        {
            InitializeComponent();

            tbYear.Scroll +=
            (ss, _e) =>
            {
                toolTip1.SetToolTip(tbYear, tbYear.Value.ToString());

                if (onYearChanged != null) onYearChanged(this, new EventArgs());
            };

            rdbEachYear.CheckedChanged += (ss, e) => { tbYear.Enabled = DisplayByYear; if (onYearChanged != null) onYearChanged(this, new EventArgs()); };
            rdbAllYears.CheckedChanged += (ss, e) => { tbYear.Enabled = DisplayByYear; if (onYearChanged != null) onYearChanged(this, new EventArgs()); };
           
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

        private ArcSWAT.SWATUnitColumnYearObservationData _observedData = null;

        /// <summary>
        /// For used in project view
        /// </summary>
        public ArcSWAT.SWATUnitColumnYearObservationData ObservedData
        {
            set
            {
                this.Enabled = value != null;
                if (value == null) return;

                //don't change when same observed data is displayed
                if (_observedData != null && 
                    _observedData.UnitID == value.UnitID && //same unit id
                    _observedData.Column == value.Column && //same column
                    _observedData.UnitType == value.UnitType) return; //same unit type
                
                _observedData = value;
                tbYear.Minimum = value.FirstDay.Year;
                tbYear.Maximum = value.LastDay.Year;
                tbYear.Value = value.FirstDay.Year;
                rdbAllYears.Checked = true;              
            }
        }

        public bool DisplayByYear { get { return rdbEachYear.Checked; } }
        public int Year { get { if (DisplayByYear) return tbYear.Value; return -1; } }
    }
}
