namespace PeletonSoft.Tools.Model.ObjectEvent.ChangedItem.ChangedInfo
{
    public sealed class MoveItemChangedInfo : ItemChangedInfo
    {
        public int SourceIndex { get; }
        public int DestionationIndex { get; }

        public MoveItemChangedInfo(int sourceIndex, int destionationIndex)
        {
            SourceIndex = sourceIndex;
            DestionationIndex = destionationIndex;
        }

        public override int RecalculateIndex(int index)
        {
            if (index == -1)
            {
                return -1;
            }

            if (index == SourceIndex)
            {
                return DestionationIndex;
            }

            var result = index;
            result -= result > SourceIndex ? 1 : 0;
            result += result >= DestionationIndex ? 1 : 0;
            return result;
        }

        public override bool IsEmptyChanged(int count) => false;
    }
}
