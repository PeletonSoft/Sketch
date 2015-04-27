namespace PeletonSoft.Tools.Model.SketchMath.Wave.WavyBorderBuilder.ExtraStrategy
{
    public class HalfStepExtraFinishStrategy : IExtraStrategy
    {
        double GetStep(WavyBorderParameters parameters, WavyBorderOffset shareOffset)
        {
            return (parameters.Width - shareOffset.A)/
                   (parameters.WaveCount + shareOffset.K + 0.5);

        }

        public double GetExtraStart(WavyBorderParameters parameters, WavyBorderOffset shareOffset)
        {
            return 0;
        }

        public double GetExtraFinish(WavyBorderParameters parameters, WavyBorderOffset shareOffset)
        {
            return GetStep(parameters, shareOffset) / 2; 
        }
    }
}