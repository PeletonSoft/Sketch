using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Windows.Data;

namespace PeletonSoft.Tools.View.Converter
{
    public class TypeToListDictionaryItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            var list = (ListDictionary) parameter;
            return list[value.GetType()];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
