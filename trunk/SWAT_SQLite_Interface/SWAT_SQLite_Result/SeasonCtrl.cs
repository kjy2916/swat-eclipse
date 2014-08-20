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
    public partial class SeasonCtrl : UserControl
    {
        public event EventHandler onSeasonTypeChanged = null;

        public SeasonCtrl()
        {
            InitializeComponent();

            rdbWholeYear.Checked = true;
            rdbWholeYear.CheckedChanged += (ss,ee) =>{if(onSeasonTypeChanged != null) onSeasonTypeChanged(this,new EventArgs());};
            rdbSnowMelt.CheckedChanged += (ss,ee) =>{if(onSeasonTypeChanged != null) onSeasonTypeChanged(this,new EventArgs());};
            rdbGrowingSeason.CheckedChanged += (ss, ee) => { if (onSeasonTypeChanged != null) onSeasonTypeChanged(this, new EventArgs()); };
            rdbHydrologicalYear.CheckedChanged += (ss, ee) => { if (onSeasonTypeChanged != null) onSeasonTypeChanged(this, new EventArgs()); };

        }

        public ArcSWAT.SeasonType Season
        {
            get
            {
                if (rdbSnowMelt.Checked) return ArcSWAT.SeasonType.SnowMelt;
                if (rdbGrowingSeason.Checked) return ArcSWAT.SeasonType.GrowingSeason;
                if (rdbHydrologicalYear.Checked) return ArcSWAT.SeasonType.HydrologicalYear;
                return ArcSWAT.SeasonType.WholeYear;
            }
        }
    }
}
