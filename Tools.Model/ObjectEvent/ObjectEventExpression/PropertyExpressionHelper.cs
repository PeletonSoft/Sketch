using System;
using System.Linq.Expressions;

namespace PeletonSoft.Tools.Model.ObjectEvent.ObjectEventExpression
{
    public static class PropertyExpressionHelper
    {
        public static string GetPropertyName<T, TU>(this Expression<Func<T, TU>> expression)
            where T : class
        {
            var mapper = new PropertyMapper<T>();
            return mapper.PropertyName(expression);
        }

        public static string GetPropertyName<T, TU>(this T sender, Expression<Func<T, TU>> expression)
            where T : class
        {
            return expression.GetPropertyName();
        }
    }
}
