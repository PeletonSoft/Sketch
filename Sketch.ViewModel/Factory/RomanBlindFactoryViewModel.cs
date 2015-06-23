using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;

namespace PeletonSoft.Sketch.ViewModel.Factory
{
    public class RomanBlindFactoryViewModel: CustomElementFactoryViewModel, 
        IElementFactoryViewModel<RomanBlindViewModel>
    {
        protected override IElementViewModel CreateRawElement(IWorkspaceBit workspaceBit)
        {
            return new RomanBlindViewModel(workspaceBit); 
        }

        public new RomanBlindViewModel CreateElement(IWorkspaceBit workspaceBit)
        {
            return (RomanBlindViewModel)base.CreateElement(workspaceBit);   
        }
    }
}
