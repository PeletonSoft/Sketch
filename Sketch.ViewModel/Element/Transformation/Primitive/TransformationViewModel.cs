using System.Windows;
using PeletonSoft.Sketch.ViewModel.Interface.Layout;

namespace PeletonSoft.Sketch.ViewModel.Element.Transformation.Primitive
{
    public abstract class TransformationViewModel
    {
        public abstract Point[] GetPoints(Point[] points, ILayoutViewModel layout);
    }
}
