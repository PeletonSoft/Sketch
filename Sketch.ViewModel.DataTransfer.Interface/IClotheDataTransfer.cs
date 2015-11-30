using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.DataTransfer.Interface
{
    public interface IClotheDataTransfer : IDataTransfer
    {
        double? Width { get; set; }
        double? Height { get; set; }
    }
}
