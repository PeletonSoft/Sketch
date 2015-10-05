namespace PeletonSoft.Tools.Model.ObjectEvent.ChangedItem.ChangedInfo
{
    public class AppendItemChangedInfo : ItemChangedInfo
    {

        public override int RecalculateIndex(int index) => index;
        public override bool IsEmptyChanged(int count) => count == 1;
    }
}
