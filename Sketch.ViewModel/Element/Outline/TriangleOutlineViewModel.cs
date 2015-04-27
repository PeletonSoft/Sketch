using System.Windows;
using PeletonSoft.Sketch.ViewModel.Element.Outline.Primitive;

namespace PeletonSoft.Sketch.ViewModel.Element.Outline
{
    public class TriangleOutlineViewModel : OutlineViewModel
    {


        public override Point[] GetPoints(double width, double height, double thickness)
        {
            var a = width;
            var b = height;
            var d = thickness;

            if (d > b)
            {
                return null;
            }

            return new Point[]
            {
                new Point(0, b),
                new Point(0, b - d),
                new Point(d*a/b, b)
            };
        }

    }
}
