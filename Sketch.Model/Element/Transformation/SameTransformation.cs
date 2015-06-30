using System.Windows;

namespace PeletonSoft.Sketch.Model.Element.Transformation
{
    public class SameTransformation : Primitive.Transformation
    {
        public override Point[] GetPoints(Point[] points, Size size)
        {
            return points;
        }
    }
}
