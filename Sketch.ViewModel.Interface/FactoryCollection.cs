using System.Collections.ObjectModel;
using PeletonSoft.Sketch.ViewModel.Interface.Element;

namespace PeletonSoft.Sketch.ViewModel.Interface
{
    public class FactoryCollection : ObservableCollection<IElementFactoryViewModel<IElementViewModel>>
    {
    }

}
