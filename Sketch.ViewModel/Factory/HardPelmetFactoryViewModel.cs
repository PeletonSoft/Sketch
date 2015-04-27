using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;

namespace PeletonSoft.Sketch.ViewModel.Factory
{
    public sealed class HardPelmetFactoryViewModel : CustomElementFactoryViewModel,
        IElementFactoryViewModel<HardPelmetViewModel>
    {
        protected override IElementViewModel CreateRawElement(IWorkspaceBit workspaceBit)
        {
            return new HardPelmetViewModel(workspaceBit);
        }

        public new HardPelmetViewModel CreateElement(IWorkspaceBit workspaceBit)
        {
            return (HardPelmetViewModel)base.CreateElement(workspaceBit);
        }


    }
}
