using System.Collections.Generic;
using System.Linq;
using System.Windows;
using PeletonSoft.Tools.Model.Draw;
using PeletonSoft.Tools.Model.SketchMath.Wave;

namespace PeletonSoft.Sketch.Model.Element.Primitive
{
    public class TieBackSide
    {
        public double TailScatter { get; set; }
        public double Weight { get; set; }

        public double GetLength(IWave<IEnumerable<Point>> wave, Point point)
        {
            var chain = wave.Peak.ToList();
            var min = chain.First().DistanceTo(point);
            var max = chain.Length();
            return min + Weight*(max - min);
        }
    }
}
