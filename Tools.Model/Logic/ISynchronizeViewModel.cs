namespace PeletonSoft.Tools.Model.Logic
{
    public interface ISynchronizeViewModel<T> : IViewModel<T>
    {
        void Synchronize(IViewModel<T> source, IViewModel<T> destination);
    }
}
