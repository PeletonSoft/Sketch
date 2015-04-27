using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Custom;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element
{
    public class PortiereVisualViewModel : SheetVisualViewModel
    {
        public PortiereVisualViewModel(VisualOptions visualOptions, PortiereViewModel element) 
            : base(visualOptions, element)
        {
        }

        private new PortiereViewModel Element
        {
            get
            {
                return (PortiereViewModel)base.Element;
            }
        }
    }
}
