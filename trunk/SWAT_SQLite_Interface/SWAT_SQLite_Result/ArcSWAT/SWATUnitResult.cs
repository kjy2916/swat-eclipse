using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace SWAT_SQLite_Result.ArcSWAT
{
    /// <summary>
    /// One type result of a SWAT Unit
    /// </summary>
    class SWATUnitResult
    {
        private SWATUnit _unit = null;
        private string _tableName = null;
        private SWATResultIntervalType _interval = SWATResultIntervalType.UNKNOWN;
        private DataTable _dataTable = null; //this would be the datatable corresponding to the whole table

        public SWATUnitResult(string tableName, SWATUnit parentUnit)
        {
            _tableName = tableName;
            _unit = parentUnit;
        }

        private DataTable DataTable
        {
            get
            {
                if (_dataTable == null)
                    _dataTable = _unit.Scenario.GetDataTable(
                        string.Format("select * from {0} where {1}={2}",Name,_unit.Type.ToString(),_unit.ID));
                return _dataTable;
            }
        }

        /// <summary>
        /// Name of the result, also the table name
        /// </summary>
        public string Name { get { return _tableName; } }

        /// <summary>
        /// Result interval
        /// </summary>
        public SWATResultIntervalType Interval
        {
            get
            {
                if(_interval == SWATResultIntervalType.UNKNOWN)
                    _interval = _unit.Scenario.Structure.getInterval(Name);
                return _interval;
            }
        }

        /// <summary>
        /// Data Columns
        /// </summary>
        public StringCollection Columns { get { return _unit.Scenario.Structure.getDataColumns(Name); } }
    }
}
