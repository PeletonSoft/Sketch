using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace PeletonSoft.Tools.Model
{
    public class PixelPerUnit
    {
        public int Value { get; set; }

        public double Transform(double size)
        {
            return size*Value;
        }

        public Point Transform(Point point)
        {
            return new Point(Transform(point.X), Transform(point.Y));
        }

        public Size Transform(Size size)
        {
            return new Size(Transform(size.Width), Transform(size.Height));
        }

        public Rect Transform(Rect rect)
        {
            return new Rect(Transform(rect.Location), Transform(rect.Size));
        }

        public IEnumerable<Point> Transform(IEnumerable<Point> points)
        {
            return points != null ? points.Select(Transform) : null;
        }

        public IEnumerable<Rect> Transform(IEnumerable<Rect> rects)
        {
            return rects != null ? rects.Select(Transform) : null;
        }

        public IEnumerable<IEnumerable<Point>> Transform(IEnumerable<IEnumerable<Point>> pointss)
        {
            return pointss != null ? pointss.Select(Transform) : null;
        }
    }
}
