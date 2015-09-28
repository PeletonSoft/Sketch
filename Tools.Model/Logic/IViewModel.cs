namespace PeletonSoft.Tools.Model.Logic
{
    public interface IViewModel
    {
    }

    public interface IViewModel<out T> : IViewModel
    {
        T Model { get; }
    }

}
