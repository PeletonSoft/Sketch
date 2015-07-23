using PeletonSoft.Sketch.ViewModel.Interface.Geometry;
using PeletonSoft.Tools.Model.Collection;
using PeletonSoft.Tools.Model.Memento.Container;

namespace PeletonSoft.Sketch.ViewModel.Design
{
    public class ReflectableViewModel : IReflectableViewModel
    {
        public IReflectionViewModel Reflection { get; set; }
        public IContainer<IReflectionViewModel> Reflections { get; private set; }
    }
}
