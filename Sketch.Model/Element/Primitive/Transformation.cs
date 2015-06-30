using System.Windows;

namespace PeletonSoft.Sketch.Model.Element.Primitive
{
    public abstract class Transformation
    {
        public abstract Point[] GetPoints(Point[] points, Size size);
    }
}
