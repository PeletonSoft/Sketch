using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PeletonSoft.Tools.View.Converter
{
    public class ShiftMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var shift = (double) value;
            return new Thickness(-shift, -shift, 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
