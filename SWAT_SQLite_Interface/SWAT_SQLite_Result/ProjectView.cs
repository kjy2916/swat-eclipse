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
    public partial class ProjectView : UserControl
    {
        public ProjectView()
        {
            InitializeComponent();
        }

        private void bLoadObservationData_Click(object sender, EventArgs e)
        {
            if (_id <= 0 || _col == null) return;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (!System.IO.File.Exists(openFileDialog1.FileName)) return;

                try
                {
                    if (_prj.Observation.loadCSV(openFileDialog1.FileName,
                        _unitType, _id, _col))
                    {
                        _prj.Observation.LoadData(_unitType, _id, _col);
                        updateTableAndChart();
                    }
                }
                catch (System.Exception ee)
                {
                    SWAT_SQLite.showInformationWindow(ee.Message);
                }
            }
        }

        public ArcSWAT.Project Project
        {
            set
            {
                _prj = value;
                subbasinMap1.setProjectScenario(value, null, _unitType);                
            }
        }

        private ArcSWAT.SWATUnitType _unitType = ArcSWAT.SWATUnitType.RCH;
        private int _id = -1;
        private ArcSWAT.Project _prj = null;
        private string _col = null;

        private void ProjectView_Load(object sender, EventArgs e)
        {
            cmbObservedColumns.SelectedIndexChanged += (ss, ee) =>
                {
                    if (cmbObservedColumns.SelectedIndex == -1) _col = null;
                    else
                        _col = cmbObservedColumns.SelectedItem.ToString();
                    updateTableAndChart();

                };
            
            subbasinMap1.onLayerSelectionChanged += (id) =>
            {
                _id = id;
                cmbObservedColumns.DataSource = null;
                cmbObservedColumns.DataSource = 
                    ArcSWAT.ObservationData.getObservationDataColumns(_unitType);
                cmbObservedColumns.SelectedIndex = 0;
            };            
        }

        private void updateTableAndChart()
        {
            tableView1.DataTable = null;
            outputDisplayChart1.ObservedData = null;

            if (_col != null && _id > 0)
            {
                ArcSWAT.SWATUnitObservationData data =
                    _prj.Observation.getObservedData(_unitType, _id, _col);
                if (data != null)
                {
                    //show the data
                    tableView1.ObservedData = data.getObservedData(-1);
                    outputDisplayChart1.ObservedData = data.getObservedData(-1);
                }
            }
        }
    }
}
