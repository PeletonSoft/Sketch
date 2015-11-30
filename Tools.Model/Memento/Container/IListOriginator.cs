using PeletonSoft.Tools.Model.Collection;

namespace PeletonSoft.Tools.Model.Memento.Container
{
    public interface IContainerOriginator<out T> : IContainer<T>
    {
    }

    public interface IListOriginator<T> : IContainer<IOriginator<T>>, IOriginator<IListDataTransfer<T>>
        where T : IDataTransfer
    {
    }

}
