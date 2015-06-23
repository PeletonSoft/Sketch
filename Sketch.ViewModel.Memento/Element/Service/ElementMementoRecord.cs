using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Memento.Element.Service
{
    public class ElementMementoRecord
    {
        public string ElementType { get; set; }
        public IMemento<IElementViewModel> Element { get; set; }
    }
}
