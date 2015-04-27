using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace PeletonSoft.Tools.Model.SketchMath.ConnectStrategy
{
    public class CatenaryLengthConnectStrategy : IConnectStrategy
    {
        public IEnumerable<Point> Connect(Point start, Point finish)
        {
            if (Math.Abs(finish.X - start.X) < 1e-6)
            {
                return new[] {start, finish};
            }
            if (finish.X < start.X)
            {
                return Connect(finish, start).Reverse();
            }
            var catenary = Catenary.FromLength(
                start.X, Sign * start.Y,
                finish.X, Sign * finish.Y, 
                Length);
            var connection = new PointConnection(x => new Point(x, catenary.GetValue(x)), PointCount);
            return connection.Connect(start.X, finish.X)
                .Select(point => new Point(point.X, Sign * point.Y));
        }

        public CatenaryLengthConnectStrategy(double length, int pointCount, bool inverse)
        {
            Length = length;
            PointCount = pointCount;
            Inverse = inverse;
        }
        public bool Inverse { get; private set; }

        public double Sign
        {
            get { return Inverse ? -1 : 1; }
        }
        public int PointCount { get; private set; }

        public double Length { get; private set; }
    }
}
