using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SWAT_SQLite_Result
{
    public delegate void ResultLevelChangedEventHandler(ArcSWAT.ScenarioResult scenario, ArcSWAT.SWATModelType modelType, ArcSWAT.SWATUnitType type);
    public delegate void ScenarioSelectionChangedEventHandler(ArcSWAT.Scenario scenario);

    class ProjectTree : System.Windows.Forms.TreeView
    {
        private ArcSWAT.Project _prj;

        public event ResultLevelChangedEventHandler onResultLevelChanged = null;
        public event ScenarioSelectionChangedEventHandler onScenarioSelectionChanged = null;
        public event EventHandler onProjectNodeSelected = null;
        public event EventHandler onDifferenceNodeSelected = null;
        public event EventHandler onPerformanceNodeSelected = null;

        public ProjectTree()
        {
            NodeMouseClick += (s, e) =>
            {
                if(onResultLevelChanged == null) return;
                _scenarioResult = null;

                if (e.Node == null) return;
                
                ArcSWAT.SWATUnitType type = ArcSWAT.SWATUnitType.UNKNOWN;
                if (e.Node.Text.Equals("Watershed"))
                    type = ArcSWAT.SWATUnitType.WSHD;
                else if (e.Node.Text.Equals("HRU"))
                    type = ArcSWAT.SWATUnitType.HRU;
                else if (e.Node.Text.Equals("Subbasin"))
                    type = ArcSWAT.SWATUnitType.SUB;
                else if (e.Node.Text.Equals("Reach"))
                    type = ArcSWAT.SWATUnitType.RCH;
                else if (e.Node.Text.Equals("Reservoir"))
                    type = ArcSWAT.SWATUnitType.RES;

                if (type != ArcSWAT.SWATUnitType.UNKNOWN)
                    onResultLevelChanged(e.Node.Tag as ArcSWAT.ScenarioResult, (ArcSWAT.SWATModelType)e.Node.Parent.Tag, type);

                //click on scenario node
                if (e.Node.Tag != null && e.Node.Tag is ArcSWAT.Scenario && onScenarioSelectionChanged != null)
                    onScenarioSelectionChanged(e.Node.Tag as ArcSWAT.Scenario);

                //click on model node
                if (e.Node.Tag != null && e.Node.Tag is ArcSWAT.SWATModelType && e.Node.Nodes.Count > 0 && e.Node.Nodes[0].Tag != null && e.Node.Nodes[0].Tag is ArcSWAT.ScenarioResult)
                    onResultLevelChanged(e.Node.Nodes[0].Tag as ArcSWAT.ScenarioResult, (ArcSWAT.SWATModelType)e.Node.Tag, ArcSWAT.SWATUnitType.WSHD);

                //click on project node
                if (e.Node.Tag != null && e.Node.Tag is ArcSWAT.Project && onProjectNodeSelected != null)
                    onProjectNodeSelected(this, new EventArgs());

                //click on difference node
                if (e.Node.Text.Equals("Difference") && e.Node.Tag != null && e.Node.Tag is ArcSWAT.ScenarioResult && onDifferenceNodeSelected != null)
                {
                    _scenarioResult = e.Node.Tag as ArcSWAT.ScenarioResult;
                    onDifferenceNodeSelected(this, new EventArgs());
                }

                //click on difference node
                if (e.Node.Text.Equals("Performance") && e.Node.Tag != null && e.Node.Tag is ArcSWAT.ScenarioResult && onPerformanceNodeSelected != null)
                {
                    _scenarioResult = e.Node.Tag as ArcSWAT.ScenarioResult;
                    onPerformanceNodeSelected(this, new EventArgs());
                }
            };       
        }

        private ArcSWAT.ScenarioResult _scenarioResult = null;

        public ArcSWAT.ScenarioResult ScenarioResult
        {
            get { return _scenarioResult; }
        }

        public ArcSWAT.Project Project
        {
            set
            {                
                if(value == null) return;
                _prj = value;
                setProject(value);
             }
            get
            {
                return _prj;
            }
        }

        private delegate void setProjectDelegate(ArcSWAT.Project p);

        private void setProject(ArcSWAT.Project p)
        {
            if (p == null && p.Folder.Equals(_prj.Folder)) return;

            this.Nodes.Clear();

            TreeNode prjNode = this.Nodes.Add("Project");
            prjNode.Tag = p;                

            foreach (ArcSWAT.Scenario s in p.Scenarios.Values)
                addNodes(prjNode,s);

            prjNode.ExpandAll();

            //select project node
            //if(prjNode.Nodes.Count > 0)
            //    this.OnNodeMouseClick(
            //        new TreeNodeMouseClickEventArgs(prjNode, System.Windows.Forms.MouseButtons.Left,-1,-1,-1));
        }

        public void update(ArcSWAT.Scenario scenario, ArcSWAT.SWATModelType modelType)
        {
            foreach (TreeNode node in this.Nodes[0].Nodes)
            {
                if (node.Text.Equals(scenario.Name))
                    AddScenarioResult(node, scenario, modelType);
            }
        }

        private void addNodes(TreeNode projectNode, ArcSWAT.Scenario s)
        {
            TreeNode scenNode = projectNode.Nodes.Add(s.Name);
            scenNode.Tag = s;

            if (s.hasResults)
            {
                for (int i = Convert.ToInt32(ArcSWAT.SWATModelType.SWAT_488); i <= Convert.ToInt32(ArcSWAT.SWATModelType.CanSWAT); i++)
                {
                    ArcSWAT.SWATModelType modelType = (ArcSWAT.SWATModelType)i;
                    AddScenarioResult(scenNode, s, modelType);
                }
            }                    
        }

        private delegate void addScenarioResultDelegate(TreeNode scenNode, ArcSWAT.Scenario scenario, ArcSWAT.SWATModelType modelType);

        /// <summary>
        /// This method may be called in another thread.
        /// </summary>
        /// <param name="scenNode"></param>
        /// <param name="scenario"></param>
        /// <param name="modelType"></param>
        private void AddScenarioResult(TreeNode scenNode, ArcSWAT.Scenario scenario, ArcSWAT.SWATModelType modelType)
        {
            if (InvokeRequired)
                BeginInvoke(new addScenarioResultDelegate(AddScenarioResult), scenNode, scenario, modelType);
            else
            {
                foreach (TreeNode n in scenNode.Nodes)
                {
                    if (n.Text.Equals(modelType.ToString()))
                    {
                        scenNode.Nodes.Remove(n); //remove the existing one and then add to update its status.
                        break;
                    }
                }

                TreeNode modelTypeNode = null;
                for (int j = Convert.ToInt32(ArcSWAT.SWATResultIntervalType.MONTHLY); j <= Convert.ToInt32(ArcSWAT.SWATResultIntervalType.YEARLY); j++)
                {
                    ArcSWAT.SWATResultIntervalType interval = (ArcSWAT.SWATResultIntervalType)j;

                    ArcSWAT.ScenarioResult result = scenario.getModelResult(modelType,interval);
                    if (result == null) continue;
                    if (result.Status != ArcSWAT.ScenarioResultStatus.NORMAL) continue;

                    if (modelTypeNode == null)
                    {
                        modelTypeNode = scenNode.Nodes.Add(modelType.ToString());
                        modelTypeNode.Tag = modelType;
                    }

                    TreeNode resultNode = modelTypeNode.Nodes.Add(interval.ToString());
                    resultNode.Tag = modelType;
                    resultNode.Nodes.Add("Watershed").Tag = result;
                    resultNode.Nodes.Add("HRU").Tag = result;
                    resultNode.Nodes.Add("Subbasin").Tag = result;
                    resultNode.Nodes.Add("Reach").Tag = result;
                    if (result.Reservoirs.Count > 0)
                        resultNode.Nodes.Add("Reservoir").Tag = result;
                    resultNode.Nodes.Add("Difference").Tag = result;
                    resultNode.Nodes.Add("Performance").Tag = result;
                }

                scenNode.ExpandAll();
            }
        }
    }
}
