namespace PeletonSoft.Tools.Model.Memento
{
    public interface IOriginator
    {
        void RestoreDefault();
    }

    public interface IOriginator<T> where T : IDataTransfer
    {
        T Save();
        void Restore(T state);

        //void RestoreDefault();
    }
}
