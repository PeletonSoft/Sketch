using System;
using System.Windows.Data;

namespace PeletonSoft.Tools.View.Converter
{
    public class IntToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var strVal = value.ToString();

            if (string.IsNullOrEmpty(strVal))
            {
                return 0;
            }
            return int.Parse(strVal);
        }



        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.ToString();
        }
    }
}
