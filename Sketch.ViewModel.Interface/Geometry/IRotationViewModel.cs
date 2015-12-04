using System.Windows;

namespace PeletonSoft.Sketch.ViewModel.Interface.Geometry
{
    public interface IRotationViewModel
    {
        double Angle { get; }
        Size Rotate(Size size);
    }
}
