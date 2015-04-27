using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;

namespace PeletonSoft.Sketch.ViewModel.Factory
{
    public sealed class TulleFactoryViewModel : CustomElementFactoryViewModel,
        IElementFactoryViewModel<TulleViewModel>
    {
        protected override IElementViewModel CreateRawElement(IWorkspaceBit workspaceBit)
        {
            return new TulleViewModel(workspaceBit);
        }

        public new TulleViewModel CreateElement(IWorkspaceBit workspaceBit)
        {
            return (TulleViewModel) base.CreateElement(workspaceBit);
        }
    }
}
