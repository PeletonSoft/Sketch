namespace PeletonSoft.Tools.Model.Memento
{
    public interface ICaretaker<T> where T : IDataTransfer
    {
        void SaveToFile(IVisualOriginator<T> originator);
        void RestoreFromFile(IVisualOriginator<T> originator);
    }
}