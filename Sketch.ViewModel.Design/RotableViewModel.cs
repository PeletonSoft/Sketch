using PeletonSoft.Sketch.ViewModel.Interface.Geometry;
using PeletonSoft.Tools.Model.Collection;
using PeletonSoft.Tools.Model.Memento.Container;

namespace PeletonSoft.Sketch.ViewModel.Design
{
    public class RotableViewModel : IRotableViewModel
    {
        public IRotationViewModel Rotation { get; set; }
        public IContainer<IRotationViewModel> Rotations { get; private set; }
    }
}
