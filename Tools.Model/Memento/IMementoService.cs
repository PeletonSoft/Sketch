using System;
using System.Collections.Generic;

namespace PeletonSoft.Tools.Model.Memento
{
    public interface IMementoService<T> where T : IOriginator
    {
        IDictionary<Type, Func<IMemento<T>>> Items { get; }
        void Register(Type type, Func<IMemento<T>> creator);
    }
}
