using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace PeletonSoft.Tools.View.Drawing
{
    public static class PointCollectionHelper
    {
        public static Geometry ToPathGeometry(this IEnumerable<Point> points, bool isClosed = true)
        {
            var pointArray = points.ToArray();
            var start = pointArray[0];
            var segments = new List<LineSegment>();

            for (var i = 1; i < pointArray.Length; i++)
            {
                segments.Add(new LineSegment(pointArray[i], true));
            }

            var figure = new PathFigure(start, segments, isClosed); 
            var geometry = new PathGeometry();
            geometry.Figures.Add(figure);
            return geometry;
        }
    }
}
