using System;

namespace PeletonSoft.Tools.Model.Collection
{
    public interface IContainerRecord<out T>
    {
        string Key { get; }
        T Value { get; }
    }
}