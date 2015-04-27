using System.ComponentModel;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Interface
{
    public interface IWorkModeViewModel : IOriginator, INotifyPropertyChanged
    {
        IWorkspaceViewModel Workspace { get; }
    }
}
