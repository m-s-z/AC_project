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
            String path = "";
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
