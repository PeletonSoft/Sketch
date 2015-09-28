using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Interface.Visual;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Primitive;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element.Custom
{
    public abstract class PleatableVisualViewModel : ElementVisualViewModel, IElementVisualViewModel<PleatableViewModel>
    {
        public new PleatableViewModel Element => (PleatableViewModel)base.Element;

        protected PleatableVisualViewModel(VisualOptions visualOptions, PleatableViewModel element) :
            base(visualOptions, element)
        {
            Element
                .SetPropertyChanged(nameof(Element.WavySurface), () => OnPropertyChanged(nameof(WavySurface)));
        }

        public WavySurfaceVisualViewModel WavySurface => new WavySurfaceVisualViewModel(VisualOptions, Element.WavySurface);
    }
}
