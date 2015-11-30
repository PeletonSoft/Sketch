namespace PeletonSoft.Tools.Model.Memento
{
    public interface IOriginator<T> where T : IDataTransfer
    {
        T CreateState();
        void Save(T state);
        void Restore(T state);
    }
}
