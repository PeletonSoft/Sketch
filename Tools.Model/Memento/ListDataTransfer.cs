using System;
using System.Collections.Generic;

namespace PeletonSoft.Tools.Model.Memento
{
    [Serializable]
    public sealed class ListDataTransfer<T> : IListDataTransfer<T>
        where T : IDataTransfer
    {
        public IList<TypeContentDataTransfer<T>> List { get; } = 
            new List<TypeContentDataTransfer<T>>();
    }
}
