using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Tools.Model.Logic;

namespace PeletonSoft.Sketch.ViewModel.Interface.Visual
{
    public interface IElementVisualViewModel<out T> : INotifyVisualViewModel<T>
        where T : IElementViewModel
    {
        ILayoutVisualViewModel Layout { get; }
        bool Visibility { get; }
        double Opacity { get; }
    
    }
}
