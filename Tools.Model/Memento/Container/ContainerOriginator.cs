using System;
using System.Collections.Generic;
using System.Linq;
using PeletonSoft.Tools.Model.Collection;

namespace PeletonSoft.Tools.Model.Memento.Container
{
    public sealed class ContainerOriginator<T> : IContainerOriginator<T>
    {
        public IEnumerable<IContainerRecord<T>> Items { get; }
        public T Default { get; }

        public ContainerOriginator(IEnumerable<IContainerRecord<T>> items)
        {
            Items = items;
            var first = Items.FirstOrDefault();

            if (first != null)
            {
                Default = first.Value;
            }
        }

        public ContainerOriginator(IEnumerable<IContainerRecord<T>> items, 
            Func<IEnumerable<IContainerRecord<T>>, T> getDefaultFunc)
        {
            Items = items;
            Default = getDefaultFunc(Items);
        }

    }
}
