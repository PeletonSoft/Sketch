using PeletonSoft.Tools.Model.Collection;

namespace PeletonSoft.Sketch.ViewModel.Interface.Geometry
{
    public interface IReflectableViewModel
    {
        IReflectionViewModel Reflection { get; set; }
        IContainer<IReflectionViewModel> Reflections { get; }

    }
}
