using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Primitive;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element
{
    public class TieBackVisualViewModel : ElementVisualViewModel
    {
        private void OnPropertyChanged<T>(Expression<Func<TieBackVisualViewModel, T>> expression)
        {
            expression.OnPropertyChanged(OnPropertyChanged);
        }

        public TieBackVisualViewModel(VisualOptions visualOptions, TieBackViewModel element)
            : base(visualOptions, element)
        {
            Element
                .SetPropertyChanged(el => el.Lane, () => OnPropertyChanged(v => v.Lane))
                .SetPropertyChanged(el => el.WavySurface, () => OnPropertyChanged(v => v.WavySurface));
        }

        private new TieBackViewModel Element
        {
            get { return (TieBackViewModel) base.Element; }
        }

        public IEnumerable<Point> Lane
        {
            get { return VisualOptions.PixelPerUnit.Transform(Element.Lane); }
        }

        public WavySurfaceVisualViewModel WavySurface
        {
            get { return new WavySurfaceVisualViewModel(VisualOptions, Element.WavySurface); }
        }

    }
}
