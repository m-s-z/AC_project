﻿using System;
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

        public int[] Features { get { return _features; } }
        public double Difficulty { get { return _difficulty; } }

        public int Index
        {
            get { return _index; }
            set  { _index = value; }
        }

        public bool IsCompleted
        {
            get
            {
                for (int i = 0; i < Features.Count(); i++)
                {
                    if(Features[i] > 0)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public Project(string readLine)
        {
            string[] values = readLine.Split(';');
            if (values.Count() <= 1)
            {
                values = readLine.Split(',');
            }

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
            if ( featureIndex < _features.Count() && _features[featureIndex] > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CalculateDifficulty(double[] arrayOfFeatureSupplyDifficulty, double[] arrayOfFeatureDemandDifficulty, int[] supply)
        {
            for (int i = 0; i < _features.Count(); i++)
            {
                _difficulty += _features[i] * arrayOfFeatureSupplyDifficulty[i] - _features[i] * arrayOfFeatureDemandDifficulty[i];
                if (_features[i] > supply[i])
                {
                    _difficulty = int.MaxValue;
                    return;
                }
            }
        }
    }
}
