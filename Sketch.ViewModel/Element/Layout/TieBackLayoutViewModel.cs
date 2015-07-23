using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Layout;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Element.Layout
{
    public sealed class TieBackLayoutViewModel : ILayoutViewModel
    {
        #region implement INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(PropertyChanged, propertyName);
        }

        private bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            Action notificator = () => OnPropertyChanged(propertyName);
            return notificator.SetField(ref field, value);
        }

        private void OnPropertyChanged<T>(Expression<Func<TieBackLayoutViewModel, T>> expression)
        {
            expression.OnPropertyChanged(OnPropertyChanged);
        }
        #endregion

        public void RestoreDefault()
        {
        }

        public double Width
        {
            get { return Rect.Width; }
        }

        public double Height
        {
            get { return Rect.Height; }
        }

        public double Left
        {
            get { return Transform(Element.Rect).X; }
        }

        public double Top
        {
            get { return Transform(Element.Rect).Y; }
        }

        public Rect Transform(Rect rect)
        {
            var layout = Element.Sheet.Layout;
            return layout.Transform(rect);
        }

        public Point LocalTransform(Point point)
        {
            var layout = Element.Sheet.Layout;
            return layout.LocalTransform(point);
        }

        private IEnumerable<IEnumerable<Point>> _opacityMask;
        public IEnumerable<IEnumerable<Point>> OpacityMask
        {
            get { return _opacityMask; }
            set { SetField(ref _opacityMask, value); }
        }

        public Rect Rect
        {
            get { return new Rect(new Point(0, 0), Element.Rect.Size); }
        }

        public TieBackLayoutViewModel(TieBackViewModel element)

        {
            Element = element;

            var dispatcher = Element.WorkspaceBit.RenderChangedDispatcher;
            dispatcher.RenderChanged +=
                (sender, args) =>
                {
                    if (sender == Element)
                    {
                        OpacityMask = args.RenderData;
                    }
                };

            Element
                .SetPropertyChanged(
                    new[]
                    {
                        Element.GetPropertyName(el => el.Sheet),
                        Element.GetPropertyName(el => el.Rect)
                    }, () =>
                    {
                        OnPropertyChanged(l => l.Rect);
                        OnPropertyChanged(l => l.Width);
                        OnPropertyChanged(l => l.Height);
                        OnPropertyChanged(l => l.Top);
                        OnPropertyChanged(l => l.Left);
                    });

        }

        private TieBackViewModel Element { get; set; }

        IElementViewModel ILayoutViewModel.Element
        {
            get { return Element; }
        }
    }
}
