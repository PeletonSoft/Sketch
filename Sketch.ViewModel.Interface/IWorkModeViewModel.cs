using System.ComponentModel;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Interface
{
    public interface IWorkModeViewModel : INotifyPropertyChanged, IOriginator<IWorkModeDataTransfer>
    {
        IWorkspaceViewModel Workspace { get; }
    }
}
