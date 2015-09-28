using System;
using System.Security.Permissions;
using PeletonSoft.Tools.Model.NotifyChanged.ChangedItem.ChangedInfo;

namespace PeletonSoft.Tools.Model.NotifyChanged.ChangedItem
{
    [HostProtection(SecurityAction.LinkDemand, SharedState = true)]
    public class ItemChangedEventArgs : EventArgs
    {
        public ItemChangedInfo ChangedInfo { get; private set; }

        public ItemChangedEventArgs(ItemChangedInfo changedInfo)
        {
            ChangedInfo = changedInfo;
        }
    }
}
