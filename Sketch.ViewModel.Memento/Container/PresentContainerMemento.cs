using PeletonSoft.Sketch.ViewMode.Memento.Service;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewMode.Memento.Container
{
    public class PresentContainerMemento : ContainerMemento<IPresentViewModel>
    {
        public PresentContainerMemento() : 
            base(new PresentMementoService(), "Present")
        {
        }
    }
}
