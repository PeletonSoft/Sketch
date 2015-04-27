using System;
using System.Collections.Generic;
using System.Linq;

namespace PeletonSoft.Tools.Model.SketchMath
{
    public static class Polinom
    {
        public const double Delta = 1e-10;
        public const int Iteration = 1000;

        public static double GetValue(this double[] p, double x)
        {
            return p
                .Select((t, i) => t*Math.Pow(x, i))
                .Sum();
        }

        public static double[] Derivate(this double[] p)
        {
            if (p.Length <= 1)
            {
                return new double[0];
            }

            var r = new double[p.Length - 1];
            for (var i = 1; i < p.Length; i++)
            {
                r[i - 1] = i * p[i];
            }
            return r;
        }

        public static double? Solve(this double[] p, double x0)
        {
            var dp = p.Derivate();
            var ddp = dp.Derivate();

            for (var i = 0; i < Iteration; i++)
            {
                var f = p.GetValue(x0);

                if (Math.Abs(f) < Delta)
                {
                    return x0;
                }

                var df = dp.GetValue(x0);
                var ddf = ddp.GetValue(x0);
                x0 = x0 - f/df - f/df*f/df*ddf/df/2;
            }

            return null;
        }

        public static double[] Reduce(this double[] p, double x0)
        {
            if (p.Length < 3)
            {
                return new double[0];
            }
            var f = p.GetValue(x0);

            if (Math.Abs(f) > Delta)
            {
                return new double[0];
            }

            var n = p.Length - 1;
            var r = new double[n];
            r[n - 1] = p[n];

            for (var i = 1; i < n; i++)
            {
                r[n - 1 - i] = p[n - i] + x0 * r[n - i];
            }
            return r;
        }

        public static double[] SolveAll(this double[] p, double x0)
        {
            var solve = new List<double>();

            while (true)
            {
                var r = p.Solve(x0);
                if (r == null)
                {
                    return solve.ToArray();
                }
                solve.Add((double)r);

                if (p.Length <= 2)
                {
                    return solve.ToArray();
                }

                p = p.Reduce((double)r);

            }
        }

    }
}
