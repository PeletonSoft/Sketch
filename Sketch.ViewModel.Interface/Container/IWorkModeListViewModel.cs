using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Tools.Model.Memento.Container;

namespace PeletonSoft.Sketch.ViewModel.Interface.Container
{
    public interface IWorkModeListViewModel : IListOriginator<IWorkModeDataTransfer>
    {
        new IWorkModeViewModel Default { get; }
    }
}
