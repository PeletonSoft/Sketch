using System.Windows;
using PeletonSoft.Tools.Model.Draw;

namespace PeletonSoft.Tools.Model.Dragable
{
    public class InsertPointTransit
    {
        public Point Point { get; }
        public ILineViewModel Line { get; }

        public InsertPointTransit(Point point, ILineViewModel line)
        {
            Point = point;
            Line = line;
        }
    }
}
