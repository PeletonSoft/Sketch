using System.Collections.Generic;
using System.Linq;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Interface.Element;

namespace PeletonSoft.Sketch.ViewModel.Element.Custom
{
    public static class SwagViewModelHelper
    {
        public static IEnumerable<Point> GetCircuit(this ISwagViewModel swag)
        {
            {
                var bottoms = swag.WavySurface.Bottoms.ToList();
                return bottoms.Any()
                    ? bottoms.First().Start.Concat(new[]
                    {
                        bottoms.Last().Finish.Last(),
                        bottoms.Last().Finish.First()
                    })
                    : null;
            }
        }
    }
}