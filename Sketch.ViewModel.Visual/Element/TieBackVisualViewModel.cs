using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Primitive;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element
{
    public class TieBackVisualViewModel : ElementVisualViewModel
    {
        public TieBackVisualViewModel(VisualOptions visualOptions, TieBackViewModel element) 
            : base(visualOptions, element)
        {
            Element.PropertyChanged += ElementOnPropertyChanged;
        }

        private void ElementOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Lane":
                    OnPropertyChanged("Lane");
                    break;
                case "WavySurface":
                    OnPropertyChanged("WavySurface");
                    break;
            }
        }

        private new TieBackViewModel Element
        {
            get
            {
                return (TieBackViewModel)base.Element;
            }
        }

        public IEnumerable<Point> Lane
        {
            get { return VisualOptions.PixelPerUnit.Transform(Element.Lane); }
        }

        public WavySurfaceVisualViewModel WavySurface
        {
            get
            {
                if (Element.WavySurface == null)
                {
                    return null;
                }

                var size = new Size(Element.Rect.Width, Element.Rect.Height);
                return new WavySurfaceVisualViewModel(VisualOptions, Element.WavySurface, size);
            }
        }

    }
}
