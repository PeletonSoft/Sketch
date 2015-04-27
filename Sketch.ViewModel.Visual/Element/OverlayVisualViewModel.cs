using System.ComponentModel;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Visual;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Custom;
using PeletonSoft.Tools.Model;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element
{
    public class OverlayVisualViewModel : ElementVisualViewModel
    {
        public OverlayVisualViewModel(InjectContainer visualContainer, VisualOptions visualOptions, OverlayViewModel element) 
            : base(visualOptions, element)
        {
            Element.PropertyChanged += ElementOnPropertyChanged;
            VisualContainer = visualContainer;
        }

        private InjectContainer VisualContainer { get; set; }

        private void ElementOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            switch (args.PropertyName)
            {
                case "SelectedItem" :
                    OnPropertyChanged("OverlayElement");
                    OnPropertyChanged("Layout");
                    break;
                case "OverRect":
                    OnPropertyChanged("OverRect");
                    break;
                case "Layout":
                    OnPropertyChanged("Layout");
                    break;

            }
        }

        private new OverlayViewModel Element
        {
            get { return (OverlayViewModel) base.Element; }
        }

        public IElementVisualViewModel OverlayElement
        {
            get
            {
                if (Element.SelectedItem != null)
                {
                    var element = Element.SelectedItem;
                    return (IElementVisualViewModel) VisualContainer.Resolve(element.GetType(), element);
                }
                return null;
            }
        }

        public Rect OverRect
        {
            get { return VisualOptions.PixelPerUnit.Transform(Element.OveRect); }
        }
    }
}
