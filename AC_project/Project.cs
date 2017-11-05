using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC_project
{
    public class Project
    {
        private int _index;
        private int[] _features;
        private double _difficulty;

        public double Difficulty { get { return _difficulty; } }

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

        public bool HasFeature(int featureIndex)
        {
            if (_features[featureIndex] > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CalculateDifficulty(double[] arrayOfFeatureDifficulty)
        {
            for (int i = 0; i < _features.Count(); i++)
            {
                Console.WriteLine("Difficult {0} = feature[{1}] {2} * fD {3}", _difficulty, i, _features[i], arrayOfFeatureDifficulty[i]);
                _difficulty += _features[i] * arrayOfFeatureDifficulty[i];
            }
        }
    }
}
