using System;
using System.Linq;

namespace PeletonSoft.Tools.Model.Collection
{
    public static class ContainerHelper
    {
        public static string GetKeyByValue<T>(this IContainer<T> container, T value)
        {
            var list = container.Items
                .Where(r => r.Value.Equals(value))
                .ToList();
            return list.Any() ? list.First().Key : null;
        }

        public static T GetValueByKeyOrDefault<T>(this IContainer<T> container, string key)
        {
            var list = container.Items
                .Where(r => r.Key == key)
                .ToList();
            if (list.Any())
            {
                return list.First().Value;
            }
            if (container.Items.Any())
            {
                return container.Items.First().Value;
            }
            return default(T);
        }

        public static T GetValueByKeyOrDefault<T>(this IContainer<T> container, Enum key)
        {
            return container.GetValueByKeyOrDefault(key.ToString());
        }


        public static T GetValueByKey<T>(this IContainer<T> container, string key)
        {
            var list = container.Items
                .Where(r => r.Key == key)
                .ToList();
            return list.Any() ? list.First().Value : default(T);
        }

        public static T GetValueByKey<T>(this IContainer<T> container, Enum key)
        {
            return container.GetValueByKey(key.ToString());
        }

    }
}
