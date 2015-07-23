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
            get { return PixelPerUnit.Transform(Layout.Width); }
        }

        public double Height
        {
            get { return PixelPerUnit.Transform(Layout.Height); }
        }

        public double Top
        {
            get { return PixelPerUnit.Transform(Layout.Top); }
        }

        public double Left
        {
            get { return PixelPerUnit.Transform(Layout.Left); }
        }

        public IEnumerable<IEnumerable<Point>> OpacityMask
        {
            get { return PixelPerUnit.Transform(Layout.OpacityMask); }
        }

        public Rect Rect
        {
            get { return PixelPerUnit.Transform(Layout.Rect); }
        }

        public LayoutVisualViewModel(VisualOptions visualOptions, ILayoutViewModel layout)
        {
            VisualOptions = visualOptions;
            PixelPerUnit = VisualOptions.PixelPerUnit;
            Layout = layout;

            Layout
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

        private ILayoutViewModel Layout { get; set; }

        private PixelPerUnit PixelPerUnit { get; set; }

    }
}
