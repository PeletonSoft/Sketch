using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace PeletonSoft.Tools.View.Converter
{
    public class PointCollectionToPathGeometryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is IEnumerable<Point>))
            {
                return null;
            }

            var points = (value as IEnumerable<Point>).ToArray();
            var start = points[0];
            var segments = new List<LineSegment>();
            for (var i = 1; i < points.Length; i++)
            {
                segments.Add(new LineSegment(points[i], true));
            }
            var figure = new PathFigure(start, segments, true); //true if closed
            var geometry = new PathGeometry();
            geometry.Figures.Add(figure);
            return geometry;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}