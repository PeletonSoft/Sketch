using System.Collections.Generic;
using System.Windows;
using PeletonSoft.Tools.Model.SketchMath;
using PeletonSoft.Tools.Model.SketchMath.Wave;

namespace PeletonSoft.Tools.Model.Draw.Wave
{
    public class WavySurfaceBuilder
    {
        public WavySurfaceBuilder(IWavyBorder<Point> start,
            IWavyBorder<Point> finish,
            IWavyBorder<IConnectStrategy> connectStrategy)
        {
            WavySurface = GetWavySurface(start, finish, connectStrategy);
        }

        public WavySurfaceBuilder(IWavyBorder<Point> start,
            IWavyBorder<Point> finish,
            IConnectStrategy connectStrategy)
        {
            WavySurface = GetWavySurface(start, finish, connectStrategy);
        }

        public IWavyBorder<IEnumerable<Point>> WavySurface { get; private set; }

        public static IWavyBorder<IEnumerable<Point>> GetWavySurface(
            IWavyBorder<Point> start,
            IWavyBorder<Point> finish,
            IWavyBorder<IConnectStrategy> connectStrategy)
        {
            return start.Zip(finish, connectStrategy, (s, f, cs) => cs.Connect(s, f));
        }

        public static IWavyBorder<IEnumerable<Point>> GetWavySurface(
            IWavyBorder<Point> start,
            IWavyBorder<Point> finish,
            IConnectStrategy connectStrategy)
        {
            return start.Zip(finish, connectStrategy.Connect);
        }
    }
}
