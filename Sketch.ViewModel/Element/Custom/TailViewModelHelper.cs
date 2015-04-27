using System.Collections.Generic;
using System.Linq;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Interface.Element;

namespace PeletonSoft.Sketch.ViewModel.Element.Custom
{
    public static class TailViewModelHelper
    {
        public static IEnumerable<Point> GetCircuit(this ITailViewModel tail)
        {
            {
                var bottoms = tail.WavySurface.Bottoms.ToList();
                return bottoms.Any()
                    ? bottoms.First().Start.Concat(bottoms.Last().Finish.Reverse())
                    : null;
            }
        }
    }
}
