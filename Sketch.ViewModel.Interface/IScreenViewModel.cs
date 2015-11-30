using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Tools.Model.Logic;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Interface
{
    public interface IScreenViewModel : INotifyViewModel, IOriginator<IScreenDataTransfer>
    {
        double Width { get; set; }
        double Height { get; set; }

    }
}