using MSC.Socrata.Device.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Socrata.Device.Soql.Datatypes
{
    [SodaEntity]
    public class Url
    {
        [SodaField("description")]
        public string Description { get; set; }

        [SodaField("url")]
        public string AbsoluteUrl { get; set; }

    }
}
