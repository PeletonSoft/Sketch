using System.Linq;
using System.Windows;

namespace PeletonSoft.Sketch.Model.Element.Transformation
{
    public class HFlipTransformation : Primitive.Transformation
    {
        public override Point[] GetPoints(Point[] points, Size size)
        {
            return points
                .Select(point => new Point(size.Width - point.X, point.Y))
                .ToArray();
        }
    }
}
