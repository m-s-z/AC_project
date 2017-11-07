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
    public class ProblemTests
    {
        [TestMethod()]
        public void ProblemPropertiesTest()
        {
            int numberOfFeatures = 5;
            List<Project> projects = new List<Project>();
            for(int i = 0; i < 5 ; i++)
            {
                String line = "";
                for(int f = 0; f < numberOfFeatures; f++)
                {
                    line += String.Format("{0}", i);
                    if(f < numberOfFeatures-1)
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
                    line += String.Format("{0}", i);
                    if (f < numberOfFeatures - 1)
                    {
                        line += ",";
                    }
                }
                Expert e = new Expert(line);
                experts.Add(e);
            }

            Problem problem = new Problem(projects, experts);

            Assert.AreEqual(problem.numberOfFeatures, numberOfFeatures);
            Assert.AreEqual(problem.numberOfProjects, projects.Count);
            Assert.AreEqual(problem.numberOfExperts, experts.Count);
        }

        [TestMethod()]
        public void SetIndicesTest()
        {
            int numberOfFeatures = 5;
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
                    line += String.Format("{0}", i);
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

            Assert.AreEqual(3, problem.listProjects[3].Index);
            Assert.AreEqual(0, problem.listProjects[0].Index);
            Assert.AreEqual(3, problem.listExperts[3].Index);
            Assert.AreEqual(0, problem.listExperts[0].Index);
        }
    }
}