using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;

namespace SWAT_SQLite_Result
{
    public partial class TableResultsCtrl : UserControl
    {
        public TableResultsCtrl()
        {
            InitializeComponent();

            _chart = new OutputDisplayChart();
            _chart.Dock = DockStyle.Fill;
            this.tabPage2.Controls.Add(_chart);       
        }

        OutputDisplayChart _chart = null;
        Dictionary<int, ArcSWAT.SWATUnit> _units = null;
        ArcSWAT.SWATUnit _unit = null;
        ArcSWAT.SWATUnitResult _result = null;

        public Dictionary<int, ArcSWAT.SWATUnit> SWATUnits
        {
            set
            {
                _units = value;

                if (_units == null || _units.Count == 0) return;

                this.cmbIDs.Items.Clear();
                foreach (int id in _units.Keys)
                    cmbIDs.Items.Add(id.ToString());

                if (cmbIDs.Items.Count > 0)
                    cmbIDs.SelectedIndex = 0;
            }
        }

        private void TableResultsCtrl_Load(object sender, EventArgs e)
        {
            cmbIDs.SelectedIndexChanged += (s, ee) =>
            {
                _unit = _units[Convert.ToInt32(cmbIDs.SelectedItem.ToString())];                

                if (_unit == null) return;
                lblInfo.Text = _unit.ToStringBasicInfo();
                if (cmbResultTypes.Items.Count > 0) 
                {
                    _result = _unit.Results[cmbResultTypes.SelectedItem.ToString()];
                    updateResult(); 
                    return; 
                }

                cmbResultTypes.Items.Clear();
                foreach (string type in _unit.Results.Keys)
                    cmbResultTypes.Items.Add(type);

                if (cmbResultTypes.Items.Count > 0)
                    cmbResultTypes.SelectedIndex = 0;
            };

            cmbResultTypes.SelectedIndexChanged += (s, ee) =>
            {
                if (_unit == null) return;

                _result = _unit.Results[cmbResultTypes.SelectedItem.ToString()];
                if (_result == null) return;

                cmbColumns.Items.Clear();
                foreach (string col in _result.Columns)
                    cmbColumns.Items.Add(col);

                if (cmbColumns.Items.Count > 0)
                    cmbColumns.SelectedIndex = 0;

                updateResult();
            };

            cmbColumns.SelectedIndexChanged += (s, ee) => 
            { 
                //to-do change the char while columns changes
                if (_result == null) return;

                updateResult();
            };
        }

        private void updateResult()
        {
            if (_result == null) return;

            string col = cmbColumns.SelectedItem.ToString();
            StringCollection cols = new StringCollection() { col };
            DataTable dt = _result.getDataTable(col);
            dataGridView1.DataSource = dt;
            this._chart.DrawGraph(dt.Rows, ArcSWAT.SWATUnitResult.COLUMN_NAME_DATE, cols, _result.Interval);
            //lblStatistics.Text = _result.getStatistics(col).ToString();
        }
    }
}
