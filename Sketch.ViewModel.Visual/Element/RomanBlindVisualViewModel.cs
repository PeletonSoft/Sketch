using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Visual;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Primitive;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element
{
    public sealed class RomanBlindVisualViewModel : ElementVisualViewModel, IElementVisualViewModel<RomanBlindViewModel>
    {
        private void OnPropertyChanged<T>(Expression<Func<RomanBlindVisualViewModel, T>> expression)
        {
            expression.OnPropertyChanged(OnPropertyChanged);
        }
        public RomanBlindVisualViewModel(VisualOptions visualOptions, RomanBlindViewModel element)
            : base(visualOptions, element)
        {
            Element
                .SetPropertyChanged(el => el.WavySurface, () => OnPropertyChanged(v => v.WavySurface))
                .SetPropertyChanged(el => el.Points, () => OnPropertyChanged(v => v.Points))
                .SetPropertyChanged(el => el.Circuit, () => OnPropertyChanged(v => v.Circuit));
        }

        public new RomanBlindViewModel Element
        {
            get { return (RomanBlindViewModel)base.Element; }
        }

        public WavySurfaceVisualViewModel WavySurface
        {
            get { return new WavySurfaceVisualViewModel(VisualOptions, Element.WavySurface); }
        }

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


