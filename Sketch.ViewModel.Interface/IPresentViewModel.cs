using System.ComponentModel;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Interface
{
    public interface IPresentViewModel : INotifyPropertyChanged, IOriginator<IPresentDataTransfer>
    {
        IWorkspaceViewModel Workspace { get; }
        double Zoom { get; set; }
    }
}
