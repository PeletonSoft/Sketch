using PeletonSoft.Tools.Model.SketchMath.Wave;
using PeletonSoft.Tools.Model.SketchMath.Wave.WavyBorderBuilder;

namespace PeletonSoft.Tools.Model.SketchMath
{
    public interface IWavyBorderBuilder
    {
        double Width { get; }
        int WaveCount { get; }
        double WaveHeight { get; }
        double ExtraStart { get; }
        double ExtraFinish { get; }
        WavyBorder<Position> WavyBorder { get; } 
        double FullWidth { get; }
        double Step { get; }

        WavyBorderOffset WavyBorderOffset { get; }
    }
}