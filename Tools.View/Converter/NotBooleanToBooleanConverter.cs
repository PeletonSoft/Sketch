using System;
using System.Windows.Data;

namespace PeletonSoft.Tools.View.Converter
{
    public class NotBooleanToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (!(value is bool) || !(bool)value);
        }



        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (!(value is bool) || !(bool)value);
        }
    }
}
