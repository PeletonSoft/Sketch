using System.Collections.Generic;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Interface.Visual;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Primitive;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element
{
    public sealed class SwagTailVisualViewModel : ElementVisualViewModel, IElementVisualViewModel<SwagTailViewModel>
    {
        public SwagTailVisualViewModel(VisualOptions visualOptions, SwagTailViewModel element)
            : base(visualOptions, element)
        {
            Element
                .SetPropertyChanged(nameof(Element.WavySurface), () => OnPropertyChanged(nameof(WavySurface)))
                .SetPropertyChanged(nameof(Element.Circuit), () => OnPropertyChanged(nameof(Circuit)));
        }

        public new SwagTailViewModel Element => (SwagTailViewModel) base.Element;

        public IEnumerable<Point> Circuit
        {
            get
            {
                var pixelPerUnit = VisualOptions.PixelPerUnit;
                return pixelPerUnit.Transform(Element.Circuit);
            }
        }

        public WavySurfaceVisualViewModel WavySurface => new WavySurfaceVisualViewModel(VisualOptions, Element.WavySurface);
    }
}
