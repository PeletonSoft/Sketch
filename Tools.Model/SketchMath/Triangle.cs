using System.Windows;
using PeletonSoft.Tools.Model.Draw;

namespace PeletonSoft.Tools.Model.SketchMath
{
    public static class Triangle
    {
        public static Point SaveOrthogonal(this Point r, Point f, Point m)
        {
            var d =
                r.Y*r.Y -
                2*r.Y*f.Y +
                f.Y*f.Y +
                r.X*r.X -
                2*f.X*r.X +
                f.X*f.X;

            return
                new Point
                {
                    X = (
                        r.X*f.Y*f.Y -
                        f.X*r.Y*f.Y +
                        f.Y*f.X*m.Y -
                        r.X*r.Y*f.Y -
                        f.Y*r.X*m.Y +
                        r.X*r.Y*m.Y +
                        f.X*f.X*m.X +
                        r.X*r.X*m.X -
                        f.X*r.Y*m.Y +
                        f.X*r.Y*r.Y -
                        2*r.X*f.X*m.X)/d,
                    Y = (
                        -r.X*r.Y*f.X -
                        r.Y*f.X*m.X +
                        m.Y*r.Y*r.Y -
                        2*r.Y*m.Y*f.Y +
                        r.Y*r.X*m.X +
                        f.Y*f.X*m.X +
                        m.Y*f.Y*f.Y -
                        f.Y*r.X*m.X -
                        f.X*f.Y*r.X +
                        f.Y*r.X*r.X +
                        r.Y*f.X*f.X)/d
                };

        }

        public static Point SaveDiagonal(this Point r, Point f, Point m)
        {
            var d =
                -f.Y*m.Y +
                f.Y*f.Y +
                m.Y*r.Y -
                f.Y*r.Y +
                r.X*m.X -
                m.X*f.X -
                r.X*f.X +
                f.X*f.X;
            return
                new Point
                {
                    X = (
                        -r.Y*f.X*f.Y +
                        f.X*m.Y*r.Y +
                        r.X*f.Y*f.Y -
                        m.X*m.X*f.X -
                        f.X*m.Y*m.Y -
                        m.X*r.X*f.X -
                        2*r.X*m.Y*f.Y +
                        r.X*m.X*m.X +
                        r.X*m.Y*m.Y +
                        m.X*f.X*f.X +
                        f.Y*f.X*m.Y)/d,
                    Y = (
                        -f.Y*m.X*m.X +
                        m.X*r.X*f.Y -
                        f.Y*m.Y*m.Y +
                        f.Y*f.Y*m.Y +
                        r.Y*m.Y*m.Y +
                        f.Y*m.X*f.X +
                        r.Y*m.X*m.X -
                        2*r.Y*m.X*f.X -
                        m.Y*f.Y*r.Y +
                        r.Y*f.X*f.X -
                        f.X*r.X*f.Y)/d
                };
        }

        public static Point FindSymmetry(this Point m, Point f, Point r)
        {
            return new Point(f.X + r.X - m.X, f.Y + r.Y - m.Y);
        }

        public static Point SaveDiagonal(this IPoint r, IPoint f, IPoint m)
        {
            return r.ToPoint().SaveDiagonal(f.ToPoint(), m.ToPoint());
        }

        public static Point FindSymmetry(this IPoint m, IPoint f, IPoint r)
        {
            return m.ToPoint().FindSymmetry(f.ToPoint(), r.ToPoint());
        }

        public static Point SaveOrthogonal(this IPoint r, IPoint f, IPoint m)
        {
            return r.ToPoint().SaveOrthogonal(f.ToPoint(), m.ToPoint());
        }
    }
}
