using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;

namespace PeletonSoft.Tools.View.Converter
{
    public class GeometryCollectionToGeometryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return new GeometryCollection();
            }

            if (value is Geometry)
            {
                var geometry = (Geometry) value;
                return new GeometryCollection(new[] {geometry});
            }

            if (value is IEnumerable<object>)
            {
                var list = (IEnumerable<object>) value;
                return new GeometryCollection(list.OfType<Geometry>());
            }

            return new GeometryCollection();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}