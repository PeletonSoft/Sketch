using System;
using System.Collections.Generic;
using System.Windows;

namespace SketchTools.Draw
{
    public static class RectHelper
    {
        public static IEnumerable<Point> ToPoints(this Rect rect)
        {
            return new[] {rect.TopLeft, rect.TopRight, rect.BottomRight, rect.BottomLeft};
        }
    }
}
