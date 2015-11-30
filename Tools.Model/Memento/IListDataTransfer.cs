using System.Collections.Generic;

namespace PeletonSoft.Tools.Model.Memento
{
    public interface IListDataTransfer<T> : IDataTransfer
        where T : IDataTransfer
    {
        IList<TypeContentDataTransfer<T>> List { get; }
    }
}
