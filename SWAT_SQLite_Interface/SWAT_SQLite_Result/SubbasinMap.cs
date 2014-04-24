using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotSpatial.Controls;
using DotSpatial.Symbology;
using DotSpatial.Data;
using System.Diagnostics;
using System.Data;
using System.Drawing;

namespace SWAT_SQLite_Result
{
    public delegate void LayerSelectionChangedEventHandler(int selectedid);

    /// <summary>
    /// Display subbasin results
    /// </summary>
    class SubbasinMap : Map
    {
        private static string RESULT_COLUMN = "RESULT";
        private static string ID_COLUMN_NAME = "subbasin";

        private ArcSWAT.Project _project = null;
        private ArcSWAT.ScenarioResult _scenario = null;
        private IFeatureLayer _workingLayer = null;
        public event LayerSelectionChangedEventHandler onLayerSelectionChanged = null;

        public void setProjectScenario(ArcSWAT.Project project, ArcSWAT.ScenarioResult scenario, ArcSWAT.SWATUnitType type)
        {
            if (type != ArcSWAT.SWATUnitType.SUB && type != ArcSWAT.SWATUnitType.RCH) return;

            _project = project;
            _scenario = scenario;

            this.MapFrame.ProjectionModeDefine = DotSpatial.Controls.ActionMode.Always;
            this.MapFrame.ProjectionModeReproject = DotSpatial.Controls.ActionMode.Never;

            this.Layers.Clear();

            if (type == ArcSWAT.SWATUnitType.SUB)
            {
                _workingLayer = this.addLayer(project.Spatial.SubbasinShapefile, "Subbasin", true);
                this.addLayer(project.Spatial.ReachShapefile, "Reach", false);
            }
            else
            {
                this.addLayer(project.Spatial.SubbasinShapefile, "Subbasin", false);
                _workingLayer = this.addLayer(project.Spatial.ReachShapefile, "Reach", true);
            }
            
            _workingLayer.Select(0);
            this.FunctionMode = DotSpatial.Controls.FunctionMode.Select;
        }

        private IFeatureLayer addLayer(string path, string name,bool isWorkingLayer)
        {
            Debug.WriteLine(DateTime.Now);
            Debug.WriteLine("Adding Layer..., " + name);

            IFeatureLayer layer = this.Layers.Add(path) as IFeatureLayer;
            layer.SelectionEnabled = isWorkingLayer;
            layer.LegendText = name;

            foreach (DataColumn col in layer.DataSet.DataTable.Columns)
                col.ColumnName = col.ColumnName.ToLower();

            if (isWorkingLayer)
            {
                //add result column
                DataTable dt = layer.DataSet.DataTable;
                dt.Columns.Add(RESULT_COLUMN, typeof(double));

                //create schema
                layer.Symbology.EditorSettings.ClassificationType = ClassificationType.Quantities;
                layer.Symbology.EditorSettings.FieldName = RESULT_COLUMN;
                layer.Symbology.EditorSettings.IntervalMethod = IntervalMethod.Quantile;
                layer.Symbology.EditorSettings.IntervalSnapMethod = IntervalSnapMethod.SignificantFigures;
                layer.Symbology.EditorSettings.IntervalRoundingDigits = 3; //3 significant number
                layer.Symbology.EditorSettings.StartSize = 5;
                layer.Symbology.EditorSettings.EndSize = 25;
                layer.Symbology.EditorSettings.NumBreaks = 5;
                layer.Symbology.EditorSettings.UseSizeRange = true;

                //start and end color
                layer.Symbology.EditorSettings.StartColor = Color.Green;
                layer.Symbology.EditorSettings.EndColor = Color.Red;

                //for selection changed event
                layer.SelectionChanged += (ss, _e) =>
                    {
                        if (_workingLayer == null) return;
                        if (onLayerSelectionChanged == null) return;
                        if (_workingLayer.Selection.NumRows() == 0) return;

                        IFeature fea = _workingLayer.Selection.ToFeatureList()[0];
                        DataRow r = fea.DataRow;
                        int idIndex = r.Table.Columns.IndexOf(ID_COLUMN_NAME);
                        int id = int.Parse(r[idIndex].ToString());

                        onLayerSelectionChanged(id);
                    };
            }
            else
            {
                if (layer.DataSet.FeatureType == DotSpatial.Topology.FeatureType.Polygon) //subbasin
                {
                    layer.Symbolizer = new PolygonSymbolizer(System.Drawing.Color.LightGray, System.Drawing.Color.Black, 0.5); 
                }
                else if (layer.DataSet.FeatureType == DotSpatial.Topology.FeatureType.Line) //reach
                {
                    layer.Symbolizer = new LineSymbolizer(System.Drawing.Color.Black, 1.5);
                }
            }

            if (layer.DataSet.FeatureType == DotSpatial.Topology.FeatureType.Polygon) //subbasin
            {
                MapLabelLayer label = new MapLabelLayer();
                label.Symbology.Categories[0].Expression = "[" + ID_COLUMN_NAME + "]";

                layer.LabelLayer = label;
                layer.ShowLabels = true;
            }


            return layer;
        }       

        /// <summary>
        /// update corresponding layer 
        /// </summary>
        /// <param name="type"></param>
        public void drawLayer(string resultType, string col, DateTime date)
        {
            if (_workingLayer == null) return;

            Debug.WriteLine(DateTime.Now);
            Debug.WriteLine("Draw Layer, " + _workingLayer.ToString());
            Debug.WriteLine("Getting results...");

            DataTable dt = _workingLayer.DataSet.DataTable;
            int resultIndex = dt.Columns.IndexOf(RESULT_COLUMN);
            int idIndex = dt.Columns.IndexOf(ID_COLUMN_NAME);
            foreach (DataRow r in dt.Rows)
            {
                r[resultIndex] = ArcSWAT.ScenarioResultStructure.EMPTY_VALUE;

                int id = int.Parse(r[idIndex].ToString());
                if (!_scenario.Subbasins.ContainsKey(id)) continue;
                
                ArcSWAT.SWATUnit unit = _scenario.Subbasins[id];
                if (!unit.Results.ContainsKey(resultType)) continue;

                ArcSWAT.SWATUnitResult result = unit.Results[resultType];
                if (!result.Columns.Contains(col)) continue;

                r[resultIndex] = result.getData(col, date);
            }

            Debug.WriteLine(DateTime.Now);
            Debug.WriteLine("setLayerSchema");

            //update symbol
            setLayerSchema(_workingLayer);

            ////update chart
            //onLayerSelectionChanged(type);
        }

        private void setLayerSchema(IFeatureLayer layer)
        {
            int selectFeaIndex = -1;
            for (int i = 0; i < layer.DrawnStates.Count(); i++)
                if (layer.DrawnStates[i].Selected)
                {
                    selectFeaIndex = i;
                    break;
                }

            layer.Symbology.CreateCategories(layer.DataSet.DataTable);

            foreach (IFeatureCategory fc in layer.Symbology.GetCategories())
                fc.ContextMenuItems.Clear();

            try
            {
                layer.ApplyScheme(layer.Symbology); //must have this to make changes work
            }
            catch
            {
            }
            

            //must after updateLayerResultColumnName to get column name correct
            if (selectFeaIndex > -1) layer.Select(selectFeaIndex);
        }
    }
}
