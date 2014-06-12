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
    public partial class SWATFileList : UserControl
    {
        public SWATFileList()
        {
            InitializeComponent();
        }

        public event EventHandler onSWATInputFileExtensionChanged = null;

        public ArcSWAT.SWATUnitType SWATUnitType
        {
            set
            {
                cmbExtension.DataSource = ArcSWAT.SWATUnit.getSWATFileExtentions(value);
            }
        }

        public string Extension
        {
            get { return cmbExtension.SelectedItem.ToString(); }
        }

        private void bOpen_Click(object sender, EventArgs e)
        {
            if (onSWATInputFileExtensionChanged != null) onSWATInputFileExtensionChanged(null, null);
        }

        private void cmbExtension_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
