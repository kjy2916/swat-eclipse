using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWAT_SQLite_Result.ArcSWAT
{
    public class RowItem
    {
        protected System.Data.DataRow _row;

        public RowItem(System.Data.DataRow row)
        {
            _row = row;
        }

        private bool isColumnValueInvalid(int columnIndex)
        {
            if (_row == null) return false;
            if (columnIndex < 0) return false;
            if (_row.Table.Columns.Count <= columnIndex) return false;
            if (_row[columnIndex] is System.DBNull) return false;

            return true;
        }

        public string getColumnValue_String(int columnIndex)
        {
            if (isColumnValueInvalid(columnIndex)) return _row[columnIndex].ToString();
            return "";
        }

        public int getColumnValue_Int(int columnIndex)
        {
            string v = getColumnValue_String(columnIndex);

            int v_int = -1;
            if (int.TryParse(v, out v_int)) return v_int;
            return -1;
        }

        private bool isColumnValueInvalid(string columnName)
        {
            if (_row == null) return false;
            if (_row.Table.Columns[columnName] == null) return false;
            if (_row[columnName] is System.DBNull) return false;

            return true;
        }

        public string getColumnValue_String(string columnName)
        {
            if (isColumnValueInvalid(columnName)) return _row[columnName].ToString();
            return "";
        }

        public int getColumnValue_Int(string columnName)
        {
            string v = getColumnValue_String(columnName);

            int v_int = -1;
            if (int.TryParse(v, out v_int)) return v_int;
            return -1;
        }

        public double getColumnValue_Double(string columnName)
        {
            string v = getColumnValue_String(columnName);

            double v_double = -99.0;
            if (double.TryParse(v, out v_double)) return v_double;
            return -99.0;
        }
    }
}
