using PeletonSoft.Sketch.ViewModel.Interface.Layout;
using PeletonSoft.Tools.Model.Memento.Container;

namespace PeletonSoft.Sketch.ViewModel.Interface.Element
{
    public interface IAlignable : ILayoutable
    {
        double Width { get; set;  }
        double Height { get; set; }
        double OffsetX { get; set; }
        double OffsetY { get; set; }
        new ILayoutViewModel Layout { get; set; }
        IContainerOriginator<ILayoutViewModel> Layouts { get; }

    }
}
