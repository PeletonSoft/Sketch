using System;
using System.Collections.Generic;
using System.Windows;
using PeletonSoft.Sketch.Model.Element.Primitive;
using PeletonSoft.Tools.Model.Draw;
using PeletonSoft.Tools.Model.Draw.Wave;
using PeletonSoft.Tools.Model.SketchMath;
using PeletonSoft.Tools.Model.SketchMath.ConnectStrategy;
using PeletonSoft.Tools.Model.SketchMath.Wave;
using PeletonSoft.Tools.Model.SketchMath.Wave.WavyBorderBuilder;
using PeletonSoft.Tools.Model.SketchMath.Wave.WavyBorderBuilder.ExtraStrategy;


namespace PeletonSoft.Sketch.Model.Element.Custom
{
    public abstract class SwagTail : AlignableElement
    {
        public Shoulder LeftShoulder { get; private set; }
        public Shoulder RightShoulder { get; private set; }
        public double IndentDepth { get; set; }
        public int WaveCount { get; set; }
        private int PointCount { get; set; }
        public SwagTail(int pointCount)
        {
            LeftShoulder = new Shoulder();
            RightShoulder = new Shoulder();
            PointCount = pointCount;
        }

        private IWavyBorder<double> GetVerticalWavyBorder()
        {
            Func<Position, double> transformer = pos => Height - pos.X;
            var parameters = new WavyBorderParameters(Height - IndentDepth, 1, WaveCount);
            var builder = new UprightWavyBorderBuilder(parameters, new HalfStepExtraStartStrategy());
            return builder.WavyBorder.Transform(transformer);
        }

        private IWavyBorder<Point> GetWawyBorder(Shoulder shoulder,
        Func<double, Shoulder, Point> transformer)
        {
            var wavyBorder = shoulder.GetWavyBorder((p, es) => new FoldingWavyBorderBuilder(p, es), WaveCount);
            return wavyBorder.Transform(x => transformer(x, shoulder));
        }

        public IWavyBorder<IEnumerable<Point>> GetWavySurface()
        {
            var leftWawyBorder = GetWawyBorder(LeftShoulder, 
                (z, sh) => sh.GetTransformer()(z).Transform(x => x, y => y));
            var rightWavyBorder = GetWawyBorder(RightShoulder,
                (z, sh) => sh.GetTransformer()(z).Transform(x => Width - x, y => y));

            var connectStrategy = GetVerticalWavyBorder()
                    .Transform(y0 => (IConnectStrategy)new CatenaryY0ConnectStrategy(y0, PointCount, true));
            var builder = new WavySurfaceBuilder(leftWawyBorder, rightWavyBorder, connectStrategy);
            var surface = builder.WavySurface;
            return surface;
        }

        public abstract IEnumerable<Point> GetCircuit();
    }
}
