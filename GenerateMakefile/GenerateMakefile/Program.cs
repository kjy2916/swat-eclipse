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

        enum CODE_TYPE
        {
            C,
            FORTRAN
        }

        /// <summary>
        /// generate objects string and compile string baed on given source codes type
        /// </summary>
        /// <param name="swatFolder"></param>
        /// <param name="type"></param>
        /// <param name="isDebug"></param>
        /// <param name="inSameFolder"></param>
        /// <param name="objs"></param>
        /// <param name="compiles"></param>
        private static void generateMakefile(string swatFolder, CODE_TYPE type, bool isDebug, bool inSameFolder,
            out string objs, out string compiles)
        {
            objs = "";
            compiles = "";

            Console.WriteLine("Looking for " + type.ToString() + " source codes in " + swatFolder);
            DirectoryInfo info = new DirectoryInfo(swatFolder);
            FileInfo[] files = info.GetFiles(type == CODE_TYPE.FORTRAN ? "*.f*" : "*.c");
            if (files == null || files.Length == 0)
            {
                Console.WriteLine("Don't find any " + type.ToString() + " codes in " + swatFolder);
                return;
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
            string commonFlag = " -Wall -fmessage-length=0";
            if (type == CODE_TYPE.FORTRAN) commonFlag = " -funderscoring -fbacktrace -ffpe-trap=invalid,zero,overflow,underflow" + commonFlag;
            string debugFlag = " -O0 -g -fbounds-check -Wextra" + commonFlag;
            if (!isDebug) debugFlag = " -O3" + commonFlag;
            string compiler = "FC";
            if (type == CODE_TYPE.C) compiler = "CC";

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
                else if (f.Name.Contains(".f"))
                {
                    o_file = o_prefix + f.Name.ToLower().Replace(".f", ".o");
                    if (System.Array.IndexOf(LONG_F_NAMES, f.Name) > -1) flag = " -ffixed-line-length-132";
                }
                else if (f.Name.Contains(".c"))
                {
                    o_file = o_prefix + f.Name.ToLower().Replace(".c", ".o");
                }

                string f_file = f_prefix + f.Name;

                if (f.Name.Equals("modparm.f")) //insert into the first to generate parm.mod before other files are compiled
                {
                    StringBuilder parm_sb = new StringBuilder();
                    parm_sb.AppendLine(o_file + ": " + f_file);
                    parm_sb.AppendLine("\t${FC} " + string.Format("-c{3}{2} {0} -o {1} ", f_file, o_file, flag, debugFlag));

                    makefilesb.Insert(0, parm_sb.ToString());
                    objfilesb.Insert(0, o_file);
                }
                else
                {
                    makefilesb.AppendLine("");
                    makefilesb.AppendLine(o_file + ": " + f_file);
                    makefilesb.AppendLine("\t${"+compiler+"} " + string.Format("-c{3}{2} {0} -o {1} ", f_file, o_file, flag, debugFlag));

                    objfilesb.Append(" " + o_file);
                }
            }

            objs = objfilesb.ToString();
            compiles = makefilesb.ToString();
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

            string objs_c = "";
            string objs_fortran = "";
            string compiles_c = "";
            string compiles_fortran = "";

            generateMakefile(swatFolder, CODE_TYPE.C, 
                isDebug, inSameFolder, out objs_c, out compiles_c);
            generateMakefile(swatFolder, CODE_TYPE.FORTRAN, 
                isDebug, inSameFolder, out objs_fortran, out compiles_fortran);

            if (objs_c.Length == 0 && objs_fortran.Length == 0)
            {
                Console.WriteLine("Don't find any source codes in " + swatFolder);
                return;
            }

            //get the file path
            DirectoryInfo info = new DirectoryInfo(swatFolder);
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

            //write to file
            using (StreamWriter writer = new StreamWriter(makefile))
            {
                writer.WriteLine("CC=gcc");
                writer.WriteLine("FC=gfortran");
                writer.WriteLine("NAME=SWAT");
                writer.WriteLine("OBJECTS=" + objs_c + " " + objs_fortran); //c is before fortran
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
                writer.WriteLine(compiles_c);
                writer.WriteLine(compiles_fortran); //c is before fortran
                writer.WriteLine("clean:");
                writer.WriteLine("\trm -f ${NAME}.exe");
                writer.WriteLine("\trm -f *.o");
                writer.WriteLine("\trm -f *.mod");
                writer.WriteLine("\trm -f *~");
            }

            Console.WriteLine("Write " + makefile + " successfully!");
        }
    }
}
