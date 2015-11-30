using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Sketch.ViewModel.DataTransfer.WorkMode;
using PeletonSoft.Sketch.ViewModel.Interface;

namespace PeletonSoft.Sketch.ViewModel.WorkMode
{
    public class ReportWorkModeViewModel : CustomWorkModeViewModel
    {
        public ReportWorkModeViewModel(IWorkspaceViewModel workspace) : base(workspace)
        {
        }

        public override IWorkModeDataTransfer CreateState() => new ReportWorkModeDataTransfer();
    }
}
