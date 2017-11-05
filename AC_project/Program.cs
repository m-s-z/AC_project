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
        private static List<Project> _listProjects;
        private static List<Expert> _listExpers;
        private static List<Edge> _listEdges;


        static void Main(string[] args)
        {
            ReadFile("ac_input.txt");
            BuildGraph();
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

                _listProjects = File.ReadLines(pathToFile)
                                       .Skip(1)
                                       .Take(_numberOfProjects)
                                       .Select(v => new Project(v))
                                       .ToList();

                _listExpers = File.ReadLines(pathToFile)
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

        private static void BuildGraph()
        {
            for (int i = 0; i < _numberOfFeatures; i++)
            {
                foreach(var expert in _listExpers)
                {
                    foreach (var project in _listProjects)
                    {
                        expert
                    }
                }
            }
        } 

    }
}
