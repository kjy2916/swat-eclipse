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

        private ArcSWAT.ScenarioResult _scenario = null;
        private ArcSWAT.SWATModelType _modelType = ArcSWAT.SWATModelType.UNKNOWN;
        private ArcSWAT.SWATUnitType _unitType = ArcSWAT.SWATUnitType.UNKNOWN;
        private Dictionary<string, UserControl> _views = new Dictionary<string, UserControl>();

        private void SWATSQLiteFrm_Load(object sender, EventArgs e)
        {
            projectTree1.onResultLevelChanged += (scenario, modelType, type) =>
                {
                    switchView(scenario, modelType, type);
                };
        }

        private UserControl switchView(ArcSWAT.ScenarioResult scenario, ArcSWAT.SWATModelType modelType, ArcSWAT.SWATUnitType unitType)
        {

            UserControl view = getView(scenario, modelType, unitType);
            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel2.Controls.Add(view);

            _scenario = scenario;
            _modelType = modelType;
            _unitType = unitType;

            return view;
        }        

        private ArcSWAT.Project _prj = null;

        private UserControl getView(ArcSWAT.ScenarioResult scenario, ArcSWAT.SWATModelType modelType, ArcSWAT.SWATUnitType unitType)
        {
            string key = string.Format("{0}_{1}_{2}",
                scenario.Scenario.Name,modelType,unitType);
            if (!_views.ContainsKey(key))
            {
                if (unitType == ArcSWAT.SWATUnitType.WSHD)
                {
                    WatershedView view = new WatershedView();
                    view.Dock = DockStyle.Fill;
                    view.setProjectScenario(_prj, scenario);

                    _views.Add(key, view);
                }
                else
                {
                    SubbasinView view = new SubbasinView();
                    view.Dock = DockStyle.Fill;
                    view.setProjectScenario(_prj,scenario,unitType);

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

        
        private void bOpen_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = Properties.Settings.Default.PreviousProjectFolder;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                _prj = new ArcSWAT.Project(folderBrowserDialog1.SelectedPath);
                if (!_prj.IsValid)
                {
                    _prj = null;
                    System.Windows.Forms.MessageBox.Show(folderBrowserDialog1.SelectedPath + " is not a valid ArcSWAT project folder.");
                    return;
                }

                projectTree1.Project = _prj;

                //save current path
                Properties.Settings.Default.PreviousProjectFolder = folderBrowserDialog1.SelectedPath;
                Properties.Settings.Default.Save();
            }
        }
    }
}
