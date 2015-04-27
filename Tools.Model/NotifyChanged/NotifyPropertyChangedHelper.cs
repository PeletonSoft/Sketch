using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace PeletonSoft.Tools.Model.NotifyChanged
{
    public static class NotifyPropertyChangedHelper
    {
        public static void OnPropertyChanged(this INotifyPropertyChanged sender, 
            PropertyChangedEventHandler handler, string propertyName)
        {
            if (handler != null)
            {
                handler(sender, new PropertyChangedEventArgs(propertyName));
            }
        }
        public static bool SetField<T>(this Action notificator, ref T field, T value)
        {
            var changed = !EqualityComparer<T>.Default.Equals(field, value);

            if (changed)
            {
                field = value;
                notificator();
            }

            return changed;
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

        public static void SetPropertyChanged(this INotifyPropertyChanged sender,
            string propertyName, Action handler)
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

        public static void SetPropertyChanged(this INotifyPropertyChanged sender,
            IEnumerable<string> properties, Action handler)
        {
            if (properties == null || sender == null)
            {
                return;
            }

            sender.PropertyChanged +=
                (o, args) =>
                {
                    if (properties.Contains(args.PropertyName))
                    {
                        handler();
                    }
                };
        }

        public static void SetPropertyChanged(this INotifyPropertyChanged sender,
            IEnumerable<string> properties, Action<string> handler)
        {
            if (properties == null || sender == null)
            {
                return;
            }

            sender.PropertyChanged +=
                (o, args) =>
                {
                    if (properties.Contains(args.PropertyName))
                    {
                        handler(args.PropertyName);
                    }
                };
        }
        public static void SetPropertyChanged(
            this IEnumerable<INotifyPropertyChanged> senders,
            IEnumerable<string> properties, Action handler)
        {
            if (properties == null)
            {
                return;
            }

            var propertyArray = properties.ToArray();
            foreach (var sender in senders)
            {
                sender.SetPropertyChanged(propertyArray, handler);
            }
        }

        public static void SetPropertyChanged(
            this IEnumerable<KeyValuePair<INotifyPropertyChanged, Action>> senders,
            IEnumerable<string> properties)
        {
            if (properties == null)
            {
                return;
            }

            var propertyArray = properties.ToArray();
            foreach (var sender in senders)
            {
                sender.Key.SetPropertyChanged(propertyArray, sender.Value);
            }
        }

        public static void SetPropertyChanged(
            this IEnumerable<KeyValuePair<INotifyPropertyChanged, string>> senders,
            IEnumerable<string> properties, Action<string> handler)
        {
            senders
                .Select(
                    pair => new KeyValuePair<INotifyPropertyChanged, Action>(
                        pair.Key, () => handler(pair.Value)))
                .SetPropertyChanged(properties);
        }
    }
}
