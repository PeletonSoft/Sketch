using System.Windows;

namespace PeletonSoft.Sketch.Model.Element.Outline
{
    public class VRectangleOutline : Primitive.Outline
    {
        public override Point[] GetPoints(Size size, double thickness)
        {
            return new[]
            {
                new Point(size.Width/2 - thickness/2, 0),
                new Point(size.Width/2 - thickness/2, size.Height),
                new Point(size.Width/2 + thickness/2, size.Height),
                new Point(size.Width/2 + thickness/2, 0)
            };
        }
    }
}
