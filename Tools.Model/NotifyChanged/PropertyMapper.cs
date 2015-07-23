using System;
using System.Linq.Expressions;
using System.Reflection;

namespace PeletonSoft.Tools.Model.NotifyChanged
{
    public sealed class PropertyMapper<T>
    {
        public PropertyInfo PropertyInfo<TU>(Expression<Func<T, TU>> expression)
        {
            var member = expression.Body as MemberExpression;
            if (member != null && member.Member is PropertyInfo)
                return member.Member as PropertyInfo;

            throw new ArgumentException("Expression is not a Property", "expression");
        }

        public string PropertyName<TU>(Expression<Func<T, TU>> expression)
        {
            return PropertyInfo<TU>(expression).Name;
        }

        public Type PropertyType<TU>(Expression<Func<T, TU>> expression)
        {
            return PropertyInfo<TU>(expression).PropertyType;
        }
    }
}
