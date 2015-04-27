using PeletonSoft.Tools.Model.NotifyChanged.ElementList.ChangedInfo.Primitive;

namespace PeletonSoft.Tools.Model.NotifyChanged.ElementList.ChangedInfo
{
    public class RemoveElementListChangedInfo : ElementListChangedInfo
    {
        public int Index { get; private set; }

        public RemoveElementListChangedInfo(int index)
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
    }
}
