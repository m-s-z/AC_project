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
    public class ExpertTests
    {
        [TestMethod()]
        public void ExpertTest()
        {
            Expert p = new Expert("1");
            Assert.IsNotNull(p);
        }

        [TestMethod()]
        public void ExpertCSVFormatFailsTest()
        {
            try
            {
                Expert p = new Expert("3;4;5;6;7;1;23;4");
            }
            catch (Exception e)
            {
                Assert.AreEqual("Input string was not in a correct format.", e.Message);
                return;
            }
            Assert.Fail("Program did not fail on wrong input.");
        }

        [TestMethod()]
        public void ExpertInvalidFormatFailsTest()
        {
            try
            {
                Expert p = new Expert("");
            }
            catch (Exception e)
            {
                Assert.AreEqual("Input string was not in a correct format.", e.Message);
                return;
            }
            Assert.Fail("Program did not fail on wrong input.");
        }

        [TestMethod()]
        public void ExpertInvalidFormat2Test()
        {
            try
            {
                Expert p = new Expert("1,2,3,");
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
            Expert p = new Expert("1,1,0,1");
            Assert.IsTrue(p.HasFeature(1));
            Assert.IsFalse(p.HasFeature(2));
            p = new Expert("0,1,0,1");
            Assert.IsFalse(p.HasFeature(0));
            Assert.IsTrue(p.HasFeature(3));
        }

        [TestMethod()]
        public void HasFeatureTestEmptyNotCrashes()
        {
            Expert p = new Expert("0");
            Assert.IsFalse(p.HasFeature(99));
        }

        [TestMethod()]
        public void CalculateFitnessTest()
        {
            int[] features = new int[] { 1, 2, 0, 4 };
            Expert e = new Expert("1,2,0,4");
            double[] arrayOfFeatureDifficulty = new double[] { 1, 1, 2, 2 };
            double fitness = 0;
            for (int i = 0; i < features.Count(); i++)
            {
                fitness += features[i] * arrayOfFeatureDifficulty[i];
            }
            e.CalculateFitness(arrayOfFeatureDifficulty);
            Assert.AreEqual(fitness, e.Fitness);
        }
    }
}