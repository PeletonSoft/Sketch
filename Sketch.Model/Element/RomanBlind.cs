using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using PeletonSoft.Sketch.Model.Element.Custom;
using PeletonSoft.Sketch.Model.Element.Primitive;
using PeletonSoft.Tools.Model.Draw.Wave;
using PeletonSoft.Tools.Model.SketchMath.ConnectStrategy;
using PeletonSoft.Tools.Model.SketchMath.Wave;

namespace PeletonSoft.Sketch.Model.Element
{
    public sealed class RomanBlind : AlignableElement
    {
        public double CoulisseThickness { get; set; }
        public double DenseHeight { get; set; }
        public int WaveCount { get; set; }
        public DecorativeBorder DecorativeBorder { get; }
        public RomanBlind()
        {
            DecorativeBorder = new DecorativeBorder();
        }

        public double CheckedDenseHeight(double denseHeight)
        {
            if (denseHeight > Height)
            {
                return Height;
            }
            if (denseHeight < DecorativeBorder.Height + WaveCount * CoulisseThickness)
            {
                return DecorativeBorder.Height + WaveCount * CoulisseThickness;
            }
            return denseHeight;
        }

        public IEnumerable<Point> GetPoints()
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

        public IEnumerable<Point> GetCircuit()
        {
            return new[]
            {
                new Point(0, 0),
                new Point(Width, 0),
                new Point(Width, DenseHeight - DecorativeBorder.Height),
                new Point(0, DenseHeight - DecorativeBorder.Height)
            };
        }

        private double GetWaveLength()
        {
            return (Height - DecorativeBorder.Height) / WaveCount - CoulisseThickness; 
        }

        private double GetStrainedWaveCount()
        {
            return (int) Math.Floor(
                (DenseHeight - DecorativeBorder.Height - WaveCount*CoulisseThickness)
                /
                (Height - DecorativeBorder.Height - WaveCount*CoulisseThickness)
                *
                WaveCount);
        }


        private double GetFirstFoldedWaveLength()
        {
            return GetWaveLength() - DenseHeight + 
                DecorativeBorder.Height + 
                WaveCount * CoulisseThickness + 
                GetStrainedWaveCount() * GetWaveLength(); 
        }

        public IWavyBorder<IEnumerable<Point>> GetWavySurface()
        {
            var h = 0.0;
            var bottoms = new List<IBottom<double>> { new Bottom<double>(0, 0) };
            var waves = new List<IWave<double>>();

            var waveLength = GetWaveLength();
            var strainedWaveCount = GetStrainedWaveCount();
            var firstFoldedWaveLength = GetFirstFoldedWaveLength();

            for (var i = 0; i < GetStrainedWaveCount() && i < WaveCount; i++)
            {

                h += CoulisseThickness + GetWaveLength();
                waves.Add(new Wave<double>(h - CoulisseThickness, h - CoulisseThickness - waveLength, h - CoulisseThickness));
                bottoms.Add(new Bottom<double>(h - CoulisseThickness, h));

            }

            if (strainedWaveCount < WaveCount)
            {
                h += CoulisseThickness + (waveLength - firstFoldedWaveLength);
                bottoms.Add(new Bottom<double>(h - CoulisseThickness, h));
                waves.Add(new Wave<double>(h - CoulisseThickness + firstFoldedWaveLength - waveLength / 2,
                    h - CoulisseThickness - (waveLength - firstFoldedWaveLength), h - CoulisseThickness));
            }

            for (var i = 0; i < WaveCount - strainedWaveCount - 1; i++)
            {
                h += CoulisseThickness;
                waves.Add(new Wave<double>(h - CoulisseThickness + waveLength / 2, h - CoulisseThickness, h - CoulisseThickness));
                bottoms.Add(new Bottom<double>(h - CoulisseThickness, h));
            }

            var border = new WavyBorder<double>(waves, bottoms);
            var left = border.Transform(y => new Point(0, y));
            var right = border.Transform(y => new Point(Width, y));
            return left.Connect(right, new LineConnectStrategy());
        }
    }
}
