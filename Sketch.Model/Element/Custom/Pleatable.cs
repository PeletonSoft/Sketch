using System;
using System.Collections.Generic;
using System.Windows;
using PeletonSoft.Sketch.Model.Interface.Element;
using PeletonSoft.Tools.Model;
using PeletonSoft.Tools.Model.Draw.Wave;
using PeletonSoft.Tools.Model.SketchMath.ConnectStrategy;
using PeletonSoft.Tools.Model.SketchMath.Wave;
using PeletonSoft.Tools.Model.SketchMath.Wave.WavyBorderBuilder;
using PeletonSoft.Tools.Model.SketchMath.Wave.WavyBorderBuilder.ExtraStrategy;

namespace PeletonSoft.Sketch.Model.Element.Custom
{
    public abstract class Pleatable : IVisibleElement
    {
        #region implement IVisibleElement
        public string Description { get; set; }
        public double Opacity { get; set; }
        public bool Visibility { get; set; }
        #endregion

        public double DenseWidth { get; set; }
        public int WaveCount { get; set; }
        public ElementAlignment Alignment { get; set; }
        public ISheet Sheet { get; set; }

        public IEnumerable<Point> GetRenderArea()
        {
            if (!Visibility)
            {
                return null;
            }
            return new[]
            {
                new Point(0, 0),
                new Point(Sheet.Width, 0),
                new Point(Sheet.Width, Sheet.Height),
                new Point(0, Sheet.Height)
            };
        }

        protected Func<Point, Point> GetAlignmentTransform()
        {
            return point =>
                Alignment == ElementAlignment.Left
                    ? new Point(point.X, point.Y)
                    : new Point(Sheet.Width - point.X, point.Y);
        }

        public virtual IWavyBorder<IEnumerable<Point>> GetWavySurface()
        {
            var borderBuilder = new UprightWavyBorderBuilder(
                new WavyBorderParameters(DenseWidth, 1, WaveCount),
                new FixedExtraStrategy());
            var borderUp = borderBuilder.WavyBorder.Transform(pos => new Point(pos.X, 0));
            var borderDown = borderBuilder.WavyBorder.Transform(pos => new Point(pos.X, Sheet.Height));
            return borderUp.Connect(borderDown, new LineConnectStrategy());
        }

        public virtual Rect GetRect()
        {
            return new Rect(0, 0, Sheet.Width, Sheet.Height);
        }

    }
}