using PeletonSoft.Tools.Model.Collection;
using PeletonSoft.Tools.Model.Memento.Container;

namespace PeletonSoft.Sketch.ViewModel.Interface.Geometry
{
    public interface IRotableViewModel
    {
        IRotationViewModel Rotation { get; set; }
        IContainer<IRotationViewModel> Rotations { get; }
    }
}
