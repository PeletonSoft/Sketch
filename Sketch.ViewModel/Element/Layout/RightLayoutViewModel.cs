using System.ComponentModel;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;

namespace PeletonSoft.Sketch.ViewModel.Element.Layout
{
    public sealed class RightLayoutViewModel : LayoutViewModel
    {
        public override Rect Transform(Rect rect)
        {
            return new Rect()
            {
                X = WorkspaceBit.Screen.Width - Element.OffsetX - rect.Width - rect.X,
                Y = Element.OffsetY,
                Width = rect.Width,
                Height = rect.Height
            };
        }

        public override Point LocalTransform(Point point)
        {
            return new Point(Element.Width - point.X, point.Y);
        }

        public RightLayoutViewModel(IWorkspaceBit workspaceBit, IAlignableElementViewModel element)
            : base(workspaceBit, element)
        {
        }

        protected override void ElementOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            switch (args.PropertyName)
            {
                case "Width":
                    OnPropertyChanged("Width");
                    OnPropertyChanged("Left");
                    break;
                case "Height":
                    OnPropertyChanged("Height");
                    break;
                case "OffsetX":
                    OnPropertyChanged("Left");
                    break;
                case "OffsetY":
                    OnPropertyChanged("Top");
                    break;
            }
        }

    }
}
