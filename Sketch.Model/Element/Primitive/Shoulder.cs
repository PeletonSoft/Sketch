using System;
using System.Windows;
using PeletonSoft.Tools.Model.SketchMath;
using PeletonSoft.Tools.Model.SketchMath.Wave;
using PeletonSoft.Tools.Model.SketchMath.Wave.WavyBorderBuilder;
using PeletonSoft.Tools.Model.SketchMath.Wave.WavyBorderBuilder.ExtraStrategy;

namespace PeletonSoft.Sketch.Model.Element.Primitive
{
    public sealed class Shoulder
    {
        public double Length { get; set; }
        public double OffsetY { get; set; }
        public double WaveHeight { get; set; }
        public double Slope { get; set; }

        public Func<double, Point> GetTransformer()
        {
            var angle = Math.Asin(Slope / Length);
            return x => new Point(x * Math.Cos(angle), x * Math.Sin(angle) + OffsetY);
        }
        public IWavyBorder<double> GetWavyBorder(
            Func<WavyBorderParameters, IExtraStrategy, IWavyBorderBuilder> createBuilder, int waveCount)
        {
            var parameters = new WavyBorderParameters(Length, WaveHeight, waveCount);
            var builder = createBuilder(parameters, new HalfStepExtraFinishStrategy());
            return builder.WavyBorder.Transform(pos => pos.X);
        }

    }
}
