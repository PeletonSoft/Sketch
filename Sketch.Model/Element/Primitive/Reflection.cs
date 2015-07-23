using System.Windows;

namespace PeletonSoft.Sketch.Model.Element.Primitive
{
    public abstract class Reflection
    {
        public abstract Point[] GetPoints(Point[] points, Size size);
        public Point Scale { get; private set; }

        public Reflection(Point scale)
        {
            Scale = scale;
        }
    }
}
