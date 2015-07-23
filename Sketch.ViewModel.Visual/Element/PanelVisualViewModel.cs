using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Custom;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element
{
    public class PanelVisualViewModel : ElementVisualViewModel
    {
        public PanelVisualViewModel(VisualOptions visualOptions, PanelViewModel element)
            : base(visualOptions, element)
        {
        }

        private new PanelViewModel Element
        {
            get { return (PanelViewModel) base.Element; }
        }
    }
}
