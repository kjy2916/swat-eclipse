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
        private static string[] LONG_F_NAMES = { "bmpinit.f", "ovr_sed.f", "percmain.f", "rthsed.f", "main.f","biozone.f" };
        private static string[] LONG_F90_NAMES = { "carbon_zhang2.f90" };

        private static string NAME_C_FLAG = "CFLAG";
        private static string NAME_FORTRAN_FLAG = "FFLAG";
        private static string NAME_DEBUG_FLAG = "DFLAG";
        private static string NAME_RELEASE_FLAG = "RFLAG";
        private static string NAME_LONG_FIX_FORMAT = "LONGFIX";
        private static string NAME_LONG_FREE_FORMAT = "LONGFREE";
        private static string NAME_ARCHITECTURE_32= "ARCH32";
        private static string NAME_ARCHITECTURE_64 = "ARCH64";

        private static string TARGET_DEBUG_32 = "debug32";
        private static string TARGET_DEBUG_64 = "debug64";
        private static string TARGET_RELEASE_32 = "rel32";
        private static string TARGET_RELEASE_64 = "rel64";

        /// <summary>
        /// Target name for different configuration
        /// </summary>
        /// <param name="isDebug"></param>
        /// <param name="is64bit"></param>
        /// <returns></returns>        
        private static string getMakeTargetName(bool isDebug, bool is64bit)
        {
            if (isDebug && is64bit) return TARGET_DEBUG_64;
            else if (isDebug && !is64bit) return TARGET_DEBUG_32;
            else if (!isDebug && is64bit) return TARGET_RELEASE_64;
            else return TARGET_RELEASE_32;
        }

        /// <summary>
        /// Sub-folder name for different configuration
        /// </summary>
        /// <param name="isDebug"></param>
        /// <param name="is64bit"></param>
        /// <returns></returns>
        /// <remarks>The subfolder name is same as the target name</remarks>
        private static string getSubFolderName(bool isDebug, bool is64bit)
        {
            return getMakeTargetName(isDebug, is64bit);
        }

        /// <summary>
        /// The executable name for different configuration
        /// </summary>
        /// <param name="isDebug"></param>
        /// <param name="is64bit"></param>
        /// <returns></returns>
        private static string getSWATExecutableName(bool isDebug, bool is64bit)
        {
            return "swat_" + getMakeTargetName(isDebug, is64bit);
        }

        /// <summary>
        /// The Name variable for different configuration
        /// </summary>
        /// <param name="isDebug"></param>
        /// <param name="is64bit"></param>
        /// <returns></returns>
        private static string getMakeNameVariable(bool isDebug, bool is64bit)
        {
            return "NAME" + getMakeTargetName(isDebug, is64bit).ToUpper();
        }

        /// <summary>
        /// The objects name for different configuration
        /// </summary>
        /// <param name="isDebug"></param>
        /// <param name="is64bit"></param>
        /// <returns></returns>
        private static string getObjectsName(bool isDebug, bool is64bit)
        {
            return "OBJECTS_" + getMakeTargetName(isDebug, is64bit).ToUpper();
        }

        private static string getMkdirName(bool isDebug, bool is64bit)
        {
            return getMakeTargetName(isDebug, is64bit) + "_mkdir";
        }

        private static string Header
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                                //header
                sb.AppendLine("#Generated using GenerateMakefile by Zhiqiang Yu, hawklorry@gmail.com");
                sb.AppendLine("#" + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());
                sb.AppendLine("");

                //define compiler
                sb.AppendLine("#Define compiler, change it if using other compiler");
                sb.AppendLine("CC=gcc");
                sb.AppendLine("FC=gfortran");

                //flags
                sb.AppendLine("");
                sb.AppendLine("#C Flag");
                sb.AppendLine(NAME_C_FLAG + "=-c -Wall -fmessage-length=0");
                sb.AppendLine("#Fortran Flag");
                sb.AppendLine("#Remove -Wall if don't want all the warning information");
                sb.AppendLine("#Remove invalid, zero or overflow if don't want SWAT stop running when these floating point exception happens");
                sb.AppendLine(NAME_FORTRAN_FLAG + "=-c -Wall -fmessage-length=0 -funderscoring -fbacktrace -ffpe-trap=invalid,zero,overflow");
                sb.AppendLine("#Dedug Flag");
                sb.AppendLine(NAME_DEBUG_FLAG + "=-O0 -g -fbounds-check -Wextra");
                sb.AppendLine("#Release Flag");
                sb.AppendLine(NAME_RELEASE_FLAG + "=-O3");
                sb.AppendLine("#Flag for long fix fortran codes, used for some special fortran files");
                sb.AppendLine(NAME_LONG_FIX_FORMAT + "=-ffixed-line-length-132");
                sb.AppendLine("#Flag for long free fortran codes, used for some special fortran files");
                sb.AppendLine(NAME_LONG_FREE_FORMAT + "=-ffree-line-length-200");
                sb.AppendLine("#Flag for target machine architecture.");
                sb.AppendLine("#Note: MinGW doesn't support 64-bit architecture. Replace -m64 with empty string instead.");
                sb.AppendLine(NAME_ARCHITECTURE_32 + "=-m32");
                sb.AppendLine(NAME_ARCHITECTURE_64 + "=-m64");
                sb.AppendLine("");

                return sb.ToString();
            }
        }

        static void Main(string[] args)
        {
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            createSWATMakefile_FourVersion(path);
            //createSWATMakefile_OneVersion(path);
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }

        enum CODE_TYPE
        {
            C,
            FORTRAN
        }


        /// <summary>
        /// Generate string for one configuration
        /// </summary>
        /// <param name="swatFolder"></param>
        /// <param name="inSameFolder">Should be always be true for single Makefile and be false for separated Makefile</param>
        /// <param name="isDebug"></param>
        /// <param name="is64bit"></param>
        /// <returns></returns>
        private static string generateMakefile(string swatFolder, bool inSameFolder,
            bool isDebug, bool is64bit)
        {
            string objs_c = "";
            string objs_fortran = "";
            string compiles_c = "";
            string compiles_fortran = "";

            generateMakefile(swatFolder, CODE_TYPE.C,
                inSameFolder,isDebug,is64bit, out objs_c, out compiles_c);
            generateMakefile(swatFolder, CODE_TYPE.FORTRAN,
                inSameFolder,isDebug,is64bit, out objs_fortran, out compiles_fortran);

            if(objs_fortran.Length == 0)return "";

            StringBuilder sb = new StringBuilder();
            string makeNameVariable = getMakeNameVariable(isDebug,is64bit);
            string objectsName = getObjectsName(isDebug,is64bit);
            string architecture = "${" + (is64bit ? NAME_ARCHITECTURE_64 : NAME_ARCHITECTURE_32) + "}";
            string targetName = getMakeTargetName(isDebug,is64bit);
            string mkdir = getMkdirName(isDebug, is64bit);

            //define objects            
            sb.AppendLine(objectsName + "=" + objs_c + " " + objs_fortran); //c is before fortran
            sb.AppendLine("");

            //define the executable name
            sb.AppendLine(string.Format("{0}={1}",makeNameVariable, getSWATExecutableName(isDebug,is64bit)));

            //define target
            if(inSameFolder)
                sb.AppendLine(targetName + ":" + mkdir+" ${" + makeNameVariable + "}");
            else
                sb.AppendLine(targetName + ": ${" + makeNameVariable + "}");
            
            //define mkdir
            if (inSameFolder)
            {
                sb.AppendLine("");
                sb.AppendLine(mkdir + ":");
                sb.AppendLine("\tmkdir -p " + getSubFolderName(isDebug, is64bit));
                sb.AppendLine("");
            }

            //define rules
            sb.AppendLine("${"+makeNameVariable+"}: ${" +objectsName+ "}");
            sb.AppendLine("\t${FC} ${" + objectsName + "} " + architecture + " -static -o ${" + makeNameVariable + "}"); //c is before fortran
            
            //define compilers
            sb.AppendLine(compiles_c);
            sb.AppendLine(compiles_fortran); //c is before fortran

            //http://help.eclipse.org/juno/index.jsp?topic=%2Forg.eclipse.cdt.doc.user%2Fconcepts%2Fcdt_c_makefile.htm
            //This means that mingw32-make was unable to find the utility "rm". Unfortunately, MinGW does not come with "rm". 
            //To correct this, replace the clean rule in your Makefile with:

            //clean : 
            //    -del $(REBUILDABLES)
            //    echo Clean done
            //The leading minus sign tells make to consider the clean rule to be successful even if the del command returns failure. 
            //This may be acceptable since the del command will fail if the specified files to be deleted do not exist yet (or anymore).
            //The rm command is located in C:\MinGW\msys\1.0\bin. Need to install mingw-developer-toolkit and msys-base package.
            sb.AppendLine(targetName +"_clean:");
            sb.AppendLine("\trm -f ${" + makeNameVariable + "}.exe");//the exe is always in the same folder as Makefile

            string subFolder = getSubFolderName(isDebug, is64bit) + "/";
            if (!inSameFolder) subFolder = ""; 
            sb.AppendLine("\trm -f " +subFolder+ "*.o");
            sb.AppendLine("\trm -f " +subFolder+ "*.mod");
            sb.AppendLine("\trm -f " +subFolder+ "*~");

            return sb.ToString();
        }

        /// <summary>
        /// generate objects string and compile string baed on given source codes type
        /// </summary>
        /// <param name="swatFolder">The SWAT source codes folder</param>
        /// <param name="type">C or Fortran</param>        
        /// <param name="inSameFolder">If the Makefile would be in the same folder as the source codes</param>
        /// <param name="isDebug">If it's for debug version</param>
        /// <param name="is64bit">If it's for release version</param>
        /// <param name="objs">The string for objectives</param>
        /// <param name="compiles">The string for compilers</param>
        private static void generateMakefile(string swatFolder, CODE_TYPE type, bool inSameFolder, 
            bool isDebug, bool is64bit,
            out string objs, out string compiles)
        {
            objs = "";
            compiles = "";

            DirectoryInfo info = new DirectoryInfo(swatFolder);
            FileInfo[] files = info.GetFiles(type == CODE_TYPE.FORTRAN ? "*.f*" : "*.c");
            if (files == null || files.Length == 0)
            {
                Console.WriteLine("Don't find any " + type.ToString() + " codes in " + swatFolder);
                return;
            }

            string subFolder = getSubFolderName(isDebug, is64bit);
            string o_prefix = subFolder + "/";
            string f_prefix = "";
            if (!inSameFolder)
            {
                o_prefix = "./";
                f_prefix = "../";
            }

            //-ffpe-trap=invalid,zero,overflow,underflow will make program stop when these errors happen
            //-fbacktrace:  Specifies that if the program crashes, a backtrace should be produced if possible, showing what functions or subroutines were being called at the time of the error.
            string compiler = "${" + (type == CODE_TYPE.FORTRAN ? "FC" : "CC") + "}";
            string commonFlag = "${" + (type == CODE_TYPE.FORTRAN ? NAME_FORTRAN_FLAG : NAME_C_FLAG) + "}";
            string debugFlag = "${" + (isDebug ? NAME_DEBUG_FLAG : NAME_RELEASE_FLAG) + "}";            
            string architecture = "${" + (is64bit ? NAME_ARCHITECTURE_64 : NAME_ARCHITECTURE_32) + "}";
            string modLocation = "";
            if (inSameFolder) modLocation = subFolder;

            StringBuilder makefilesb = new StringBuilder();
            StringBuilder objfilesb = new StringBuilder();

            foreach (FileInfo f in files)
            {
                string sourceName = f.Name.ToLower(); //BIOZONE.F is upper case, need to convert to lowercase
                if (sourceName.Equals("modparm.f")) continue;
                if (sourceName.Equals("readmgtsave.f")) continue; //for rev435, double define readmgt in readmgt.f and readmgtsave.f

                string longFortranflag = "";
                string o_file = "";

                if (sourceName.Contains(".f90"))
                {
                    o_file = o_prefix + sourceName.Replace(".f90", ".o");
                    if (System.Array.IndexOf(LONG_F90_NAMES, sourceName) > -1)
                        longFortranflag = "${" +NAME_LONG_FREE_FORMAT+ "}";
                }
                else if (sourceName.Contains(".f"))
                {
                    o_file = o_prefix + sourceName.Replace(".f", ".o");
                    if (System.Array.IndexOf(LONG_F_NAMES, sourceName) > -1)
                        longFortranflag = "${" + NAME_LONG_FIX_FORMAT + "}";
                }
                else if (sourceName.Contains(".c"))
                {
                    o_file = o_prefix + sourceName.Replace(".c", ".o");
                }

                string f_file = f_prefix + sourceName;


                //geneate the line for the rule
                string line_rule = o_file + ": " + f_file;
                //Add dependency modparm.f to main.f to recompile main.f when moadparm.f is changed
                //Add dependency main.o to other files to recompile main.f when moadparm.f is changed
                if (type == CODE_TYPE.FORTRAN)
                    line_rule += sourceName.Equals("main.f") ? " " + f_prefix + "modparm.f" : " " + o_prefix + "main.o";

                //generate the line for compile
                string line_compile = string.Format("\t{0} {6} {1} {2} {3} {4} -o {5}",
                    compiler, commonFlag, debugFlag, longFortranflag, f_file, o_file, architecture);                
                
                //As gfortran will try to search for the mod file in the same folder as Makefile, there is need
                //to specify the location of the mod file.
                //-J specify the location where the mod file will be saved, only used by main.f
                //-I specify the location where the mod file will be search for, used by other files except main.f
                if (type == CODE_TYPE.FORTRAN && inSameFolder)
                    line_compile += (sourceName.Equals("main.f") ? " -J " : " -I ") + modLocation;

                makefilesb.AppendLine("");
                makefilesb.AppendLine(line_rule);
                makefilesb.AppendLine(line_compile);
                objfilesb.Append(" " + o_file);                
            }

            objs = objfilesb.ToString();
            compiles = makefilesb.ToString();
        }

        private static void createSWATMakefile_FourVersion(string swatFolder)
        {
            if (!Directory.Exists(swatFolder))
            {
                Console.WriteLine(swatFolder + " doesn't exist.");
                return;
            }

            DirectoryInfo info = new DirectoryInfo(swatFolder);
            string makefile = info.FullName + "\\Makefile";

            //write to file
            using (StreamWriter writer = new StreamWriter(makefile))
            {
                //header
                writer.WriteLine(Header);
                writer.WriteLine(generateMakefile(swatFolder, true, true, false));
                writer.WriteLine(generateMakefile(swatFolder, true, true, true));
                writer.WriteLine(generateMakefile(swatFolder, true, false, false));
                writer.WriteLine(generateMakefile(swatFolder, true, false, true));
            }

            Console.WriteLine("Write " + makefile + " successfully!");
        }

        private static void createSWATMakefile_OneVersion(string swatFolder, bool inSameFolder = false)
        {
            if (!Directory.Exists(swatFolder))
            {
                Console.WriteLine(swatFolder + " doesn't exist.");
                return;
            }

            //input debug/release
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

            //input 32-bit or 64-bit
            Console.WriteLine("32-bit or 64-bit version? (32/64)");
            string bit = Console.ReadLine().ToLower().Trim();

            bool is64bit = false;
            if (bit.Equals("3") || bit.Equals("32"))
                is64bit = false;
            else if (bit.Equals("6") || bit.Equals("64"))
                is64bit = true;
            else
            {
                Console.WriteLine("Wrong input.");
                return;
            }

            //get the file path
            DirectoryInfo info = new DirectoryInfo(swatFolder);
            string makefile = info.FullName + "\\Makefile";
            string subFolderName = getSubFolderName(isDebug,is64bit);
            if (!inSameFolder)
            {
                makefile = info.FullName + "\\" + subFolderName;

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
                //header
                writer.WriteLine(Header);
                writer.WriteLine(generateMakefile(swatFolder,inSameFolder,isDebug,is64bit));
            }

            Console.WriteLine("Write " + makefile + " successfully!");
        }
    }
}
