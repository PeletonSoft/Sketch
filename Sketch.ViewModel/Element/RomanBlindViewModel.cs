using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using PeletonSoft.Sketch.Model.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model.Draw.Wave;
using PeletonSoft.Tools.Model.NotifyChanged;
using PeletonSoft.Tools.Model.SketchMath.ConnectStrategy;
using PeletonSoft.Tools.Model.SketchMath.Wave;

namespace PeletonSoft.Sketch.ViewModel.Element
{
    public class RomanBlindViewModel : AlignableElementViewModel
    {
        public RomanBlindViewModel(IWorkspaceBit workspaceBit)
            : base(workspaceBit, new AlignableElement())
        {
            Height = workspaceBit.Screen.Height;
            Width = workspaceBit.Screen.Width/2;
            WaveCount = 5;
            CoulisseThickness = 0.1;
            DenseHeight = 0.8*Height;
            DecorativeBorder = new DecorativeBorderViewModel(workspaceBit) {Width = Width, Height = 0.3*Height};

            this.SetPropertyChanged(
                new[] {"Width", "Height", "CoulisseThickness", "DenseHeight", "WaveCount"},
                () =>
                {
                    OnPropertyChanged("WavySurface");
                    OnPropertyChanged("Points");
                    OnPropertyChanged("Circuit");
                });

            this.SetPropertyChanged("Width",
                () =>
                {
                    DecorativeBorder.Width = Width;
                });

            DecorativeBorder.SetPropertyChanged("Height",
                () =>
                {
                    OnPropertyChanged("WavySurface");
                    OnPropertyChanged("Circuit");
                });
            DecorativeBorder.SetPropertyChanged("Points",
                () => OnPropertyChanged("Points"));
            DecorativeBorder.ResetChains();
        }

        private double _coulisseThickness;
        public double CoulisseThickness
        {
            get { return _coulisseThickness; }
            set { SetField(ref _coulisseThickness, value); }
        }

        private double _denseHeight;
        public double DenseHeight
        {
            get { return _denseHeight; }
            set
            {
                var v = value;
                if (DecorativeBorder != null && v < DecorativeBorder.Height + WaveCount * CoulisseThickness)
                {
                    v = DecorativeBorder.Height + WaveCount*CoulisseThickness;
                }
                if (v > Height)
                {
                    v = Height;
                }
                SetField(ref _denseHeight, v);
            }
        }

        private int _waveCount;

        public int WaveCount
        {
            get { return _waveCount; }
            set { SetField(ref _waveCount, value); }
        }

        public double WaveLength
        {
            get { return (Height - DecorativeBorder.Height)/WaveCount - CoulisseThickness; }
        }

        public double StrainedWaveCount
        {
            get
            {
                return (int) Math.Floor(
                    (DenseHeight - DecorativeBorder.Height - WaveCount*CoulisseThickness)
                    /
                    (Height - DecorativeBorder.Height - WaveCount * CoulisseThickness)
                    *
                    WaveCount);
            }
        }

        public DecorativeBorderViewModel DecorativeBorder { get; set; }

        public double FirstFoldedWaveLength
        {
            get { return WaveLength - DenseHeight + DecorativeBorder.Height + WaveCount * CoulisseThickness + StrainedWaveCount * WaveLength; }
        }

        public IWavyBorder<IEnumerable<Point>> WavySurface
        {
            get
            {
                var h = 0.0;
                var bottoms = new List<IBottom<double>> {new Bottom<double>(0, 0)};
                var waves = new List<IWave<double>>();

                for(var i = 0; i < StrainedWaveCount && i < WaveCount; i++)
                {
                    
                    h += CoulisseThickness + WaveLength;
                    waves.Add(new Wave<double>(h - CoulisseThickness, h - CoulisseThickness - WaveLength, h - CoulisseThickness));
                    bottoms.Add(new Bottom<double>(h - CoulisseThickness, h));
                    
                }

                if (StrainedWaveCount < WaveCount)
                {
                    h += CoulisseThickness + (WaveLength - FirstFoldedWaveLength);
                    bottoms.Add(new Bottom<double>(h - CoulisseThickness, h));
                    waves.Add(new Wave<double>(h - CoulisseThickness + FirstFoldedWaveLength - WaveLength/2,
                        h - CoulisseThickness - (WaveLength - FirstFoldedWaveLength), h - CoulisseThickness));
                }

                for (var i = 0; i < WaveCount - StrainedWaveCount - 1; i++)
                {
                    h += CoulisseThickness;
                    waves.Add(new Wave<double>(h - CoulisseThickness + WaveLength / 2, h - CoulisseThickness, h - CoulisseThickness));
                    bottoms.Add(new Bottom<double>(h - CoulisseThickness, h));
                }

                var border = new WavyBorder<double>(waves, bottoms);
                var left = border.Transform(y => new Point(0, y));
                var right = border.Transform(y => new Point(Width, y));
                return left.Connect(right, new LineConnectStrategy());
            }
        }

        public IEnumerable<Point> Points
        {
            get
            {
                Func<Point, Point> transformer =
                    point => new Point(point.X, point.Y + DenseHeight - DecorativeBorder.Height);

                var decorative = DecorativeBorder.Points.Select(transformer);

                var points = new List<Point>
                {
                    new Point(0, DenseHeight - DecorativeBorder.Height),
                    new Point(Width, DenseHeight - DecorativeBorder.Height)
                };

                return points.Concat(decorative.Reverse());

            }
        }

        public IEnumerable<Point> Circuit
        {
            get
            {
                return new[]
                {
                    new Point(0, 0),
                    new Point(Width, 0),
                    new Point(Width, DenseHeight - DecorativeBorder.Height),
                    new Point(0, DenseHeight - DecorativeBorder.Height)
                };
            }
        }
    }
}
