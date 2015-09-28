using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Visual;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Primitive;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element
{
    public sealed class DeJabotVisualViewModel : ElementVisualViewModel, IElementVisualViewModel<DeJabotViewModel>
    {
        public DeJabotVisualViewModel(VisualOptions visualOptions, DeJabotViewModel element)
            : base(visualOptions, element)
        {
            Element.SetPropertyChanged(nameof(Element.WavySurface), () => OnPropertyChanged(nameof(WavySurface)));
        }

        public new DeJabotViewModel Element => (DeJabotViewModel) base.Element;
        public WavySurfaceVisualViewModel WavySurface
        {
            get
            {
                if (Element.WavySurface == null)
                {
                    return null;
                }
                
                return new WavySurfaceVisualViewModel(VisualOptions, Element.WavySurface);
            }
        }
    }
}