using System;
using System.Security.Permissions;

namespace PeletonSoft.Tools.Model.ObjectEvent.Render
{
    [HostProtection(SecurityAction.LinkDemand, SharedState = true)]
    public class RenderChangedEventHandlerArgs<T> : EventArgs
    {

        public T RenderData { get; }

        public RenderChangedEventHandlerArgs(T renderData)
        {
            RenderData = renderData;
        }
    }
}