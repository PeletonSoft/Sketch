namespace PeletonSoft.Tools.Model.Memento
{
    public class TypeContentDataTransfer<T> : IDataTransfer
        where T : IDataTransfer
    {
        public string Type { get; set; }
        public string DataTransferType { get; set; }
        public T Content { get; set; }
    }
}
