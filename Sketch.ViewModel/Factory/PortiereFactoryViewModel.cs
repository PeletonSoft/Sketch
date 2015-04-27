using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;

namespace PeletonSoft.Sketch.ViewModel.Factory
{
    public sealed class PortiereFactoryViewModel : CustomElementFactoryViewModel,
        IElementFactoryViewModel<PortiereViewModel>
    {
        protected override IElementViewModel CreateRawElement(IWorkspaceBit workspaceBit)
        {
            return new PortiereViewModel(workspaceBit);
        }

        public new PortiereViewModel CreateElement(IWorkspaceBit workspaceBit)
        {
            return (PortiereViewModel)base.CreateElement(workspaceBit);
        }
    }
}
