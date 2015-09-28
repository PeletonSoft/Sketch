using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Visual;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Custom;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element
{
    public sealed class PanelVisualViewModel : ElementVisualViewModel, IElementVisualViewModel<PanelViewModel>
    {
        public PanelVisualViewModel(VisualOptions visualOptions, PanelViewModel element)
            : base(visualOptions, element)
        {
        }

        public new PanelViewModel Element
        {
            get { return (PanelViewModel) base.Element; }
        }
    }
}
