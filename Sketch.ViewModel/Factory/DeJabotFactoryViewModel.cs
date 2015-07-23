using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;

namespace PeletonSoft.Sketch.ViewModel.Factory
{
    public sealed class DeJabotFactoryViewModel : ElementFactoryViewModel,
        IElementFactoryViewModel<DeJabotViewModel>
    {
        protected override IElementViewModel CreateRawElement(IWorkspaceBit workspaceBit)
        {
            return new DeJabotViewModel(workspaceBit, new DeJabot());
        }

        public new DeJabotViewModel CreateElement(IWorkspaceBit workspaceBit)
        {
            return (DeJabotViewModel)base.CreateElement(workspaceBit);
        }
    }
}
