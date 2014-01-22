using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Collections.Specialized;

namespace GenerateMakefile
{
    class Program
    {
        private static string[] LONG_F_NAMES = { "bmpinit.f", "ovr_sed.f", "percmain.f", "rthsed.f", "modparm.f","biozone.f" };
        private static string[] LONG_F90_NAMES = { "carbon_zhang2.f90" };

        static void Main(string[] args)
        {
            createSWATMakefile(@"C:\Users\yuz\Downloads\rev615_source");
        }

        private static void createSWATMakefile(string swatFolder, bool inSameFolder = false, bool isDebug = true)
        {
            if (!Directory.Exists(swatFolder)) return;

            DirectoryInfo info = new DirectoryInfo(swatFolder);
            FileInfo[] files = info.GetFiles("*.f*");
            if (files == null || files.Length == 0) return;

            //get the file path
            string makefile = info.FullName + "\\Makefile";
            if (!inSameFolder)
            {
                if(isDebug)
                    makefile = info.FullName + "\\debug";
                else
                    makefile = info.FullName + "\\release";

                if (!Directory.Exists(makefile))
                {
                    try
                    {
                        Directory.CreateDirectory(makefile);
                    }
                    catch (System.Exception e)
                    {
                        Console.WriteLine(e.Message);
                        return;
                    }
                }
                makefile += "\\Makefile";
            }

            string o_prefix = "";
            string f_prefix = "";
            if (!inSameFolder)
            {
                o_prefix = "./";
                f_prefix = "../";
            }

            string debugFlag = " -O0 -g";
            if (!isDebug) debugFlag = " -O3";

            StringBuilder makefilesb = new StringBuilder();
            StringBuilder objfilesb = new StringBuilder();
            foreach (FileInfo f in files)
            {               
                Console.WriteLine(f.Name);

                string flag = "";
                string o_file = "";

                if (f.Name.Contains(".f90"))
                {
                    o_file = o_prefix + f.Name.ToLower().Replace(".f90", ".o");
                    if (System.Array.IndexOf(LONG_F90_NAMES, f.Name) > -1) flag = " -ffree-line-length-200";
                }
                else
                {
                    o_file = o_prefix + f.Name.ToLower().Replace(".f", ".o");
                    if (System.Array.IndexOf(LONG_F_NAMES, f.Name) > -1) flag = " -ffixed-line-length-132";
                }

                string f_file = f_prefix + f.Name;

                if (f.Name.Equals("modparm.f"))
                {                    
                    StringBuilder parm_sb = new StringBuilder();
                    parm_sb.AppendLine(o_file + ": " + f_file);
                    parm_sb.AppendLine("\t${FC} " + string.Format("-c{3}{2} {0} -o {1} ", f_file, o_file, flag,debugFlag));

                    makefilesb.Insert(0, parm_sb.ToString());
                    objfilesb.Insert(0, o_file);
                }
                else
                {
                    makefilesb.AppendLine("");
                    makefilesb.AppendLine(o_file + ": " + f_file);
                    makefilesb.AppendLine("\t${FC} " + string.Format("-c{3}{2} {0} -o {1} ", f_file, o_file, flag,debugFlag));

                    objfilesb.Append(" " + o_file);
                }

            }

            using (StreamWriter writer = new StreamWriter(makefile))
            {
                writer.WriteLine("FC=gfortran");
                writer.WriteLine("NAME=SWAT");
                writer.WriteLine("OBJECTS=" + objfilesb.ToString());
                writer.WriteLine("");
                writer.WriteLine("all: ${NAME}");
                writer.WriteLine("");
                writer.WriteLine("${NAME}: ${OBJECTS}");
                writer.WriteLine("\t${FC} ${OBJECTS} -static -o ${NAME}");//reomve static library dependency
                writer.WriteLine("");              

                writer.WriteLine(makefilesb.ToString());
                writer.WriteLine("Clean:");
                writer.WriteLine("\trm -f ${NAME} $");
                writer.WriteLine("\trm -f *.o");
                writer.WriteLine("\trm -f *.mod");
                writer.WriteLine("\trm -f *~");
            }
        }
    }
}
