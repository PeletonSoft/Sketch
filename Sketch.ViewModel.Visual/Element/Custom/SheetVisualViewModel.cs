using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Interface.Visual;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element.Custom
{
    public abstract class SheetVisualViewModel : ElementVisualViewModel, IElementVisualViewModel<SheetViewModel>
    {
        protected SheetVisualViewModel(VisualOptions visualOptions, SheetViewModel element) 
            : base(visualOptions, element)
        {
        }

        public new SheetViewModel Element
        {
            get { return (SheetViewModel) base.Element; }
        }
    }
}
