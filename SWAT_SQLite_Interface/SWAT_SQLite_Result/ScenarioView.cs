using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace SWAT_SQLite_Result
{
    public delegate void SimulationFinishedEventHandler(ArcSWAT.SWATModelType modelType, ArcSWAT.SWATResultIntervalType interval);

    public partial class ScenarioView : UserControl
    {
        public ScenarioView()
        {
            InitializeComponent();
        }

        private ArcSWAT.Scenario _scenario = null;
        public event SimulationFinishedEventHandler onSimulationFinished = null;

        public ArcSWAT.Scenario Scenario
        {
            set
            {
                _scenario = value;
                updateSimulationTime();
            }
        }

        private void RunSWAT(ArcSWAT.SWATModelType modelType, ArcSWAT.SWATResultIntervalType interval)
        {
            if (_scenario == null) return;
            if (modelType == ArcSWAT.SWATModelType.UNKNOWN) return;
            if (interval == ArcSWAT.SWATResultIntervalType.UNKNOWN) return;
            if (_scenario.getModelResult(modelType,interval).Status == ArcSWAT.ScenarioResultStatus.NORMAL)
                if (MessageBox.Show("There is a pre-generated model result. Do you want to overwrite?", SWAT_SQLite.NAME, MessageBoxButtons.YesNoCancel) != DialogResult.Yes) return;

            //find the corresponding executables
            string swatexe = SWAT_SQLite.InstallationFolder + @"swat_exes\" + ArcSWAT.ScenarioResultStructure.getSWATExecutableName(modelType);
         
            if (!System.IO.File.Exists(swatexe))
            {
                SWAT_SQLite.showInformationWindow("Can't find " + swatexe);
                return;
            }

            //change output interval
            _scenario.modifyOutputInterval(interval);

            //start to run the model
            Process myProcess = new Process();
            try
            {
                myProcess.EnableRaisingEvents = true;
                myProcess.StartInfo.UseShellExecute = false;
                myProcess.StartInfo.FileName = swatexe;
                myProcess.StartInfo.CreateNoWindow = true;
                myProcess.StartInfo.RedirectStandardError = true;
                myProcess.StartInfo.RedirectStandardOutput = true;
                myProcess.StartInfo.WorkingDirectory = _scenario.ModelFolder;
                myProcess.OutputDataReceived += (sender, agrs) =>
                {
                    if (agrs.Data != null) updateMessage(agrs.Data);

                };
                myProcess.ErrorDataReceived += (sender, agrs) =>
                {
                    if (agrs.Data != null) updateMessage(agrs.Data);

                };
                myProcess.Exited += (send, agrs) =>
                    {
                        //update the results
                        if (onSimulationFinished != null)
                            onSimulationFinished(modelType,interval);

                        //update the date time of the result
                        //must be called after onSimulationFinished as the result status is updated in onSimulationFinished
                        updateSimulationTime();
                    };
                updateMessage("Runing " + ModelType.ToString() + " in " + _scenario.ModelFolder);
                myProcess.Start();
                myProcess.BeginOutputReadLine();
                //myProcess.WaitForExit();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        private delegate void valueDelegate(string msg);

        private void updateMessage(string msg)
        {
            if (this.richTextBox1.InvokeRequired)
            {
                this.richTextBox1.BeginInvoke(new valueDelegate(updateMessage), msg);
            }
            else
            {
                this.richTextBox1.AppendText(msg);
                this.richTextBox1.AppendText("\n");
            }
        }

        private void updateSimulationTime(string msg)
        {
            if (this.lblSimulationTime.InvokeRequired)
            {
                this.lblSimulationTime.BeginInvoke(new valueDelegate(updateSimulationTime), msg);
            }
            else
            {
                this.lblSimulationTime.Text = msg;
            }
        }

        private void bOpenModelFolder_Click(object sender, EventArgs e)
        {
            if (_scenario == null) return;
            Process.Start(_scenario.ModelFolder);
        }

        private void updateSimulationTime()
        {
            if (_modelType == ArcSWAT.SWATModelType.UNKNOWN) return;

            //_scenario.reReadResults(_modelType);
            updateSimulationTime(_scenario.getResultStatus(_modelType));
        }

        private void ScenarioView_Load(object sender, EventArgs e)
        {
            cmbModelType.SelectedIndexChanged += (ss, ee) => { _modelType = ModelType; updateSimulationTime(); };

            //add models based on executables
            cmbModelType.Items.Clear();
            for (int i = Convert.ToInt32(ArcSWAT.SWATModelType.SWAT_488); i <= Convert.ToInt32(ArcSWAT.SWATModelType.CanSWAT); i++)
            {
                ArcSWAT.SWATModelType modelType = (ArcSWAT.SWATModelType)i;

                string swatexe = SWAT_SQLite.InstallationFolder + @"swat_exes\" + ArcSWAT.ScenarioResultStructure.getSWATExecutableName(modelType);
                if (System.IO.File.Exists(swatexe))
                    cmbModelType.Items.Add(modelType);
            }
            cmbModelType.SelectedIndex = 0;
        }

        private ArcSWAT.SWATModelType _modelType = ArcSWAT.SWATModelType.UNKNOWN;

        private ArcSWAT.SWATModelType ModelType
        {
            get
            {
                if (cmbModelType.SelectedIndex < 0) return ArcSWAT.SWATModelType.UNKNOWN;
                return (ArcSWAT.SWATModelType)(cmbModelType.SelectedItem);
            }
        }

        private void bRun_Click(object sender, EventArgs e)
        {            
            RunSWAT(ModelType, ArcSWAT.SWATResultIntervalType.DAILY);
        }

        private void bRunMonthly_Click(object sender, EventArgs e)
        {
            RunSWAT(ModelType, ArcSWAT.SWATResultIntervalType.MONTHLY);
        }

        private void bRunYearly_Click(object sender, EventArgs e)
        {
            RunSWAT(ModelType, ArcSWAT.SWATResultIntervalType.YEARLY);
        }
    }
}
