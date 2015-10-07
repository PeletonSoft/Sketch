using System.Windows;
using PeletonSoft.Tools.Model.Draw;

namespace PeletonSoft.Tools.Model.Dragable
{
    public class PointTransit<T>
    {
        public Point Point { get; }
        public T Area { get; }

        public PointTransit(Point point, T area)
        {
            Point = point;
            Area = area;
        }
    }

    public class PointTransit : PointTransit<object>
    {
        public PointTransit(Point point, object area)
            : base(point, area)
        {
        }

        public PointTransit<T> Cast<T>()
        {
            return new PointTransit<T>(Point, (T)Area);
        }
    }
}
