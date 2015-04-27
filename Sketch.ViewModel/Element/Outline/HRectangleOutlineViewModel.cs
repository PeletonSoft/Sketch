using System.Windows;
using PeletonSoft.Sketch.ViewModel.Element.Outline.Primitive;

namespace PeletonSoft.Sketch.ViewModel.Element.Outline
{
    public class HRectangleOutlineViewModel : OutlineViewModel
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
                new Point(0, b/2 - d/2),
                new Point(a, b/2 - d/2),
                new Point(a, b/2 + d/2),
                new Point(0, b/2 + d/2)
            };
        }

    }
}
