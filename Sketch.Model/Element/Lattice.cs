using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using PeletonSoft.Sketch.Model.Element.Custom;

namespace PeletonSoft.Sketch.Model.Element
{
    public sealed class Lattice : AlignableElement
    {
        public double CellWidth { get; set; }
        public double CellHeight { get; set; }

        public IEnumerable<Rect> GetLines()
        {
            var widthCount = (CellWidth > 0 && Width > 0)
                ? (int) Math.Floor(Width/CellWidth) + 1
                : 1;
            var heightCount = (CellHeight > 0 && Height > 0)
                ? (int) Math.Floor(Height/CellHeight) + 1
                : 1;
            var vertical = Enumerable.Range(0, widthCount)
                .Select(x => new Rect(new Point(x*CellWidth, 0), new Point(x*CellWidth, Height)));
            var horizontal = Enumerable.Range(0, heightCount)
                .Select(x => new Rect(new Point(0, x*CellHeight), new Point(Width, x*CellHeight)));
            return vertical.Concat(horizontal);

        }
    }
}
