using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;

namespace PeletonSoft.Sketch.ViewModel.Factory
{
    public sealed class FilletFactoryViewModel : ElementFactoryViewModel,
        IElementFactoryViewModel<FilletViewModel>
    {

        protected override IElementViewModel CreateRawElement(IWorkspaceBit workspaceBit)
        {
            return new FilletViewModel(workspaceBit, new Fillet());
        }

        public new FilletViewModel CreateElement(IWorkspaceBit workspaceBit)
        {
            return (FilletViewModel)base.CreateElement(workspaceBit);
        }
    }
}
