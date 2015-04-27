using System.Windows;
using PeletonSoft.Sketch.ViewModel.Element.Outline.Primitive;

namespace PeletonSoft.Sketch.ViewModel.Element.Outline
{
    public class ParallelogramOutlineViewModel : OutlineViewModel
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
                new Point(0, 0),
                new Point(0, d),
                new Point(a, b),
                new Point(a, b - d)
            };
        }

    }
}
