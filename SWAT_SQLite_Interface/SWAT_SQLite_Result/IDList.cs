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
    public partial class IDList : UserControl
    {
        public IDList()
        {
            InitializeComponent();

            cmbIDs.SelectedIndexChanged += (s, e) => { if (onIDChanged != null) onIDChanged(null, null); };
        }

        private List<int> _ids = null;
        public event EventHandler onIDChanged = null;

        public List<int> IDs
        {
            set
            {
                cmbIDs.Items.Clear();
                _ids = null;

                if (value == null || value.Count == 0) return;

                cmbIDs.DataSource = value;
                _ids = value;
            }
        }

        public int ID 
        { 
            get 
            {
                if (cmbIDs.SelectedIndex >= 0 && _ids != null &&
                    cmbIDs.SelectedIndex < _ids.Count)
                    return _ids[cmbIDs.SelectedIndex];
                return -1;
            }
            set
            {
                if (_ids == null) return;
                EventHandler handler = onIDChanged;
                onIDChanged = null;
                cmbIDs.SelectedIndex = _ids.IndexOf(value);
                onIDChanged = handler;
            }
        }
    }
}
