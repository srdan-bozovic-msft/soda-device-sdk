using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Socrata.Device.Client
{
    public class SodaTypeConversionException : Exception
    {
        public SodaTypeConversionException()
            :base()
        {
        }

        public SodaTypeConversionException(string message)
            : base(message)
        {
        }

        public SodaTypeConversionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public SodaTypeConversionException(Exception innerException)
            : base("", innerException)
        {
        }
    }
}
