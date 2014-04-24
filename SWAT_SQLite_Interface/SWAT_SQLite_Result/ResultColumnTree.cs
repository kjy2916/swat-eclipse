using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SWAT_SQLite_Result
{
    public delegate void ResultTypeAndColumnChanged(string resultType,string col);

    /// <summary>
    /// Display the result columns in a tree view
    /// </summary>
    public class ResultColumnTree : TreeView
    {
        public event ResultTypeAndColumnChanged onResultTypeAndColumnChanged = null;

        public ResultColumnTree()
        {
            NodeMouseClick += (s, e) =>
                {
                    if (e.Node == null) return;
                    if (e.Node.Parent == null) return;
                    if(onResultTypeAndColumnChanged == null) return;

                    string resultType = e.Node.Parent.Text;
                    string col = e.Node.Text;
                    onResultTypeAndColumnChanged(resultType,col);
                };
        }

        public void setScenarioAndUnit(ArcSWAT.ScenarioResult scenario, ArcSWAT.SWATUnitType type)
        {
            this.Nodes.Clear();

            if (scenario == null) return;
            if (type == ArcSWAT.SWATUnitType.UNKNOWN) return;

            StringCollection tbls = scenario.Structure.getResultTablesWithData(type);
            if (tbls.Count == 0) return;

            foreach (string tbl in tbls)
            {
                TreeNode tblNode = Nodes.Add(tbl);
                
                StringCollection cols = scenario.Structure.getDataColumns(tbl);
                foreach (string col in cols)
                    tblNode.Nodes.Add(col);

                tblNode.ExpandAll();
            }        
            onResultTypeAndColumnChanged(Nodes[0].Text, Nodes[0].Nodes[0].Text);
        }
    }

    
}
