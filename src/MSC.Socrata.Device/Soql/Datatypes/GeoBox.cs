using MSC.Socrata.Device.Soql.Clauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Socrata.Device.Soql.Datatypes
{
    public class GeoBox : BuildCapable
    {
        public double North { get; private set; }

        public double East { get; private set; }

        public double South { get; private set; }

        public double West { get; private set; }

        public GeoBox(double north, double east, double south, double west)
        {
            North = north;
            East = east;
            South = south;
            West = west;
        }

        public override string Build()
        {
            return string.Join(", ", North, East, South, West);
        }
    }
}
