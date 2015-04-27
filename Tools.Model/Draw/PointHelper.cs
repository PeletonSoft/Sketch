using System;
using System.Windows;
using PeletonSoft.Tools.Model.SketchMath;

namespace PeletonSoft.Tools.Model.Draw
{
    public static class PointHelper
    {
        public static Point Transform(this Point point, Func<Point, Point> transformer)
        {
            return transformer(point);
        }

        public static Point Transform(this Point point, Func<double, double> xTransformer, Func<double, double> yTransformer)
        {
            return new Point(xTransformer(point.X), yTransformer(point.Y));
        }

        public static Point ToPoint(this IPoint point)
        {
            return new Point(point.X, point.Y);
        }

        public static double DistanceTo(this Point start, Point finish)
        {
            return Math.Sqrt(
                Math.Pow(finish.X - start.X, 2) +
                Math.Pow(finish.Y - start.Y, 2));
        }

        public static double DistanceTo(this IPoint start, IPoint finish)
        {
            return start.ToPoint().DistanceTo(finish.ToPoint());
        }

        public static Point MoveTo(this Point start, Point direction, double distance)
        {
            var alpha = distance / start.DistanceTo(direction);
            return new Point(
                start.X + alpha * (direction.X - start.X),
                start.Y + alpha * (direction.Y - start.Y));
        }

        public static Point MoveTo(this IPoint start, IPoint direction, double distance)
        {
            return MoveTo(start.ToPoint(), direction.ToPoint(), distance);
        }

        public static double Projection(this Point current, Point start, Point finish)
        {
            var dx = finish.X - start.X;
            var dy = finish.Y - start.Y;
            var x0 = current.X - start.X;
            var y0 = current.Y - start.Y;
            return (x0 * dx + y0 * dy) / (dx * dx + dy * dy);
        }

    }
}
