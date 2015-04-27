using System;
using System.Collections.Generic;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model;
using PeletonSoft.Tools.Model.Draw.Wave;
using PeletonSoft.Tools.Model.NotifyChanged;
using PeletonSoft.Tools.Model.SketchMath;
using PeletonSoft.Tools.Model.SketchMath.ConnectStrategy;
using PeletonSoft.Tools.Model.SketchMath.Wave;
using PeletonSoft.Tools.Model.SketchMath.Wave.WavyBorderBuilder;
using PeletonSoft.Tools.Model.SketchMath.Wave.WavyBorderBuilder.ExtraStrategy;

namespace PeletonSoft.Sketch.ViewModel.Element
{
    public class DeJabotViewModel : AlignableElementViewModel
    {
        public DeJabotViewModel(IWorkspaceBit workspaceBit)
            : base(workspaceBit)
        {
            Alignment = ElementAlignment.Left;
            Height = 0.5*Screen.Height;
            SmallHeight = 0.6*Height;
            Width = 0.5*Screen.Width;
            WaveCount = 5;
            WaveHeight = 1.3*Layout.Width/WaveCount;
            WaveAlignment = ElementAlignment.Right;

            this.SetPropertyChanged(
                new[]
                {
                    "Width", "Height", "SmallHeight", "Alignment",
                    "WaveCount", "WaveHeight", "WaveAlignment"
                }, 
                VisualChanged);
        }


        private void VisualChanged()
        {
            if (Width > 0.001 && Height > 0.005 && WaveCount > 0)
            {
                OnPropertyChanged("WavySurface");
            }
        }

        private double _smallHeight;
        public double SmallHeight
        {
            get { return _smallHeight; }
            set { SetField(ref _smallHeight, value); }
        }

        private int _waveCount;
        public int WaveCount
        {
            get { return _waveCount; }
            set { SetField(ref _waveCount, value); }
        }

        private double _waveHeight;
        public double WaveHeight
        {
            get { return _waveHeight; }
            set { SetField(ref _waveHeight, value); }
        }

        private ElementAlignment _alignment;
        public ElementAlignment Alignment
        {
            get { return _alignment; }
            set { SetField(ref _alignment, value); }
        }

        private ElementAlignment _waveAlignment;
        public ElementAlignment WaveAlignment
        {
            get { return _waveAlignment; }
            set { SetField(ref _waveAlignment, value); }
        }

        public IWavyBorderBuilder WavyBorderBuilder
        {
            get
            {
                return
                    new FoldingWavyBorderBuilder(
                        new WavyBorderParameters(Width, WaveHeight, WaveCount),
                        new HalfStepExtraFinishStrategy());
            }
        }

        private Func<double, double> GetXTransformer()
        {
            Func<double, double> transformX =
                x => WaveAlignment == ElementAlignment.Right ? Layout.Width - x : x;
            return transformX;
        }

        private Func<double, double> GetYTransformer()
        {
            var fullWidth = WavyBorderBuilder.FullWidth;
            Func<double, double> yTransformer = x =>
                WaveAlignment == Alignment
                    ? SmallHeight + x * (Height - SmallHeight) / fullWidth
                    : Height - x * (Height - SmallHeight) / fullWidth;
            return yTransformer;
        }

        public IWavyBorder<Point> UpWavyBorder
        {
            get
            {
                var xTransformer = GetXTransformer();
                return WavyBorderBuilder.WavyBorder
                    .Transform(pos => new Point(xTransformer(pos.X), 0));
            }
        }

        public IWavyBorder<Point> DownWavyBorder
        {
            get
            {
                var xTransformer = GetXTransformer();
                var yTransformer = GetYTransformer();
                return WavyBorderBuilder.WavyBorder
                    .Transform(pos => new Point(xTransformer(pos.X), yTransformer(pos.Track)));
            }
        }

        public IWavyBorder<IEnumerable<Point>> WavySurface
        {
            get
            {
                var builder = new WavySurfaceBuilder(UpWavyBorder, DownWavyBorder, new LineConnectStrategy());
                return builder.WavySurface;
            }
        }

    }
}
