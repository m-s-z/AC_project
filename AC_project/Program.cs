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
            String problemName = null;

            //problemName = "problem1.txt";
            //problemName = "zeros.txt";
            //problemName = "all4all.txt";
            //problemName = "all4all2.txt";

            //problemName = "TestKinga.txt";

            problemName = "test1.csv";
            //problemName = "test2.csv";
            //problemName = "INPUT-3.csv";
            //problemName = "test3littleExperts.csv";
            //problemName = "test4ManyExperts.csv";
            //problemName = "test5BigProject.csv";
            //problemName = "test6BigSample.csv";
            //problemName = "VERYbigsample.csv";

            String path = Directory.GetCurrentDirectory() + "..\\..\\..\\..\\input_files\\" + problemName;
            double methodCoefficient = 0;
            if (args.Count() == 2)
            {
                // TODO: print error infos if failed
                path = args[0];
                Double.TryParse(args[1], out methodCoefficient);
            }
            Problem problem = new Problem(path);
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
