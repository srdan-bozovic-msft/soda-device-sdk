using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Socrata.Device.Client
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SodaFieldAttribute : Attribute
    {
        public string Value { get; private set; }
        public SodaFieldAttribute(string value)
        {
            Value = value;
        }
    }
}
