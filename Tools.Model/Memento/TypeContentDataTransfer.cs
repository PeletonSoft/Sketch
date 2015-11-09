namespace PeletonSoft.Tools.Model.Memento
{
    public class TypeContentDataTransfer<T> where T : IDataTransfer
    {
        public string Type { get; set; }
        public T Content { get; set; }
    }
}
