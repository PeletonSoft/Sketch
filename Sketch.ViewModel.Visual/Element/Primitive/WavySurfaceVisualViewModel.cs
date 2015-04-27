using System.Collections.Generic;
using System.Linq;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Geometry.WavySurface;
using PeletonSoft.Sketch.ViewModel.Interface.Primitive;
using PeletonSoft.Tools.Model.SketchMath.Wave;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element.Primitive
{
    public class WavySurfaceVisualViewModel
    {
        public WavySurfaceVisualViewModel(VisualOptions visualOptions, IWavyBorder<IEnumerable<Point>> wavySurface, Size size)
        {
            var pixelPerUnit = visualOptions.PixelPerUnit;
            Width = pixelPerUnit.Transform(size.Width);
            Height = pixelPerUnit.Transform(size.Height);
            var vavySurfaceTransform = wavySurface.Transform(pixelPerUnit.Transform);

            IEnumerable<IWavySurfaceItemViewModel> bottoms = vavySurfaceTransform.Bottoms
                .Select(bottom => new BottomViewModel(bottom));
            IEnumerable<IWavySurfaceItemViewModel> waves = vavySurfaceTransform.Waves
                .Select(wave => new WaveViewModel(wave));

            Items = bottoms.Concat(waves);

        }
        public IEnumerable<IWavySurfaceItemViewModel> Items { get; private set; }
        public double Width { get; private set; }
        public double Height { get; private set; }    
    }
}
