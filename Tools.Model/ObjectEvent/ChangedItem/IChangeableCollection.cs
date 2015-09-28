using PeletonSoft.Tools.Model.Collection;

namespace PeletonSoft.Tools.Model.ObjectEvent.ChangedItem
{
    public interface IChangeableCollection<T> : INotifyItemChanged, ISelectableList<T>
    {
        INotifyChangedReadOnlyCollection<T> Collection { get; }
        bool IsEmpty { get; }
        void MoveTo(int sourceIndex, int destinationIndex);
        void Remove();
        void MoveDown();
        void MoveUp();
        void Append(T element);
        void Clear();
    }
}
