using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace PeletonSoft.Sketch.ViewModel.Interface.Visual
{
    public interface ILayoutVisualViewModel : INotifyPropertyChanged
    {
        double Width { get; }
        double Height { get; }
        double Top { get; }
        double Left { get; }
        Rect Rect { get; }
        IEnumerable<IEnumerable<Point>> OpacityMask { get; }
    }
}
