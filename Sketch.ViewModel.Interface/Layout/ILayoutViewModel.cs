using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Interface.Layout
{
    public interface ILayoutViewModel : INotifyPropertyChanged, IOriginator
    {
        IElementViewModel Element { get; }
        double Width { get; }
        double Height { get; }
        double Left { get; }
        double Top { get; }
        Rect Transform(Rect rect);
        Point LocalTransform(Point point);
        IEnumerable<IEnumerable<Point>> OpacityMask { get; }
        Rect Rect { get; }

    }
}
