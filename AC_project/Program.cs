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
            String inputFilesPath = Directory.GetCurrentDirectory() + "..\\..\\..\\..\\input_files\\";
            Problem problem = new Problem(inputFilesPath + problemName);

            _solver = new ProblemSolver(problem);
            _solver.Solve();
        }
    }
}
