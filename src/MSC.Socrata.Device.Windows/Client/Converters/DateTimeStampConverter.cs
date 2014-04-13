using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Socrata.Device.Client.Converters
{
    public class DateTimeStampConverter : IDataTypeConverter
    {
        public object GetValue(DataTypesMapper dataTypeMapper, PropertyInfo field, string type, JToken value)
        {
            return new DateTime(value.Value<long>() + 1000);
        }
    }
}
