using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Socrata.Device.Client.Converters
{
    public class DefaultDateConverter : IDataTypeConverter
    {
        public object GetValue(DataTypesMapper dataTypeMapper, PropertyInfo field, string type, JToken value)
        {
            return ((JValue)value).Value;
        }
    }
}
