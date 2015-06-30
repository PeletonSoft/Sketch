using System.Windows;

namespace PeletonSoft.Sketch.Model.Element.Primitive
{
    public abstract class Outline
    {
        public abstract Point[] GetPoints(Size size, double thickness);
    }
}
