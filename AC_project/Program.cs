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
        private static Stack<int> _stackOfFeaturesPopularity;
        private static Stack<int> _stackOfProjectsDifficulty;
        private static List<Tuple<int, int>> _listOfFeaturesPopularity;
        private static int[] _arrayOfFeaturePopularity;
        private static double[] _arrayOfFeatureDifficulty;
        private static int _sumOfFeaturesExpertsProvide;


        static void Main(string[] args)
        {
            ReadFile("ac_input2.txt");
            BuildConnections();
            Console.ReadLine();
            CreateStackOfFeaturesPopularity();
            CreateStackOfProjectsDifficulty();
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
                SetIndices();
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        private static void SetIndices()
        {
            for (int i = 0; i < _listExpers.Count(); i++)
            {
                _listExpers[i].Index = i;
            }

            for (int i = 0; i < _listProjects.Count(); i++)
            {
                _listProjects[i].Index = i;
            }
        }

        private static void BuildConnections()
        {
            _listEdges = new List<Edge>();
            _listOfFeaturesPopularity = new List<Tuple<int, int>>();
            _arrayOfFeaturePopularity = new int[_numberOfFeatures];
            for (int i = 0; i < _numberOfFeatures; i++)
            {
                for (int j = 0; j < _listExpers.Count(); j++)
                {
                    if (_listExpers[j].HasFeature(i))
                    {
                        _arrayOfFeaturePopularity[i]++;
                        _sumOfFeaturesExpertsProvide++;
                        for (int k = 0; k < _listProjects.Count(); k++)
                        {
                            if (_listProjects[k].HasFeature(i))
                            {
                                _listEdges.Add(new Edge(k, j, i));
                                Console.WriteLine(_listEdges.Last().EdgeDescription());
                            }
                        }
                    }
                }
                _listOfFeaturesPopularity.Add(new Tuple<int, int>(i, _arrayOfFeaturePopularity[i]));
            }

            //foreach(var p in _listOfFeaturesPopularity)
            //{
            //    Console.WriteLine("Feature {0}: {1}", p.Item1, p.Item2);
            //}
        }

        private static void CreateStackOfFeaturesPopularity()
        {
            _listOfFeaturesPopularity = _listOfFeaturesPopularity.OrderByDescending(i => i.Item2).ToList();
            _stackOfFeaturesPopularity = new Stack<int>();
            Console.WriteLine("Feature podaż {0}", _sumOfFeaturesExpertsProvide);
            foreach (var p in _listOfFeaturesPopularity)
            {
                Console.WriteLine("Feature {0}: {1}", p.Item1, p.Item2);
                _stackOfFeaturesPopularity.Push(p.Item1);
            }
            foreach (var p in _stackOfFeaturesPopularity)
            {
                Console.WriteLine("Stack {0}", p);
            }
        }

        private static void CreateStackOfProjectsDifficulty()
        {
            _arrayOfFeatureDifficulty = new double[_numberOfFeatures];
            for (int i = 0; i < _numberOfFeatures; i++)
            {
                _arrayOfFeatureDifficulty[i] = (double)_sumOfFeaturesExpertsProvide / _arrayOfFeaturePopularity[i];
            }

            foreach (var project in _listProjects)
            {
                project.CalculateDifficulty(_arrayOfFeatureDifficulty);
                Console.WriteLine("Project difficulty {0}", project.Difficulty);
                Console.WriteLine("----------------------------");
            }

            var tempList = _listProjects.OrderBy(i => i.Difficulty).ToList();
            _stackOfProjectsDifficulty = new Stack<int>();
            foreach (var project in tempList)
            {
                _stackOfProjectsDifficulty.Push(project.Index);
                Console.WriteLine("Project stack {0} - {1}", _stackOfProjectsDifficulty.First(), _listProjects[_stackOfProjectsDifficulty.First()].Difficulty);
            }

            foreach (var feature in _stackOfFeaturesPopularity)
            {
                Console.WriteLine("Feature stack {0} - {1}", feature, _arrayOfFeaturePopularity[feature]);
            }
        }
    }
}
