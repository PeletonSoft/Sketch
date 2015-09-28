using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Interface.Layout;
using PeletonSoft.Sketch.ViewModel.Interface.Visual;
using PeletonSoft.Tools.Model;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element.Primitive
{
    public sealed class LayoutVisualViewModel : ILayoutVisualViewModel
    {

        #region implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) => this.OnPropertyChanged(PropertyChanged, propertyName);
        #endregion

        public double Width => PixelPerUnit.Transform(Element.Width);
        public double Height => PixelPerUnit.Transform(Element.Height);
        public double Top => PixelPerUnit.Transform(Element.Top);
        public double Left => PixelPerUnit.Transform(Element.Left);
        public IEnumerable<IEnumerable<Point>> OpacityMask => PixelPerUnit.Transform(Element.OpacityMask);

        public Rect Rect => PixelPerUnit.Transform(Element.Rect);

        public LayoutVisualViewModel(VisualOptions visualOptions, ILayoutViewModel element)
        {
            VisualOptions = visualOptions;
            PixelPerUnit = VisualOptions.PixelPerUnit;
            Element = element;

            Element
                .SetPropertyChanged(nameof(Element.Width),
                    () =>
                    {
                        OnPropertyChanged(nameof(Width));
                        OnPropertyChanged(nameof(Rect));
                    })
                .SetPropertyChanged(nameof(Element.Height),
                    () =>
                    {
                        OnPropertyChanged(nameof(Height));
                        OnPropertyChanged(nameof(Rect));
                    })
                .SetPropertyChanged(nameof(Element.Top),
                    () =>
                    {
                        OnPropertyChanged(nameof(Top));
                        OnPropertyChanged(nameof(Rect));
                    })
                .SetPropertyChanged(nameof(Element.Left),
                    () =>
                    {
                        OnPropertyChanged(nameof(Left));
                        OnPropertyChanged(nameof(Rect));
                    })
                .SetPropertyChanged(nameof(Element.OpacityMask), () => OnPropertyChanged(nameof(OpacityMask)))
                .SetPropertyChanged(nameof(Element.Rect), () => OnPropertyChanged(nameof(Rect)));
        }

        private VisualOptions VisualOptions { get; }

        public ILayoutViewModel Element { get; set; }

        private PixelPerUnit PixelPerUnit { get; }


    }
}
