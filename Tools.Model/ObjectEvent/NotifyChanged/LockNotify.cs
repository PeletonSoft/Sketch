using System;
using System.ComponentModel;

namespace PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged
{
    public static class LockNotify
    {
        public static void LockAction(this LockFlag lockFlag, Action handler)
        {
            if (lockFlag.Lock)
            {
                return;
            }

            lockFlag.Lock = true;
            if (lockFlag.Lock)
            {
                try
                {
                    handler();
                }
                finally
                {
                    lockFlag.Lock = false;
                }
            }
        }

        public static T SetPropertyChanged<T>(this T sender, LockFlag lockFlag,
            string propertyName, Action handler) where T : class, INotifyPropertyChanged
        {
            return sender.SetPropertyChanged(propertyName, () => lockFlag.LockAction(handler));
        }
    }
}
