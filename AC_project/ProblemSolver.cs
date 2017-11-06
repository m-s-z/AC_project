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
        private Stack<int> _stackOfProjectsDifficulty; // a.k.a projects_priority

        private int[] _featureSupplies; // a.k.a "Vp"
        private int _featuresSupplySum; // sum of all _featureSupplies elements - a.k.a "supply_sum"

        // Temps?
        private List<Tuple<int, int>> _listOfFeaturesPopularity; // i.e. "Vp" after indexing it and sorting by popularity

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
            _featureSupplies = new int[_problem.numberOfFeatures];
            for (int f = 0; f < _problem.numberOfFeatures; f++)
            {
                for (int e = 0; e < _problem.listExpers.Count(); e++)
                {
                    if (_problem.listExpers[e].HasFeature(f))
                    {
                        _featureSupplies[f]++;
                        _featuresSupplySum++;
                        for (int p = 0; p < _problem.listProjects.Count(); p++)
                        {
                            if (_problem.listProjects[p].HasFeature(f))
                            {
                                _listEdges.Add(new Edge(p, e, f));
                                Console.WriteLine(_listEdges.Last().EdgeDescription());
                            }
                        }
                    }
                }
                _listOfFeaturesPopularity.Add(new Tuple<int, int>(f, _featureSupplies[f]));
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
            Console.WriteLine("Feature supply {0}", _featuresSupplySum);
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
            double[] arrayOfFeatureDifficulty = new double[_problem.numberOfFeatures];
            for (int i = 0; i < _problem.numberOfFeatures; i++)
            {
                arrayOfFeatureDifficulty[i] = (double)_featuresSupplySum / _featureSupplies[i];
            }

            foreach (var project in _problem.listProjects)
            {
                project.CalculateDifficulty(arrayOfFeatureDifficulty);
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
                Console.WriteLine("Feature stack {0} - {1}", feature, _featureSupplies[feature]);
            }
        }
    }
}
