using PeletonSoft.Tools.Model.NotifyChanged.ElementList.ChangedInfo.Primitive;

namespace PeletonSoft.Tools.Model.NotifyChanged.ElementList.ChangedInfo
{
    public class MoveElementListChangedInfo : ElementListChangedInfo
    {
        public int SourceIndex { get; private set; }
        public int DestionationIndex { get; private set; }

        public MoveElementListChangedInfo(int sourceIndex, int destionationIndex)
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
    }
}
