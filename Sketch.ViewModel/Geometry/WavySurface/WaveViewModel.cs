using System.Collections.Generic;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Interface.Primitive;
using PeletonSoft.Tools.Model.SketchMath.Wave;

namespace PeletonSoft.Sketch.ViewModel.Geometry.WavySurface
{
    public class WaveViewModel : IWavySurfaceItemViewModel
    {
        public IWave<IEnumerable<Point>> Wave { get; private set; }

        public WaveViewModel(IWave<IEnumerable<Point>> wave)
        {
            Wave = wave;
        }
    }
}
