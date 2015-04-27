using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using PeletonSoft.Tools.Model;

namespace PeletonSoft.Tools.View.Converter
{
    public class InjectContainerCollectionConverter : IValueConverter
    {
        public InjectContainer InjectContainer { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new TransformedCollection<object, object>(
                (IReadOnlyCollection<object>)value, val => InjectContainer.Resolve(val.GetType(), val));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}