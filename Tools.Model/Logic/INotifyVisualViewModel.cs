using System.ComponentModel;

namespace PeletonSoft.Tools.Model.Logic
{
    public interface INotifyVisualViewModel<out T> : IVisualViewModel<T>, INotifyPropertyChanged
        where T : INotifyViewModel
    {
    }
}
