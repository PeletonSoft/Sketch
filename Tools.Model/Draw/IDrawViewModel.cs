using System.ComponentModel;

namespace PeletonSoft.Tools.Model.Draw
{
    public interface IDrawViewModel : INotifyPropertyChanged
    {
        double X { get; set; }
        double Y { get; set; }
    }

}
