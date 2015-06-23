using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Primitive;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element
{
    public class RomanBlindVisualViewModel : ElementVisualViewModel
    {
        public RomanBlindVisualViewModel(VisualOptions visualOptions, RomanBlindViewModel element)
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
                case "Points":
                    OnPropertyChanged("Points");
                    break;
                case "Circuit":
                    OnPropertyChanged("Circuit");
                    break;
            }
        }

        private new RomanBlindViewModel Element
        {
            get { return (RomanBlindViewModel)base.Element; }
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
        public IEnumerable<Point> Points
        {
            get
            {
                var pixelPerUnit = VisualOptions.PixelPerUnit;
                var points = pixelPerUnit.Transform(Element.Points);
                return points;
            }
        }
        public IEnumerable<Point> Circuit
        {
            get
            {
                var pixelPerUnit = VisualOptions.PixelPerUnit;
                var points = pixelPerUnit.Transform(Element.Circuit);
                return points;
            }
        }

    }
}


