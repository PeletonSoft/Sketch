using PeletonSoft.Tools.Model.Collection;

namespace PeletonSoft.Sketch.ViewModel.Interface.Geometry
{
    public interface IRotableViewModel
    {
        IRotationViewModel Rotation { get; set; }
        IContainer<IRotationViewModel> Rotations { get; }
    }
}
