using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace SWATPerformanceTest
{
    enum SourceType
    {
        REACH,
        SUBBASIN,
        HRU,
        WATER,
        RESERVOIR
    }

    class ExtractSWAT_Text
    {

        public ExtractSWAT_Text(string dir, string name)
        {
            scenariosDir = dir;
            scenarioName = name;
            setSettings();
            setScenario();
        }

        #region Structure for HRU and basin

        /// <summary>
        /// Stores the range of hru numbers associated with a subbasin
        /// Also records if a subbasin has a pond and if it has a reservoir
        /// </summary>
        public struct Hrus
        {
            // the following two lists are assumed to be sorted
            public List<int> hruNumbers; // hrus in output.hru
            public List<int> pondNumbers; // hrus in output.wtr
        }

        /// <summary>
        /// Maps from basin number to hru numbers, pond  numbers and reservoir numbers
        /// Newest SWAT2005.exe uses 5 digit hru numbers, which changes positions
        /// in output.hru, so we record this.
        /// </summary>
        public struct Basins
        {
            public Dictionary<int, Hrus> hrus; // mapping from basin to hru numbers and pond numbers
            public Dictionary<int, int> reservoirs; // mapping from basin number to reservoir number (only 
            // for basins with reservoirs)
            public int fiveDigitHruShift; // set to 1 if hru numbers in output.hru are 5 digits, else 0
        }

        #endregion

        #region SWAT Output Format 

        // The following constants are also found in mwSWATResults.cs
        // Both files should be changed in the same way.
        // common to all SWAT output files
        private const int headerLines = 8;
        // column numbers start from 0
        // so if a field starts in column c
        // and has width w
        // then a field value in a line l is obtained from the string
        // l.Substring(c, w)
        private const int dateWidth = 4;
        private const string figFile = @"fig.fig";
        private const int figCommandCol = 10;
        private const int figCommandWidth = 6;
        private const int figResCol = 22;
        private const int figResWidth = 6;
        private const int figRouteBasinCol = 22;
        private const int figRouteBasinWidth = 6;
        private const string reachDataFile = @"output.rch";
        private const int reachFirstDataCol = 37;
        private const int reachDataWidth = 12;
        private const int reachNumCol = 5;
        private const int reachNumWidth = 5;
        private const int reachDateCol = 21;
        private const string subDataFile = @"output.sub";
        private const int subFirstDataCol = 34;
        private const int subDataWidth = 10;
        private const int subNumCol = 6;
        private const int subNumWidth = 4;
        private const int subDateCol = 20;
        private const string hruDataFile = @"output.hru";
        private int hruFirstDataCol = 42;
        private const int hruDataWidth = 10;
        private int hruNumCol = 4;
        private const int hruNumWidth = 4;
        private int hruSubNumCol = 17;
        private const int hruSubNumWidth = 5;
        private int hruDateCol = 28;
        private const string waterDataFile = @"output.wtr";
        private int waterFirstDataCol = 42;
        private const int waterDataWidth = 10;
        private int waterNumCol = 4;
        private const int waterNumWidth = 4;
        private int waterSubNumCol = 17;
        private const int waterSubNumWidth = 5;
        private int waterDateCol = 28;
        private const string reservoirDataFile = @"output.rsv";
        private const int reservoirFirstDataCol = 19;
        private const int reservoirDataWidth = 12;
        private const int reservoirNumCol = 9;
        private const int reservoirNumWidth = 5;
        private const int reservoirDateCol = 15;

        #endregion

        #region Initialize Basic Structure, just call setSettings and setScenario

        private string scenariosDir = "";
        private string scenarioName = "";
        private List<string> reachVars = new List<string>();
        private List<string> subVars = new List<string>();
        private List<string> hruVars = new List<string>();
        private List<string> waterVars = new List<string>();
        private List<string> reservoirVars = new List<string>();
        private Basins basins;

        public int NumberofSubbasin { get { return basins.hrus.Count; } }
        public int NumberofHRU { get { return basins.hrus[NumberofSubbasin].hruNumbers.Last(); } }
        public int StartYear { get { return startYear; } }
        public int EndYear { get { return finishYear; } }

        private int startYear, finishYear, startDay, finishDay;
        private int IPRINT, NYSKIP;

        /// <summary>
        /// Read basic settings from file.cio
        /// </summary>
        void setSettings()
        {
            string cioFile = scenariosDir + @"\" + scenarioName + @"\TxtInOut\file.cio";

            string line;
            // get number of records
            int numYears;
            using (StreamReader sr = new StreamReader(cioFile))
            {
                // skip 7 lines
                for (int i = 1; i <= 7; i++) sr.ReadLine();
                line = sr.ReadLine();
                numYears = Int32.Parse(line.Substring(0, 16));
                line = sr.ReadLine();
                startYear = Int32.Parse(line.Substring(0, 16));
                finishYear = startYear + numYears - 1;
                line = sr.ReadLine();
                try // may be blank, so allow parse to fail
                {
                    startDay = Int32.Parse(line.Substring(0, 16));
                    if (startDay == 0) startDay = 1;
                }
                catch (Exception)
                {
                    startDay = 1;
                }
                line = sr.ReadLine();
                try // may be blank, so allow parse to fail
                {
                    finishDay = Int32.Parse(line.Substring(0, 16));
                    if (finishDay == 0) finishDay = 365 + (isLeapYear(finishYear) ? 1 : 0);
                }
                catch (Exception)
                {
                    finishDay = 365 + (isLeapYear(finishYear) ? 1 : 0);
                }
                // skip 47 lines, so next is line 59
                for (int i = 1; i <= 47; i++) sr.ReadLine();
                line = sr.ReadLine();
                IPRINT = Int32.Parse(line.Substring(0, 16));
                line = sr.ReadLine();
                NYSKIP = Int32.Parse(line.Substring(0, 16));
                startYear += NYSKIP;
            }
        }

        /// <summary>
        /// Read all basic information of all scenarios in the given folder
        /// </summary>
        void setScenario()
        {
            if (!System.IO.Directory.Exists(scenariosDir)) return;
            if (!System.IO.Directory.Exists(scenariosDir + @"\" + scenarioName)) return;

            basins = new Basins();
            basins.hrus = new Dictionary<int, Hrus>();
            basins.reservoirs = new Dictionary<int, int>();
            basins.fiveDigitHruShift = 0;
            PopulateBasins(ref basins, scenarioName);
        }

        /// <summary>
        /// 1. Get variables for reach, subbasin, hru, pond and reservoir outputs
        /// 2. Get all hrus for each subbasin
        /// 3. Get all ponds for each subbasin
        /// </summary>
        /// <param name="basins"></param>
        /// <param name="dirName">should be the scenario name</param>
        /// <returns></returns>
        /// <remarks>Should be just called once for each output</remarks>
        bool PopulateBasins(ref Basins basins, string dirName)
        {
            string dir = scenariosDir + @"\" + dirName;
            string hruData = dir + @"\TxtInOut\" + hruDataFile;
            string reachData = dir + @"\TxtInOut\" + reachDataFile;
            string figData = dir + @"\TxtInOut\" + figFile;
            string line;
            try
            {
                // we should be using rch.dat, sub.dat and hru.dat for the field widths and 
                // variables to be found in output.rch etc, but rch.dat at least is unreliable.
                // So we assume we know the field positions and widths, and read the variables from the
                // output files.
                using (StreamReader sr1 = new StreamReader(reachData))
                {
                    reachVars = new List<string>();
                    // check if SWAT 2012
                    sr1.ReadLine();
                    line = sr1.ReadLine();
                    if (line.Contains("2012"))
                    {
                        // SWAT 2012
                        // HRU column in output.wtr and output.hru has an extra space before and after it
                        waterFirstDataCol = 44;
                        waterNumCol = 5;
                        waterDateCol = 30;
                        waterSubNumCol = 19;
                        hruFirstDataCol = 44;
                        hruNumCol = 5;
                        hruDateCol = 30;
                        hruSubNumCol = 19;
                    }
                    // skip remaining header lines
                    for (int i = 0; i < headerLines - 2; i++) sr1.ReadLine();
                    line = sr1.ReadLine();
                    int count = (line.Length - reachFirstDataCol) / reachDataWidth;
                    for (int i = 0; i < count; i++)
                    {
                        reachVars.Add(line.Substring(reachFirstDataCol + i * reachDataWidth, reachDataWidth).Trim());
                    }
                }
                string subData = dir + @"\TxtInOut\" + subDataFile;
                using (StreamReader sr2 = new StreamReader(subData))
                {
                    subVars = new List<string>();
                    // skip header lines
                    for (int i = 0; i < headerLines; i++) sr2.ReadLine();
                    line = sr2.ReadLine();
                    int count = (line.Length - subFirstDataCol) / subDataWidth;
                    for (int i = 0; i < count; i++)
                    {
                        subVars.Add(line.Substring(subFirstDataCol + i * subDataWidth, subDataWidth).Trim());
                    }
                }
                string waterData = dir + @"\TxtInOut\" + waterDataFile;
                if (File.Exists(waterData))
                {
                    using (StreamReader sr3 = new StreamReader(waterData))
                    {
                        waterVars = new List<string>();
                        // skip header lines
                        for (int i = 0; i < headerLines; i++) sr3.ReadLine();
                        line = sr3.ReadLine();
                        int count = (line.Length - waterFirstDataCol) / waterDataWidth;
                        for (int i = 0; i < count; i++)
                        {
                            waterVars.Add(line.Substring(waterFirstDataCol + i * waterDataWidth, waterDataWidth).Trim());
                        }
                    }
                }
                string reservoirData = dir + @"\TxtInOut\" + reservoirDataFile;
                using (StreamReader sr4 = new StreamReader(reservoirData))
                {
                    reservoirVars = new List<string>();
                    // skip header lines
                    for (int i = 0; i < headerLines; i++) sr4.ReadLine();
                    line = sr4.ReadLine();
                    int count = (line.Length - reservoirFirstDataCol) / reservoirDataWidth;
                    for (int i = 0; i < count; i++)
                    {
                        reservoirVars.Add(line.Substring(reservoirFirstDataCol + i * reservoirDataWidth, reservoirDataWidth).Trim());
                    }
                }
                // use output.hru to determine hru numbers for each basin
                // Note we do this for the current, not Default, run since different runs may have different hru selections
                using (StreamReader sr = new StreamReader(hruData))
                {
                    // skip header lines
                    for (int i = 0; i < headerLines; i++) sr.ReadLine();
                    // next is var names line
                    line = sr.ReadLine();
                    // store hru variables
                    hruVars = new List<string>();
                    int count = (line.Length - hruFirstDataCol) / hruDataWidth;
                    for (int i = 0; i < count; i++)
                    {
                        hruVars.Add(line.Substring(hruFirstDataCol + i * hruDataWidth, hruDataWidth).Trim());
                    }
                    int currentBasin = 0; // basin numbers start from 1
                    List<int> hruNums = new List<int>();
                    while ((line = sr.ReadLine()) != null)
                    {
                        // first time round, check for change in output.hru in latest
                        // swat2005.exe: hrus allowed 5 digits, so following numbers shifted 1 place to right
                        // detected by seeing if last character of 5-digit hru number is a non-space 
                        // (should be a 1 if hrus have 5 digits)
                        if ((currentBasin == 0) && (line.Substring(hruNumCol + hruNumWidth, 1) != " "))
                        {
                            basins.fiveDigitHruShift = 1;
                        }
                        int basin = Int32.Parse(line.Substring(hruSubNumCol + basins.fiveDigitHruShift, hruSubNumWidth));
                        int hru = Int32.Parse(line.Substring(hruNumCol, hruNumWidth + basins.fiveDigitHruShift));
                        if (basin == currentBasin)
                        {
                            if (!hruNums.Contains(hru)) hruNums.Add(hru);
                        }
                        else
                        {
                            // store last basin, unless this is the first loop cycle
                            if (currentBasin != 0)
                            {
                                Hrus hrus = new Hrus();
                                hrus.hruNumbers = new List<int>();
                                hrus.pondNumbers = new List<int>();
                                foreach (int i in hruNums) hrus.hruNumbers.Add(i);
                                basins.hrus.Add(currentBasin, hrus);
                            }
                            if (basin < currentBasin) break; // finished with hrus
                            // start a new basin
                            currentBasin = basin;
                            hruNums.Clear();
                            hruNums.Add(hru);
                        }
                    }
                    // may have curtailed hru file: check to see if we got the last entry
                    if ((currentBasin > 0) && (!basins.hrus.ContainsKey(currentBasin)))
                    {
                        Hrus hrus = new Hrus();
                        hrus.hruNumbers = new List<int>();
                        hrus.pondNumbers = new List<int>();
                        foreach (int i in hruNums) hrus.hruNumbers.Add(i);
                        basins.hrus.Add(currentBasin, hrus);
                    }
                }
                // hru data can be curtailed to just some hrus, so we have to read
                // the reach data file to make sure we have all subbasins
                using (StreamReader sr7 = new StreamReader(reachData))
                {
                    // skip header lines and var line
                    for (int i = 0; i <= headerLines; i++) sr7.ReadLine();
                    int currentBasin = 0; // basin numbers start from 1
                    while ((line = sr7.ReadLine()) != null)
                    {
                        int basin = Int32.Parse(line.Substring(reachNumCol, reachNumWidth));
                        if (currentBasin != 0)
                        {
                            if (!basins.hrus.ContainsKey(currentBasin))
                            // new basin that was not in output.hru
                            {
                                Hrus hrus = new Hrus();
                                hrus.hruNumbers = new List<int>();
                                hrus.pondNumbers = new List<int>();
                                basins.hrus.Add(currentBasin, hrus);
                            }
                        }
                        if (basin < currentBasin) break; // finished with reach file
                        currentBasin = basin;
                    }
                }

                string waterData2 = dir + @"\TxtInOut\" + waterDataFile;
                if (File.Exists(waterData2))
                {
                    using (StreamReader sr5 = new StreamReader(waterData2))
                    {
                        // skip header lines and var line
                        for (int i = 0; i <= headerLines; i++) sr5.ReadLine();
                        int currentBasin = 0; // basin numbers start from 1
                        List<int> pondNums = new List<int>();
                        int basin = 0;
                        int hru = 0;
                        while ((line = sr5.ReadLine()) != null)
                        {
                            basin = Int32.Parse(line.Substring(waterSubNumCol + basins.fiveDigitHruShift, waterSubNumWidth));
                            hru = Int32.Parse(line.Substring(waterNumCol, waterNumWidth + basins.fiveDigitHruShift));
                            if (basin == currentBasin)
                            {
                                if (!pondNums.Contains(hru)) pondNums.Add(hru);
                            }
                            else
                            {
                                // store last basin, unless first loop
                                if (currentBasin != 0) // not first loop cycle
                                {
                                    foreach (int i in pondNums)
                                    {
                                        basins.hrus[currentBasin].pondNumbers.Add(i);
                                    }
                                }
                                if (basin < currentBasin) break; // finished with ponds
                                currentBasin = basin;
                                pondNums.Clear();
                                pondNums.Add(hru);
                            }
                        }
                    }
                }
                // use fig.fig file to set up reservoir data
                using (StreamReader sr6 = new StreamReader(figData))
                {
                    int lastBasin = 0;
                    while ((line = sr6.ReadLine()) != null)
                    {
                        int command;
                        try
                        {
                            command = Int32.Parse(line.Substring(figCommandCol, figCommandWidth));
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                        if (command == 2) // route command
                        {
                            // save basin in case next command is a reservoir routing
                            lastBasin = Int32.Parse(line.Substring(figRouteBasinCol, figRouteBasinWidth));
                        }
                        else if (command == 3) // routres command
                        {
                            int res = Int32.Parse(line.Substring(figResCol, figResWidth));
                            // basin removed from routres in SWAT 2012
                            //int basin = Int32.Parse(line.Substring(figResBasinCol, figResBasinWidth));
                            basins.reservoirs.Add(lastBasin, res);
                            //MessageBox.Show("Run " + dirName + " has reservoir " + res.ToString() + 
                            //                " in basin " + lastBasin.ToString());
                        }
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        #endregion

        #region Utility Functions

        private static bool isLeapYear(int yr)
        {
            if (yr % 4 == 0)
            {
                if (yr % 100 == 0) return (yr % 400 == 0);
                else return true;
            }
            else return false;
        }

        /// <summary>
        /// Convert day, month string and year to Julian day within that year
        /// </summary>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static int toJulianDay(int day, int month, int year)
        {
            int leapAdjust = isLeapYear(year) ? 1 : 0;
            if (month == 1) return day;
            if (month == 2) return 31 + day;
            if (month == 3) return 59 + day + leapAdjust;
            if (month == 4) return 90 + day + leapAdjust;
            if (month == 5) return 120 + day + leapAdjust;
            if (month == 6) return 151 + day + leapAdjust;
            if (month == 7) return 181 + day + leapAdjust;
            if (month == 8) return 212 + day + leapAdjust;
            if (month == 9) return 243 + day + leapAdjust;
            if (month == 10) return 273 + day + leapAdjust;
            if (month == 11) return 304 + day + leapAdjust;
            if (month == 12) return 334 + day + leapAdjust;
            return -1;
        }

        /// <summary>
        /// Convert date from Julian to md
        /// </summary>
        /// <param name="year">year as int</param>
        /// <param name="julian">Julian day in year as int</param>
        /// <param name="month">output month as string</param>
        /// <param name="monNum">output month as int</param>
        /// <param name="day">output day in month as int</param>
        private static void fromJulian(int year, int julian, out string month, out int monNum, out int day)
        {
            int leapAdjust = isLeapYear(year) ? 1 : 0;
            if (julian > 334 + leapAdjust)
            {
                month = "December";
                monNum = 12;
                day = julian - (334 + leapAdjust);
            }
            else if (julian > 304 + leapAdjust)
            {
                month = "November";
                monNum = 11;
                day = julian - (304 + leapAdjust);
            }
            else if (julian > 273 + leapAdjust)
            {
                month = "October";
                monNum = 10;
                day = julian - (273 + leapAdjust);
            }
            else if (julian > 243 + leapAdjust)
            {
                month = "September";
                monNum = 9;
                day = julian - (243 + leapAdjust);
            }
            else if (julian > 212 + leapAdjust)
            {
                month = "August";
                monNum = 8;
                day = julian - (212 + leapAdjust);
            }
            else if (julian > 181 + leapAdjust)
            {
                month = "July";
                monNum = 7;
                day = julian - (181 + leapAdjust);
            }
            else if (julian > 151 + leapAdjust)
            {
                month = "June";
                monNum = 6;
                day = julian - (151 + leapAdjust);
            }
            else if (julian > 120 + leapAdjust)
            {
                month = "May";
                monNum = 5;
                day = julian - (120 + leapAdjust);
            }
            else if (julian > 90 + leapAdjust)
            {
                month = "April";
                monNum = 4;
                day = julian - (90 + leapAdjust);
            }
            else if (julian > 59 + leapAdjust)
            {
                month = "March";
                monNum = 3;
                day = julian - (59 + leapAdjust);
            }
            else if (julian > 31)
            {
                month = "February";
                monNum = 2;
                day = julian - 31;
            }
            else
            {
                month = "January";
                monNum = 1;
                day = julian;
            }
        }

        /// <summary>
		/// Calculate period in months from start to finish Julian dates
		/// </summary>
		/// <param name="startYear"></param>
		/// <param name="startDay"></param>
		/// <param name="finishYear"></param>
		/// <param name="finishDay"></param>
		/// <returns></returns>
		private int PeriodInMonths(int startYear, int startDay, int finishYear, int finishDay)
		{
			int startMonth;
			int finishMonth;
			string dummyMonth;
			int dummyDay;
			fromJulian(startYear, startDay, out dummyMonth, out startMonth, out dummyDay);
			fromJulian(finishYear, finishDay, out dummyMonth, out finishMonth, out dummyDay);
			if (startYear < finishYear)
			{
				// months in first year
				int result = 13 - startMonth;
				// add months in intermediate years
				int currYear = startYear + 1;
				while (currYear < finishYear)
				{
					result += 12;
					currYear++;
				}
				// add months in final year
				result += finishMonth;
				return result;
			}
			else if (startYear == finishYear)
				if (finishMonth >= startMonth)
					return finishMonth - startMonth + 1;
				else return 0;
			else return 0;
		}

        /// <summary>
        /// Calculate period in days from start to finish Julian dates
        /// </summary>
        /// <param name="startYear"></param>
        /// <param name="startDay"></param>
        /// <param name="finishDay"></param>
        /// <param name="finishYear"></param>
        /// <returns></returns>
        private int PeriodInDays(int startYear, int startDay, int finishYear, int finishDay)
        {
            if (startYear < finishYear)
            {
                // days in first year
                int result = 366 + (isLeapYear(startYear) ? 1 : 0) - startDay;
                // add days in intermediate years
                int currYear = startYear + 1;
                while (currYear < finishYear)
                {
                    result += 365 + (isLeapYear(currYear) ? 1 : 0);
                    currYear++;
                }
                // add days in final year
                result += finishDay;
                return result;
            }
            else if (startYear == finishYear)
                if (finishDay >= startDay)
                    return finishDay - startDay + 1;
                else return 0;
            else return 0;
        }

        #endregion

        #region Extract

        string FileForRow(string run, SourceType source)
        {
            string dir = scenariosDir + @"\" + run + @"\TxtInOut\";
            switch (source)
            {
                case SourceType.REACH:
                    return dir + reachDataFile;
                case SourceType.SUBBASIN:
                    return dir + subDataFile;
                case SourceType.HRU:
                    return dir + hruDataFile;
                case SourceType.WATER:
                    return dir + waterDataFile;
                case SourceType.RESERVOIR:
                    return dir + reservoirDataFile;
                default: // incomplete row
                    return "";
            }
        }

        /// <summary>
        /// Find number of lines to skip after header to first record in output.hru
        /// and number of lines to skip between records in output.hru
        /// </summary>
        /// <param name="run"></param>
        /// <param name="hru"></param>
        /// <param name="skipNum">number of lines to skip between pond records</param>
        /// <param name="nextRecordNum">number of line to skip after header</param>
        private void HruSkipCount(int hru, ref int skipNum, ref int nextRecordNum)
        {
            foreach (KeyValuePair<int, Hrus> kvp in basins.hrus)
            {
                Hrus hrus = kvp.Value;
                int indx = hrus.hruNumbers.IndexOf(hru); // assumes list is sorted
                if (indx >= 0)
                    nextRecordNum = skipNum + indx + 1;
                skipNum = skipNum + hrus.hruNumbers.Count;
            }
        }

        /// <summary>
        /// Find number of lines to skip after header to first record in output.wtr
        /// and number of lines to skip between records in output.wtr
        /// </summary>
        /// <param name="run"></param>
        /// <param name="hru"></param>
        /// <param name="skipNum">number of lines to skip between pond records</param>
        /// <param name="nextRecordNum">number of line to skip after header</param>
        private void PondSkipCount(int hru, ref int skipNum, ref int nextRecordNum)
        {
            foreach (KeyValuePair<int, Hrus> kvp in basins.hrus)
            {
                Hrus hrus = kvp.Value;
                int indx = hrus.pondNumbers.IndexOf(hru); // assumes list is sorted
                if (indx >= 0)
                    nextRecordNum = skipNum + indx + 1;
                skipNum = skipNum + hrus.pondNumbers.Count;
            }
        }

        private void SkipCount(SourceType source, int id, ref int skipNum, ref int nextRecordNum)
        {
            if (source == SourceType.HRU)
            {
                int hruNum = id;
                HruSkipCount(hruNum, ref skipNum, ref nextRecordNum);
                if (skipNum == 0)
                {
                    Console.WriteLine("There no HRUs!");
                    return;
                }
            }
            else if (source == SourceType.WATER)
            {
                int hruNum = id;
                PondSkipCount(hruNum, ref skipNum, ref nextRecordNum);
                if (skipNum == 0)
                {
                    Console.WriteLine("There no ponds!");
                    return;
                }
            }
            else if (source == SourceType.RESERVOIR)
            {
                int basin = id;
                nextRecordNum = basins.reservoirs[basin]; // reservoir number
                skipNum = basins.reservoirs.Count; // number of reservoirs
                if (skipNum == 0)
                {
                    Console.WriteLine("There no reservois!");
                    return;
                }
            }
            else // reach or sub
            {
                nextRecordNum = id;
                skipNum = basins.hrus.Count; // number of subbasins
            }

        }

        void VarPos(SourceType source, string var, out int length, out int pos, out int datePos)
        {
            int index = 0;
            length = 0;
            pos = 0;
            datePos = 0;
            switch (source)
            {
                case SourceType.REACH:
                    index = reachVars.IndexOf(var);
                    length = reachDataWidth;
                    pos = reachFirstDataCol + index * reachDataWidth;
                    datePos = reachDateCol;
                    return;
                case SourceType.SUBBASIN:
                    index = subVars.IndexOf(var);
                    length = subDataWidth;
                    pos = subFirstDataCol + index * subDataWidth;
                    datePos = subDateCol;
                    return;
                case SourceType.HRU:
                    index = hruVars.IndexOf(var);
                    length = hruDataWidth;
                    pos = hruFirstDataCol + index * hruDataWidth;
                    datePos = hruDateCol;
                    return;
                case SourceType.WATER:
                    index = waterVars.IndexOf(var);
                    length = waterDataWidth;
                    pos = waterFirstDataCol + index * waterDataWidth;
                    datePos = waterDateCol;
                    return;
                case SourceType.RESERVOIR:
                    index = reservoirVars.IndexOf(var);
                    length = reservoirDataWidth;
                    pos = reservoirFirstDataCol + index * reservoirDataWidth;
                    datePos = reservoirDateCol;
                    return;
                default:
                    return;
            }
        }

        /// <summary>
        /// convert start and finish day to Julian format
        /// </summary>
        /// <param name="requestedStartYear"></param>
        /// <param name="requestedStartMonth"></param>
        /// <param name="requestedStartDate"></param>
        /// <param name="requestedFinishYear"></param>
        /// <param name="requestedFinishMonth"></param>
        /// <param name="requestedFinishDate"></param>
        /// <param name="requestedStartDay"></param>
        /// <param name="requestedFinishDay"></param>
        private void GetRequestStartFinishDay(int requestedStartYear, int requestedStartMonth, int requestedStartDate,
            int requestedFinishYear, int requestedFinishMonth, int requestedFinishDate,
            ref int requestedStartDay, ref int requestedFinishDay)
        {
            try
            {
                requestedStartDay = toJulianDay(requestedStartDate, requestedStartMonth, requestedStartYear);
            }
            catch (Exception)
            {
                requestedStartDay = startDay;
            }
            try
            {
                requestedFinishDay = toJulianDay(requestedFinishDate, requestedFinishMonth, requestedFinishYear);
            }
            catch (Exception)
            {
                requestedFinishDay = finishDay;
            }
            if ((requestedStartDay < 0) ||
                (requestedStartYear < startYear) ||
                ((requestedStartYear == startYear) && (requestedStartDay < startDay)))
            {
                int day;
                string month;
                int dummy;
                fromJulian(startYear, startDay, out month, out dummy, out day);
                Console.WriteLine("Earlist start day is " + day.ToString() + " " + month + " " + startYear.ToString(), "SWATPlot");
                requestedStartDay = startDay;
                requestedStartYear = startYear;
            }
            if ((requestedFinishDay < 0) ||
                (requestedFinishYear > finishYear) ||
                ((requestedFinishYear == finishYear) && (requestedFinishDay > finishDay)))
            {
                int day;
                string month;
                int dummy;
                fromJulian(finishYear, finishDay, out month, out dummy, out day);
                Console.WriteLine("Latest finish day is " + day.ToString() + " " + month + " " + startYear.ToString());
                requestedFinishDay = finishDay;
                requestedFinishYear = finishYear;
            }
        }
        
        /// <summary>
        /// Extract results for all simulation period
        /// </summary>
        /// <param name="source"></param>
        /// <param name="id"></param>
        /// <param name="var"></param>
        public DataTable Extract(SourceType source, int id, string var)
        {
            int requestStartDay = 0, requestStartMonth = 0;
            int requestFinishDay = 0, requestFinishMonth = 0;
            string month;
            fromJulian(startYear, startDay, out month, out requestStartMonth, out requestStartDay);
            fromJulian(finishYear, finishDay, out month, out requestFinishMonth, out requestFinishDay);

            return Extract(startYear, requestStartMonth, requestStartDay,
                finishYear, requestFinishMonth, requestFinishDay,
                source, id, var);
        }

        /// <summary>
        /// Extract results for given start and finish year
        /// </summary>
        /// <param name="requestedStartYear"></param>
        /// <param name="requestedFinishYear"></param>
        /// <param name="source"></param>
        /// <param name="id"></param>
        /// <param name="var"></param>
        public DataTable Extract(int year,
            SourceType source, int id, string var)
        {
            return Extract(year, 1, 1,
                year, 12, 31,
                source, id, var);
        }

        /// <summary>
        /// Extract results for given start and finish year
        /// </summary>
        /// <param name="requestedStartYear"></param>
        /// <param name="requestedFinishYear"></param>
        /// <param name="source"></param>
        /// <param name="id"></param>
        /// <param name="var"></param>
        public DataTable Extract(int requestedStartYear, int requestedFinishYear, 
            SourceType source, int id, string var)
        {
            return Extract(requestedStartYear, 1, 1,
                requestedFinishYear, 12, 31,
                source, id, var);
        }

        /// <summary>
        /// Extract results for given start and finish date
        /// </summary>
        /// <param name="requestedStartYear"></param>
        /// <param name="requestedStartMonth"></param>
        /// <param name="requestedStartDate"></param>
        /// <param name="requestedFinishYear"></param>
        /// <param name="requestedFinishMonth"></param>
        /// <param name="requestedFinishDate"></param>
        /// <param name="source"></param>
        /// <param name="id"></param>
        /// <param name="var"></param>
        public DataTable Extract(
            int requestedStartYear, int requestedStartMonth, int requestedStartDate,
            int requestedFinishYear, int requestedFinishMonth, int requestedFinishDate,
            SourceType source, int id, string var)
        {
            try
            {
                //convert start and finish day to Julian format
                int requestedStartDay = 0, requestedFinishDay = 0;
                GetRequestStartFinishDay(requestedStartYear, requestedStartMonth, requestedStartDate,
                    requestedFinishYear, requestedFinishMonth, requestedFinishDate,
                    ref requestedStartDay, ref requestedFinishDay);

                //get number of records and offset
                int numRecords;
                int startOffset;
                int finishOffset; // not in fact used

                if (IPRINT == 0)
                {
                    numRecords = PeriodInMonths(requestedStartYear, requestedStartDay, requestedFinishYear, requestedFinishDay);
                    startOffset = PeriodInMonths(startYear, startDay, requestedStartYear, requestedStartDay) - 1;
                    finishOffset = PeriodInMonths(requestedFinishYear, requestedFinishDay, finishYear, finishDay) - 1;
                }
                else if (IPRINT == 1)
                {
                    numRecords = PeriodInDays(requestedStartYear, requestedStartDay, requestedFinishYear, requestedFinishDay);
                    startOffset = PeriodInDays(startYear, startDay, requestedStartYear, requestedStartDay) - 1;
                    finishOffset = PeriodInDays(requestedFinishYear, requestedFinishDay, finishYear, finishDay) - 1;
                }
                else
                {
                    numRecords = requestedFinishYear - requestedStartYear + 1;
                    startOffset = requestedStartYear - startYear;
                    finishOffset = finishYear - requestedFinishYear;
                }                

                //get number of row which could be skip
                int nextRecordNum = 0;
                int skipNum = 0;
                SkipCount(source, id, ref skipNum, ref nextRecordNum);

               
                //get variable location
                int varLength;
                int varPos;
                int datePos;
                VarPos(source, var, out varLength, out varPos, out datePos);

                if (source == SourceType.HRU || source == SourceType.WATER)
                {
                    // for output.hru and output.wtr need to shift by 1 if hrus have five digits
                    int shift = this.basins.fiveDigitHruShift;
                    varPos += shift;
                    datePos += shift;
                }



                //the file
                string nextFile = FileForRow(scenarioName, source); //get result file from type

                int count = 0;
                //we  will start actually recording data when count reaches 0
                count = 0 - startOffset;

                //data save here in the array
                DataTable dt = new DataTable("TEXT");             
                dt.Columns.Add("TIME", typeof(DateTime));
                dt.Columns.Add(var, typeof(double));


                bool finished = false;
                int currentYear = startYear;
                int currentDay = startDay;
                bool yearChecked = false;

                string valstr;
                double val;
                string line;

                using (StreamReader next = new StreamReader(nextFile))
                {
                    DateTime readStartTime = DateTime.Now;
                    int linesToSkip = headerLines + nextRecordNum;
                    for (int i = 1; i <= linesToSkip; i++) next.ReadLine();
                    while (!finished)
                    {
                        line = next.ReadLine();
                        if (line == null) finished = true;
                        else
                        {
                            string date = line.Substring(datePos, dateWidth).Trim();
                            if ((date.Length == 4) && !yearChecked)
                            {
                                if (!date.Equals(startYear.ToString()))
                                {
                                    Console.WriteLine("Start year " + startYear.ToString() +
                                                    " from file.cio does not agree with start year " + date +
                                                    " from " + nextFile + ". ");
                                }
                                yearChecked = true;
                            }
                            if (((IPRINT != 2) && (date.Length == 4)) ||
                                (date.IndexOf('.') >= 0))
                            {
                                // skip end of year or summary lines
                                for (int j = 1; j < skipNum; j++) next.ReadLine();
                                line = next.ReadLine();
                                if (line == null)
                                {
                                    finished = true;
                                    break;
                                }
                                date = line.Substring(datePos, dateWidth).Trim();
                                if (date.IndexOf('.') >= 0) // summary line
                                {
                                    finished = true;
                                    break;
                                }
                            }


                            // if count < 0 still skipping records before requested start datete
                            if (count >= 0)
                            {
                                DataRow newRow = dt.NewRow();

                                //get date
                                DateTime d = DateTime.Now;
                                switch (IPRINT)
                                {
                                    case 0: // monthly: format YYYY\MM
                                        bool endYear = date.Equals("12");
                                        d = new DateTime(currentYear, int.Parse(date), 1);
                                        date = currentYear.ToString() + @"\" + date;                                        
                                        if (endYear) currentYear++;
                                        break;
                                    case 1: // daily: Julian format YYYYDDD
                                        int lastDay = isLeapYear(currentYear) ? 366 : 365;
                                        date = currentYear.ToString() + currentDay.ToString("D3");
                                        d = (new DateTime(currentYear, 1, 1)).AddDays(currentDay - 1);
                                        if (currentDay == lastDay)
                                        {
                                            currentYear++;
                                            currentDay = 1;
                                        }
                                        else currentDay++;
                                        break;
                                    default: // IPRINT = 2: annual: format already YYYY
                                        d = new DateTime(currentYear, 1, 1);
                                        break;
                                }
                                newRow[0] = d;

                                val = -1;
                                valstr = line.Substring(varPos, varLength).Trim();
                                bool parseFail = false;
                                // parse and then convert to string as it gives more readable numbers								// parse and then convert to string as it gives more readable numbers
                                // than SWAT's scientific notation
                                try
                                {
                                    val = Double.Parse(valstr);
                                }
                                catch (Exception)
                                {
                                    // bug in output.sub
                                    if (source == SourceType.SUBBASIN && (varPos >= 224))
                                    {
                                        // try shifted one place to right
                                        try
                                        {
                                            valstr = line.Substring(varPos + 1, varLength).Trim();
                                            val = Double.Parse(valstr);
                                        }
                                        catch (Exception)
                                        {
                                            parseFail = true;
                                        }
                                    }
                                    // bug in output.hru
                                    else if (source == SourceType.HRU && (varPos >= 714))
                                    {
                                        //try shifted one or two places to right
                                        try
                                        {
                                            valstr = line.Substring(varPos + 1, varLength).Trim();
                                            val = Double.Parse(valstr);
                                        }
                                        catch (Exception)
                                        {
                                            try
                                            {
                                                valstr = line.Substring(varPos + 2, varLength).Trim();
                                                val = Double.Parse(valstr);
                                            }
                                            catch (Exception)
                                            {
                                                parseFail = true;
                                            }
                                        }
                                    }
                                    else
                                        parseFail = true;
                                }
                                if (parseFail)
                                {
                                    Console.WriteLine("Cannot parse as a number " + valstr);
                                }
                                newRow[1] = val;
                                dt.Rows.Add(newRow);
                            }
                            count++;
                            if (count == numRecords) finished = true;
                            for (int j = 1; j < skipNum; j++) next.ReadLine();
                        }
                    }

                    Console.WriteLine(string.Format("Reading {0}: {1} ms",
                    string.Format("{0}-{1}-{2}-{3}-{4}-{5}",
                        scenarioName,source,id,var.Trim(),requestedStartYear, requestedFinishYear), 
                    DateTime.Now.Subtract(readStartTime).TotalMilliseconds));

                    //output data
                    //for(int i=0;i<numRecords;i++)
                    //    Console.WriteLine(string.Format("{0},{1}",data[i,0],data[i,1])); 
                }
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        #endregion
    }
}
