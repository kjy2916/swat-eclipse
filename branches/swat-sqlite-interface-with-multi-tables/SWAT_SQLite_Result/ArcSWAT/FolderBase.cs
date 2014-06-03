using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWAT_SQLite_Result.ArcSWAT
{
    public class FolderBase
    {
        private string _folder = null;
        protected bool _isValid = false;
        protected string _error = null;

        public FolderBase(string f)
        {
            if (System.IO.Directory.Exists(f))
            {
                _folder = System.IO.Path.GetFullPath(f);
                _isValid = true;
            }
            else
                _error = f + " doesn't exist!";
        }

        public string Error
        {
            get { return _error; }
        }

        public string Folder
        {
            get { return _folder; }
        }

        public bool IsValid
        {
            get { return _isValid; }
        }
    }
}
