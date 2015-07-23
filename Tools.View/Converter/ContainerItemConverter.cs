using System;
using System.Globalization;
using System.Windows.Data;
using PeletonSoft.Tools.Model.Collection;
using PeletonSoft.Tools.Model.Memento.Container;

namespace PeletonSoft.Tools.View.Converter
{
    public class ContainerItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var col = (IContainer<object>) value;
                var key = (string) parameter;
                return col.GetValueByKeyOrDefault(key);
            }
            catch
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
