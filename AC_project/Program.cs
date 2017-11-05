using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AC_project
{
    class Program
    {
        static void Main(string[] args)
        {
            //string[] values = File.ReadLines("ac_input.txt");
            List<DailyValues> values = File.ReadAllLines("ac_input.txt")
                                           .Skip(1)
                                           .Select(v => DailyValues.FromCsv(v))
                                           .ToList();

            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("ac_input.txt"))
                {
                    // Read the stream to a string, and write the string to the console.
                    string line = sr.ReadLine();

                    Console.WriteLine(line);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

    }

    class DailyValues
    {
        decimal Open;
        decimal High;
        decimal Low;

        public static DailyValues FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            DailyValues dailyValues = new DailyValues();
            dailyValues.Open = Convert.ToDecimal(values[0]);
            dailyValues.High = Convert.ToDecimal(values[1]);
            dailyValues.Low = Convert.ToDecimal(values[2]);
            Console.WriteLine("Open {0}, High {1}, Low {2}", dailyValues.Open, dailyValues.High, dailyValues.Low);
            return dailyValues;
        }
    }
}
