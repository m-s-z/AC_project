using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC_project
{
    public class Expert
    {
        private int _index;
        private int[] _features;
        private double _fitness;
        public double Fitness { get { return _fitness; } }

        public int Index
        {
            get
            {
                return _index;
            }

            set
            {
                _index = value;
            }
        }

        public Expert(string readLine)
        {
            string[] values = readLine.Split(';');
            if (values.Count() <= 1)
            {
                values = readLine.Split(',');
            }

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
            if(featureIndex < _features.Count() && _features[featureIndex] == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CalculateFitness(double[] arrayOfFeatureDifficulty)
        {
            for (int i = 0; i < _features.Count(); i++)
            {
                Console.WriteLine("Fitness {0} = feature[{1}] {2} * fD {3}", _fitness, i, _features[i], arrayOfFeatureDifficulty[i]);
                _fitness += _features[i] * arrayOfFeatureDifficulty[i];
            }
        }
    }
}
