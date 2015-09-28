using System;

namespace PeletonSoft.Tools.Model.Collection
{
    public class ContainerRecord<T> : IContainerRecord<T>
    {
        public string Key { get; }
        public Type Type { get; }
        public T Value { get; }

        public ContainerRecord(string key, Type type, T value)
        {
            Key = key;
            Type = type;
            Value = value;
        }

        public ContainerRecord(Enum key, Type type, T value) : this(key.ToString(), type, value)
        {
        }
    }
}
