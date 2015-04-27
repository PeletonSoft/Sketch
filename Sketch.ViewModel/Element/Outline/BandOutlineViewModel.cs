using System;
using System.Linq;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Element.Outline.Primitive;
using PeletonSoft.Tools.Model.SketchMath;

namespace PeletonSoft.Sketch.ViewModel.Element.Outline
{
    public class BandOutlineViewModel : OutlineViewModel
    {


        public override Point[] GetPoints(double width, double height, double thickness)
        {
            var a = width;
            var b = height;
            var d = thickness;

            if (a < Polinom.Delta || b < Polinom.Delta || d < Polinom.Delta)
            {
                return null;
            }

            var p = new[]
            {
                - (a*a + b*b - d*d)*d*d,
                4*a*b*d,
                - (a*a + b*b + 2*d*d),
                0,
                1
            };

            var x0 = a/2 + b/2;
            var s = p.SolveAll(x0);

            if (s == null || s.Length == 0)
            {
                return null;
            }

            var sv = s
                .Where(x => x > -Polinom.Delta)
                .Where(x => x <= Math.Sqrt(a*a + b*b) + 1e-8)
                .Select(x => new {x = x, alpha = Math.Atan((a*d - b*x)/(b*d - a*x))})
                .Where(x => x.alpha >= -Polinom.Delta)
                .ToArray();

            if (!sv.Any())
            {
                return null;
            }

            var solve = sv.First();

            return new[]
            {
                new Point(d*Math.Sin(solve.alpha), 0),
                new Point(0, d*Math.Cos(solve.alpha)),
                new Point(a - d*Math.Sin(solve.alpha), b),
                new Point(a, b - d*Math.Cos(solve.alpha)),

            };
        }

    }
}
