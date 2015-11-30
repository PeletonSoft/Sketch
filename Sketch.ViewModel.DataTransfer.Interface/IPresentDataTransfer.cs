using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.DataTransfer.Interface
{
    public interface IPresentDataTransfer : IDataTransfer
    {
        double Zoom { get; set; }
    }
}
