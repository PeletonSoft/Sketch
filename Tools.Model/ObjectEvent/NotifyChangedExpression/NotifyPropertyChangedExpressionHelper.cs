using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace PeletonSoft.Tools.Model.ObjectEvent.NotifyChangedExpression
{
    public static class NotifyPropertyChangedExpressionHelper
    {
        public static void OnPropertyChanged<T, TU>(this Expression<Func<T, TU>> expression,
            Action<string> propertyChandedAction) where T : class, INotifyPropertyChanged
        {
            var mapper = new PropertyMapper<T>();
            var propertyName = mapper.PropertyName(expression);
            propertyChandedAction(propertyName);
        }

    }
}
