using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged
{
    public static class NotifyPropertyChangedHelper
    {
        public static void OnPropertyChanged<T>(this T sender,
            PropertyChangedEventHandler handler, string propertyName) where T : class, INotifyPropertyChanged
        {
            handler?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
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

        public static bool SetFieldValue<T>(Action notificator, ref T field, T value)
        {
            return notificator.SetField(ref field, value);
        }
        public static bool SetFieldValue<T>(Action notificator, Func<T> getValue, Action<T> setValue, T value)
        {
            return notificator.SetField(getValue, setValue, value);
        }


    }
}
