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
            Problem problem = new Problem("ac_input2.txt");
            ProblemSolver solver = new ProblemSolver(problem);
            solver.Solve();
        }
    }
}
