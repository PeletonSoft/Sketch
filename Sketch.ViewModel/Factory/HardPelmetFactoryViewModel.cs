using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;

namespace PeletonSoft.Sketch.ViewModel.Factory
{
    public sealed class HardPelmetFactoryViewModel : ElementFactoryViewModel,
        IElementFactoryViewModel<HardPelmetViewModel>
    {
        protected override IElementViewModel CreateRawElement(IWorkspaceBit workspaceBit)
        {
            return new HardPelmetViewModel(workspaceBit, new HardPelmet());
        }

        public new HardPelmetViewModel CreateElement(IWorkspaceBit workspaceBit)
        {
            return (HardPelmetViewModel)base.CreateElement(workspaceBit);
        }


    }
}
