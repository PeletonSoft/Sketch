using System.Windows;

namespace PeletonSoft.Sketch.Model.Element.Outline
{
    public class HRectangleOutline : Primitive.Outline
    {


        public override Point[] GetPoints(Size size, double thickness)
        {
            return new[]
            {
                new Point(0, size.Height/2 - thickness/2),
                new Point(size.Width, size.Height/2 - thickness/2),
                new Point(size.Width, size.Height/2 + thickness/2),
                new Point(0, size.Height/2 + thickness/2)
            };
        }

    }
}
