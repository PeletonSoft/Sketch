namespace PeletonSoft.Tools.Model.Logic
{
    public interface IViewModel<out T>
    {
        T Model { get; }
    }
}
