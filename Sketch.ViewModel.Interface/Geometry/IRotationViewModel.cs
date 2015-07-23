using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PeletonSoft.Sketch.ViewModel.Interface.Geometry
{
    public interface IRotationViewModel
    {
        double Angle { get; }
        Size Rotate(Size size);
    }
}
