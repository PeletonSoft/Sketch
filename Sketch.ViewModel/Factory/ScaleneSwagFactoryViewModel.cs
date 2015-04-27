using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;

namespace PeletonSoft.Sketch.ViewModel.Factory
{
    public sealed class ScaleneSwagFactoryViewModel : SwagTailFactoryViewModel,
        IElementFactoryViewModel<ScaleneSwagViewModel>
    {
        protected override IElementViewModel CreateRawElement(IWorkspaceBit workspaceBit)
        {
            return new ScaleneSwagViewModel(workspaceBit);
        }

        public new ScaleneSwagViewModel CreateElement(IWorkspaceBit workspaceBit)
        {
            return (ScaleneSwagViewModel) base.CreateElement(workspaceBit);
        }
    }
}
