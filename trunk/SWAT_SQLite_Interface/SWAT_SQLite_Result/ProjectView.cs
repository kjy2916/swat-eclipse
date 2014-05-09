using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DotSpatial.Symbology;
using DotSpatial.Controls;

namespace SWAT_SQLite_Result
{
    /// <summary>
    /// TODO
    /// 1. Add a list to show all the reaches, reservoirs which has observed data.
    /// 2. Add reservoir layer to select
    /// 3. Set selection color as read and same width with unselect feature.
    /// </summary>
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
                        updateTableAndChart();
                        subbasinMap1.updateObservedStatus(_unitType, _id);
                        SWAT_SQLite.showInformationWindow(
                            string.Format("Data is successfully loaded to {0} {1}.", _unitType, _id));

                    }
                }
                catch (System.Exception ee)
                {
                    SWAT_SQLite.showInformationWindow(ee.Message);
                }
            }
        }

        private void bDeleteObservationData_Click(object sender, EventArgs e)
        {
            if (_col != null && _id > 0 && tableView1.DataSource != null)
            {
                try
                {
                    if (System.Windows.Forms.MessageBox.Show(
                        string.Format("Do you really want to delete the observed data for {0} {1} {2}?", _unitType, _id, _col),
                        SWAT_SQLite.NAME, MessageBoxButtons.YesNo) == DialogResult.No) return;

                    if (_prj.Observation.delete(_unitType, _id, _col))
                    {
                        updateTableAndChart();
                        subbasinMap1.updateObservedStatus(_unitType, _id);
                        SWAT_SQLite.showInformationWindow(
                            string.Format("Data for {0} {1} is successfully deleted.", _unitType, _id));
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
                subbasinMap1.setProject(value);   
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
            
            subbasinMap1.onLayerSelectionChanged += (unitType, id) =>
            {
                _id = id;
                _unitType = unitType;
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

        public DotSpatial.Controls.Map Map { get { return subbasinMap1; } }


    }
}
