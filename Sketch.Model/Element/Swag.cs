using System.Collections.Generic;
using System.Linq;
using System.Windows;
using PeletonSoft.Sketch.Model.Element.Custom;

namespace PeletonSoft.Sketch.Model.Element
{
    public sealed class Swag : SwagTail
    {
        public Swag(int pointCount) : base(pointCount)
        {
        }

        public override IEnumerable<Point> GetCircuit()
        {
            var bottoms = GetWavySurface().Bottoms.ToList();
            return bottoms.Any()
                ? bottoms.First().Start
                    .Concat(
                        new[]
                        {
                            bottoms.Last().Finish.Last(),
                            bottoms.Last().Finish.First()
                        })
                : new List<Point>();
        }
    }
}
