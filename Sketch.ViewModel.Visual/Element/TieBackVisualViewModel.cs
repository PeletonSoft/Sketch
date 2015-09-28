using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Visual;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Custom;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element
{
    public sealed class TieBackVisualViewModel : PleatableVisualViewModel, IElementVisualViewModel<TieBackViewModel>
    {
        private void OnPropertyChanged<T>(Expression<Func<TieBackVisualViewModel, T>> expression)
        {
            expression.OnPropertyChanged(OnPropertyChanged);
        }

        public TieBackVisualViewModel(VisualOptions visualOptions, TieBackViewModel element)
            : base(visualOptions, element)
        {
            Element
                .SetPropertyChanged(el => el.Lane, () => OnPropertyChanged(v => v.Lane));
        }

        public new TieBackViewModel Element
        {
            get { return (TieBackViewModel) base.Element; }
        }

        public IEnumerable<Point> Lane
        {
            get { return VisualOptions.PixelPerUnit.Transform(Element.Lane); }
        }


    }
}
