using System.Collections.Generic;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.WorkMode;
using PeletonSoft.Tools.Model.Collection;
using PeletonSoft.Tools.Model.Memento.Container;

namespace PeletonSoft.Sketch.ViewModel.Container
{
    public class WorkModeViewModels : IContainerOriginator<IWorkModeViewModel>
    {
        public void RestoreDefault()
        {
        }

        public IEnumerable<IContainerRecord<IWorkModeViewModel>> Items
        {
            get
            {
                return new[]
                {
                    new ContainerRecord<IWorkModeViewModel>("Editor",
                        typeof (EditorWorkModeViewModel), Editor),
                    new ContainerRecord<IWorkModeViewModel>("Report",
                        typeof (ReportWorkModeViewModel), Report)
                };
            }
        }

        public IWorkModeViewModel Default
        {
            get { return Editor; }
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
