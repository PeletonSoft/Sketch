using System.Windows;
using PeletonSoft.Sketch.Model.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Interface.Geometry;
using PeletonSoft.Sketch.ViewModel.Interface.Layout;

namespace PeletonSoft.Sketch.ViewModel.Geometry
{
    public class ReflectionViewModel : IReflectionViewModel
    {
        private readonly Reflection _reflection;

        public Point[] GetPoints(Point[] points, ILayoutViewModel layout)
        {
            return _reflection.GetPoints(points, new Size(layout.Width, layout.Height));
        }

        public Point Scale => _reflection.Scale;

        public ReflectionViewModel(Reflection reflection)
        {
            _reflection = reflection;
        }
    }
}
