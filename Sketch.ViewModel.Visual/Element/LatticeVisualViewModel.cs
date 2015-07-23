using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Custom;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element
{
    public class LatticeVisualViewModel : ElementVisualViewModel
    {
        private void OnPropertyChanged<T>(Expression<Func<LatticeVisualViewModel, T>> expression)
        {
            expression.OnPropertyChanged(OnPropertyChanged);
        }

        public LatticeVisualViewModel(VisualOptions visualOptions, LatticeViewModel element)
            : base(visualOptions, element)
        {
            Element.SetPropertyChanged(el => el.Lines, () => OnPropertyChanged(v => v.Lines));
        }

        private new LatticeViewModel Element
        {
            get { return (LatticeViewModel) base.Element; }
        }

        public IEnumerable<Rect> Lines
        {
            get
            {
                var pixelPerUnit = VisualOptions.PixelPerUnit;
                var lines = pixelPerUnit.Transform(Element.Lines);
                return lines;
            }
        }
    }
}
