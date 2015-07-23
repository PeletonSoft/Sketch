using PeletonSoft.Tools.Model.Collection;

namespace PeletonSoft.Tools.Model.Memento.Container
{
    public interface IContainerOriginator<out T> : IOriginator,IContainer<T>
    {
    }
}
