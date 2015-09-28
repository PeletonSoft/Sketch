namespace PeletonSoft.Tools.Model.NotifyChanged.ChangedItem.ChangedInfo
{
    public class AppendItemChangedInfo : ItemChangedInfo
    {
        public override int RecalculateIndex(int index)
        {
            return index;
        }

        public override bool IsEmptyChanged(int count)
        {
            return count == 1;
        }
    }
}
