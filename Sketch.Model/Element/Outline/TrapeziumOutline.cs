using System.Windows;

namespace PeletonSoft.Sketch.Model.Element.Outline
{
    public class TrapeziumOutline : Primitive.Outline
    {


        public override Point[] GetPoints(Size size, double thickness)
        {
            return new[]
            {
                new Point(0, 0),
                new Point(0, thickness),
                new Point(size.Width - thickness*size.Width/size.Height, size.Height),
                new Point(size.Width, size.Height)
            };
        }

    }
}
