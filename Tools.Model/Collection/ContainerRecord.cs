using System;

namespace PeletonSoft.Tools.Model.Collection
{
    public class ContainerRecord<T> : IContainerRecord<T>
    {
        public string Key { get; }
        public T Value { get; }

        public ContainerRecord(string key, T value)
        {
            Key = key;
            Value = value;
        }

        public ContainerRecord(Enum key, T value) : this(key.ToString(), value)
        {
        }
    }
}
