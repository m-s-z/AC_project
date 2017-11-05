using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC_project
{
    public class Expert
    {
        private int[] _features;

        public Expert(string readLine)
        {
            string[] values = readLine.Split(',');
            _features = new int[values.Count()];
            Console.Write("Expert:");
            for (int i = 0; i < values.Count(); i++)
            {
                _features[i] = int.Parse(values[i]);
                Console.Write(_features[i] + ", ");
            }
            Console.WriteLine();
        }

        public bool HasFeature(int featureIndex)
        {
            //_features[featureIndex];
        }

    }
}
