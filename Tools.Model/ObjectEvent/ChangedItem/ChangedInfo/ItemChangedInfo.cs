namespace PeletonSoft.Tools.Model.ObjectEvent.ChangedItem.ChangedInfo
{
    public abstract class ItemChangedInfo
    {
        public abstract int RecalculateIndex(int index);
        public abstract bool IsEmptyChanged(int count);
    }
}
