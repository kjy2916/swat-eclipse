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
    public partial class ProjectView : UserControl
    {
        public ProjectView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Happens when new feature is selected
        /// </summary>
        public event EventHandler onMapSelectionChanged = null;

        /// <summary>
        /// Happens when statistic information is changed
        /// </summary>
        public event EventHandler onDataStatisticsChanged = null;

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
                        _observedData = _prj.Observation.getObservedData(_unitType, _id, _col);
                        if (_observedData != null) yearCtrl1.ObservedData = _observedData.getObservedData(-1); //update year control
                        
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
                        yearCtrl1.ObservedData = null;
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
                yearCtrl1.ObservedData = null;
            }
        }

        private ArcSWAT.SWATUnitType _unitType = ArcSWAT.SWATUnitType.RCH;
        private int _id = -1;
        private ArcSWAT.Project _prj = null;
        private string _col = null;
        private int _year = -1;

        private void ProjectView_Load(object sender, EventArgs e)
        {
            this.Resize += (ss, ee) => { this.splitContainer1.SplitterDistance = this.Width - 200; };

            cmbObservedColumns.SelectedIndexChanged += (ss, ee) =>
                {
                    if (cmbObservedColumns.SelectedIndex == -1) _col = null;
                    else
                        _col = ArcSWAT.ObservationData.getObservationSWATColumn(cmbObservedColumns.SelectedItem.ToString());
                    updateTableAndChart();

                };
            
            subbasinMap1.onLayerSelectionChanged += (unitType, id) =>
            {
                cmbObservedColumns.DataSource = null;
                updateTableAndChart();

                if (id <= 0) return;

                _id = id;
                _unitType = unitType;                
                cmbObservedColumns.DataSource = 
                    ArcSWAT.ObservationData.getObservationDataColumns(_unitType);
                cmbObservedColumns.SelectedIndex = 0;
                if (onMapSelectionChanged != null) onMapSelectionChanged(this, new EventArgs());
                 
            };

            yearCtrl1.onYearChanged += (ss, ee) => { _year = yearCtrl1.Year; updateTableAndChart(); };
        }


        private ArcSWAT.SWATUnitObservationData _observedData = null;
        public ArcSWAT.SWATUnitObservationData MapSelection { get { return _observedData; } }

        private string _statistics = "";
        public string Statistics { get { return _statistics; } }

        private void updateTableAndChart()
        {
            tableView1.DataTable = null;
            outputDisplayChart1.ObservedData = null;
            yearCtrl1.ObservedData = null;
            _observedData = null;
            _statistics = "";

            if (_col != null && _id > 0)
            {
                _observedData = _prj.Observation.getObservedData(_unitType, _id, _col);
                if (_observedData != null)
                {
                    //show the data
                    ArcSWAT.SWATUnitColumnYearObservationData observedData = _observedData.getObservedData(_year);                    
                    tableView1.ObservedData = observedData;
                    outputDisplayChart1.ObservedData = observedData;
                    yearCtrl1.ObservedData = _observedData.getObservedData(-1);

                    if (onDataStatisticsChanged != null)
                    {
                        _statistics = observedData.Statistics.ToString();
                        onDataStatisticsChanged(this, new EventArgs());
                    }                        
                }
            }
        }

        public DotSpatial.Controls.Map Map { get { return subbasinMap1; } }


    }
}
