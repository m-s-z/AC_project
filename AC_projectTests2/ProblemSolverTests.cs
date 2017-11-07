using Microsoft.VisualStudio.TestTools.UnitTesting;
using AC_project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC_project.Tests
{
    [TestClass()]
    public class ProblemSolverTests
    {
        [TestMethod()]
        public void ProblemSolverCoefficientTest()
        {
            Problem problem = GenerateProblem(5);
            double methodCoefficient = 0.5;
            ProblemSolver solver = new ProblemSolver(problem, methodCoefficient);
            Assert.AreEqual(0.5, solver.MethodCoefficient);
        }

        [TestMethod()]
        public void ProblemSolverCoefficient1Test()
        {
            Problem problem = GenerateProblem(1);
            double methodCoefficient = -200;
            ProblemSolver solver = new ProblemSolver(problem, methodCoefficient);
            Assert.AreEqual(0, solver.MethodCoefficient);
        }

        [TestMethod()]
        public void ProblemSolverCoefficient2Test()
        {
            Problem problem = GenerateProblem(7);
            double methodCoefficient = 3;
            ProblemSolver solver = new ProblemSolver(problem, methodCoefficient);
            Assert.AreEqual(1, solver.MethodCoefficient);
        }

        [TestMethod()]
        public void BuildConnectionsTest()
        {
            Problem problem = GenerateProblem(4);
            double methodCoefficient = 0;
            ProblemSolver solver = new ProblemSolver(problem, methodCoefficient);
            solver.BuildConnections();
            Assert.IsNotNull(solver.ListEdges);
            Assert.IsTrue(solver.ListEdges.Count > 0);
        }

        // Tool:
        Problem GenerateProblem(int numberOfFeatures = 5, int numberOfProjects = 5, int numberOfExperts = 5, bool expertsAreKnowladgable = false)
        {
            List<Project> projects = new List<Project>();
            for (int i = 0; i < 5; i++)
            {
                String line = "";
                for (int f = 0; f < numberOfFeatures; f++)
                {
                    line += String.Format("{0}", i);
                    if (f < numberOfFeatures - 1)
                    {
                        line += ",";
                    }
                }
                Project p = new Project(line);
                projects.Add(p);
            }

            List<Expert> experts = new List<Expert>();
            for (int i = 0; i < 5; i++)
            {
                String line = "";
                for (int f = 0; f < numberOfFeatures; f++)
                {
                    int knowsF = expertsAreKnowladgable ? 1 : i % 2;
                    line += String.Format("{0}", knowsF);
                    if (f < numberOfFeatures - 1)
                    {
                        line += ",";
                    }
                }
                Expert e = new Expert(line);
                experts.Add(e);
            }

            Problem problem = new Problem(projects, experts);
            problem.SetIndices();

            return problem;
        }

        [TestMethod()]
        public void CreateStackOfFeaturesPopularityTest()
        {
            int numberOfFeatures = 4;
            Problem problem = GenerateProblem(numberOfFeatures);
            double methodCoefficient = 0;
            ProblemSolver solver = new ProblemSolver(problem, methodCoefficient);
            solver.BuildConnections();
            solver.CreateStackOfFeaturesPopularity();
            Assert.AreEqual(numberOfFeatures, solver.StackOfFeaturesByPopularity.Count);
        }

        [TestMethod()]
        public void CreateStackOfProjectsDifficultyTest()
        {
            int numberOfFeatures = 4;
            int numberOfProjects = 5;
            int numberOfExperts = 5;
            Problem problem = GenerateProblem(numberOfFeatures, numberOfProjects, numberOfExperts);
            double methodCoefficient = 0;
            ProblemSolver solver = new ProblemSolver(problem, methodCoefficient);
            solver.BuildConnections();
            solver.CreateStackOfFeaturesPopularity();
            solver.CreateStackOfProjectsDifficulty();
            Assert.AreEqual(numberOfProjects, solver.StackOfProjectsByLeastDifficulty.Count);
        }

        [TestMethod()]
        public void CorrectFeaturesSupplyVectorTest()
        {
            int numberOfProjects = 3;
            int numberOfExperts = 3;
            int numberOfFeatures = 4;
            Problem problem = GenerateProblem(numberOfFeatures, numberOfProjects, numberOfExperts);
            double methodCoefficient = 0;

            ProblemSolver solver = new ProblemSolver(problem, methodCoefficient);
            solver.BuildConnections();
            solver.CreateStackOfFeaturesPopularity();
            solver.CreateStackOfProjectsDifficulty();

            int[] featuresSupply = new int[numberOfFeatures];
            for (int f = 0; f < numberOfFeatures; f++)
            {
                foreach (Expert expert in problem.listExperts)
                {
                    if (expert.HasFeature(f))
                    {
                        featuresSupply[f] += 1;
                    }
                }
            }

            for (int f = 0; f < numberOfFeatures; f++)
            {
                Assert.AreEqual(featuresSupply[f], solver._featureSupplies[f]);
            }
        }


        [TestMethod()]
        public void CorrectFeaturesSupplYSumTest()
        {
            int numberOfProjects = 3;
            int numberOfExperts = 3;
            int numberOfFeatures = 4;
            Problem problem = GenerateProblem(numberOfFeatures, numberOfProjects, numberOfExperts);
            double methodCoefficient = 0;

            ProblemSolver solver = new ProblemSolver(problem, methodCoefficient);
            solver.BuildConnections();
            solver.CreateStackOfFeaturesPopularity();
            solver.CreateStackOfProjectsDifficulty();

            int sum = 0;
            for (int f = 0; f < numberOfFeatures; f++)
            {
                foreach (Expert expert in problem.listExperts)
                {
                    if (expert.HasFeature(f))
                    {
                        sum += 1;
                    }
                }
            }

            Assert.AreEqual(sum, solver._featuresSupplySum);
        }

        [TestMethod()]
        public void AssignedCompletedProjectsAreRemovedTest()
        {
            int numberOfProjects = 3;
            int numberOfExperts = 3;
            int numberOfFeatures = 4;
            Problem problem = GenerateProblem(numberOfFeatures, numberOfProjects, numberOfExperts);
            Project completedProject = new Project("1,0,0,0");
            problem.listProjects.Add(completedProject);

            double methodCoefficient = 0;
            ProblemSolver solver = new ProblemSolver(problem, methodCoefficient);
            solver.BuildConnections();
            solver.CreateStackOfFeaturesPopularity();
            solver.CreateStackOfProjectsDifficulty();

            int projCountBefore = problem.listProjects.Count;
            Expert expert = problem.listExperts.First();
            solver.Assign(expert, completedProject, 0);
            Assert.AreEqual(projCountBefore - 1, solver.Problem.listProjects.Count);
        }

        [TestMethod()]
        public void AssignedExpertsAreRemovedTest()
        {
            int numberOfProjects = 5;
            int numberOfExperts = 2;
            int numberOfFeatures = 3;
            Problem problem = GenerateProblem(numberOfFeatures, numberOfProjects, numberOfExperts);

            double methodCoefficient = 0;
            ProblemSolver solver = new ProblemSolver(problem, methodCoefficient);
            solver.BuildConnections();
            solver.CreateStackOfFeaturesPopularity();
            solver.CreateStackOfProjectsDifficulty();

            int expertsCountBefore = problem.listExperts.Count;
            Expert expert = problem.listExperts.First();
            Project proj = problem.listProjects.First();
            solver.Assign(expert, proj, 0);
            Assert.AreEqual(expertsCountBefore - 1, solver.Problem.listExperts.Count);
        }

        [TestMethod()]
        public void SolveForFeatureTest()
        {
            int numberOfProjects = 5;
            int numberOfExperts = 2;
            int numberOfFeatures = 3;
            Problem problem = GenerateProblem(numberOfFeatures, numberOfProjects, numberOfExperts, true);

            double methodCoefficient = 0;
            ProblemSolver solver = new ProblemSolver(problem, methodCoefficient);
            solver.BuildConnections();
            solver.CreateStackOfFeaturesPopularity();
            solver.CreateStackOfProjectsDifficulty();

            bool matched = solver.SolveForFeature(0);
            Assert.IsTrue(matched);
        }

        [TestMethod()]
        public void SolveForProjectTest()
        {
            int numberOfProjects = 5;
            int numberOfExperts = 2;
            int numberOfFeatures = 3;
            Problem problem = GenerateProblem(numberOfFeatures, numberOfProjects, numberOfExperts, true);

            double methodCoefficient = 0;
            ProblemSolver solver = new ProblemSolver(problem, methodCoefficient);
            solver.BuildConnections();
            solver.CreateStackOfFeaturesPopularity();
            solver.CreateStackOfProjectsDifficulty();

            Project proj = problem.listProjects.First();
            bool matched = solver.SolveForProject(proj);
            Assert.IsTrue(matched);
        }
    }
}