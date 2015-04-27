using System;
using System.Windows.Data;

namespace PeletonSoft.Tools.View.Converter
{
    public class DoubleFormatConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (values[0] == null)
                return Binding.DoNothing;

            return string.Format(values[0].ToString(), values[1]);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
            System.Globalization.CultureInfo culture)
        {
            double resultNumber;
            var result = Double.TryParse(value.ToString(), out resultNumber);
            return 
                new[]
                {
                    Binding.DoNothing, 
                    result ? resultNumber : Binding.DoNothing
                };
        }
    }
}