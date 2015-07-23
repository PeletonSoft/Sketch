using System.Windows;

namespace PeletonSoft.Sketch.Model.Element.Transformation.Reflection
{
    public class SameReflection : Primitive.Reflection
    {
        public SameReflection() : base(new Point(1, 1))
        {
        }

        public override Point[] GetPoints(Point[] points, Size size)
        {
            return points;
        }
    }
}
