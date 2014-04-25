using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SWAT_SQLite_Result
{
    public delegate void ResultLevelChangedEventHandler(ArcSWAT.ScenarioResult scenario, ArcSWAT.SWATModelType modelType, ArcSWAT.SWATUnitType type);

    class ProjectTree : System.Windows.Forms.TreeView
    {
        private ArcSWAT.Project _prj;
        private TreeNode _prjNode;

        public event ResultLevelChangedEventHandler onResultLevelChanged = null;

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
            if (this.InvokeRequired)
            {
                Invoke(new setProjectDelegate(setProject), new object[] { p });
            }
            else
            {
                this.Nodes.Clear();

                TreeNode prjNode = this.Nodes.Add("Project:");
                prjNode.Tag = p;                

                TreeNode infoNode = prjNode.Nodes.Add("Information");
                infoNode.Tag = p;

                _prjNode = prjNode;

                foreach (ArcSWAT.Scenario s in p.Scenarios.Values)
                    addNodes(s);

                prjNode.ExpandAll();

                this.OnNodeMouseClick(new TreeNodeMouseClickEventArgs(prjNode, System.Windows.Forms.MouseButtons.Left,-1,-1,-1));
            }
        }

        private delegate void addNodesDelegate(ArcSWAT.Scenario s);

        private void addNodes(ArcSWAT.Scenario s)
        {
            //add corresponding node
            if (this.InvokeRequired)
            {
                Invoke(new addNodesDelegate(addNodes), new object[] { s });
            }
            else
            {
               try
                {
                    if (s.ResultNormal.Status == ArcSWAT.ScenarioResultStatus.NORMAL ||
                        s.ResultCanSWAT.Status == ArcSWAT.ScenarioResultStatus.NORMAL)
                    {
                        TreeNode scenNode = _prjNode.Nodes.Add(s.Name);
                        scenNode.Tag = s;

                        if (s.ResultNormal.Status == ArcSWAT.ScenarioResultStatus.NORMAL)
                        {
                            TreeNode normalNode = scenNode.Nodes.Add("SWAT");
                            normalNode.Tag = ArcSWAT.SWATModelType.SWAT;

                            AddScenarioResult(normalNode, s.ResultNormal);
                        }
                        if (s.ResultCanSWAT.Status == ArcSWAT.ScenarioResultStatus.NORMAL)
                        {
                            TreeNode canSWATNode = scenNode.Nodes.Add("CanSWAT");
                            canSWATNode.Tag = ArcSWAT.SWATModelType.CanSWAT;

                            AddScenarioResult(canSWATNode, s.ResultCanSWAT);
                        }
                    }                    
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
            }
        }

        private void AddScenarioResult(TreeNode parentNode, ArcSWAT.ScenarioResult scenario)
        {
            parentNode.Nodes.Add("Watershed").Tag = scenario;
            parentNode.Nodes.Add("HRU").Tag = scenario;
            parentNode.Nodes.Add("Subbasin").Tag = scenario;
            parentNode.Nodes.Add("Reach").Tag = scenario;
            if (scenario.Reservoirs.Count > 0)
                parentNode.Nodes.Add("Reservoir").Tag = scenario;
        }
    }
}
