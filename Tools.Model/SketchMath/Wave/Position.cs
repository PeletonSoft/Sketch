namespace PeletonSoft.Tools.Model.SketchMath.Wave
{
    public class Position
    {
        public double X { get; private set; }
        public double Track { get; private set; }
        public Position(double x, double track)
        {
            X = x;
            Track = track;
        }
    }
}