using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.DataTransfer
{
    public class ScreenDataTransfer : IScreenDataTransfer
    {
        public double Width { get; set; }
        public double Height { get; set; }
    }
}
