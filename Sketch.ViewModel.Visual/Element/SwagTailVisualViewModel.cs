using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Interface.Visual;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Primitive;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element
{
    public sealed class SwagTailVisualViewModel : ElementVisualViewModel, IElementVisualViewModel<SwagTailViewModel>
    {
        private void OnPropertyChanged<T>(Expression<Func<SwagTailVisualViewModel, T>> expression)
        {
            expression.OnPropertyChanged(OnPropertyChanged);
        }

        public SwagTailVisualViewModel(VisualOptions visualOptions, SwagTailViewModel element)
            : base(visualOptions, element)
        {
            Element
                .SetPropertyChanged(el => el.WavySurface, () => OnPropertyChanged(v => v.WavySurface))
                .SetPropertyChanged(el => el.Circuit, () => OnPropertyChanged(v => v.Circuit));
        }

        public new SwagTailViewModel Element
        {
            get { return (SwagTailViewModel) base.Element; }
        }

        public IEnumerable<Point> Circuit
        {
            get
            {
                var pixelPerUnit = VisualOptions.PixelPerUnit;
                return pixelPerUnit.Transform(Element.Circuit);
            }
        }

        public WavySurfaceVisualViewModel WavySurface
        {
            get { return new WavySurfaceVisualViewModel(VisualOptions, Element.WavySurface); }
        }

    }
}
