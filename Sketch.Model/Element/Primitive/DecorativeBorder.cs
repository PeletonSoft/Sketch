using System.Collections.Generic;
using System.Windows;

namespace PeletonSoft.Sketch.Model.Element.Primitive
{
    public sealed class DecorativeBorder
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public IEnumerable<Point> Points { get; set; }
    }
}
