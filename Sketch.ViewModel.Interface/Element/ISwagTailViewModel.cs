using System.Collections.Generic;
using System.Windows;
using PeletonSoft.Tools.Model.SketchMath.Wave;

namespace PeletonSoft.Sketch.ViewModel.Interface.Element
{
    public interface ISwagTailViewModel
    {
        IWavyBorder<IEnumerable<Point>> WavySurface { get; }
    }
}