namespace PeletonSoft.Tools.Model.SketchMath.Wave.WavyBorderBuilder
{
    public class WavyBorderParameters
    {
        public double Width { get; private set; }
        public int WaveCount { get; private set; }
        public double WaveHeight { get; private set; }

        public WavyBorderParameters(double width, double waveHeight, int waveCount)
        {
            Width = width;
            WaveCount = waveCount;
            WaveHeight = waveHeight;
        }
    }
}