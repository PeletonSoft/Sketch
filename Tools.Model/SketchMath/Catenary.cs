using System.Collections.Generic;
using System.Windows;

namespace PeletonSoft.Tools.Model.SketchMath
{
    public class Catenary
    {
        public double X0 { get; private set; }
        public double Y0 { get; private set; }
        public double A { get; private set; }
        public double GetValue(double x)
        {
            return Y0 + A * (System.Math.Cosh((x - X0) / A) - 1);
        }

        public double GetLength(double x1, double x2)
        {
            return A * (System.Math.Sinh((x2 - X0) / A) - System.Math.Sinh((x1 - X0) / A));
        }

        public double GetTangent(double x)
        {
            return System.Math.Sinh((x - X0) / A);
        }

        public Catenary(double x0, double y0, double a)
        {
            A = a;
            X0 = x0;
            Y0 = y0;
        }

        public static Catenary FromTwoPoint(double x1, double y1, double x2, double y2, double a)
        {
            var v = 1 - System.Math.Exp(-(x2 - x1) / a);
            var d = y2 - y1;
            var det = (1 - v) * (a * a * v * v + d * d * (1 - v));
            var z = ((1 - v) * d + System.Math.Sqrt(det)) / (a * v * (1 - v));
            var x0 = x2 - a * System.Math.Log(z);
            var y0 = y2 - a * (System.Math.Cosh((x2 - x0) / a) - 1);
            return new Catenary(x0, y0, a);
        }

        public const int LimitDepth = 30;

        public static Catenary FromLength(double x1, double y1, double x2, double y2, double length)
        {
            double left = 1;

            var catenary = FromTwoPoint(x1, y1, x2, y2, left);
            var leftLength = catenary.GetLength(x1, x2);

            var right = left;
            var rightLength = leftLength;

            while (leftLength < length)
            {
                right = left;
                rightLength = leftLength;
                left = left / 2;
                catenary = FromTwoPoint(x1, y1, x2, y2, left);
                leftLength = catenary.GetLength(x1, x2);
            }

            while (rightLength > length)
            {
                left = right;
                right = right * 2;
                catenary = FromTwoPoint(x1, y1, x2, y2, right);
                rightLength = catenary.GetLength(x1, x2);
            }

            var limit = LimitDepth;
            while (limit > 0)
            {
                var middle = (left + right) / 2;
                catenary = FromTwoPoint(x1, y1, x2, y2, middle);
                var l = catenary.GetLength(x1, x2);
                if (l < length)
                {
                    right = middle;
                }
                else
                {
                    left = middle;
                }
                limit--;
            }


            var a = (left + right) / 2;
            return Catenary.FromTwoPoint(x1, y1, x2, y2, a);
        }

        public static Catenary FromY0(double x1, double y1, double x2, double y2, double y0)
        {
            double left = 1;
            var catenary = Catenary.FromTwoPoint(x1, y1, x2, y2, left);
            var leftY0 = catenary.Y0;
            var leftX0 = catenary.X0;

            while (leftY0 > y0 || leftX0 < x1 || leftX0 > x2)
            {
                left = left / 2;
                catenary = FromTwoPoint(x1, y1, x2, y2, left);
                leftY0 = catenary.Y0;
                leftX0 = catenary.X0;
            }

            var right = left;
            catenary = Catenary.FromTwoPoint(x1, y1, x2, y2, right);
            var rightY0 = catenary.Y0;
            var rightX0 = catenary.X0;

            var limit = LimitDepth;
            var step = 1.0;
            while (limit > 0 && (rightY0 < y0 || rightX0 < x1 || rightX0 > x2))
            {
                var next = right * (1 + step);
                catenary = Catenary.FromTwoPoint(x1, y1, x2, y2, next);
                if (catenary.X0 >= x1 && catenary.X0 <= x2)
                {
                    right = next;
                    rightY0 = catenary.Y0;
                    rightX0 = catenary.X0;
                }
                else
                {
                    step = step / 2;
                }
                limit--;
            }

            limit = LimitDepth;
            while (limit > 0)
            {
                var middle = (left + right) / 2;
                catenary = FromTwoPoint(x1, y1, x2, y2, middle);
                if (catenary.Y0 > y0)
                {
                    right = middle;
                }
                else
                {
                    left = middle;
                }
                limit--;
            }

            var a = (left + right) / 2;
            return FromTwoPoint(x1, y1, x2, y2, a);
        }

        public IEnumerable<Point> Points(double x1, double x2, int segmentCount)
        {
            var list = new List<Point>();
            for (var i = 0; i <= segmentCount; i++)
            {
                var alpha = ((double)i) / segmentCount;
                var x = x1 + (x2 - x1) * alpha;
                var y = GetValue(x);
                list.Add(new Point(x, -y));
            }
            return list;
        }
    }
}
