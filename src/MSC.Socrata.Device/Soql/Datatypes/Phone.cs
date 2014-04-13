using MSC.Socrata.Device.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Socrata.Device.Soql.Datatypes
{
    [SodaEntity]
    public class Phone
    {
        [SodaField("phone_number")]
        public string Number { get; set; }

        [SodaField("phone_type")]
        public string Type { get; set; }

    }
}
