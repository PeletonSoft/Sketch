using System.Windows;
using PeletonSoft.Sketch.ViewModel.Interface.Layout;

namespace PeletonSoft.Sketch.ViewModel.Interface.Geometry
{
    public interface IReflectionViewModel
    {
        Point[] GetPoints(Point[] points, ILayoutViewModel layout);

        Point Scale { get; }
    }
}
