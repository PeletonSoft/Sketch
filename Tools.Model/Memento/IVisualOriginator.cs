using PeletonSoft.Tools.Model.File;

namespace PeletonSoft.Tools.Model.Memento
{
    public interface IVisualOriginator<T> : IOriginator<T> where T : IDataTransfer
    {
        ImageBox ImageBox { get; }
    }
}
