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
    /// <summary>
    /// List R2 and NSE for all HRUs, Subbasins and Reaches in table
    /// May take a little bit longer for big model.
    /// </summary>
    public partial class ScenarioComparasionReportView : UserControl
    {
        public ScenarioComparasionReportView()
        {
            InitializeComponent();
        }
    }
}
