using System.ComponentModel;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Geometry;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Interface.Element
{
    public interface ISuperimposeOptionViewModel : INotifyPropertyChanged,
        IOriginator<SuperimposeOptionDataTransfer>
    {
        double ForegroundOpacity { get; set; }
        double BackgroundOpacity { get; set; }
        double MarkerOpacity { get; set; }
        double MarkerRadius { get; set; }
    }
}
