using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;

namespace UniversalAppExample.Common
{
    public class StringToCharConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var index = Int32.Parse((string)parameter);
            var text = (string)value;
            if (index >= text.Length)
                return ' ';
            return text[index];
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return true;
        }
    }
}
