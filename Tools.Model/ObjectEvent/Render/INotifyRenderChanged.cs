namespace PeletonSoft.Tools.Model.ObjectEvent.Render
{
    public interface INotifyRenderChanged<TR, TS, TD>
    {
        RenderChangedDispatcher<TR, TS, TD> RenderChangedDispatcher { get; }
    }

    
}