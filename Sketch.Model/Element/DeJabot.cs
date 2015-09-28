using System;
using System.Collections.Generic;
using System.Windows;
using PeletonSoft.Sketch.Model.Element.Custom;
using PeletonSoft.Tools.Model;
using PeletonSoft.Tools.Model.Draw.Wave;
using PeletonSoft.Tools.Model.SketchMath;
using PeletonSoft.Tools.Model.SketchMath.ConnectStrategy;
using PeletonSoft.Tools.Model.SketchMath.Wave;
using PeletonSoft.Tools.Model.SketchMath.Wave.WavyBorderBuilder;
using PeletonSoft.Tools.Model.SketchMath.Wave.WavyBorderBuilder.ExtraStrategy;

namespace PeletonSoft.Sketch.Model.Element
{
    public sealed class DeJabot : AlignableElement
    {
        public double SmallHeight { get; set; }
        public int WaveCount { get; set; }
        public double WaveHeight { get; set; }
        public ElementAlignment Alignment { get; set; }
        public ElementAlignment WaveAlignment { get; set; }

        private IWavyBorderBuilder GetWavyBorderBuilder()
        {
            return
                new FoldingWavyBorderBuilder(
                    new WavyBorderParameters(Width, WaveHeight, WaveCount),
                    new HalfStepExtraFinishStrategy());
        }

        private Func<double, double> GetXTransformer()
        {
            Func<double, double> transformX =
                x => WaveAlignment == ElementAlignment.Right ? Width - x : x;
            return transformX;
        }

        private Func<double, double> GetYTransformer()
        {
            var fullWidth = GetWavyBorderBuilder().FullWidth;
            Func<double, double> yTransformer = x =>
                WaveAlignment == Alignment
                    ? SmallHeight + x * (Height - SmallHeight) / fullWidth
                    : Height - x * (Height - SmallHeight) / fullWidth;
            return yTransformer;
        }

        private IWavyBorder<Point> GetUpWavyBorder()
        {
            var xTransformer = GetXTransformer();
            return GetWavyBorderBuilder().WavyBorder
                .Transform(pos => new Point(xTransformer(pos.X), 0));
        }

        private IWavyBorder<Point> GetDownWavyBorder()
        {
            var xTransformer = GetXTransformer();
            var yTransformer = GetYTransformer();
            return GetWavyBorderBuilder().WavyBorder
                .Transform(pos => new Point(xTransformer(pos.X), yTransformer(pos.Track)));
        }

        public IWavyBorder<IEnumerable<Point>> GetWavySurface()
        {
            var builder = new WavySurfaceBuilder(
                GetUpWavyBorder(),
                GetDownWavyBorder(),
                new LineConnectStrategy());
            return builder.WavySurface;
        }
    }
}
