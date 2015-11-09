namespace PeletonSoft.Tools.Model.Memento
{
    public class TypeContentDataTransferHelper
    {
        public TypeContentDataTransfer<T> GetDataTransfer<T>(IOriginator<T> item) 
            where T : IDataTransfer
        {
            return new TypeContentDataTransfer<T>
            {
                Type = item.GetType().ToString(),
                Content = item.Save()
            };
        }
    }
}
