using System;
using System.Windows;
using System.Windows.Data;

namespace PeletonSoft.Tools.View.Converter
{
    public class StatePatternToBooleanVisibility : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            
            var condition = value.Length >= 2 &&
                value[0] != null && value[1] != null &&
                value[0].GetType() == value[1].GetType();
            return condition ? Visibility.Visible : Visibility.Hidden;
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new[] { Binding.DoNothing, Binding.DoNothing };
        }
    }
}
