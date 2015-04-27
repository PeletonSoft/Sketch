using System.ComponentModel;

namespace PeletonSoft.Sketch.ViewModel.Interface.Visual
{
    public interface IScreenVisualViewModel : INotifyPropertyChanged
    {
        double Width { get; }
        double Height { get; }
    }
}