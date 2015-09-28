using System.Collections.Generic;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Visual;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Primitive;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element
{
    public sealed class RomanBlindVisualViewModel : ElementVisualViewModel, IElementVisualViewModel<RomanBlindViewModel>
    {
        public RomanBlindVisualViewModel(VisualOptions visualOptions, RomanBlindViewModel element)
            : base(visualOptions, element)
        {
            Element
                .SetPropertyChanged(nameof(Element.WavySurface), () => OnPropertyChanged(nameof(WavySurface)))
                .SetPropertyChanged(nameof(Element.Points), () => OnPropertyChanged(nameof(Points)))
                .SetPropertyChanged(nameof(Element.Circuit), () => OnPropertyChanged(nameof(Circuit)));
        }

        public new RomanBlindViewModel Element => (RomanBlindViewModel)base.Element;
        public WavySurfaceVisualViewModel WavySurface => new WavySurfaceVisualViewModel(VisualOptions, Element.WavySurface);

        public IEnumerable<Point> Points
        {
            get
            {
                var pixelPerUnit = VisualOptions.PixelPerUnit;
                var points = pixelPerUnit.Transform(Element.Points);
                return points;
            }
        }
        public IEnumerable<Point> Circuit
        {
            get
            {
                var pixelPerUnit = VisualOptions.PixelPerUnit;
                var points = pixelPerUnit.Transform(Element.Circuit);
                return points;
            }
        }

    }
}


