using System.Linq;
using System.Windows;

namespace PeletonSoft.Sketch.Model.Element.Transformation.Reflection
{
    public class VFlipReflection : Primitive.Reflection
    {
        public VFlipReflection() : base(new Point(1, -1))
        {
        }

        public override Point[] GetPoints(Point[] points, Size size)
        {
            return points
                .Select(point => new Point(point.X, size.Height - point.Y))
                .ToArray();
        }
    }
}
