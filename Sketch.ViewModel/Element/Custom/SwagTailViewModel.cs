using System;
using System.Collections.Generic;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model.Draw;
using PeletonSoft.Tools.Model.Draw.Wave;
using PeletonSoft.Tools.Model.NotifyChanged;
using PeletonSoft.Tools.Model.SketchMath;
using PeletonSoft.Tools.Model.SketchMath.ConnectStrategy;
using PeletonSoft.Tools.Model.SketchMath.Wave;
using PeletonSoft.Tools.Model.SketchMath.Wave.WavyBorderBuilder;
using PeletonSoft.Tools.Model.SketchMath.Wave.WavyBorderBuilder.ExtraStrategy;

namespace PeletonSoft.Sketch.ViewModel.Element.Custom
{
    public abstract class SwagTailViewModel : AlignableElementViewModel
    {
        public SwagTailViewModel(IWorkspaceBit workspaceBit)
            : base(workspaceBit)
        {
            Height = 0.5*Screen.Height;
            Width = 0.5*Screen.Width;

            IndentDepth = 0.3*Layout.Height;
            WaveCount = 3;

            PointCount = 100;

            LeftShoulder = new ShoulderViewModel();
            RightShoulder = new ShoulderViewModel();

            this.SetPropertyChanged(
                new[] {"Width", "Height", "IndentDepth", "WaveCount"},
                VisualChanged);

            new[] {LeftShoulder, RightShoulder}
                .SetPropertyChanged(
                    new[] {"Length", "WaveHeight", "OffsetY", "Slope"},
                    VisualChanged);

        }

        private void VisualChanged()
        {
            if (Width > 5e-4 && Height > 5e-4 && WaveCount > 0
                && LeftShoulder.Length > 5e-4 && RightShoulder.Length > 5e-4)
            {
                OnPropertyChanged("Circuit");
                OnPropertyChanged("WavySurface");
            }
        }

        private double _indentDepth;
        public double IndentDepth
        {
            get { return _indentDepth; }
            set { SetField(ref _indentDepth, value); }
        }

        private int _waveCount;
        public int WaveCount
        {
            get { return _waveCount; }
            set { SetField(ref _waveCount, value); }
        }

        public int PointCount { get; set; }

        public abstract IEnumerable<Point> Circuit{get; }

        public IWavyBorder<IEnumerable<Point>> WavySurface
        {
            get
            {
                var connectStrategy = VerticalWavyBorder
                    .Transform(y0 => (IConnectStrategy) new CatenaryY0ConnectStrategy(y0, PointCount, true));
                var builder = new WavySurfaceBuilder(LeftWawyBorder, RightWavyBorder, connectStrategy);
                return builder.WavySurface;
            }
        }

        private IWavyBorder<double> VerticalWavyBorder
        {
            get
            {
                Func<Position, double> transformer = pos => Height - pos.X;
                var parameters = new WavyBorderParameters(Height - IndentDepth, 1, WaveCount);
                var builder = new UprightWavyBorderBuilder(parameters, new HalfStepExtraStartStrategy());
                return builder.WavyBorder.Transform(transformer);
            }
        }

        private IWavyBorder<Point> GetWawyBorder(ShoulderViewModel shoulder, 
            Func<double, ShoulderViewModel, Point> transformer)
        {
            var wavyBorder = shoulder.GetWavyBorder((p, es) => new FoldingWavyBorderBuilder(p, es), WaveCount);
            return wavyBorder.Transform(x => transformer(x, shoulder));
        }

        private IWavyBorder<Point> LeftWawyBorder
        {
            get { return GetWawyBorder(LeftShoulder, (z, sh) => sh.Transformer(z).Transform(x => x, y => y)); }
        }

        private IWavyBorder<Point> RightWavyBorder
        {
            get { return GetWawyBorder(RightShoulder,(z, sh) => sh.Transformer(z).Transform(x => Width - x, y => y)); }
        }

        protected ShoulderViewModel LeftShoulder { get; private set; }
        protected ShoulderViewModel RightShoulder { get; private set; }
    }
}
