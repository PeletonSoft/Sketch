using System;
using System.Collections.Generic;
using System.Linq;

namespace PeletonSoft.Tools.Model.MetaData
{
    public static class TypeHelper
    {
        public static IEnumerable<Type> GetGenericArgs(this Type type, Type generic)
        {
            return type.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == generic)
                .Select(i => i.GetGenericArguments().First());
        }
    }
}
