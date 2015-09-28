using System.Collections.Generic;
using System.Linq;
using System.Windows;
using PeletonSoft.Sketch.Model.Element.Custom;
using PeletonSoft.Tools.Model.SketchMath.Wave;

namespace PeletonSoft.Sketch.Model.Element
{
    public sealed class Pleat : Pleatable
    {
        public override IWavyBorder<IEnumerable<Point>> GetWavySurface()
        {
            return base.GetWavySurface()
                .Transform(points => points.Select(GetAlignmentTransform()));
        }
    }
}
