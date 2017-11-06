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
            //String problemName = "problem1.txt";
            //String problemName = "zeros.txt";
            //String problemName = "all4all.txt";
            //String problemName = "all4all2.txt";
            String problemName = "TestKinga.txt";

            double methodCoefficient = 1;
            if (args.Count() == 2)
            {
                // TODO: print error infos if failed
                problemName = args[0];
                Double.TryParse(args[2], out methodCoefficient);
            }
            String inputFilesPath = Directory.GetCurrentDirectory() + "..\\..\\..\\..\\input_files\\";
            Problem problem = new Problem(inputFilesPath + problemName);
            _solver = new ProblemSolver(problem, methodCoefficient);
            Solution solution = _solver.Solve();

            Console.WriteLine("----------------------------\n-----------FINISHED---------\n");
            Console.WriteLine("w = {0}", methodCoefficient);
            solution.Print();
            Console.WriteLine("\nAny key to continue...");
            Console.ReadLine();
        }
    }
}
