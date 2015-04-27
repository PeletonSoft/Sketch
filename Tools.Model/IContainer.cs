using System.Collections.Generic;

namespace PeletonSoft.Tools.Model
{
    public interface IContainer<out T>
    {
        IEnumerable<T> Items { get; } 
    }
}
