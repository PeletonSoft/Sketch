using System;
using System.Windows;
using PeletonSoft.Tools.Model.SketchMath;

namespace PeletonSoft.Sketch.Model.Element.Outline
{
    public class HexagonOutline : Primitive.Outline
    {
        public override Point[] GetPoints(Size size, double thickness)
        {
            var alpha = size.FitRectangle(thickness);
            return new[]
            {
                new Point(thickness*Math.Sin(alpha), 0),
                new Point(0, 0),
                new Point(0, thickness*Math.Cos(alpha)),
                new Point(size.Width - thickness*Math.Sin(alpha), size.Height),
                new Point(size.Width, size.Height),
                new Point(size.Width, size.Height - thickness*Math.Cos(alpha))
            };
        }

    }
}
