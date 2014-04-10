using MSC.Socrata.Device.Client.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Socrata.Device.Client
{
    public class DataTypesMapper
    {
        public const string UNKNOWN_TYPE = "__unknown_type__";

        private static readonly IDataTypeConverter EmbeddedEntityConverter = new EmbeddedSodaEntityConverter();

        private static readonly IDictionary<string, IDataTypeConverter> Converters = new Dictionary<string, IDataTypeConverter>() 
        {
            {"percent", new IntegerConverter()},
            {"date", new DateTimeStampConverter()},
            {"percent", new IntegerConverter()},
            {"text", new StringTypeConverter()},
            {"dataset_link", new StringTypeConverter()},
            {"html", new StringTypeConverter()},
            {"money", new DoubleConverter()},
            {"calendar_date", new FormattedDateConverter("yyyy-MM-dd'T'HH:mm:ss")},
            {"phone", EmbeddedEntityConverter},
            {"location", EmbeddedEntityConverter},
            {"stars", new IntegerConverter()},
            {"photo", new StringTypeConverter()},
            {"url", EmbeddedEntityConverter},
            {"document", EmbeddedEntityConverter},
            {"drop_down_list", new StringTypeConverter()},
            {"flag", new StringTypeConverter()}
        };

        private static readonly IDictionary<Type, IDataTypeConverter> DefaultJavaConverters = new Dictionary<Type, IDataTypeConverter>() 
        {
            {typeof(double), new DoubleConverter()},
            {typeof(int), new IntegerConverter()},
            {typeof(bool), new BooleanConverter()},
            {typeof(long), new LongConverter()},
            {typeof(DateTime), new DateTimeStampConverter()},
            {typeof(string), new StringTypeConverter()}
        };

        public void SetConverter(string type, IDataTypeConverter dataTypeConverter)
        {
            Converters.Add(type, dataTypeConverter);
        }

        public void SetJavaConverter(Type type, IDataTypeConverter dataTypeConverter)
        {
            DefaultJavaConverters.Add(type, dataTypeConverter);
        }

        public object GetValue(PropertyInfo targetField, string type, JToken rawValue)
        {
            object result = rawValue.Value<object>();
            var converter = Converters[type];
            if (converter != null)
            {
                result = converter.GetValue(this, targetField, type, rawValue);
            }
            else
            {
                var fieldType = targetField.GetType();
                var sodaFieldAttribute = fieldType.GetTypeInfo().GetCustomAttribute<SodaEntityAttribute>();
                if (sodaFieldAttribute != null)
                {
                    result = EmbeddedEntityConverter.GetValue(this, targetField, type, rawValue);
                }
                else
                {
                    if (DefaultJavaConverters.ContainsKey(fieldType))
                    {
                        result = DefaultJavaConverters[fieldType].GetValue(this, targetField, type, rawValue);
                    }
                }
            }
            return result;
        }
    }
}
