using System.Windows;

namespace PeletonSoft.Sketch.ViewModel.Element.Outline.Primitive
{
    public abstract class OutlineViewModel
    {
        public abstract Point[] GetPoints(double width, double height, double thickness);
    }
}
