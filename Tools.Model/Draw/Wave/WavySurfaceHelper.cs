using System.Collections.Generic;
using System.Linq;
using System.Windows;
using PeletonSoft.Tools.Model.Collection;
using PeletonSoft.Tools.Model.SketchMath;
using PeletonSoft.Tools.Model.SketchMath.Wave;

namespace PeletonSoft.Tools.Model.Draw.Wave
{
    public static class WavySurfaceHelper
    {
        /// <summary>
        /// Vertical concat
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static IWavyBorder<IEnumerable<Point>> Glue(
            this IWavyBorder<IEnumerable<Point>> first,
            IWavyBorder<IEnumerable<Point>> second)
        {
            return first.Zip(second, (f, s) => f.Init().Concat(s));
        }

        /// <summary>
        /// Horizontal concat
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static IWavyBorder<IEnumerable<Point>> Merge(
            this IWavyBorder<IEnumerable<Point>> first,
            IWavyBorder<IEnumerable<Point>> second)
        {
            var left = first.Bottoms.Last();
            var right = second.Bottoms.First();
            var middle = new Bottom<IEnumerable<Point>>(left.Start, right.Finish); 
            var bottoms = (first.Bottoms.Init())
                .Concat(new[]{middle})
                .Concat(second.Bottoms.Tail());
            var waves = first.Waves.Concat(second.Waves);
            return new WavyBorder<IEnumerable<Point>>(waves,bottoms);
        }


        public static IWavyBorder<Point> Start(
            this IWavyBorder<IEnumerable<Point>> wavySurface)
        {
            return wavySurface.Transform(points => points.First());
        }

        public static IWavyBorder<Point> Finish(
            this IWavyBorder<IEnumerable<Point>> wavySurface)
        {
            return wavySurface.Transform(points => points.Last());
        }

        public static IWavyBorder<IEnumerable<Point>> Connect(
            this IWavyBorder<Point> start,
            IWavyBorder<Point> finish,
            IWavyBorder<IConnectStrategy> connectStrategy)
        {

            return WavySurfaceBuilder.GetWavySurface(start, finish, connectStrategy);
        }

        public static IWavyBorder<IEnumerable<Point>> Connect(
            this IWavyBorder<Point> start,
            IWavyBorder<Point> finish,
            IConnectStrategy connectStrategy)
        {
            return WavySurfaceBuilder.GetWavySurface(start, finish, connectStrategy);
        }

        public static IWavyBorder<IEnumerable<Point>> Connect(
            this IWavyBorder<IEnumerable<Point>> start,
            IWavyBorder<Point> finish,
            IWavyBorder<IConnectStrategy> connectStrategy)
        {

            return start.Glue(WavySurfaceBuilder.GetWavySurface(start.Finish(), finish, connectStrategy));
        }

        public static IWavyBorder<IEnumerable<Point>> Connect(
            this IWavyBorder<IEnumerable<Point>> start,
            IWavyBorder<Point> finish,
            IConnectStrategy connectStrategy)
        {
            return start.Glue(WavySurfaceBuilder.GetWavySurface(start.Finish(), finish, connectStrategy));
        }

        public static IWavyBorder<IEnumerable<Point>> Cut(
            this IWavyBorder<IEnumerable<Point>> wavySurface,
            double length)
        {
            return wavySurface.Transform(points => points.Cut(length));
        }

        public static IWavyBorder<IEnumerable<Point>> Cut(
            this IWavyBorder<IEnumerable<Point>> wavySurface,
            IWavyBorder<double> length)
        {
            return wavySurface.Zip(length, (points, l) => points.Cut(l));
        }

        public static double Length(this IWavyBorder<Point> border)
        {
            return border.Bottoms
                .SelectMany(bottom => new[] {bottom.Start, bottom.Finish})
                .Length();
        }

        public static Point Middle(this IBottom<Point> bottom)
        {
            return new Point(
                (bottom.Start.X + bottom.Finish.X)/2,
                (bottom.Start.Y + bottom.Finish.Y)/2);
        }

        public static double Middle(this IBottom<double> bottom)
        {
            return (bottom.Start + bottom.Finish)/2;
        }

        public static double Length(this IBottom<Point> bottom)
        {
            return bottom.Start.DistanceTo(bottom.Finish);
        }

        public static IWavyBorder<double> Normalize(this IWavyBorder<Point> border)
        {
            var length = border.Length();

            var waves = border.Waves.ToArray();
            var bottoms = border.Bottoms.ToArray();

            var resultBottoms = new List<IBottom<double>>();
            var resultWaves = new List<IWave<double>>();

            var current = 0.0;

            for (var i = 1; i <= waves.Length && i < bottoms.Length; i++)
            {
                var wave = waves[i - 1];
                var left = bottoms[i - 1];
                var rigth = bottoms[i];

                if (!resultBottoms.Any())
                {
                    current += left.Length();
                }

                var distance = (left.Finish).DistanceTo(rigth.Start);

                var leftB = new Bottom<double>(current - left.Length(), current)
                    .Transform(t => t/length);
                var rigthB = new Bottom<double>(current + distance, current + distance + rigth.Length())
                    .Transform(t => t/length);

                if (!resultBottoms.Any())
                {
                    resultBottoms.Add(leftB);
                }

                resultBottoms.Add(rigthB);

                var w = wave
                    .Transform(p => p.Projection(left.Middle(), rigth.Middle()))
                    .Transform(t => leftB.Middle() + (rigthB.Middle() - leftB.Middle())*t);

                resultWaves.Add(w);

                current +=  distance + rigth.Length();
            }
            return new WavyBorder<double>(resultWaves, resultBottoms);
        }

    }
}
