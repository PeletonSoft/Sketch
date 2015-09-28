using System.Security.Permissions;

namespace PeletonSoft.Tools.Model.ObjectEvent.ChangedItem
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
