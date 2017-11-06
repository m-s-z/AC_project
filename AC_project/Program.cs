using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AC_project
{
    class Program
    {
        private static Problem _problem;
        private static List<Edge> _listEdges;

        private static Stack<int> _stackOfFeaturesPopularity;
        private static Stack<int> _stackOfProjectsDifficulty;
        private static List<Tuple<int, int>> _listOfFeaturesPopularity;
        private static int[] _arrayOfFeaturePopularity;
        private static double[] _arrayOfFeatureDifficulty;
        private static int _sumOfFeaturesExpertsProvide;

        static void Main(string[] args)
        {
            _problem = new Problem("ac_input2.txt");
            BuildConnections();
            Console.ReadLine();
            CreateStackOfFeaturesPopularity();
            CreateStackOfProjectsDifficulty();
        }

        private static void BuildConnections()
        {
            _listEdges = new List<Edge>();
            _listOfFeaturesPopularity = new List<Tuple<int, int>>();
            _arrayOfFeaturePopularity = new int[_problem.numberOfFeatures];
            for (int i = 0; i < _problem.numberOfFeatures; i++)
            {
                for (int j = 0; j < _problem.listExpers.Count(); j++)
                {
                    if (_problem.listExpers[j].HasFeature(i))
                    {
                        _arrayOfFeaturePopularity[i]++;
                        _sumOfFeaturesExpertsProvide++;
                        for (int k = 0; k < _problem.listProjects.Count(); k++)
                        {
                            if (_problem.listProjects[k].HasFeature(i))
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
            _arrayOfFeatureDifficulty = new double[_problem.numberOfFeatures];
            for (int i = 0; i < _problem.numberOfFeatures; i++)
            {
                _arrayOfFeatureDifficulty[i] = (double)_sumOfFeaturesExpertsProvide / _arrayOfFeaturePopularity[i];
            }

            foreach (var project in _problem.listProjects)
            {
                project.CalculateDifficulty(_arrayOfFeatureDifficulty);
                Console.WriteLine("Project difficulty {0}", project.Difficulty);
                Console.WriteLine("----------------------------");
            }

            var tempList = _problem.listProjects.OrderBy(i => i.Difficulty).ToList();
            _stackOfProjectsDifficulty = new Stack<int>();
            foreach (var project in tempList)
            {
                _stackOfProjectsDifficulty.Push(project.Index);
                Console.WriteLine("Project stack {0} - {1}", _stackOfProjectsDifficulty.First(), _problem.listProjects[_stackOfProjectsDifficulty.First()].Difficulty);
            }

            foreach (var feature in _stackOfFeaturesPopularity)
            {
                Console.WriteLine("Feature stack {0} - {1}", feature, _arrayOfFeaturePopularity[feature]);
            }
        }
    }
}
