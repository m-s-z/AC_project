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
    public class ProjectTests
    {
        [TestMethod()]
        public void IsCompletedTest()
        {
            int[] features = new int[] { 1, 2, 0, 4 };
            Project p = new Project("0,0,0,0");
            Assert.IsTrue(p.IsCompleted);
        }

        [TestMethod()]
        public void IsCompletedFalseTest()
        {
            int[] features = new int[] { 1, 2, 0, 4 };
            Project p = new Project("12,20,10,43");
            Assert.IsFalse(p.IsCompleted);
        }

        [TestMethod()]
        public void ProjectTest()
        {
            Project p = new Project("1");
            Assert.IsNotNull(p);
        }

        [TestMethod()]
        public void ProjectCSVFormatFailsTest()
        {
            try
            {
                Project p = new Project("3;4;5;6;7;1;23;4");
            }
            catch (Exception e)
            {
                Assert.Fail("Program did fail on wrong input.");
                return;
            }
        }

        [TestMethod()]
        public void ProjectInvalidFormatFailsTest()
        {
            try
            {
                Project p = new Project("");
            }
            catch(Exception e)
            {
                Assert.AreEqual("Input string was not in a correct format.", e.Message);
                return;
            }
            Assert.Fail("Program did not fail on wrong input.");
        }

        [TestMethod()]
        public void ProjectInvalidFormat2Test()
        {
            try
            {
                Project p = new Project("1,2,3,");
            }
            catch (Exception e)
            {
                Assert.AreEqual("Input string was not in a correct format.", e.Message);
                return;
            }
            Assert.Fail("Program did not fail on wrong input.");
        }

        [TestMethod()]
        public void HasFeatureTest()
        {
            Project p = new Project("1,2,0,4");
            Assert.IsTrue(p.HasFeature(1));
            Assert.IsFalse(p.HasFeature(2));
            p = new Project("0,2,0,1");
            Assert.IsFalse(p.HasFeature(0));
            Assert.IsTrue(p.HasFeature(3));
        }

        [TestMethod()]
        public void HasFeatureTestEmptyNotCrashes()
        {
            Project p = new Project("0");
            Assert.IsFalse(p.HasFeature(99));
        }


        [TestMethod()]
        public void CalculateDifficultyTest()
        {
            int[] features = new int[] {1,2,0,4};
            Project p = new Project("1,2,0,4");
            double[] arrayOfFeatureSupplyDifficulty = new double[] { 1, 1, 2, 2 };
            double[] arrayOfFeatureDemandDifficulty = new double[] { 1, 1, 2, 2 };

            double difficulty = 0;
            for (int i = 0; i < features.Count(); i++)
            {
                difficulty += features[i] * arrayOfFeatureSupplyDifficulty[i] - features[i] * arrayOfFeatureDemandDifficulty[i];
            }
            p.CalculateDifficulty(arrayOfFeatureSupplyDifficulty, arrayOfFeatureDemandDifficulty);
            Assert.AreEqual(difficulty, p.Difficulty);
        }

        [TestMethod()]
        public void CalculateDifficultyNaNTest()
        {
            int[] features = new int[] { 1, 2, 0, 4 };
            Project p = new Project("12,20,10,43");
            double[] arrayOfFeatureSupplyDifficulty = new double[] { 0, 0, 0, 0 };
            double[] arrayOfFeatureDemandDifficulty = new double[] { 0, 0, 0, 0 };
            double difficulty = 0;
            p.CalculateDifficulty(arrayOfFeatureSupplyDifficulty, arrayOfFeatureDemandDifficulty);
            Assert.AreEqual(difficulty, p.Difficulty);
        }
    }
}