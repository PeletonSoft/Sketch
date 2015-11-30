using System.ComponentModel;
using System.Windows.Input;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Interface.Element
{
    public interface IClotheViewModel : INotifyPropertyChanged, IOriginator<IClotheDataTransfer>
    {
        double? Height { get; set; }
        double? Width { get; set; }
        double? Area { get; }
        ICommand CalculateCommand { get; }
    }
}