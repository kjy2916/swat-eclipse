using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Data;
using System.IO;

namespace SWATPerformanceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TestSQLiteComparedToText();
            //TestExtractFromSQLite();
            //TestExtractFromText();
            Console.ReadLine();
            //ExtractSWAT_SQLite.ExtractAll();
            return;

            //find all swat exes
            string workingpath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(workingpath);
            System.IO.FileInfo[] files = dir.GetFiles("*.exe");
            foreach (System.IO.FileInfo f in files)
            {
                //run all swat exes and give the execution time
                if (f.FullName.Contains("SWATPerformanceTest")) continue;
                Console.WriteLine("Runing " + f.FullName);
                DateTime before = DateTime.Now;
                RunSWAT(f.FullName);
                DateTime after = DateTime.Now;
                Console.WriteLine(string.Format("******\nTime Used: {0} seconds\n******",after.Subtract(before).TotalSeconds));
            }
            Console.ReadLine();
        }

        static void RunSWAT(string swatexe)
        {
            Process myProcess = new Process();

            try
            {
                myProcess.StartInfo.UseShellExecute = false;
                myProcess.StartInfo.FileName = swatexe;
                myProcess.StartInfo.CreateNoWindow = true;
                myProcess.StartInfo.RedirectStandardError = true;
                myProcess.StartInfo.RedirectStandardOutput = true;
                myProcess.OutputDataReceived += (sender, agrs) =>
                {
                    if (agrs.Data != null) Console.WriteLine(agrs.Data);
                };
                myProcess.ErrorDataReceived += (sender, agrs) =>
                {
                    if (agrs.Data != null) Console.WriteLine(agrs.Data);
                };
                myProcess.Start();
                myProcess.BeginOutputReadLine();
                myProcess.BeginErrorReadLine();
                myProcess.WaitForExit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void TestExtractFromText()
        {
            Console.WriteLine("********************Text********************");
            ExtractSWAT_Text extract = new ExtractSWAT_Text(
                @"C:\zhiqiang\ModelTestWithSWATSQLite\LaSalle\LaSalle2012\Scenarios", "default");

            Console.WriteLine("******************** First Try ********************");
            extract.Extract(SourceType.REACH, 1, "FLOW_OUTcms");//case sensitive
            extract.Extract(1993, SourceType.REACH, 1, "FLOW_OUTcms");
            extract.Extract(2000, SourceType.REACH, 1, "FLOW_OUTcms");
            extract.Extract(2007, SourceType.REACH, 1, "FLOW_OUTcms");
            extract.Extract(SourceType.HRU, 1, "ETmm");
            extract.Extract(1993, SourceType.HRU, 1, "ETmm");
            extract.Extract(2000, SourceType.HRU, 1, "ETmm");
            extract.Extract(2007, SourceType.HRU, 1, "ETmm");

            Console.WriteLine("******************** Second Try ********************");
            extract.Extract(SourceType.REACH, 1, "FLOW_OUTcms");//case sensitive
            extract.Extract(1993, SourceType.REACH, 1, "FLOW_OUTcms");
            extract.Extract(2000, SourceType.REACH, 1, "FLOW_OUTcms");
            extract.Extract(2007, SourceType.REACH, 1, "FLOW_OUTcms");
            extract.Extract(SourceType.HRU, 1, "ETmm");
            extract.Extract(1993, SourceType.HRU, 1, "ETmm");
            extract.Extract(2000, SourceType.HRU, 1, "ETmm");
            extract.Extract(2007, SourceType.HRU, 1, "ETmm");
        }

        static void TestExtractFromSQLite()
        {
            Console.WriteLine("********************SQLite********************");
            using(ExtractSWAT_SQLite extract = 
                new ExtractSWAT_SQLite(@"C:\zhiqiang\ModelTestWithSWATSQLite\LaSalle\LaSalle2012\Scenarios\Default\txtinout\result_627.db3"))
            {
                Console.WriteLine("******************** First Try ********************");
                extract.Extract(SourceType.REACH, 1, "FLOW_OUTcms");//not case sensitive
                extract.Extract(1993, SourceType.REACH, 1, "FLOW_OUTcms");
                extract.Extract(2000, SourceType.REACH, 1, "FLOW_OUTcms");
                extract.Extract(2007, SourceType.REACH, 1, "FLOW_OUTcms");
                extract.Extract(SourceType.HRU, 1, "ETmm");
                extract.Extract(1993, SourceType.HRU, 1, "ETmm");
                extract.Extract(2000, SourceType.HRU, 1, "ETmm");
                extract.Extract(2007, SourceType.HRU, 1, "ETmm");

                Console.WriteLine("******************** Second Try ********************");
                extract.Extract(SourceType.REACH, 1, "FLOW_OUTcms");//not case sensitive
                extract.Extract(1993, SourceType.REACH, 1, "FLOW_OUTcms");
                extract.Extract(2000, SourceType.REACH, 1, "FLOW_OUTcms");
                extract.Extract(2007, SourceType.REACH, 1, "FLOW_OUTcms");
                extract.Extract(SourceType.HRU, 1, "ETmm");
                extract.Extract(1993, SourceType.HRU, 1, "ETmm");
                extract.Extract(2000, SourceType.HRU, 1, "ETmm");
                extract.Extract(2007, SourceType.HRU, 1, "ETmm");
            }            
        }

        static void TestSQLiteComparedToText()
        {
            Console.WriteLine("********************SQLite vs Text********************");

            SQLiteTest test = new SQLiteTest(@"C:\zhiqiang\ModelTestWithSWATSQLite\LaSalle\LaSalle2012\Scenarios", "default");

            //reach FLOW_OUTcms
            //Dictionary<string, double> reachResults = test.Compare(SourceType.REACH, "FLOW_OUTcms");
            Dictionary<string, double> hruResults = test.Compare(SourceType.HRU, "ETmm");

            Console.WriteLine("Press any key to show results");
            Console.ReadLine();

            Console.WriteLine("********************Results********************");
            string format = "{0}: R2 = {1:F4}";
            //foreach (string label in reachResults.Keys)
            //    Console.WriteLine(string.Format(format, label, reachResults[label]));
            foreach (string label in hruResults.Keys)
                Console.WriteLine(string.Format(format, label, hruResults[label]));
        }
    }
}
