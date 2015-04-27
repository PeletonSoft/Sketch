using System.Linq;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Element.Transformation.Primitive;
using PeletonSoft.Sketch.ViewModel.Interface.Layout;

namespace PeletonSoft.Sketch.ViewModel.Element.Transformation
{
    public class DFlipTransformationViewModel : TransformationViewModel
    {
        public override Point[] GetPoints(Point[] points, ILayoutViewModel layout)
        {
            return points
                .Select(point => new Point(layout.Width - point.X, layout.Height - point.Y))
                .ToArray();
        }
    }
}
