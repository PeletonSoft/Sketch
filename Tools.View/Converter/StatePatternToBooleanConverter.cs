using System;
using System.Windows.Data;

namespace PeletonSoft.Tools.View.Converter
{
    public class StatePatternToBooleanConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            
            return value.Length >= 2 &&
                value[0] != null && value[1] != null &&
                value[0].GetType() == value[1].GetType();
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new[] { Binding.DoNothing, Binding.DoNothing };
        }
    }
}
