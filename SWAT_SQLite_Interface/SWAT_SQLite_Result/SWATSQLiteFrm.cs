using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SWAT_SQLite_Result
{
    public partial class SWATSQLiteFrm : Form
    {
        public SWATSQLiteFrm()
        {
            InitializeComponent();
        }

        private ArcSWAT.Scenario _scenario = null;
        private ArcSWAT.SWATModelType _modelType = ArcSWAT.SWATModelType.UNKNOWN;
        private ArcSWAT.SWATUnitType _unitType = ArcSWAT.SWATUnitType.UNKNOWN;
        private Dictionary<string, UserControl> _views = new Dictionary<string, UserControl>();
        private ProjectView _projectView = null;

        private void SWATSQLiteFrm_Load(object sender, EventArgs e)
        {
            projectTree1.onResultLevelChanged += (scenario, modelType, type) =>
                {
                    switchView(scenario.Scenario, modelType, type);
                };
            projectTree1.onScenarioSelectionChanged += (scenario) => {updateScenarioView(scenario);};
            projectTree1.onProjectNodeSelected += (ss, ee) => { updateProjectView(); };
            projectTree1.onDifferenceNodeSelected += (ss, ee) =>
                {
                    ScenarioComparasionReportView compareView = new ScenarioComparasionReportView();
                    compareView.Result = projectTree1.ScenarioResult;
                    compareView.Dock = DockStyle.Fill;

                    splitContainer1.Panel2.Controls.Clear();
                    splitContainer1.Panel2.Controls.Add(compareView);
                    updateStatus(compareView);
                    Map = null;
                };
            projectTree1.onPerformanceNodeSelected += (ss, ee) =>
            {
                PerformanceView performanceView = new PerformanceView();
                performanceView.Result = projectTree1.ScenarioResult;
                performanceView.Dock = DockStyle.Fill;

                splitContainer1.Panel2.Controls.Clear();
                splitContainer1.Panel2.Controls.Add(performanceView);
                updateStatus(performanceView);
                Map = null;
            };


            if (Properties.Settings.Default.Projects == null)
                Properties.Settings.Default.Projects = new System.Collections.Specialized.StringCollection();
 
            foreach (string p in Properties.Settings.Default.Projects)
                cmbProjects.Items.Add(p);

            cmbProjects.SelectedIndexChanged += (ss, ee) => { openProject(cmbProjects.SelectedItem.ToString()); };
        
            Map = null;
        }

        private void updateProjectView()
        {
            if (_projectView == null)
            {
                _projectView = new ProjectView();
                _projectView.Dock = DockStyle.Fill;
                _projectView.Project = _prj;
                _projectView.onMapSelectionChanged += (sss, eee) => { onMapSelectionChanged(_projectView); };
                _projectView.onDataStatisticsChanged += (sss, eee) => { onDataStatisticsChanged(_projectView); };
            }
            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel2.Controls.Add(_projectView);
            updateStatus(_projectView);
            Map = _projectView.Map;
        }

        private void updateScenarioView(ArcSWAT.Scenario scenario)
        {
            if (scenario == null)
            {
                splitContainer1.Panel2.Controls.Clear();
                return;
            }

            ScenarioView view = new ScenarioView();
            view.Scenario = scenario;  
            view.Dock = DockStyle.Fill;
            view.onSimulationFinished += (ArcSWAT.SWATModelType modelType) =>
            {
                removeView(scenario, modelType);
                //scenario.reReadResults(modelType);
                projectTree1.update(scenario, modelType);
            };        

            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel2.Controls.Add(view);
            updateStatus(view);
            Map = null;
        }

        private UserControl switchView(ArcSWAT.Scenario scenario, ArcSWAT.SWATModelType modelType, ArcSWAT.SWATUnitType unitType)
        {
            UserControl view = getView(scenario, modelType, unitType);
            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel2.Controls.Add(view);

            if (view is SubbasinView)
                Map = (view as SubbasinView).Map;
            else
                Map = null;

            _scenario = scenario;
            _modelType = modelType;
            _unitType = unitType;

            //change status
            updateStatus(view);

            return view;
        }
        
        

        private ArcSWAT.Project _prj = null;

        private void removeView(ArcSWAT.Scenario scenario, ArcSWAT.SWATModelType modelType)
        {
            removeView(scenario, modelType, ArcSWAT.SWATUnitType.WSHD);
            removeView(scenario, modelType, ArcSWAT.SWATUnitType.HRU);
            removeView(scenario, modelType, ArcSWAT.SWATUnitType.SUB);
            removeView(scenario, modelType, ArcSWAT.SWATUnitType.RCH);
        }

        private void removeView(ArcSWAT.Scenario scenario, ArcSWAT.SWATModelType modelType, ArcSWAT.SWATUnitType unitType)
        {
            string key = getViewName(scenario, modelType, unitType);
            if (_views.ContainsKey(key)) _views.Remove(key);
        }

        private string getViewName(ArcSWAT.Scenario scenario, ArcSWAT.SWATModelType modelType, ArcSWAT.SWATUnitType unitType)
        {
            return string.Format("{0}_{1}_{2}",
                scenario.Name, modelType, unitType);
        }

        private void updateStatus(UserControl view)
        {
            //change status
            onMapTimeChanged(view);
            onMapSelectionChanged(view);
            onDataStatisticsChanged(view);
        }

        private void onMapTimeChanged(UserControl view) 
        {
            if (view != null && view is SubbasinView)
                lblMapTime.Text = string.Format("Map Display Time: {0:yyyy-MM-dd}", (view as SubbasinView).MapTime);
            else
                lblMapTime.Text = "Map Display Time:";
        }

        private void onMapSelectionChanged(UserControl view)
        {
            if (view != null && view is SubbasinView)
            {
                if ((view as SubbasinView).MapSelection == null)
                    lblSelectionInformation.Text = "No Selection";
                else
                    lblSelectionInformation.Text = (view as SubbasinView).MapSelection.ToStringBasicInfo();
            }
            else if (view == _projectView)
            {
                if (_projectView.MapSelection == null) lblSelectionInformation.Text = "No Selection";
                else lblSelectionInformation.Text = _projectView.MapSelection.ToString();
            }
            else
                lblSelectionInformation.Text = "No Selection";
        }

        private void onDataStatisticsChanged(UserControl view)
        {
            if (view != null && view is SubbasinView)
                lblStatistics.Text = (view as SubbasinView).Statistics;
            else if (view != null && view is WatershedView)
                lblStatistics.Text = (view as WatershedView).Statistics;
            else if (view != null && view is ProjectView)
                lblStatistics.Text = (view as ProjectView).Statistics;
            else
                lblStatistics.Text = "No Statistics Data Available";
        }

        private UserControl getView(ArcSWAT.Scenario scenario, ArcSWAT.SWATModelType modelType, ArcSWAT.SWATUnitType unitType)
        {
            string key = getViewName(scenario, modelType, unitType);
            if (!_views.ContainsKey(key))
            {
                if (unitType == ArcSWAT.SWATUnitType.WSHD)
                {
                    WatershedView view = new WatershedView();
                    view.Dock = DockStyle.Fill;
                    view.setProjectScenario(_prj, scenario.getModelResult(modelType));
                    view.onDataStatisticsChanged += (ss, ee) => { onDataStatisticsChanged(view); };

                    _views.Add(key, view);
                }
                else
                {
                    SubbasinView view = new SubbasinView();
                    view.Dock = DockStyle.Fill;

                    view.onMapTimeChanged += (ss, ee) => { onMapTimeChanged(view); };
                    view.onMapSelectionChanged += (ss, ee) => { onMapSelectionChanged(view); };
                    view.onDataStatisticsChanged += (ss, ee) => { onDataStatisticsChanged(view); };

                    view.setProjectScenario(_prj, scenario.getModelResult(modelType), unitType);

                    if (unitType == ArcSWAT.SWATUnitType.SUB)
                        view.onSwitch2HRU += (hru) => 
                        {
                            SubbasinView hruview = switchView(_scenario, _modelType, ArcSWAT.SWATUnitType.HRU) as SubbasinView;
                            hruview.HRU = hru;
                        };
                    _views.Add(key, view);
                }                
            }
            return _views[key];
        }

        private void openProject(string prjPath)
        {
            if (_prj != null && prjPath.Equals(_prj.Folder)) return;

            _prj = new ArcSWAT.Project(prjPath);
            if (!_prj.IsValid)
            {
                _prj = null;

                //remove the path if it's not there anymore
                if (Properties.Settings.Default.Projects.Contains(prjPath))
                {
                    Properties.Settings.Default.Projects.Remove(prjPath);
                    Properties.Settings.Default.Save();
                    cmbProjects.Items.Remove(prjPath);
                }
                System.Windows.Forms.MessageBox.Show(prjPath + " is not a valid ArcSWAT project folder.");
                return;
            }

            projectTree1.Project = _prj;
            
            //clear views            
            _views.Clear();
            _projectView = null;

            //see what view is currently used
            if (splitContainer1.Panel2.Controls.Count > 0)
            {
                Control currentView = splitContainer1.Panel2.Controls[0];
                if (currentView is ProjectView)                   
                    updateProjectView();
                else if (currentView is ScenarioView)
                {
                    if (_prj.Scenarios.Count > 0)
                        updateScenarioView(_prj.Scenarios.First().Value);
                    else
                        updateScenarioView(null);
                }
                else
                    splitContainer1.Panel2.Controls.Clear();                    
            }

            //save current path
            Properties.Settings.Default.PreviousProjectFolder = prjPath;
            if (!Properties.Settings.Default.Projects.Contains(prjPath))
            {
                Properties.Settings.Default.Projects.Add(prjPath);
                cmbProjects.Items.Add(prjPath);
                cmbProjects.SelectedIndex = cmbProjects.Items.Count - 1;
            }

            Properties.Settings.Default.Save();
        }
        
        private void bOpen_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = Properties.Settings.Default.PreviousProjectFolder;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK) openProject(folderBrowserDialog1.SelectedPath);
        }

        private DotSpatial.Controls.Map _map = null;

        private DotSpatial.Controls.Map Map
        {
            get { return _map; }
            set
            {
                _map = value;

                bPan.Enabled = _map != null;
                bZoomIn.Enabled = _map != null;
                bZoomOut.Enabled = _map != null;
                bZoomExtent.Enabled = _map != null;
                bSelect.Enabled = _map != null;
            }
        }

        private void bPan_Click(object sender, EventArgs e)
        {
            if (Map != null) Map.FunctionMode = DotSpatial.Controls.FunctionMode.Pan;
        }

        private void bZoomIn_Click(object sender, EventArgs e)
        {
            if (Map != null) Map.FunctionMode = DotSpatial.Controls.FunctionMode.ZoomIn;
        }

        private void bZoomOut_Click(object sender, EventArgs e)
        {
            if (Map != null) Map.FunctionMode = DotSpatial.Controls.FunctionMode.ZoomOut;
        }

        private void bZoomExtent_Click(object sender, EventArgs e)
        {
            if (Map != null) Map.ZoomToMaxExtent();
        }

        private void bSelect_Click(object sender, EventArgs e)
        {
            if (Map != null) Map.FunctionMode = DotSpatial.Controls.FunctionMode.Select;
        }
    }
}
