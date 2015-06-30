using System;
using System.Linq;
using System.Windows;

namespace PeletonSoft.Tools.Model.SketchMath
{
    public static class RectangleHelper
    {
        public static double FitRectangle(this Size size, double thickness)
        {
            var a = size.Width;
            var b = size.Height;
            var d = thickness;

            if (a < Polinom.Delta || b < Polinom.Delta || d < Polinom.Delta)
            {
                return 0;
            }

            var p = new[]
            {
                - (a*a + b*b - d*d)*d*d,
                4*a*b*d,
                - (a*a + b*b + 2*d*d),
                0,
                1
            };

            var x0 = a / 2 + b / 2;
            var solution = p.SolveAll(x0);

            if (solution == null || solution.Length == 0)
            {
                return 0;
            }

            var sv = solution
                .Where(x => x > -Polinom.Delta && x <= Math.Sqrt(a * a + b * b) + 1e-8)
                .Select(x => Math.Atan((a * d - b * x) / (b * d - a * x)))
                .Where(x => x >= -Polinom.Delta)
                .ToArray();

            return sv.Any() ? sv.First() : 0;
        }
    }
}
