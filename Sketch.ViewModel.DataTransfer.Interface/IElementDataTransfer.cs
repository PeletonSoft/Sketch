using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.DataTransfer.Interface
{
    public interface IElementDataTransfer  : IDataTransfer
    {
        string Description { get; set; }
    }
}
