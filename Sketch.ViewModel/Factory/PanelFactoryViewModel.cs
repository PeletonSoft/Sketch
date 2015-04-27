using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;

namespace PeletonSoft.Sketch.ViewModel.Factory
{
    public sealed class PanelFactoryViewModel : CustomElementFactoryViewModel,
            IElementFactoryViewModel<PanelViewModel>
    {
        protected override IElementViewModel CreateRawElement(IWorkspaceBit workspaceBit)
        {
            return new PanelViewModel(workspaceBit);
        }

        public new PanelViewModel CreateElement(IWorkspaceBit workspaceBit)
        {
            return (PanelViewModel) base.CreateElement(workspaceBit);
        }
    }
}
