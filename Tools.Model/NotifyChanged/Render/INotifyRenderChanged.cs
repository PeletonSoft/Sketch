namespace PeletonSoft.Tools.Model.NotifyChanged.Render
{
    public interface INotifyRenderChanged<TR, TS, TD>
    {
        RenderChangedDispatcher<TR, TS, TD> RenderChangedDispatcher { get; }
    }

    
}