using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;

namespace PeletonSoft.Sketch.ViewModel.Factory
{
    public sealed class ScanFactoryViewModel : ElementFactoryViewModel,
        IElementFactoryViewModel<ScanViewModel>
    {
        protected override IElementViewModel CreateRawElement(IWorkspaceBit workspaceBit)
        {
            return new ScanViewModel(workspaceBit, new Scan());
        }

        public new ScanViewModel CreateElement(IWorkspaceBit workspaceBit)
        {
            return (ScanViewModel) base.CreateElement(workspaceBit);
        }
    }
}
