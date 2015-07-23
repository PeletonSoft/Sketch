using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace PeletonSoft.Tools.View.Converter
{
    public class LineCollectionToPathGeometryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is IEnumerable<Rect>))
            {
                return null;
            }
            var lines = (value as IEnumerable<Rect>).ToArray();
            var col = lines.Select(l => new LineGeometry(l.TopLeft, l.BottomRight));
            return new GeometryGroup {Children = new GeometryCollection(col)};;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}