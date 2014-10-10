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
    public partial class IntervalCtrl : UserControl
    {
        public IntervalCtrl()
        {
            InitializeComponent();

            rdbDaily.Checked = true;

            rdbDaily.CheckedChanged += (s,e) =>{changed();};
            rdbMonthly.CheckedChanged += (s, e) => { changed(); };
            rdbYearly.CheckedChanged += (s, e) => { changed(); };
        }

        private void changed()
        {
            if (onIntervalChanged != null) onIntervalChanged(this, null);
        }

        public event EventHandler onIntervalChanged = null;

        public ArcSWAT.SWATResultIntervalType Interval
        {
            get
            {
                if (rdbDaily.Checked) return ArcSWAT.SWATResultIntervalType.DAILY;
                if (rdbMonthly.Checked) return ArcSWAT.SWATResultIntervalType.MONTHLY;
                if (rdbYearly.Checked) return ArcSWAT.SWATResultIntervalType.YEARLY;
                return ArcSWAT.SWATResultIntervalType.UNKNOWN;
            }
        }
    }
}
