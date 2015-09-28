using System.Collections.Generic;
using PeletonSoft.Sketch.ViewModel.Interface.Element;

namespace PeletonSoft.Sketch.ViewModel.Interface
{
    public interface IFactoryCollection : IEnumerable<IElementFactoryViewModel<IElementViewModel>>
    {
    }

}
