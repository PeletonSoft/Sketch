using System;
using System.Globalization;
using System.Windows.Data;

namespace PeletonSoft.Tools.View.Converter
{
    public class FitInRectangleConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var a = (double)values[0];
                var b = (double)values[1];
                var ap = (double)values[2];
                var bp = (double)values[3];
                return a * Math.Min(ap / a, bp / b);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
