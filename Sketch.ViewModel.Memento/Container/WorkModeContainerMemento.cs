using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Memento.Service;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.Memento.Container;

namespace PeletonSoft.Sketch.ViewModel.Memento.Container
{
    public class WorkModeContainerMemento : ContainerMemento<IWorkModeViewModel>
    {
        public WorkModeContainerMemento() :
            base(new WorkModeMementoService(), "WorkMode")
        {
        }
    }
}
