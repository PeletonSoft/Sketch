using System.Collections.Generic;
using System.Collections.Specialized;

namespace PeletonSoft.Tools.Model.Collection
{
    public interface INotifyChangedReadOnlyCollection<out T> : IReadOnlyCollection<T>, INotifyCollectionChanged
    {
    }
}
