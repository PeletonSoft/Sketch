using System.Collections.Generic;
using System.Windows;

namespace PeletonSoft.Tools.Model.SketchMath
{
    public interface IConnectStrategy
    {
        IEnumerable<Point> Connect(Point start, Point finish);
    }
}
