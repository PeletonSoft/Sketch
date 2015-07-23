using PeletonSoft.Sketch.ViewModel.Element.Custom;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element.Custom
{
    public abstract class SheetVisualViewModel : ElementVisualViewModel
    {
        protected SheetVisualViewModel(VisualOptions visualOptions, SheetViewModel element) 
            : base(visualOptions, element)
        {
        }

        private new SheetViewModel Element
        {
            get { return (SheetViewModel) base.Element; }
        }
    }
}
