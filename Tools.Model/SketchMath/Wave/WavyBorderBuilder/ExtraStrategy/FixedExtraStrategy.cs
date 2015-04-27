namespace PeletonSoft.Tools.Model.SketchMath.Wave.WavyBorderBuilder.ExtraStrategy
{
    public class FixedExtraStrategy : IExtraStrategy
    {
        public double ExtraStart { get; private set; }
        public double ExtraFinish { get; private set; }

        public double GetExtraStart(WavyBorderParameters parameters, WavyBorderOffset shareOffset)
        {
            return ExtraStart;
        }

        public double GetExtraFinish(WavyBorderParameters parameters, WavyBorderOffset shareOffset)
        {
            return ExtraFinish;
        }

        public FixedExtraStrategy(double extraStart = 0, double extraFinish = 0)
        {
            ExtraStart = extraStart;
            ExtraFinish = ExtraFinish;
        }
    }
}