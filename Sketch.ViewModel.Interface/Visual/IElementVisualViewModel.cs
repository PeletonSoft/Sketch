using System.ComponentModel;

namespace PeletonSoft.Sketch.ViewModel.Interface.Visual
{
    public interface IElementVisualViewModel : INotifyPropertyChanged
    {
        ILayoutVisualViewModel Layout { get; }
        bool Visibility { get; }
        double Opacity { get; }
    
    }
}
