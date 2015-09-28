using System;
using System.Linq.Expressions;
using System.Reflection;

namespace PeletonSoft.Tools.Model.ObjectEvent
{
    public sealed class PropertyMapper<T>
    {
        public PropertyInfo PropertyInfo<TU>(Expression<Func<T, TU>> expression)
        {
            var member = expression.Body as MemberExpression;
            if (member?.Member is PropertyInfo)
                return (PropertyInfo) member.Member;

            throw new ArgumentException("Expression is not a Property");
        }

        public string PropertyName<TU>(Expression<Func<T, TU>> expression)
        {
            return PropertyInfo(expression).Name;
        }

        public Type PropertyType<TU>(Expression<Func<T, TU>> expression)
        {
            return PropertyInfo(expression).PropertyType;
        }
    }
}
