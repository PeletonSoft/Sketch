using System.Collections.Generic;

namespace PeletonSoft.Tools.Model.Collection
{
    public interface IContainer<out T>
    {
        IEnumerable<IContainerRecord<T>> Items { get; }
        T Default { get; }
    }
}
