using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace PeletonSoft.Tools.Model.NotifyChanged
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

        public static T SetPropertyChanged<T, TU>(this T sender, LockFlag lockFlag,
            Expression<Func<T, TU>> expression, Action handler) where T : class, INotifyPropertyChanged
        {
            return sender.SetPropertyChanged(expression, () => lockFlag.LockAction(handler));
        }
    }
}
