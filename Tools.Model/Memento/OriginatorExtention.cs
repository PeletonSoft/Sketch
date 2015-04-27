using System.Collections.Generic;
using System.Linq;

namespace PeletonSoft.Tools.Model.Memento
{
    public static class OriginatorExtention
    {
        public static T SetByTypeName<T>(this string senderTypeName, IEnumerable<object> list)
        {
            var find = list.FirstOrDefault(x => x.GetType().Name == senderTypeName);
            return (T) find;
        }

        public static T SetByTypeName<T>(this string senderTypeName, IContainer<object> container)
        {
            return senderTypeName.SetByTypeName<T>(container.Items);
        }

        public static string GetTypeName(this object sender)
        {
            return sender.GetType().Name;
        }
    }
}
