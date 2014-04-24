using System;
using System.Collections.Generic;
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

                    DateTime date = DateTime.Parse(this.Rows[e.RowIndex].Cells[0].Value.ToString());
                    onDateChanged(date);
                };
        }

        public DataTable SWATResultTable
        {
            set
            {                
                DataSource = null;

                Columns.Clear();
                ColumnCount = 2;
                Columns[0].Name = "DATE";
                Columns[0].DataPropertyName = ArcSWAT.SWATUnitResult.COLUMN_NAME_DATE;
                Columns[0].DefaultCellStyle.Format = "yyyy-MM-dd"; //format the date

                string col = value.Columns[value.Columns.Count - 2].ColumnName;
                Columns[1].Name = col;
                Columns[1].DataPropertyName = col;
                Columns[1].DefaultCellStyle.Format = "F2"; //format the value

                AutoGenerateColumns = false; //don't generate other columns automatically
                DataSource = value;
                AutoResizeColumns(); //resize the column width
            }
        }
    }
}
