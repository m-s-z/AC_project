using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC_project
{
    class ProblemSolver
    {
        private Problem _problem;
        private double _methodCoefficient;

        private List<Edge> _listEdges;

        /// <summary>
        /// Stack of indices of features in such a way that indices of the most popular (the simplest to fill) features are at the bottom of the stack
        /// </summary>
        private Stack<int> _stackOfFeaturesByPopularity;
        /// <summary>
        /// Stack of projects from the easiest to finish to the hardest one
        /// </summary>
        private Stack<Project> _stackOfProjectsByDifficulty; // a.k.a projects_priority

        private int[] _featureSupplies; // a.k.a "Vp"
        private int _featuresSupplySum; // sum of all _featureSupplies elements - a.k.a "supply_sum"

        // Temps?
        private List<Tuple<int, int>> _listOfFeaturesPopularity; // i.e. "Vp" after indexing it and sorting by popularity

        public ProblemSolver(Problem problem, double methodCoefficient)
        {
            _problem = problem;
            _methodCoefficient = methodCoefficient;
            // Trimming to [0, 1]
            _methodCoefficient = _methodCoefficient < 0 ? 0 : _methodCoefficient;
            _methodCoefficient = _methodCoefficient > 0 ? 1 : _methodCoefficient;
        }

        public void Solve()
        {
            // Initialization

            BuildConnections();
            Console.WriteLine("\nAny key to continue...");
            Console.ReadLine();
            Console.WriteLine("----------------------------\nFeatures supplies sum {0}", _featuresSupplySum);
            CreateStackOfFeaturesPopularity();
            CreateStackOfProjectsDifficulty();
            foreach (var feature in _stackOfFeaturesByPopularity)
            {
                Console.WriteLine("Feature stack {0} - {1}", feature, _featureSupplies[feature]);
            }
            Console.WriteLine("\nAny key to continue...");
            Console.ReadLine();

            // Main Loop

            int numberOfFeaturesFirst = (int)((double)_stackOfFeaturesByPopularity.Count * _methodCoefficient);
            
            // TODO: common STOP property for both methods.
                        
            for (int i = 0; i < numberOfFeaturesFirst; i++)
            {
                int feature = _stackOfFeaturesByPopularity.Pop();
                SolveForFeature(feature);
            }

            while(_stackOfProjectsByDifficulty.Count > 0)
            {
                Project project = _stackOfProjectsByDifficulty.Pop();
                SolveForProject(project);
            }

            Console.WriteLine("\nAny key to continue...");
            Console.ReadLine();
        }
        

        private bool SolveForFeature(int feature)
        {
            /* Based on number of edges (not sorted yet, used Fitness instead).
             * 
            List<Tuple<Expert, int>> viableExperts = new List<Tuple<Expert, int>>();
            foreach (Expert expert in _problem.listExpers)
            {
                if (expert.HasFeature(feature))
                {
                    viableExperts.Add(new Tuple<Expert, int>(expert, 0));
                }
            } 
            */

            //* Based on Fitness.
            //*
            List<Expert> viableExperts = _problem.listExperts
                .FindAll(e => e.HasFeature(feature) == true)
                .OrderBy(e => e.Fitness)
                .ToList();
            Project project = _stackOfProjectsByDifficulty
                .ToList()
                .Find(p => p.HasFeature(feature));

            if(project == null)
            {
                // No match.
                return false;
            }

            // TODO: Mark 4: Assign expert<=>project, i.e.: remove Expert, alter the Project features set, remove the edge.

            return true;
        }

        private bool SolveForProject(Project project)
        {
            int feature = -1;
            for (int i = 0; i < project.Features.Count(); i++)
            {
                if(project.Features[i] > 0)
                {
                    feature = i;
                    break;
                }
            }
            if(feature < 0)
            {
                // No match.
                return false;
            }

            List<Expert> viableExperts = _problem.listExperts
                .FindAll(e => e.HasFeature(feature) == true)
                .OrderBy(e => e.Fitness)
                .ToList();
            // Then it starts assigning experts until all places of the choosen feature in this project are filled.


            // TODO: Mark 4: Assign expert<=>project, i.e.: remove Expert, alter the Project features set, remove the edge.

            return true;
        }

        private bool Assign(Expert expert, Project project)
        {
            return false;
        }
        


        private void BuildConnections()
        {
            _listEdges = new List<Edge>();
            _listOfFeaturesPopularity = new List<Tuple<int, int>>();
            _featureSupplies = new int[_problem.numberOfFeatures];
            for (int f = 0; f < _problem.numberOfFeatures; f++)
            {
                for (int e = 0; e < _problem.listExperts.Count(); e++)
                {
                    if (_problem.listExperts[e].HasFeature(f))
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
            foreach (var p in _listOfFeaturesPopularity)
            {
                Console.WriteLine("Feature {0}: {1}", p.Item1, p.Item2);
                _stackOfFeaturesByPopularity.Push(p.Item1);
            }

            // Temp print
            String stack = "Stack _stackOfFeaturesByPopularity: [";
            foreach (var p in _stackOfFeaturesByPopularity)
            {
                stack += String.Format("{0}, ", p);
            }
            Console.WriteLine("{0}]\n", stack);
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
            _stackOfProjectsByDifficulty = new Stack<Project>(tempList);

            // Temp print
            foreach (var project in tempList)
            {
                Console.WriteLine("Project stack {0} - {1}", _stackOfProjectsByDifficulty.First(), _problem.listProjects[_stackOfProjectsByDifficulty.First().Index].Difficulty);
            }
        }
    }
}
