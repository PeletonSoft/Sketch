using System;
using System.Collections.Generic;
using System.Windows;

namespace PeletonSoft.Tools.Model.SketchMath
{
    public class PointConnection
    {
        public PointConnection(Func<double,Point> connectFunc, int pointCount)
        {
            ConnectFunc = connectFunc;
            PointCount = pointCount;
        }

        public int PointCount { get; set; }
        public Func<double, Point> ConnectFunc { get; set; }

        public IEnumerable<Point> Connect(double start, double finish)
        {
            var points = new List<Point>();
            for (var i = 0; i <= PointCount; i++)
            {
                var alpha = ((double) i)/PointCount;
                var t = start + alpha*(finish - start);
                points.Add(ConnectFunc(t));
            }
            return points;
        }
    }
}
