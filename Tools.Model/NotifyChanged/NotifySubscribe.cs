using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace PeletonSoft.Tools.Model.NotifyChanged
{
    public static class NotifySubscribe
    {
        private static void SetPropertyChanged<T>(this T sender,
            string propertyName, Action handler) 
            where T : class, INotifyPropertyChanged
        {
            if (sender == null)
            {
                return;
            }

            sender.PropertyChanged +=
                (o, args) =>
                {
                    if (args.PropertyName == propertyName)
                    {
                        handler();
                    }
                };
        }

        private static T SetPropertyChanged<T>(this T sender,
            IEnumerable<string> propertyNames, Action<string> handler)
            where T : class, INotifyPropertyChanged
        {
            if (propertyNames != null && sender != null)
            {
                sender.PropertyChanged +=
                    (o, args) =>
                    {
                        if (propertyNames.Contains(args.PropertyName))
                        {
                            handler(args.PropertyName);
                        }
                    };
            }

            return sender;
        }

        public static T SetPropertyChanged<T>(this T sender,
            IEnumerable<string> propertyNames, Action handler)
            where T : class, INotifyPropertyChanged
        {
            return sender.SetPropertyChanged(propertyNames, propertyName => handler());
        }

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
            Expression<Func<T, TM>> leftExpression,
            Expression<Func<TM, TU>> rightExpression,
            Action handler)
            where T : class, INotifyPropertyChanged
            where TM : class, INotifyPropertyChanged
        {
            if (sender == null)
            {
                return null;
            }

            var leftMapper = new PropertyMapper<T>();
            var leftPropertyName = leftMapper.PropertyName(leftExpression);

            var rightMapper = new PropertyMapper<TM>();
            var rightPropertyName = rightMapper.PropertyName(rightExpression);

            PropertyChangedEventHandler rightPropertyChangedAction =
                (o, args) =>
                {
                    if (args.PropertyName == rightPropertyName)
                    {
                        handler();
                    }
                };

            var leftValueFunc = leftExpression.Compile();
            var lastLeftValue = leftValueFunc(sender);

            if (lastLeftValue != null)
            {
                lastLeftValue.PropertyChanged += rightPropertyChangedAction;
            }

            sender.PropertyChanged +=
                (o, args) =>
                {
                    if (args.PropertyName == leftPropertyName)
                    {
                        if (lastLeftValue != null)
                        {
                            lastLeftValue.PropertyChanged -= rightPropertyChangedAction;
                        }
                        lastLeftValue = leftValueFunc(sender);

                        if (lastLeftValue != null)
                        {
                            lastLeftValue.PropertyChanged += rightPropertyChangedAction;
                        }
                        handler();
                    }
                };
            return sender;
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
