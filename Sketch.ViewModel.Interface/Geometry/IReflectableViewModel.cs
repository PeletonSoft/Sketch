using PeletonSoft.Tools.Model.Collection;
using PeletonSoft.Tools.Model.Memento.Container;

namespace PeletonSoft.Sketch.ViewModel.Interface.Geometry
{
    public interface IReflectableViewModel
    {
        IReflectionViewModel Reflection { get; set; }
        IContainer<IReflectionViewModel> Reflections { get; }

    }
}
