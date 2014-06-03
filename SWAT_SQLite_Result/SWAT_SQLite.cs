using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWAT_SQLite_Result
{
    class SWAT_SQLite
    {
        public static string NAME = "SWAT SQLite";

        /// <summary>
        /// The interface installation folder
        /// </summary>
        public static string InstallationFolder
        {
            get
            {
                return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\";
            }
        }

        public static void showInformationWindow(string msg)
        {
            System.Windows.Forms.MessageBox.Show(msg, NAME);
        }

        
    }
}
