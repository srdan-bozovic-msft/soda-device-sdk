using MSC.Socrata.Device.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Socrata.Device.Soql.Datatypes
{
    [SodaEntity]
    public class Location
    {
        [SodaField("needs_recoding")]
        public bool NeedsRecoding { get; set; }

        [SodaField("longitude")]
        public double Longitude { get; set; }

        [SodaField("latitude")]
        public double Latitude { get; set; }

        [SodaField("human_address")]
        public string HumanAddress { get; set; }
    }
}
