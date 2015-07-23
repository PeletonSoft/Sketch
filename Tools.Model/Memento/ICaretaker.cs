namespace PeletonSoft.Tools.Model.Memento
{
    public interface ICaretaker<T> where T : IOriginator
    {
        IMemento<T> Memento { get; set; }
        void GetState(T originator);
        void SetState(T originator);
        void Save(string path);
        void Load(string path);
    }
}