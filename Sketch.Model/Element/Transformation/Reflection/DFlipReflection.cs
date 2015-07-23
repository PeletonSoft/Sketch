using System.Linq;
using System.Windows;

namespace PeletonSoft.Sketch.Model.Element.Transformation.Reflection
{
    public class DFlipReflection : Primitive.Reflection
    {
        public DFlipReflection() : base(new Point(-1,-1))
        {
        }

        public override Point[] GetPoints(Point[] points, Size size)
        {
            return points
                .Select(point => new Point(size.Width - point.X, size.Height - point.Y))
                .ToArray();
        }
    }
}
