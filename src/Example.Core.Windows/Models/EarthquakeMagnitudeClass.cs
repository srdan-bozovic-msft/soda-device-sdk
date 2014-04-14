using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Core.Models
{
    public class EarthquakeMagnitudeClass
    {
        public string Name { get; private set; }
        public double LowerBound { get; private set; }
        public double UpperBound { get; private set; }

        public EarthquakeMagnitudeClass(string name, double upperBound, double lowerBound)
        {
            Name = name;
            LowerBound = lowerBound;
            UpperBound = upperBound;
        }

        public bool IsInClass(double magnitude)
        {
            return LowerBound <= magnitude && magnitude < UpperBound;
        }
    }
}
