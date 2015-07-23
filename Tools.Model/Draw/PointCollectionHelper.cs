using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using PeletonSoft.Tools.Model.Collection;

namespace PeletonSoft.Tools.Model.Draw
{
    public static class PointCollectionHelper
    {
        public static IEnumerable<Point> Glue(
            this IEnumerable<Point> first,
            IEnumerable<Point> second)
        {
            return first.Concat(second)
                .RemoveSequentialRepeats();
        }

        public static IEnumerable<Point> Cut(this IEnumerable<Point> points, double length)
        {
            var current = 0.0;
            Point? last = null;
            foreach (var point in points)
            {
                if (last == null)
                {
                    yield return point;
                    last = point;
                    continue;
                }

                var distance = point.DistanceTo((Point) last);
                var rest = current + distance - length;

                if (rest < 0)
                {
                    current += distance;
                    last = point;
                    yield return point;
                    continue;
                }

                if (Math.Abs(rest) < 1e-10)
                {
                    yield return point;
                    break;
                }

                yield return point.MoveTo((Point) last, rest);
                break;
            }
        }

        public static double Length(this IEnumerable<Point> points)
        {
            var result = 0.0;
            Point? last = null;
            foreach (var point in points)
            {

                if (last != null)
                {
                    result += point.DistanceTo((Point) last);
                }

                last = point;
            }
            return result;
        }
    }
}
