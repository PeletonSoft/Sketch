using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;

namespace PeletonSoft.Sketch.ViewModel.Factory
{
    public sealed class TieBackFactoryViewModel : WavySurfaceFactoryViewModel,
        IElementFactoryViewModel<TieBackViewModel>
    {
        protected override IElementViewModel CreateRawElement(IWorkspaceBit workspaceBit)
        {
            return new TieBackViewModel(workspaceBit, new TieBack(PointCount)); 
        }

        public new TieBackViewModel CreateElement(IWorkspaceBit workspaceBit)
        {
            return (TieBackViewModel)base.CreateElement(workspaceBit);
        }
    }
}
