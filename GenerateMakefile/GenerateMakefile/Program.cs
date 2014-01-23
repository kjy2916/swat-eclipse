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
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            createSWATMakefile(path);
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }

        private static void createSWATMakefile(string swatFolder, bool inSameFolder = false)
        {
            if (!Directory.Exists(swatFolder))
            {
                Console.WriteLine(swatFolder + " doesn't exist.");
                return;
            }

            Console.WriteLine("Debug or release version? (d/r)");
            string version = Console.ReadLine().ToLower();

            bool isDebug = true;
            if (version.Equals("d") || version.Equals("debug"))
                isDebug = true;
            else if (version.Equals("r") || version.Equals("release"))
                isDebug = false;
            else
            {
                Console.WriteLine("Wrong input.");
                return;
            }

            Console.WriteLine("Looking for fortran source codes in " + swatFolder);
            DirectoryInfo info = new DirectoryInfo(swatFolder);
            FileInfo[] files = info.GetFiles("*.f*");
            if (files == null || files.Length == 0)
            {
                Console.WriteLine("Don't find any fortran codes in " + swatFolder);
                return;
            }

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

            //-ffpe-trap=invalid,zero,overflow,underflow will make program stop when these errors happen
            //-fbacktrace:  Specifies that if the program crashes, a backtrace should be produced if possible, showing what functions or subroutines were being called at the time of the error.
            string commonFlag = " -funderscoring -Wall -fmessage-length=0 -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow";
            string debugFlag = " -O0 -g -fbounds-check -Wextra" + commonFlag;
            if (!isDebug) debugFlag = " -O3" + commonFlag;

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

                //http://help.eclipse.org/juno/index.jsp?topic=%2Forg.eclipse.cdt.doc.user%2Fconcepts%2Fcdt_c_makefile.htm
                //This means that mingw32-make was unable to find the utility "rm". Unfortunately, MinGW does not come with "rm". 
                //To correct this, replace the clean rule in your Makefile with:

                //clean : 
                //    -del $(REBUILDABLES)
                //    echo Clean done
                //The leading minus sign tells make to consider the clean rule to be successful even if the del command returns failure. 
                //This may be acceptable since the del command will fail if the specified files to be deleted do not exist yet (or anymore).
                //The rm command is located in C:\MinGW\msys\1.0\bin. Need to install mingw-developer-toolkit and msys-base package.
                writer.WriteLine(makefilesb.ToString());
                writer.WriteLine("clean:");
                writer.WriteLine("\trm -f ${NAME} $");
                writer.WriteLine("\trm -f *.o");
                writer.WriteLine("\trm -f *.mod");
                writer.WriteLine("\trm -f *~");
            }

            Console.WriteLine("Write " + makefile + " successfully!");
        }
    }
}
