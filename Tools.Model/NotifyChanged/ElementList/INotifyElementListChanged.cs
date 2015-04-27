using System.Security.Permissions;

namespace PeletonSoft.Tools.Model.NotifyChanged.ElementList
{
    [HostProtection(SecurityAction.LinkDemand, SharedState = true)]
    public delegate void ElementListChangedEventHandler(
        object sender, 
        ElementListChangedEventArgs e);
    public interface INotifyElementListChanged
    {
        event ElementListChangedEventHandler ElementListChanged;
    }
}
