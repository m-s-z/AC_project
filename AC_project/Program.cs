using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AC_project
{
    class Program
    {
        private static int _numberOfExperts;
        private static int _numberOfFeatures;
        private static int _numberOfProjects;

        static void Main(string[] args)
        {
            ReadFile("ac_input.txt");
        }

        private static void ReadFile(string pathToFile)
        {
            try
            {
                string line = File.ReadLines(pathToFile).First();
                string[] lines = line.Split(',');
                _numberOfProjects = int.Parse(lines[0]);
                _numberOfExperts = int.Parse(lines[1]);
                _numberOfFeatures = int.Parse(lines[2]);

                List<Project> listProjects = File.ReadLines(pathToFile)
                                       .Skip(1)
                                       .Take(_numberOfProjects)
                                       .Select(v => new Project(v))
                                       .ToList();

                List<Expert> listExpers = File.ReadLines(pathToFile)
                                       .Skip(_numberOfProjects + 1)
                                       .Take(_numberOfExperts)
                                       .Select(v => new Expert(v))
                                       .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

    }
}
