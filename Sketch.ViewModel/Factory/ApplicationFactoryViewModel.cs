using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;

namespace PeletonSoft.Sketch.ViewModel.Factory
{
    public sealed class ApplicationFactoryViewModel : CustomElementFactoryViewModel, 
        IElementFactoryViewModel<ApplicationViewModel>
    {
        protected override IElementViewModel CreateRawElement(IWorkspaceBit workspaceBit)
        {
            return new ApplicationViewModel(workspaceBit, new Application()); 
        }

        public new ApplicationViewModel CreateElement(IWorkspaceBit workspaceBit)
        {
            return (ApplicationViewModel)base.CreateElement(workspaceBit);
        }
    }
}
