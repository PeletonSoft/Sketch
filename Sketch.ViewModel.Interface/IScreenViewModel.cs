using System.ComponentModel;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Interface
{
    public interface IScreenViewModel : IOriginator, INotifyPropertyChanged
    {
        double Width { get; set; }
        double Height { get; set; }

    }
}