namespace PeletonSoft.Tools.Model.SketchMath.Wave.WavyBorderBuilder.ExtraStrategy
{
    public class HalfStepExtraStartStrategy : IExtraStrategy
    {
        double GetStep(WavyBorderParameters parameters, WavyBorderOffset shareOffset)
        {
            return (parameters.Width - shareOffset.A)/
                   (parameters.WaveCount + shareOffset.K + 0.5);

        }

        public double GetExtraStart(WavyBorderParameters parameters, WavyBorderOffset shareOffset)
        {
            return GetStep(parameters, shareOffset) / 2; 
        }

        public double GetExtraFinish(WavyBorderParameters parameters, WavyBorderOffset shareOffset)
        {
            return 0;
        }
    }
}