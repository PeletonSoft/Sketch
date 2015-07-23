using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Layout;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Element.Primitive
{
    public abstract class LayoutViewModel : ILayoutViewModel
    {
        #region implement INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(PropertyChanged, propertyName);
        }

        private void SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            Action notificator = () => OnPropertyChanged(propertyName);
            notificator.SetField(ref field, value);
        }

        private void OnPropertyChanged<T>(Expression<Func<LayoutViewModel, T>> expression)
        {
            expression.OnPropertyChanged(OnPropertyChanged);
        }

        #endregion

        public double Width
        {
            get { return Element.Width; }
        }

        public double Height
        {
            get { return Element.Height; }
        }

        public double Left
        {
            get { return Transform(Rect).X; }
        }

        public double Top
        {
            get { return Transform(Rect).Y; }
        }

        public virtual Rect Transform(Rect rect)
        {
            return new Rect
            {
                X = Element.OffsetX + rect.X,
                Y = Element.OffsetY + rect.Y,
                Width = rect.Width,
                Height = rect.Height
            };
        }

        public virtual Point LocalTransform(Point point)
        {
            return point;
        }

        public LayoutViewModel(IWorkspaceBit workspaceBit, IAlignableElementViewModel element)
        {
            WorkspaceBit = workspaceBit;
            Element = element;

            element
                .SetPropertyChanged(el => el.Width,
                    () =>
                    {
                        OnPropertyChanged(el => el.Width);
                        OnPropertyChanged(el => el.Rect);
                    })
                .SetPropertyChanged(el => el.Height,
                    () =>
                    {
                        OnPropertyChanged(el => el.Height);
                        OnPropertyChanged(el => el.Rect);
                    })
                .SetPropertyChanged(el => el.OffsetX, () => OnPropertyChanged(el => el.Left))
                .SetPropertyChanged(el => el.OffsetY, () => OnPropertyChanged(el => el.Top));

            WorkspaceBit
                .RenderChangedDispatcher.RenderChanged +=
                (sender, args) =>
                {
                    if (sender == Element)
                    {
                        OpacityMask = args.RenderData;
                    }
                };
        }


        protected IWorkspaceBit WorkspaceBit { get; private set; }

        IElementViewModel ILayoutViewModel.Element
        {
            get { return Element; }
        }

        protected IAlignableElementViewModel Element { get; private set; }

        public void RestoreDefault()
        {
        }

        private IEnumerable<IEnumerable<Point>> _opacityMask;
        public IEnumerable<IEnumerable<Point>> OpacityMask
        {
            get { return _opacityMask; }
            private set { SetField(ref _opacityMask, value); }
        }

        public Rect Rect
        {
            get { return Element.GetRect(); } 
        }
    }
}
