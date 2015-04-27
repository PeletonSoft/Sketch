using System.Windows;
using PeletonSoft.Sketch.ViewModel.Element.Outline.Primitive;

namespace PeletonSoft.Sketch.ViewModel.Element.Outline
{
    public class VRectangleOutlineViewModel : OutlineViewModel
    {


        public override Point[] GetPoints(double width, double height, double thickness)
        {
            var a = width;
            var b = height;
            var d = thickness;

            if (d > a)
            {
                return null;
            }

            return new Point[]
            {
                new Point(a/2 - d/2, 0),
                new Point(a/2 - d/2, b),
                new Point(a/2 + d/2, b),
                new Point(a/2 + d/2, 0)
            };
        }

    }
}
