using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;

namespace PeletonSoft.Sketch.ViewModel.Factory
{
    public sealed class OverlayFactoryViewModel : CustomElementFactoryViewModel,
        IElementFactoryViewModel<OverlayViewModel>
    {
        protected override IElementViewModel CreateRawElement(IWorkspaceBit workspaceBit)
        {
            return new OverlayViewModel(workspaceBit);            
        }

        public new OverlayViewModel CreateElement(IWorkspaceBit workspaceBit)
        {
            return (OverlayViewModel)base.CreateElement(workspaceBit);
        }
    }
}
