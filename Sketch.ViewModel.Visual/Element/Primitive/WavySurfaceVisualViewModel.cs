using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using PeletonSoft.Tools.Model.Logic;
using PeletonSoft.Tools.Model.SketchMath.Wave;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element.Primitive
{
    public class WavySurfaceVisualViewModel : IVisualViewModel
    {
        public WavySurfaceVisualViewModel(VisualOptions visualOptions, IWavyBorder<IEnumerable<Point>> element)
        {
            var pixelPerUnit = visualOptions.PixelPerUnit;
            var vavySurfaceTransform = element.Transform(pixelPerUnit.Transform);

            Func<IBottom<IEnumerable<Point>>, IEnumerable<Point>> transform = b => b.Start.Concat(b.Finish.Reverse()); 

            Peak = vavySurfaceTransform.Waves.Select(w => w.Peak);
            InSide = vavySurfaceTransform.Waves.Select(w => transform(w.InSide));
            OutSide = vavySurfaceTransform.Waves.Select(w => transform(w.OutSide));
            Bottom = vavySurfaceTransform.Bottoms.Select(transform);

        }

        public IEnumerable<IEnumerable<Point>> Peak { get; private set; }
        public IEnumerable<IEnumerable<Point>> InSide { get; private set; }
        public IEnumerable<IEnumerable<Point>> OutSide { get; private set; }
        public IEnumerable<IEnumerable<Point>> Bottom { get; private set; } 


    }
}
