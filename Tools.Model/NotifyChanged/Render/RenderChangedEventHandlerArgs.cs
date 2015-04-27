using System;
using System.Security.Permissions;

namespace PeletonSoft.Tools.Model.NotifyChanged.Render
{
    [HostProtection(SecurityAction.LinkDemand, SharedState = true)]
    public class RenderChangedEventHandlerArgs<T> : EventArgs
    {

        public T RenderData { get; private set; }

        public RenderChangedEventHandlerArgs(T renderData)
        {
            RenderData = renderData;
        }
    }
}