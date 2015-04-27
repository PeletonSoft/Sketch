namespace PeletonSoft.Tools.Model.Memento
{
    public interface IContainerOriginator<out T> : IOriginator,IContainer<T>
    {
    }
}
