using System.ComponentModel;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Interface
{
    public interface IPresentViewModel : IOriginator, INotifyPropertyChanged
    {
        IWorkspaceViewModel Workspace { get; }

        double Zoom { get; set; }
    }
}
