using System;
using System.Security.Permissions;
using PeletonSoft.Tools.Model.ObjectEvent.ChangedItem.ChangedInfo;

namespace PeletonSoft.Tools.Model.ObjectEvent.ChangedItem
{
    [HostProtection(SecurityAction.LinkDemand, SharedState = true)]
    public class ItemChangedEventArgs : EventArgs
    {
        public ItemChangedInfo ChangedInfo { get; }

        public ItemChangedEventArgs(ItemChangedInfo changedInfo)
        {
            ChangedInfo = changedInfo;
        }
    }
}
