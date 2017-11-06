using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC_project
{
    class ProblemSolver
    {
        Problem _problem;

        private List<Edge> _listEdges;

        /// <summary>
        /// Stack of indices of features in such a way that indices of the most popular (the simplest to fill) features are at the bottom of the stack
        /// </summary>
        private Stack<int> _stackOfFeaturesByPopularity;
        /// <summary>
        /// Stack of indices of projects from the easiest to finish to the hardest one
        /// </summary>
        private Stack<int> _stackOfProjectsDifficulty;
        private List<Tuple<int, int>> _listOfFeaturesPopularity;
        private int[] _arrayOfFeaturePopularity;
        private double[] _arrayOfFeatureDifficulty;
        private int _sumOfFeaturesExpertsProvide;

        public ProblemSolver(Problem problem)
        {
            _problem = problem;
        }

        public void Solve()
        {
            BuildConnections();
            Console.ReadLine();
            CreateStackOfFeaturesPopularity();
            CreateStackOfProjectsDifficulty();

        }

        private void BuildConnections()
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

        private void CreateStackOfFeaturesPopularity()
        {
            _listOfFeaturesPopularity = _listOfFeaturesPopularity.OrderByDescending(i => i.Item2).ToList();
            _stackOfFeaturesByPopularity = new Stack<int>();
            Console.WriteLine("Feature podaż {0}", _sumOfFeaturesExpertsProvide);
            foreach (var p in _listOfFeaturesPopularity)
            {
                Console.WriteLine("Feature {0}: {1}", p.Item1, p.Item2);
                _stackOfFeaturesByPopularity.Push(p.Item1);
            }
            foreach (var p in _stackOfFeaturesByPopularity)
            {
                Console.WriteLine("Stack {0}", p);
            }
        }

        private void CreateStackOfProjectsDifficulty()
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

            foreach (var feature in _stackOfFeaturesByPopularity)
            {
                Console.WriteLine("Feature stack {0} - {1}", feature, _arrayOfFeaturePopularity[feature]);
            }
        }
    }
}
