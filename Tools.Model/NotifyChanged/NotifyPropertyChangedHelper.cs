using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;

namespace PeletonSoft.Tools.Model.NotifyChanged
{
    public static class NotifyPropertyChangedHelper
    {
        public static void OnPropertyChanged<T>(this T sender,
            PropertyChangedEventHandler handler, string propertyName) where T : class, INotifyPropertyChanged
        {
            handler?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
        }

        public static string GetPropertyName<T, TU>(this Expression<Func<T, TU>> expression)
            where T : class, INotifyPropertyChanged
        {
            var mapper = new PropertyMapper<T>();
            return mapper.PropertyName(expression);
        }

        public static string GetPropertyName<T, TU>(this T sender, Expression<Func<T, TU>> expression)
            where T : class, INotifyPropertyChanged
        {
            return expression.GetPropertyName();
        }

        public static void OnPropertyChanged<T, TU>(this Expression<Func<T, TU>> expression,
            Action<string> propertyChandedAction)
            where T : class, INotifyPropertyChanged
        {
            var mapper = new PropertyMapper<T>();
            var propertyName = mapper.PropertyName(expression);
            propertyChandedAction(propertyName);
        }

        public static bool SetField<T>(this Action notificator, Func<T> getValue, Action<T> setValue, T value)
        {
            var oldValue = getValue();
            var changed = !EqualityComparer<T>.Default.Equals(oldValue, value);

            if (changed)
            {
                setValue(value);
                notificator();
            }

            return changed;
        }

        public static bool SetField<T>(this Action notificator, ref T field, T value)
        {
            var oldValue = field;
            var changed = !EqualityComparer<T>.Default.Equals(oldValue, value);

            if (changed)
            {
                field = value;
                notificator();
            }

            return changed;
        }

        public static Getter<TS, TP> ExtractGetter<TS, TP>(
            this TS sender, string propertyName, 
            Func<TS, TP> getterValue) where TS : class, INotifyPropertyChanged
        {
            return new Getter<TS, TP>(propertyName, getterValue);
        }


    }
}
