namespace PeletonSoft.Tools.Model.ObjectEvent.ChangedItem.ChangedInfo
{
    public sealed class RemoveItemChangedInfo : ItemChangedInfo
    {
        public int Index { get; }

        public RemoveItemChangedInfo(int index)
        {
            Index = index;
        }

        public override int RecalculateIndex(int index)
        {
            if (index == -1 || index == Index)
            {
                return -1;
            }

            return index - (index < Index ? 0 : 1);
        }

        public override bool IsEmptyChanged(int count) => count == 0;
    }
}
