using System;
using System.Windows.Data;

namespace PeletonSoft.Tools.View.Converter
{
    public class TranslationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return ((double)value) + ((double)parameter);
        }

        public object ConvertBack(object value, Type targetTypes, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return ((double)value) - ((double)parameter);
        }
    }

}
