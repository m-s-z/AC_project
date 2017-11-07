using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC_project
{
    public class ProblemSolver
    {
        private Solution _solution = new Solution();
        private Problem _problem;
        public Problem Problem { get { return _problem; } }
        private double _methodCoefficient;
        public double MethodCoefficient { get { return _methodCoefficient; } }

        private List<Edge> _listEdges;
        public List<Edge> ListEdges { get { return _listEdges; } }

        /// <summary>
        /// Stack of indices of features in such a way that indices of the most popular (the simplest to fill) features are at the bottom of the stack
        /// </summary>
        private Stack<int> _stackOfFeaturesByPopularity;
        public Stack<int> StackOfFeaturesByPopularity { get { return _stackOfFeaturesByPopularity; } }
        /// <summary>
        /// Stack of projects from the easiest to finish to the hardest one
        /// </summary>
        private Stack<Project> _stackOfProjectsByLeastDifficulty; // a.k.a projects_priority
        public Stack<Project> StackOfProjectsByLeastDifficulty { get { return _stackOfProjectsByLeastDifficulty; } }

        public int[] _featureSupplies; // a.k.a "Vp"
        public int _featuresSupplySum; // sum of all _featureSupplies elements - a.k.a "supply_sum"

        public int[] _featureDemand; // a.k.a "Vp"
        public int _featuresDemandSum; // sum of all _featureSupplies elements - a.k.a "supply_sum"

        // Temps?
        private List<Tuple<int, int>> _listOfFeaturesByPopularity; // i.e. "Vp" after indexing it and sorting by popularity

        public ProblemSolver(Problem problem, double methodCoefficient)
        {
            _problem = problem;
            _methodCoefficient = methodCoefficient;
            // Trimming to [0, 1]
            _methodCoefficient = _methodCoefficient < 0 ? 0 : _methodCoefficient;
            _methodCoefficient = _methodCoefficient > 1 ? 1 : _methodCoefficient;
            
        }

        public Solution Solve()
        {
            // Initialization
            List<Project> completedProjects = _problem.listProjects.FindAll(p => p.IsCompleted);
            foreach(Project project in completedProjects)
            {
                _problem.listProjects.Remove(project);
                _solution.Add(project);
            }

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
            
            int i = 0;
            while((_problem.listExperts.Count > 0 && _problem.listProjects.Count > 0) && (_stackOfFeaturesByPopularity.Count > 0 && _stackOfProjectsByLeastDifficulty.Count > 0))
            {
                if (i < numberOfFeaturesFirst)
                {
                    int feature = _stackOfFeaturesByPopularity.Pop();
                    SolveForFeature(feature);
                }
                else
                {
                    Project project = _stackOfProjectsByLeastDifficulty.Pop();
                    SolveForProject(project);
                }
                i++;
            }
            return _solution;
        }
        

        public bool SolveForFeature(int feature)
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
                .OrderByDescending(e => e.Fitness)
                .ToList();

            while(viableExperts.Count > 0)
            {
                Project project = _stackOfProjectsByLeastDifficulty
                    .ToList()
                    .Find(p => p.HasFeature(feature));

                if (project == null)
                {
                    // No more matches.
                    return false;
                }

                while(project.HasFeature(feature) && viableExperts.Count > 0)
                {
                    Expert expert = viableExperts.Last();
                    viableExperts.RemoveAt(viableExperts.Count - 1);
                    Assign(expert, project, feature);
                }
            }

            return true;
        }

        public bool SolveForProject(Project project)
        {
            for (int feature = 0; feature < project.Features.Count(); feature++)
            {
                if(project.Features[feature] > 0)
                {
                    List<Expert> viableExperts = _problem.listExperts
                        .FindAll(e => e.HasFeature(feature) == true)
                        .OrderByDescending(e => e.Fitness)
                        .ToList();
                    while (project.Features[feature] > 0 & viableExperts.Count > 0)
                    {
                        Expert expert = viableExperts.Last();
                        viableExperts.RemoveAt(viableExperts.Count - 1);
                        Assign(expert, project, feature);
                    }
                }
            }

            return true;
        }

        public void Assign(Expert expert, Project project, int feature)
        {
            /* TODO: Mark 4: Assign expert<=>project, i.e.: 
             * remove Expert, 
             * alter the Project features set, 
             * remove the edge. lol
             */

            _problem.listExperts.Remove(expert);
            project.Features[feature]--;
            if(project.IsCompleted)
            {
                _problem.listProjects.Remove(project);
            }
            _solution.Add(project, expert);
            _solution.Sum++;
        }
        

        public void BuildConnections()
        {
            _listEdges = new List<Edge>();
            _listOfFeaturesByPopularity = new List<Tuple<int, int>>();
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
                _listOfFeaturesByPopularity.Add(new Tuple<int, int>(f, _featureSupplies[f]));
            }

            //foreach(var p in _listOfFeaturesPopularity)
            //{
            //    Console.WriteLine("Feature {0}: {1}", p.Item1, p.Item2);
            //}
        }

        public void CreateStackOfFeaturesPopularity()
        {
            _listOfFeaturesByPopularity = _listOfFeaturesByPopularity.OrderByDescending(i => i.Item2).ToList();
            _stackOfFeaturesByPopularity = new Stack<int>();
            foreach (var p in _listOfFeaturesByPopularity)
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

        public void CreateStackOfProjectsDifficulty()
        {
            double[] arrayOfFeatureSupplyDifficulty = new double[_problem.numberOfFeatures];
            double[] arrayOfFeatureDemandDifficulty = new double[_problem.numberOfFeatures];

            _featureDemand = new int[_problem.numberOfFeatures];

            foreach (var project in _problem.listProjects)
            {
                for (int i = 0; i < _problem.numberOfFeatures; i++)
                {
                    if (project.HasFeature(i))
                    {
                        _featureDemand[i]++;
                        _featuresDemandSum++;
                    }
                }
            }

                for (int i = 0; i < _problem.numberOfFeatures; i++)
            {
                if (_featureSupplies[i] > 0)
                {
                    arrayOfFeatureSupplyDifficulty[i] = (double)_featuresSupplySum / _featureSupplies[i];
                }
                else
                {
                    arrayOfFeatureSupplyDifficulty[i] = 0;
                }

                if (_featureDemand[i] > 0)
                {
                    arrayOfFeatureDemandDifficulty[i] =  (double)_featuresDemandSum / (double)_featureDemand[i];
                }
                else
                {
                    arrayOfFeatureDemandDifficulty[i] = 0;
                }
            }


            foreach (var project in _problem.listProjects)
            {
                
                
                project.CalculateDifficulty(arrayOfFeatureSupplyDifficulty, arrayOfFeatureDemandDifficulty, _featureSupplies);
                Console.WriteLine("Project difficulty {0}", project.Difficulty);
                Console.WriteLine("----------------------------");
            }
            foreach (var expert in _problem.listExperts)
            {
                expert.CalculateFitness(arrayOfFeatureSupplyDifficulty);
            }

            var tempList = _problem.listProjects.OrderByDescending(i => i.Difficulty).ToList();
            _stackOfProjectsByLeastDifficulty = new Stack<Project>(tempList);

            // Temp print
            foreach (var project in tempList)
            {
                Console.WriteLine("Project stack {0} - {1}", _stackOfProjectsByLeastDifficulty.First(), _problem.listProjects[_stackOfProjectsByLeastDifficulty.First().Index].Difficulty);
            }
        }
    }
}
