using System.Collections.Generic;
using System.Windows;

namespace PeletonSoft.Tools.Model.SketchMath.ConnectStrategy
{
    public class LineConnectStrategy : IConnectStrategy
    {
        public IEnumerable<Point> Connect(Point start, Point finish)
        {
            return new[] {start, finish};
        }
    }
}
