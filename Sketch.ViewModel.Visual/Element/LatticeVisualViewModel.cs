using System.Collections.Generic;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Visual;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Custom;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element
{
    public sealed class LatticeVisualViewModel : ElementVisualViewModel, IElementVisualViewModel<LatticeViewModel>
    {
        public LatticeVisualViewModel(VisualOptions visualOptions, LatticeViewModel element)
            : base(visualOptions, element)
        {
            Element.SetPropertyChanged(nameof(Element.Lines), () => OnPropertyChanged(nameof(Lines)));
        }

        public new LatticeViewModel Element => (LatticeViewModel) base.Element;

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
