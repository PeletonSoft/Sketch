using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Present;
using PeletonSoft.Sketch.ViewModel.Interface;

namespace PeletonSoft.Sketch.ViewModel.Present
{
    public class LayoutPresentViewModel : CustomPresentViewModel
    {
        public LayoutPresentViewModel(IWorkspaceViewModel workspace) : base(workspace)
        {

        }

        public override IPresentDataTransfer CreateState() => new LayoutPresentDataTransfer();
    }
}
