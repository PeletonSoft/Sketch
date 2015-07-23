using System.Collections.Generic;
using System.Linq;
using System.Windows;
using PeletonSoft.Sketch.Model.Element.Custom;

namespace PeletonSoft.Sketch.Model.Element
{
    public sealed class Tail : SwagTail
    {
        public Tail(int pointCount) : base(pointCount)
        {
        }

        public override IEnumerable<Point> GetCircuit()
        {

            var bottoms = GetWavySurface().Bottoms.ToList();
            return bottoms.Any()
                ? bottoms.First().Start
                    .Concat(bottoms.Last().Finish.Reverse())
                : new List<Point>();
        }
    }
}
