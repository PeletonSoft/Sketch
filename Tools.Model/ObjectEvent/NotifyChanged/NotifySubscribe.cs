using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged
{
    public static class NotifySubscribe
    {
        public static T SetPropertyChanged<T>(this T sender,
            string propertyName, Action handler) 
            where T : class, INotifyPropertyChanged
        {
            if (sender == null)
            {
                return null;
            }

            sender.PropertyChanged +=
                (o, args) =>
                {
                    if (args.PropertyName == propertyName)
                    {
                        handler();
                    }
                };
            return sender;
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


        public static T SetPropertyChanged<T, TM>(this T sender,
            Getter<T, TM> leftGetter,
            string rightPropertyName,
            Action handler)
            where T : class, INotifyPropertyChanged
            where TM : class, INotifyPropertyChanged
        {
            if (sender == null)
            {
                return null;
            }

            PropertyChangedEventHandler rightPropertyChangedAction =
                (o, args) =>
                {
                    if (args.PropertyName == rightPropertyName)
                    {
                        handler();
                    }
                };

            var lastLeftValue = leftGetter.GetterValue(sender);

            if (lastLeftValue != null)
            {
                lastLeftValue.PropertyChanged += rightPropertyChangedAction;
            }

            sender.PropertyChanged +=
                (o, args) =>
                {
                    if (args.PropertyName == leftGetter.PropertyName)
                    {
                        if (lastLeftValue != null)
                        {
                            lastLeftValue.PropertyChanged -= rightPropertyChangedAction;
                        }

                        lastLeftValue = leftGetter.GetterValue(sender);

                        if (lastLeftValue != null)
                        {
                            lastLeftValue.PropertyChanged += rightPropertyChangedAction;
                        }

                        handler();
                    }
                };
            return sender;
        }

        public static T SetPropertyChanged<T, TM>(this T sender,
            Getter<T, TM> leftGetter,
            string[] rightPropertyNames,
            Action handler)
            where T : class, INotifyPropertyChanged
            where TM : class, INotifyPropertyChanged
        {
            if (sender == null)
            {
                return null;
            }


            PropertyChangedEventHandler rightPropertyChangedAction =
                (o, args) =>
                {
                    if (rightPropertyNames.Contains(args.PropertyName))
                    {
                        handler();
                    }
                };

            var lastLeftValue = leftGetter.GetterValue(sender);

            if (lastLeftValue != null)
            {
                lastLeftValue.PropertyChanged += rightPropertyChangedAction;
            }

            sender.PropertyChanged +=
                (o, args) =>
                {
                    if (args.PropertyName == leftGetter.PropertyName)
                    {
                        if (lastLeftValue != null)
                        {
                            lastLeftValue.PropertyChanged -= rightPropertyChangedAction;
                        }

                        lastLeftValue = leftGetter.GetterValue(sender);

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
            IEnumerable<Getter<T,TU>> getters,
            Action<TU, string> handler)
            where T : class, INotifyPropertyChanged
        {
            getters.ToList()
                .ForEach(getter => handler(getter.GetterValue(sender), getter.PropertyName));
            return sender;
        }

    }
}
