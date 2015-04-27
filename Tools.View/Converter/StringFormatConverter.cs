using System;
using System.Windows.Data;

namespace PeletonSoft.Tools.View.Converter
{
    public class StringFormatConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return string.Format(parameter.ToString(), values);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}