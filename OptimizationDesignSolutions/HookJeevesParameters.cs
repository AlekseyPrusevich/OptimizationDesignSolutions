using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizationDesignSolutions
{
    class HookJeevesParameters
    {
        public double pointOne { get; set; }

        public double poinSecond { get; set; }

        public double stepPointOne { get; set; }

        public double stepPointSecond { get; set; }

        public double accuracy { get; set; }


        public HookJeevesParameters()
        {
            pointOne = 0;
            poinSecond = 0;
            stepPointOne = 0;
            stepPointSecond = 0;
            accuracy = 0;
        }

        public HookJeevesParameters(double _pointOne, double _poinSecond, double _stepPointOne, double _stepPointSecond, double _accuracy)
        {
            pointOne = _pointOne;
            poinSecond = _poinSecond;
            stepPointOne = _stepPointOne;
            stepPointSecond = _stepPointSecond;
            accuracy = _accuracy;
        }
    }
}
