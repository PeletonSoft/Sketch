using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace PeletonSoft.Tools.View.Converter
{
    public class EnumerableCompositeConverter : IValueConverter
    {
        public IValueConverter ListConverter { get; set; }
        public IValueConverter ItemConverter { get; set; }


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return ListConverter.Convert(null, targetType, parameter, culture);
            }

            var list = (IEnumerable<object>) value;
            return ListConverter.Convert(
                    list.Select(item => ItemConverter.Convert(item, null, null, culture)), 
                    targetType, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
