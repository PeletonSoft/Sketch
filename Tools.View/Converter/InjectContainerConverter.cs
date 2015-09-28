using System;
using System.Globalization;
using System.Windows.Data;
using PeletonSoft.Tools.Model.Dependency;
using PeletonSoft.Tools.Model.Logic;

namespace PeletonSoft.Tools.View.Converter
{
    public class InjectContainerConverter : IValueConverter
    {
        public InjectContainer InjectContainer { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return InjectContainer.Resolve(value.GetType(), (IViewModel)value);
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
