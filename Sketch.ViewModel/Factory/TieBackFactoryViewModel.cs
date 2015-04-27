using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;

namespace PeletonSoft.Sketch.ViewModel.Factory
{
    public sealed class TieBackFactoryViewModel : CustomElementFactoryViewModel,
        IElementFactoryViewModel<TieBackViewModel>
    {
        public int PointCount { get; set; }
        protected override IElementViewModel CreateRawElement(IWorkspaceBit workspaceBit)
        {
            return new TieBackViewModel(workspaceBit, PointCount); 
        }

        public new TieBackViewModel CreateElement(IWorkspaceBit workspaceBit)
        {
            return (TieBackViewModel)base.CreateElement(workspaceBit);
        }
    }
}
