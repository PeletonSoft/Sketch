using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;

namespace PeletonSoft.Sketch.ViewModel.Factory
{
    public sealed class TulleFactoryViewModel : ElementFactoryViewModel,
        IElementFactoryViewModel<TulleViewModel>
    {
        protected override IElementViewModel CreateRawElement(IWorkspaceBit workspaceBit)
        {
            return new TulleViewModel(workspaceBit, new Tulle());
        }

        public new TulleViewModel CreateElement(IWorkspaceBit workspaceBit)
        {
            return (TulleViewModel) base.CreateElement(workspaceBit);
        }
    }
}
