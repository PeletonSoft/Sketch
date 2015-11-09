namespace PeletonSoft.Tools.Model.Memento
{
    public static class TypeContentDataTransferHelper
    {
        public static TypeContentDataTransfer<T> GetDataTransfer<T>(this IOriginator<T> item) 
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
