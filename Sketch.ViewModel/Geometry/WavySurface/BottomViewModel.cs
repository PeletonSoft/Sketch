using System.Collections.Generic;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Interface.Primitive;
using PeletonSoft.Tools.Model.SketchMath.Wave;

namespace PeletonSoft.Sketch.ViewModel.Geometry.WavySurface
{
    public class BottomViewModel : IWavySurfaceItemViewModel
    {
        public IBottom<IEnumerable<Point>> Bottom { get; private set; }

        public BottomViewModel(IBottom<IEnumerable<Point>> bottom)
        {
            Bottom = bottom;
        }
    }
}
