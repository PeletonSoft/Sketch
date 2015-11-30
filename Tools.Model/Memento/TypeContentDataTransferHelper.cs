using System;
using System.Reflection;

namespace PeletonSoft.Tools.Model.Memento
{
    public static class TypeContentDataTransferHelper
    {
        public static TypeContentDataTransfer<T> GetDataTransfer<T>(this IOriginator<T> item)
            where T : class, IDataTransfer
        {
            var content = item.Save();
            return
                new TypeContentDataTransfer<T>
                {
                    Type = item.GetType().Name,
                    DataTransferType = content.GetType().Name,
                    Content = content
                };
        }

        public static bool IsListDataTransfer(this Type type) =>
            type.IsGenericType &&
            type.GetGenericTypeDefinition() == typeof (IListDataTransfer<>);
    }
}
