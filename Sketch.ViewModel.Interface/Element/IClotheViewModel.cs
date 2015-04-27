using System.ComponentModel;
using System.Windows.Input;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Interface.Element
{
    public interface IClotheViewModel : INotifyPropertyChanged, IOriginator
    {
        double? Height { get; set; }
        double? Width { get; set; }
        double? Area { get; }
        ICommand CalculateCommand { get; }
    }
}