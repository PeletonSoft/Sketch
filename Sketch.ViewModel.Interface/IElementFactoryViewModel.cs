using PeletonSoft.Sketch.ViewModel.Interface.Element;

namespace PeletonSoft.Sketch.ViewModel.Interface
{
    public interface IElementFactoryViewModel<out T> where T : IElementViewModel
    {
        T CreateElement(IWorkspaceBit workspaceBit);
    }

    public interface IElementFactoryViewModel : IElementFactoryViewModel<IElementViewModel>
    {
    }
}
