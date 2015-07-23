using System;
using System.Linq.Expressions;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Element.Layout
{
    public sealed class RightLayoutViewModel : LayoutViewModel
    {
        private void OnPropertyChanged<T>(Expression<Func<RightLayoutViewModel, T>> expression)
        {
            expression.OnPropertyChanged(OnPropertyChanged);
        }

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
            this.SetPropertyChanged(el => el.Width, () => OnPropertyChanged(el => el.Left));
        }
    }
}
