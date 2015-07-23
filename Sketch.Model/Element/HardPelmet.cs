using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using PeletonSoft.Sketch.Model.Element.Custom;
using PeletonSoft.Sketch.Model.Element.Primitive;

namespace PeletonSoft.Sketch.Model.Element
{
    public sealed class HardPelmet : AlignableElement
    {
        public DecorativeBorder DecorativeBorder { get; private set; }

        public HardPelmet()
        {
            DecorativeBorder = new DecorativeBorder();
        }

        public IEnumerable<Point> GetPoints()
        {
            Func<Point, Point> transformer =
                    point =>
                        new Point
                        {
                            X = point.X,
                            Y = point.Y + Height - DecorativeBorder.Height
                        };
            var decorative = DecorativeBorder.Points.Select(transformer);

            var points = new List<Point>
                {
                    new Point(0, 0),
                    new Point(Width, 0)
                };

            return points.Concat(decorative.Reverse());
        }
    }
}
