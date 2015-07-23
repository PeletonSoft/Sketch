using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Layout;

namespace PeletonSoft.Sketch.ViewModel.Element.Layout
{
    public class NullLayoutViewModel : ILayoutViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void RestoreDefault()
        {
        }

        public IElementViewModel Element
        {
            get { return null; }
        }

        public double Width
        {
            get { return 0; }
        }

        public double Height
        {
            get { return 0; }
        }

        public double Left
        {
            get { return 0; }
        }

        public double Top
        {
            get { return 0; }
        }

        public Rect Transform(Rect rect)
        {
            return rect;
        }

        public Point LocalTransform(Point point)
        {
            return point;
        }

        public IEnumerable<IEnumerable<Point>> OpacityMask
        {
            get { return null; }
        }

        public Rect Rect
        {
            get { return new Rect(); }
        }
    }
}
