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
    public class FormattedDateConverter : IDataTypeConverter
    {
        private string _format;

        public FormattedDateConverter(string format)
        {
            _format = format;
        }

        public object GetValue(DataTypesMapper dataTypeMapper, PropertyInfo field, string type, JToken value)
        {
            DateTime result;
            if (DateTime.TryParseExact(value.ToString(), _format, CultureInfo.CurrentUICulture, DateTimeStyles.None, out result))
            {
                return result;
            }
            else
            {
                //log
            }
            return null;
        }
    }
}
