using System.Windows;
using PeletonSoft.Sketch.Model.Element.Primitive;

namespace PeletonSoft.Sketch.ViewModel.Element.Primitive
{
    public class OutlineViewModel
    {
        private readonly Outline _outline;

        public Point[] GetPoints(Size size, double thickness)
        {
            return _outline.GetPoints(size, thickness);
        }

        public OutlineViewModel(Outline outline)
        {
            _outline = outline;
        }
    }
}
