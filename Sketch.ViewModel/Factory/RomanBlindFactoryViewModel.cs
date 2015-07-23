using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;

namespace PeletonSoft.Sketch.ViewModel.Factory
{
    public class RomanBlindFactoryViewModel: ElementFactoryViewModel, 
        IElementFactoryViewModel<RomanBlindViewModel>
    {
        protected override IElementViewModel CreateRawElement(IWorkspaceBit workspaceBit)
        {
            return new RomanBlindViewModel(workspaceBit, new RomanBlind()); 
        }

        public new RomanBlindViewModel CreateElement(IWorkspaceBit workspaceBit)
        {
            return (RomanBlindViewModel)base.CreateElement(workspaceBit);   
        }
    }
}
