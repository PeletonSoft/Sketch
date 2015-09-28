using System;
using System.Linq.Expressions;

namespace PeletonSoft.Tools.Model.ObjectEvent.ObjectEventExpression
{
    public static class GetterExpressionHelper
    {
        public static Getter<TS, TP> ExtractGetter<TS, TP>(
            this TS sender, Expression<Func<TS, TP>> expression) where TS : class
        {
            return new Getter<TS, TP>(expression.GetPropertyName(), expression.Compile());
        }

    }
}
