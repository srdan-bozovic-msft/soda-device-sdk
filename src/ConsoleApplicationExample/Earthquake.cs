using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSC.Socrata.Device;
using MSC.Socrata.Device.Client;
using MSC.Socrata.Device.Soql.Datatypes;

namespace ConsoleApplicationExample
{
    [SodaEntity]
    public class Earthquake
    {
        [SodaField("region")]
        public string Region { get; set; }

        [SodaField("source")]
        public string Source { get; set; }

        [SodaField("location")]
        public Location Location { get; set; }

        [SodaField("magnitude")]
        public double Magnitude { get; set; }

        [SodaField("number_of_stations")]
        public int NumberOfStations { get; set; }

        [SodaField("datetime")]
        public DateTime DateTime { get; set; }

        [SodaField("earthquake_id")]
        public string EarthquakeId { get; set; }

        [SodaField("depth")]
        public double Depth { get; set; }

        [SodaField("version")]
        public string Version { get; set; }
    }
}
