using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Custom;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element
{
    public class HardPelmetVisualViewModel : ElementVisualViewModel
    {
        public HardPelmetVisualViewModel(VisualOptions visualOptions, HardPelmetViewModel element) 
            : base(visualOptions, element)
        {
            Element.PropertyChanged += ElementOnPropertyChanged;
        }

        private new HardPelmetViewModel Element
        {
            get { return (HardPelmetViewModel)base.Element; }
        }

        private void ElementOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            switch (args.PropertyName)
            {
                case "Points":
                    OnPropertyChanged("Points");
                    break;
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
    }
}
