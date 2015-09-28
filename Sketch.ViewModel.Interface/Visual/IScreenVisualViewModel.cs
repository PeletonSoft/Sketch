using PeletonSoft.Tools.Model.Logic;

namespace PeletonSoft.Sketch.ViewModel.Interface.Visual
{
    public interface IScreenVisualViewModel : INotifyVisualViewModel<IScreenViewModel>
    {
        double Width { get; }
        double Height { get; }
    }
}