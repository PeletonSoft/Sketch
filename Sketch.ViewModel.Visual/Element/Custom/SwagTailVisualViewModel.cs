using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Primitive;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element.Custom
{
    public class SwagTailVisualViewModel : ElementVisualViewModel
    {
        public SwagTailVisualViewModel(VisualOptions visualOptions, SwagTailViewModel element) 
            : base(visualOptions, element)
        {
            Element.PropertyChanged += ElementOnPropertyChanged;
        }

        private void ElementOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            switch (args.PropertyName)
            {
                case "WavySurface":
                    OnPropertyChanged("WavySurface");
                    break;
                case "Circuit":
                    OnPropertyChanged("Circuit");
                    break;
            }
        }

        private new SwagTailViewModel Element
        {
            get { return (SwagTailViewModel) base.Element; }
        }

        public IEnumerable<Point> Circuit
        {
            get
            {
                var pixelPerUnit = VisualOptions.PixelPerUnit;
                return pixelPerUnit.Transform(Element.Circuit);
            }
        }

        public WavySurfaceVisualViewModel WavySurface
        {
            get
            {
                if (Element.WavySurface == null)
                {
                    return null;
                }
                
                var size = new Size(Element.Width, Element.Height);
                return new WavySurfaceVisualViewModel(VisualOptions, Element.WavySurface, size);
            }
        }

    }
}
