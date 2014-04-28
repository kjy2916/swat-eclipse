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
        private TreeNode _prjNode;

        public event ResultLevelChangedEventHandler onResultLevelChanged = null;
        public event ScenarioSelectionChangedEventHandler onScenarioSelectionChanged = null;


        public ProjectTree()
        {
            NodeMouseClick += (s, e) =>
            {
                if(onResultLevelChanged == null) return;
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

                if (type != ArcSWAT.SWATUnitType.UNKNOWN)
                    onResultLevelChanged(e.Node.Tag as ArcSWAT.ScenarioResult, (ArcSWAT.SWATModelType)e.Node.Parent.Tag, type);

                if (e.Node.Tag != null && e.Node.Tag is ArcSWAT.Scenario && onScenarioSelectionChanged != null)
                    onScenarioSelectionChanged(e.Node.Tag as ArcSWAT.Scenario);

            };       
        }

        public ArcSWAT.Project Project
        {
            set
            {                
                if(value == null) return;
                _prj = value;
                setProject(value);
             }
        }

        private delegate void setProjectDelegate(ArcSWAT.Project p);

        private void setProject(ArcSWAT.Project p)
        {
            this.Nodes.Clear();

            TreeNode prjNode = this.Nodes.Add("Project:");
            prjNode.Tag = p;                

            //TreeNode infoNode = prjNode.Nodes.Add("Information");
            //infoNode.Tag = p;

            _prjNode = prjNode;

            foreach (ArcSWAT.Scenario s in p.Scenarios.Values)
                addNodes(s);

            prjNode.ExpandAll();

            //select first scenario node
            if(prjNode.Nodes.Count > 0)
                this.OnNodeMouseClick(
                    new TreeNodeMouseClickEventArgs(prjNode.Nodes[0], System.Windows.Forms.MouseButtons.Left,-1,-1,-1));
        }

        public void update(ArcSWAT.Scenario scenario, ArcSWAT.SWATModelType modelType)
        {
            foreach (TreeNode node in this.Nodes[0].Nodes)
            {
                if (node.Text.Equals(scenario.Name))
                {
                    foreach (TreeNode resultNode in node.Nodes)
                    {
                        if(resultNode.Text.Equals(modelType.ToString())) return;
                    }

                    AddScenarioResult(node, scenario, modelType);
                }
            }
        }

        private delegate void addNodesDelegate(ArcSWAT.Scenario s);

        private void addNodes(ArcSWAT.Scenario s)
        {
            if (s.hasResults)
            {
                TreeNode scenNode = _prjNode.Nodes.Add(s.Name);
                scenNode.Tag = s;

                for (int i = Convert.ToInt32(ArcSWAT.SWATModelType.SWAT); i <= Convert.ToInt32(ArcSWAT.SWATModelType.CanSWAT); i++)
                {
                    ArcSWAT.SWATModelType modelType = (ArcSWAT.SWATModelType)i;
                    AddScenarioResult(scenNode, s, modelType);
                }
            }                    
        }        

        private void AddScenarioResult(TreeNode scenNode, ArcSWAT.Scenario scenario, ArcSWAT.SWATModelType modelType)
        {
            ArcSWAT.ScenarioResult result = scenario.getModelResult(modelType);
            if (result == null) return;
            if (result.Status != ArcSWAT.ScenarioResultStatus.NORMAL) return;

            TreeNode resultNode = scenNode.Nodes.Add(modelType.ToString());
            resultNode.Tag = modelType;

            resultNode.Nodes.Add("Watershed").Tag = result;
            resultNode.Nodes.Add("HRU").Tag = result;
            resultNode.Nodes.Add("Subbasin").Tag = result;
            resultNode.Nodes.Add("Reach").Tag = result;
            if (result.Reservoirs.Count > 0)
                resultNode.Nodes.Add("Reservoir").Tag = result;
        }
    }
}
