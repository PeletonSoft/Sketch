using System.Collections.ObjectModel;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;

namespace PeletonSoft.Sketch.ViewModel
{
    public class FactoryCollection : ObservableCollection<IElementFactoryViewModel<IElementViewModel>>, IFactoryCollection
    {
    }

}
