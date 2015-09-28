using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Interface.Layout;
using PeletonSoft.Sketch.ViewModel.Interface.Visual;
using PeletonSoft.Tools.Model;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element.Primitive
{
    public sealed class LayoutVisualViewModel : ILayoutVisualViewModel
    {

        #region implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(PropertyChanged, propertyName);
        }

        private void OnPropertyChanged<T>(Expression<Func<LayoutVisualViewModel, T>> expression)
        {
            expression.OnPropertyChanged(OnPropertyChanged);
        }
        #endregion

        public double Width
        {
            get { return PixelPerUnit.Transform(Element.Width); }
        }

        public double Height
        {
            get { return PixelPerUnit.Transform(Element.Height); }
        }

        public double Top
        {
            get { return PixelPerUnit.Transform(Element.Top); }
        }

        public double Left
        {
            get { return PixelPerUnit.Transform(Element.Left); }
        }

        public IEnumerable<IEnumerable<Point>> OpacityMask
        {
            get { return PixelPerUnit.Transform(Element.OpacityMask); }
        }

        public Rect Rect
        {
            get { return PixelPerUnit.Transform(Element.Rect); }
        }

        public LayoutVisualViewModel(VisualOptions visualOptions, ILayoutViewModel element)
        {
            VisualOptions = visualOptions;
            PixelPerUnit = VisualOptions.PixelPerUnit;
            Element = element;

            Element
                .SetPropertyChanged(l => l.Width,
                    () =>
                    {
                        OnPropertyChanged(v => v.Width);
                        OnPropertyChanged(v => v.Rect);
                    })
                .SetPropertyChanged(l => l.Height,
                    () =>
                    {
                        OnPropertyChanged(v => v.Height);
                        OnPropertyChanged(v => v.Rect);
                    })
                .SetPropertyChanged(l => l.Top,
                    () =>
                    {
                        OnPropertyChanged(v => v.Top);
                        OnPropertyChanged(v => v.Rect);
                    })
                .SetPropertyChanged(l => l.Left,
                    () =>
                    {
                        OnPropertyChanged(v => v.Left);
                        OnPropertyChanged(v => v.Rect);
                    })
                .SetPropertyChanged(l => l.OpacityMask, () => OnPropertyChanged(v => v.OpacityMask))
                .SetPropertyChanged(l => l.Rect, () => OnPropertyChanged(v => v.Rect));
        }

        private VisualOptions VisualOptions { get; set; }

        public ILayoutViewModel Element { get; set; }

        private PixelPerUnit PixelPerUnit { get; set; }


    }
}
