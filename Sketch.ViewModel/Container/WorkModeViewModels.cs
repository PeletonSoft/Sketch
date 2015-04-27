using System.Collections.Generic;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.WorkMode;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Container
{
    public class WorkModeViewModels : IContainerOriginator<IWorkModeViewModel>
    {
        public void RestoreDefault()
        {
        }

        public IEnumerable<IWorkModeViewModel> Items {
            get
            {
                return new[]
                {
                    Editor,
                    Report
                };
            }
        }

        public WorkModeViewModels(WorkspaceViewModel workspace)
        {
            Editor = new EditorWorkModeViewModel(workspace);
            Report = new ReportWorkModeViewModel(workspace);
        }

        public IWorkModeViewModel Editor { get; private set; }
        public IWorkModeViewModel Report { get; private set; }
    }
}
