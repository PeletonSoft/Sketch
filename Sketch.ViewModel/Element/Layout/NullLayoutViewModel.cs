using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Layout;
using static PeletonSoft.Tools.Model.ObjectEvent.EventAction;

namespace PeletonSoft.Sketch.ViewModel.Element.Layout
{
    public class NullLayoutViewModel : ILayoutViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public IElementViewModel Element { get; } = null;
        public double Width { get; } = 0;
        public double Height { get; } = 0;
        public double Left { get; } = 0;
        public double Top { get; } = 0;

        public Rect Transform(Rect rect) => rect;
        public Point LocalTransform(Point point) => point;
        public IEnumerable<IEnumerable<Point>> OpacityMask { get; } = null;
        public Rect Rect { get; } = new Rect();
    }
}
