using System.ComponentModel;

namespace PeletonSoft.Sketch.ViewModel.Interface.Draw
{
    public interface IDrawViewModel : INotifyPropertyChanged
    {
        double X { get; set; }
        double Y { get; set; }
    }

}
