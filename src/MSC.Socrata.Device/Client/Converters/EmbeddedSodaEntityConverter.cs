using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Socrata.Device.Client.Converters
{
    public class EmbeddedSodaEntityConverter : IDataTypeConverter
    {
        public object GetValue(DataTypesMapper dataTypesMapper, PropertyInfo field, string type, JToken value)
        {
            Type target = field.PropertyType;
            var adapter = new JsonAdapter(target, dataTypesMapper);
            object result = null;
            if(value != null)
            {
                if(typeof(JArray).GetTypeInfo().IsAssignableFrom(value.GetType().GetTypeInfo()))
                {
                    result = adapter.FromJsonArray((JArray)value);
                }
                else
                {
                    foreach (var innerField in target.GetRuntimeProperties())
                    {
                        var sodaFieldAttribute = innerField.GetCustomAttribute<SodaFieldAttribute>();
                        var sodaFieldName = sodaFieldAttribute != null ? sodaFieldAttribute.Value : innerField.Name;
                        adapter.AddFieldMapping(sodaFieldName, DataTypesMapper.UNKNOWN_TYPE);
                    }
                    result = adapter.FromJsonObject(value);
                }
            }
            return result;
        }
    }
}
