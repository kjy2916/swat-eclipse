using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace SWAT_SQLite_Result
{
    public delegate void DateChangedEventHandler(DateTime date);

    class TableView : DataGridView
    {
        public event DateChangedEventHandler onDateChanged = null;

        public TableView()
        {
            RowHeadersVisible = false;
            ReadOnly = true;
            this.CellContentDoubleClick += (s, e) =>
                {
                    if (e.RowIndex < 0) return;
                    if (onDateChanged == null) return;


                    DateTime date = DateTime.Now;
                    bool isDate = DateTime.TryParse(this.Rows[e.RowIndex].Cells[0].Value.ToString(), out date);
                    if(isDate)  onDateChanged(date);
                };
        }

        private void setDataColumn(DataTable dt,StringCollection cols)
        {
            DataSource = null;
            Columns.Clear();

            if (dt == null) return;

            if (dt.Columns.Contains(ArcSWAT.SWATUnitResult.COLUMN_NAME_DATE) &&
                cols != null && cols.Count > 0)
            {
                ColumnCount = cols.Count + 1;

                Columns[0].Name = "DATE";
                Columns[0].DataPropertyName = ArcSWAT.SWATUnitResult.COLUMN_NAME_DATE;
                Columns[0].DefaultCellStyle.Format = "yyyy-MM-dd"; //format the date

                for (int i = 0; i < cols.Count; i++)
                {
                    Columns[1 + i].Name = cols[i];
                    Columns[1 + i].DataPropertyName = cols[i];
                    if(!cols[i].Equals(ArcSWAT.ScenarioResultStructure.COLUMN_NAME_HRU_LANDUSE) &&
                        !cols[i].Equals(ArcSWAT.ScenarioResultStructure.COLUMN_NAME_HRU_MGT_OPERATION) &&
                        !cols[i].Equals(ArcSWAT.ScenarioResultStructure.COLUMN_NAME_HRU_MGT_LANDUSE))
                        Columns[1 + i].DefaultCellStyle.Format = "F2"; //format the value
                }

                AutoGenerateColumns = false; //don't generate other columns automatically  
                DataSource = dt;
            }
            else
            {
                DataSource = dt;
                if (Columns.Count >= 2)
                    Columns[1].DefaultCellStyle.Format = "F2"; //format the value
            }
            
            AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells); //resize the column width
        }

        /// <summary>
        /// compare result
        /// </summary>
        public ArcSWAT.SWATUnitColumnYearCompareResult CompareResult { set { if (value == null) DataTable = null; else setDataColumn(value.SeasonTable(Season), value.TableColumns); } }

        /// <summary>
        /// regular result
        /// </summary>
        public ArcSWAT.SWATUnitColumnYearResult Result 
        { 
            set 
            {
                if (value == null)
                    DataTable = null;
                else
                {
                    StringCollection cols = new StringCollection();
                    cols.Add(value.Column);
                    if (value.HasLanduseColumn)
                        cols.Add(ArcSWAT.ScenarioResultStructure.COLUMN_NAME_HRU_LANDUSE);
                    if (value.HasMgtOperationColumn)
                    {
                        cols.Add(ArcSWAT.ScenarioResultStructure.COLUMN_NAME_HRU_MGT_OPERATION);
                        cols.Add(ArcSWAT.ScenarioResultStructure.COLUMN_NAME_HRU_MGT_LANDUSE);
                    }
                    setDataColumn(value.SeasonTable(Season),cols); 
                }                    
            } 
        }

        /// <summary>
        /// Observed data
        /// </summary>
        public ArcSWAT.SWATUnitColumnYearObservationData ObservedData { set { if (value == null) DataTable = null; else setDataColumn(value.SeasonTable(Season), new StringCollection() { value.Column }); } }

        /// <summary>
        /// normal datatable
        /// </summary>
        public DataTable DataTable { set { setDataColumn(value, null); } }


        private ArcSWAT.SeasonType _season = ArcSWAT.SeasonType.WholeYear;

        /// <summary>
        /// set the season type
        /// </summary>
        public ArcSWAT.SeasonType Season { set { _season = value; } get { return _season; } }
    }
}
