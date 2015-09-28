using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;

namespace PeletonSoft.Sketch.ViewModel.Factory
{
    public sealed class PleatFactoryViewModel : ElementFactoryViewModel, IElementFactoryViewModel<PleatViewModel>
    {
        protected override IElementViewModel CreateRawElement(IWorkspaceBit workspaceBit)
        {
            return new PleatViewModel(workspaceBit, new Pleat());
        }

        public new PleatViewModel CreateElement(IWorkspaceBit workspaceBit)
        {
            return (PleatViewModel)base.CreateElement(workspaceBit);
        }
    }
}
