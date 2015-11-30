using System.Collections.Generic;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Tools.Model.Logic;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Interface.Layout
{
    public interface ILayoutViewModel : INotifyViewModel
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
