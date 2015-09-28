using System.ComponentModel;

namespace PeletonSoft.Tools.Model.Logic
{
    public interface INotifyViewModel : IViewModel, INotifyPropertyChanged
    {
    }
    public interface INotifyViewModel<out T> : INotifyViewModel, IViewModel<T>
    {
    }
}
