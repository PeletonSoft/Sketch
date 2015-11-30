using System.Collections.Generic;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Container;
using PeletonSoft.Sketch.ViewModel.WorkMode;
using PeletonSoft.Tools.Model.Collection;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.Memento.Container;

namespace PeletonSoft.Sketch.ViewModel.Container
{
    public class WorkModeListViewModel : IWorkModeListViewModel
    {

        public IEnumerable<ContainerRecord<IWorkModeViewModel>> Items { get; }
        public IWorkModeViewModel Default => Editor;

        IEnumerable<IContainerRecord<IOriginator<IWorkModeDataTransfer>>>
            IContainer<IOriginator<IWorkModeDataTransfer>>.Items => Items;

        IOriginator<IWorkModeDataTransfer>
            IContainer<IOriginator<IWorkModeDataTransfer>>.Default => Default;

        public WorkModeListViewModel(WorkspaceViewModel workspace)
        {
            Editor = new EditorWorkModeViewModel(workspace);
            Report = new ReportWorkModeViewModel(workspace);

            Items = new[]
            {
                new ContainerRecord<IWorkModeViewModel>("Editor", Editor),
                new ContainerRecord<IWorkModeViewModel>("Report", Report)
            };
        }

        public IWorkModeViewModel Editor { get; }
        public IWorkModeViewModel Report { get; }
        public IListDataTransfer<IWorkModeDataTransfer> CreateState() => new ListDataTransfer<IWorkModeDataTransfer>();

        public void Save(IListDataTransfer<IWorkModeDataTransfer> state)
        {
            this.PlainSave(state);
        }

        public void Restore(IListDataTransfer<IWorkModeDataTransfer> state)
        {
            this.ZipRestore(state);
        }
    }
}
