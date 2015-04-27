namespace PeletonSoft.Tools.Model.SketchMath.Wave.WavyBorderBuilder
{
    public class WavyBorderOffset
    {
        public double A { get; set; }
        public double K { get; set; }

        public WavyBorderOffset()
        {
        }

        public WavyBorderOffset(double a, double k)
        {
            A = a;
            K = k;
        }
    }
}
