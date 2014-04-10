using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Socrata.Device.Client.Converters
{
    public interface IDataTypeConverter
    {
        object GetValue(DataTypesMapper dataTypeMapper, PropertyInfo field, string type, JToken value);
    }
}
