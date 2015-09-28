using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using PeletonSoft.Sketch.Model.ClotheStrategy;
using PeletonSoft.Sketch.Model.Element.Custom;
using PeletonSoft.Sketch.Model.Element.Primitive;
using PeletonSoft.Sketch.Model.Interface.Element;
using PeletonSoft.Tools.Model;
using PeletonSoft.Tools.Model.SketchMath;
using PeletonSoft.Tools.Model.SketchMath.ConnectStrategy;
using PeletonSoft.Tools.Model.SketchMath.Wave;
using PeletonSoft.Tools.Model.Draw.Wave;
using PeletonSoft.Tools.Model.Draw;

namespace PeletonSoft.Sketch.Model.Element
{
    public sealed class TieBack : Pleatable, IClotheable
    {
        #region implement IClotheable
        public IClothe Clothe { get; }
        #endregion

        public double Length { get; set; }
        public double Depth { get; set; }
        public double DropHeight { get; set; }
        public double OffsetX { get; set; }
        public double OffsetY { get; set; }
        public double Protrusion { get; set; }
        public TieBackSide LeftSide { get; }
        public TieBackSide RightSide { get; }
        private int PointCount { get; }

        public TieBack(int pointCount)
        {
            PointCount = pointCount;
            LeftSide = new TieBackSide();
            RightSide = new TieBackSide();
            Clothe = new Clothe(new TieBackClotheCalculateStrategy(this));
        }

        public override Rect GetRect()
        {
            return Alignment == ElementAlignment.Left
                    ? new Rect(-Protrusion, 0, Sheet.Width + Protrusion, Sheet.Height)
                    : new Rect(0, 0, Sheet.Width + Protrusion, Sheet.Height);
        }

        protected override Func<Point, Point> GetAlignmentTransform()
        {
            return point =>
                Alignment == ElementAlignment.Left
                    ? new Point(point.X + Protrusion, point.Y)
                    : new Point(Sheet.Width - point.X, point.Y);
        }

        private Point LaneUpF(double t)
        {
            var width = Math.Sqrt(Math.Pow(Length, 2) - Math.Pow(DropHeight, 2));
            return new Point(
                OffsetX + Depth / 2 * DropHeight / Length + t * width,
                OffsetY - Depth / 2 * width / Length + t * DropHeight);
        }

        private Point LaneDownF(double t)
        {
            var width = Math.Sqrt(Math.Pow(Length, 2) - Math.Pow(DropHeight, 2));
            return new Point(
                OffsetX - Depth / 2 * DropHeight / Length + t * width,
                OffsetY + Depth / 2 * width / Length + t * DropHeight);
        }

        public override IWavyBorder<IEnumerable<Point>> GetWavySurface()
        {
            var parentWavySurface = base.GetWavySurface();
            var wavySurface = parentWavySurface;
            var upBorder = wavySurface.Start();
            var border = upBorder.Normalize();

            var laneUpBorder = border.Transform(LaneUpF);
            var laneDownBorder = border.Transform(LaneDownF);
            var lengths = wavySurface.Transform(points => points.Length());


            var leftLength = LeftSide.GetLength(parentWavySurface.Waves.First(), LaneUpF(0));
            var rightLength = RightSide.GetLength(parentWavySurface.Waves.Last(), LaneUpF(1));

            var connectStrategy = border
                .Transform(t => (IConnectStrategy)new CatenaryLengthConnectStrategy
                    (leftLength + t * (rightLength - leftLength), PointCount, true));
            Func<double, double> angleF =
                t => ((LeftSide.TailScatter + RightSide.TailScatter) * t - LeftSide.TailScatter) / 180 * Math.PI;
            var tailBorder = border
                .Zip(laneDownBorder, lengths,
                    (t, p, l) => new Point(
                        p.X + l * Math.Sin(angleF(t)),
                        p.Y + l * Math.Cos(angleF(t))));

            return upBorder
                .Connect(laneUpBorder, connectStrategy)
                .Connect(laneDownBorder, new LineConnectStrategy())
                .Connect(tailBorder, new LineConnectStrategy())
                .Cut(lengths)
                .Transform(points => points.Select(GetAlignmentTransform()));
        }

        public IEnumerable<Point> GetLane()
        {
            var lane = new[] { LaneUpF(0), LaneUpF(1), LaneDownF(1), LaneDownF(0) };
            return lane.Select(GetAlignmentTransform());
        }
    }
}
