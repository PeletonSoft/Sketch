using System;
using System.Security.Permissions;
using PeletonSoft.Tools.Model.NotifyChanged.ElementList.ChangedInfo.Primitive;

namespace PeletonSoft.Tools.Model.NotifyChanged.ElementList
{
    [HostProtection(SecurityAction.LinkDemand, SharedState = true)]
    public class ElementListChangedEventArgs : EventArgs
    {
        public ElementListChangedInfo ChangedInfo { get; private set; }

        public ElementListChangedEventArgs(ElementListChangedInfo changedInfo)
        {
            ChangedInfo = changedInfo;
        }
    }
}
