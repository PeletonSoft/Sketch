using System.Collections.Generic;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Visual;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Custom;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element
{
    public sealed class TieBackVisualViewModel : PleatableVisualViewModel, IElementVisualViewModel<TieBackViewModel>
    {
        public TieBackVisualViewModel(VisualOptions visualOptions, TieBackViewModel element)
            : base(visualOptions, element)
        {
            Element
                .SetPropertyChanged(nameof(Element.Lane), () => OnPropertyChanged(nameof(Lane)));
        }

        public new TieBackViewModel Element => (TieBackViewModel) base.Element;

        public IEnumerable<Point> Lane => VisualOptions.PixelPerUnit.Transform(Element.Lane);
    }
}
