namespace PeletonSoft.Tools.Model.Logic
{
    public interface IVisualViewModel
    {
    }

    public interface IVisualViewModel<out T> : IVisualViewModel
    {
        T Element { get; }
    }
}
