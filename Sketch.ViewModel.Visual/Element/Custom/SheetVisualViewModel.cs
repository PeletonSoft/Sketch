using PeletonSoft.Sketch.ViewModel.Element.Custom;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element.Custom
{
    public class SheetVisualViewModel : ElementVisualViewModel
    {
        public SheetVisualViewModel(VisualOptions visualOptions, SheetViewModel element) 
            : base(visualOptions, element)
        {
        }

        private new SheetViewModel Element
        {
            get { return (SheetViewModel) base.Element; }
        }
    }
}
