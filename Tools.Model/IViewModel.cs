namespace PeletonSoft.Tools.Model
{
    public interface IViewModel<out T>
    {
        T Model { get; }
    }
}
