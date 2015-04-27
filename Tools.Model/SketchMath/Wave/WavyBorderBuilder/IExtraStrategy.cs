namespace PeletonSoft.Tools.Model.SketchMath.Wave.WavyBorderBuilder
{
    public interface IExtraStrategy
    {
        double GetExtraStart(WavyBorderParameters parameters, WavyBorderOffset shareOffset);
        double GetExtraFinish(WavyBorderParameters parameters, WavyBorderOffset shareOffset);
    }
}