using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Custom;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element
{
    public class TulleVisualViewModel : SheetVisualViewModel
    {
        public TulleVisualViewModel(VisualOptions visualOptions, TulleViewModel element)
            : base(visualOptions, element)
        {
        }

        private new TulleViewModel Element
        {
            get { return (TulleViewModel) base.Element; }
        }
    }
}
