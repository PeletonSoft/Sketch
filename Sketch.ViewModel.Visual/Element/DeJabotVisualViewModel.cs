using System.ComponentModel;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Primitive;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element
{
    public class DeJabotVisualViewModel : ElementVisualViewModel
    {
        public DeJabotVisualViewModel(VisualOptions visualOptions, DeJabotViewModel element)
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
            }
        }

        private new DeJabotViewModel Element
        {
            get { return (DeJabotViewModel) base.Element; }
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