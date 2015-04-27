using System;
using System.Windows.Data;

namespace PeletonSoft.Tools.View.Converter
{
    public class RatioConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            try
            {
                var scale = parameter == null ? 1.0 : (double) parameter;
                var first = ((double) values[0]);
                var second = ((double) values[1]);
                var result = second != 0 ? first/second/scale : 1;
                return result;
            }
            catch
            {
                return Binding.DoNothing;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return new[] {Binding.DoNothing, Binding.DoNothing};
        }
    }
}