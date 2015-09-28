using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;

namespace PeletonSoft.Tools.Model.ObjectEvent.NotifyChangedExpression
{
    public static class NotifyExpressionSubscribe
    {
        public static T SetPropertyChanged<T, TU>(this T sender,
            IEnumerable<Expression<Func<T, TU>>> properties, Action handler)
            where T : class, INotifyPropertyChanged
        {
            if (properties != null && sender != null)
            {
                var propertyNames = properties.Select(sender.GetPropertyName);
                sender.SetPropertyChanged(propertyNames, handler);
            }

            return sender;
        }

        public static T SetPropertyChanged<T, TU>(this T sender,
            Expression<Func<T, TU>> expression, Action handler) where T : class, INotifyPropertyChanged
        {
            var mapper = new PropertyMapper<T>();
            var propertyName = mapper.PropertyName(expression);
            sender.SetPropertyChanged(propertyName, handler);
            return sender;
        }

        public static T SetPropertyChanged<T, TM, TU>(this T sender,
            Expression<Func<T, TM>> leftExpression, Expression<Func<TM, TU>> rightExpression,
            Action handler)
            where T : class, INotifyPropertyChanged
            where TM : class, INotifyPropertyChanged
        {
            var rightMapper = new PropertyMapper<TM>();
            var rightPropertyName = rightMapper.PropertyName(rightExpression);
            return sender.SetPropertyChanged(new Getter<T, TM>(leftExpression), rightPropertyName, handler);
        }

        public static T PropertyIterate<T, TU>(this T sender,
            IEnumerable<Expression<Func<T, TU>>> properties,
            Action<TU, string> handler)
            where T : class, INotifyPropertyChanged
        {
            foreach (var property in properties)
            {
                var propertyName = sender.GetPropertyName(property);
                var value = property.Compile()(sender);
                handler(value, propertyName);
            }
            return sender;
        }
    }
}
