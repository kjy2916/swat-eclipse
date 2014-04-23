using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SWAT_SQLite_Result
{
    class ProjectTree : System.Windows.Forms.TreeView
    {
        private ArcSWAT.Project _prj;
        private TreeNode _prjNode;
        private ImageList _imageList;

        private void createImageList()
        {
            if (_imageList != null) return;

            //_imageList = new ImageList();
            //_imageList.Images.Add(WEBsUI2.Properties.Resources.draw_polygon_curves1);//small dam,0
            //_imageList.Images.Add(WEBsUI2.Properties.Resources.button1);//holding pond,1
            //_imageList.Images.Add(WEBsUI2.Properties.Resources.cow_head);//grazing,2
            //_imageList.Images.Add(WEBsUI2.Properties.Resources.tractor1);//tillage,3
            //_imageList.Images.Add(WEBsUI2.Properties.Resources.grass1);//forage,4
            //_imageList.Images.Add(WEBsUI2.Properties.Resources.map);//scenario,5
            //_imageList.Images.Add(WEBsUI2.Properties.Resources.info_rhombus_16x16);//project information,6
            //_imageList.Images.Add(WEBsUI2.Properties.Resources.house);//farm level,7
            //_imageList.Images.Add(WEBsUI2.Properties.Resources.draw_ellipse);//subbasin level,8
            //_imageList.Images.Add(WEBsUI2.Properties.Resources.draw_points);//field level,9
            //_imageList.Images.Add(WEBsUI2.Properties.Resources.database_green);//project,10
            //_imageList.Images.Add(WEBsUI2.Properties.Resources._3d_glasses_16);//result,11
            //this.ImageList = _imageList;
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
                createImageList();

                TreeNode prjNode = this.Nodes.Add("Project:");
                prjNode.Tag = p;                

                TreeNode infoNode = prjNode.Nodes.Add("Information");
                infoNode.Tag = p;

                _prjNode = prjNode;

                foreach (ArcSWAT.Scenario s in p.Scenarios.Values)
                    addNodes(s);

                prjNode.Expand();

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
                    if (s.ResultNormal.Status == ArcSWAT.ScenarioResultStatus.NORMAL)
                    {
                        TreeNode scenNode = _prjNode.Nodes.Add(s.Name);
                        scenNode.Tag = s;

                        scenNode.Nodes.Add("HRU");                                         
                        scenNode.Nodes.Add("Subbasin");
                        scenNode.Nodes.Add("Reach");
                        if(s.ResultNormal.Reservoirs.Count > 0)
                            scenNode.Nodes.Add("Reservoir");
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
            }
        }
    }
}
