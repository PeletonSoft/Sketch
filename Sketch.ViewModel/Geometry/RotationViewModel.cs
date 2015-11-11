using System.Windows;
using PeletonSoft.Sketch.Model.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Interface.Geometry;

namespace PeletonSoft.Sketch.ViewModel.Geometry
{
    public class RotationViewModel : IRotationViewModel
    {
        private readonly Rotation _rotation;
        public RotationViewModel(Rotation rotation)
        {
            _rotation = rotation;
        }

        public double Angle => _rotation.Angle;
        public Size Rotate(Size size) => _rotation.Rotate(size);
    }
}
