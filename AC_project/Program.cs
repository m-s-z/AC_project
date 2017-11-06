using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AC_project
{
    class Program
    {
        private static ProblemSolver _solver;

        static void Main(string[] args)
        {
            String problemName = "problem1.txt";
            double methodCoefficient = 0;
            if (args.Count() == 2)
            {
                // TODO: print error infos if failed
                problemName = args[0];
                Double.TryParse(args[2], out methodCoefficient);
            }
            String inputFilesPath = Directory.GetCurrentDirectory() + "..\\..\\..\\..\\input_files\\";
            Problem problem = new Problem(inputFilesPath + problemName);
            _solver = new ProblemSolver(problem, methodCoefficient);
            _solver.Solve();
        }
    }
}
