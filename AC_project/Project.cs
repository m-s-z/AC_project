using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC_project
{
    public class Project
    {
        private int[] _features;

        public Project(string readLine)
        {
            string[] values = readLine.Split(',');
            _features = new int[values.Count()];
            Console.Write("Project:");
            for (int i = 0; i < values.Count(); i++)
            {
                _features[i] = int.Parse(values[i]);
                Console.Write(_features[i] + ", ");
            }
            Console.WriteLine();
        }
    }
}
