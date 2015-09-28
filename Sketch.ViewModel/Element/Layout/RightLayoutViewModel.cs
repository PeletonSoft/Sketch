using System.Windows;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;

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

        public override Point LocalTransform(Point point) => new Point(Element.Width - point.X, point.Y);

        public RightLayoutViewModel(IWorkspaceBit workspaceBit, IAlignableElementViewModel element)
            : base(workspaceBit, element)
        {
            this.SetPropertyChanged(nameof(Width), () => OnPropertyChanged(nameof(Left)));
        }
    }
}
