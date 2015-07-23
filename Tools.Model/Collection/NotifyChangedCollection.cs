using System.Collections.ObjectModel;

namespace PeletonSoft.Tools.Model.Collection
{
    public class NotifyChangedCollection<T> : ObservableCollection<T>, INotifyChangedReadOnlyCollection<T>
    {
    }
}
