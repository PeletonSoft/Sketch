using System.Collections.Generic;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Interface.Layout;
using PeletonSoft.Tools.Model.Logic;

namespace PeletonSoft.Sketch.ViewModel.Interface.Visual
{
    public interface ILayoutVisualViewModel : INotifyVisualViewModel<ILayoutViewModel>
    {
        double Width { get; }
        double Height { get; }
        double Top { get; }
        double Left { get; }
        Rect Rect { get; }
        IEnumerable<IEnumerable<Point>> OpacityMask { get; }
    }
}
