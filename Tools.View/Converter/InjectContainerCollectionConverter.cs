using System;
using System.Globalization;
using System.Windows.Data;
using PeletonSoft.Tools.Model.Collection;
using PeletonSoft.Tools.Model.Dependency;
using PeletonSoft.Tools.Model.Logic;

namespace PeletonSoft.Tools.View.Converter
{
    public class InjectContainerCollectionConverter : IValueConverter
    {
        public InjectContainer InjectContainer { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var col = (INotifyChangedReadOnlyCollection<object>)value;
            return new TransformedCollection<object, object>(col, val => InjectContainer.Resolve(val.GetType(), (IViewModel)val));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}