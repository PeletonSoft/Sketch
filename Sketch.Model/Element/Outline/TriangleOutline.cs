using System.Windows;

namespace PeletonSoft.Sketch.Model.Element.Outline
{
    public class TriangleOutline : Primitive.Outline
    {


        public override Point[] GetPoints(Size size, double thickness)
        {
            return new[]
            {
                new Point(0, size.Height),
                new Point(0, size.Height - thickness),
                new Point(thickness*size.Width/size.Height, size.Height)
            };
        }

    }
}
