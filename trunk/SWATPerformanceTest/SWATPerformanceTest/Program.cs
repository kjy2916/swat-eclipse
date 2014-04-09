using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace SWATPerformanceTest
{
    class Program
    {
        static void Main(string[] args)
        {
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
    }
}
