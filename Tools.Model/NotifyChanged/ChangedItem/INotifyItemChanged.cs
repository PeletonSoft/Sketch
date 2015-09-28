using System.Security.Permissions;

namespace PeletonSoft.Tools.Model.NotifyChanged.ChangedItem
{
    [HostProtection(SecurityAction.LinkDemand, SharedState = true)]
    public delegate void ItemChangedEventHandler(
        object sender, 
        ItemChangedEventArgs e);
    public interface INotifyItemChanged
    {
        event ItemChangedEventHandler ItemChanged;
    }
}
