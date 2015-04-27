using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Media3D;

namespace PeletonSoft.Tools.View.Converter
{
    public class Transfom3DConverter : IValueConverter

    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var points = (IEnumerable<Point>) value;
            return CalculateNonAffineTransform(points.ToArray());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static Matrix3D CalculateNonAffineTransform(Point[] points)
        {
            // Affine transform
            // ----------------
            // This matrix maps (0, 0) --> (x0, y0)
            //                  (0, 1) --> (x1, y1)
            //                  (1, 0) --> (x2, y2)
            //                  (1, 1) --> (x2 + x1 + x0, y2 + y1 + y0)
            var A = new Matrix3D
            {
                M11 = points[2].X - points[0].X,
                M12 = points[2].Y - points[0].Y,
                M21 = points[1].X - points[0].X,
                M22 = points[1].Y - points[0].Y,
                OffsetX = points[0].X,
                OffsetY = points[0].Y
            };

            // Calculate point (a, b) that get mapped by the affine transform to (x3, y3)
            var det = A.M11 * A.M22 - A.M12 * A.M21;
            var a = (A.M22 * points[3].X - A.M21 * points[3].Y +
                        A.M21 * A.OffsetY - A.M22 * A.OffsetX) / det;

            var b = (A.M11 * points[3].Y - A.M12 * points[3].X +
                        A.M12 * A.OffsetX - A.M11 * A.OffsetY) / det;

            // Non-affine transform
            // --------------------
            // This matrix maps (0, 0) --> (0, 0)
            //                  (0, 1) --> (0, 1)
            //                  (1, 0) --> (1, 0)
            //                  (1, 1) --> (a, b)

            var B = new Matrix3D
            {
                M11 = a/(a + b - 1), 
                M22 = b/(a + b - 1),
                M14 = a / (a + b - 1) - 1,
                M24 = b / (a + b - 1) - 1
            };

            return B * A;
        }

    }
}
