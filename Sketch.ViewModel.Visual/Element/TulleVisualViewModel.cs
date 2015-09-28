using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Visual;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Custom;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element
{
    public sealed class TulleVisualViewModel : SheetVisualViewModel, IElementVisualViewModel<TulleViewModel>
    {
        public TulleVisualViewModel(VisualOptions visualOptions, TulleViewModel element)
            : base(visualOptions, element)
        {
        }

        public new TulleViewModel Element
        {
            get { return (TulleViewModel) base.Element; }
        }
    }
}
