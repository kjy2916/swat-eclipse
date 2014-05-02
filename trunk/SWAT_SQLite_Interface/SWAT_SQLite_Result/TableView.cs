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
                    Columns[1 + i].DefaultCellStyle.Format = "F2"; //format the value
                }

                AutoGenerateColumns = false; //don't generate other columns automatically  
            }
            else
            {
                if (Columns.Count >= 2)
                    Columns[1].DefaultCellStyle.Format = "F2"; //format the value
            }
            DataSource = dt;
            AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells); //resize the column width
        }

        /// <summary>
        /// compare result
        /// </summary>
        public ArcSWAT.SWATUnitColumnYearCompareResult CompareResult { set { if (value == null) DataTable = null; else setDataColumn(value.Table, value.TableColumns); } }

        /// <summary>
        /// regular result
        /// </summary>
        public ArcSWAT.SWATUnitColumnYearResult Result { set { if (value == null) DataTable = null; else setDataColumn(value.Table, new StringCollection() { value.Column }); } }

        /// <summary>
        /// Observed data
        /// </summary>
        public ArcSWAT.SWATUnitColumnYearObservationData ObservedData { set { if (value == null) DataTable = null; else setDataColumn(value.Table, new StringCollection() { value.Column }); } }


        /// <summary>
        /// normal datatable
        /// </summary>
        public DataTable DataTable { set { setDataColumn(value, null); } }
    }
}
