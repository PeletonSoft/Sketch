using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Sketch.ViewModel.DataTransfer.WorkMode;
using PeletonSoft.Sketch.ViewModel.Interface;
using static PeletonSoft.Tools.Model.ObjectEvent.EventAction;

namespace PeletonSoft.Sketch.ViewModel.WorkMode
{
    public class EditorWorkModeViewModel : CustomWorkModeViewModel
    {
        public EditorWorkModeViewModel(IWorkspaceViewModel workspace) : base(workspace)
        {
        }

        public override IWorkModeDataTransfer CreateState() => new EditorWorkModeDataTransfer();
    }
}
