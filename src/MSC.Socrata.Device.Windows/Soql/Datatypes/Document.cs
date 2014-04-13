using MSC.Socrata.Device.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Socrata.Device.Soql.Datatypes
{
    [SodaEntity]
    public class Document
    {
        [SodaField("file_id")]
        public string FileId { get; set; }

        [SodaField("filename")]
        public string FileName { get; set; }

    }
}
