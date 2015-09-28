using System.Windows;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Visual;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Custom;
using PeletonSoft.Tools.Model.Dependency;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element
{
    public sealed class OverlayVisualViewModel : ElementVisualViewModel, IElementVisualViewModel<OverlayViewModel>
    {
        public OverlayVisualViewModel(InjectContainer visualContainer, VisualOptions visualOptions,
            OverlayViewModel element)
            : base(visualOptions, element)
        {
            VisualContainer = visualContainer;
            Element
                .SetPropertyChanged(nameof(Element.Layout), () => OnPropertyChanged(nameof(Layout)))
                .SetPropertyChanged(nameof(Element.OverRect), () => OnPropertyChanged(nameof(OverRect)))
                .SetPropertyChanged(nameof(Element.SelectedItem),
                    () =>
                    {
                        OnPropertyChanged(nameof(OverlayElement));
                        OnPropertyChanged(nameof(Layout));
                    });
        }

        private InjectContainer VisualContainer { get; set; }

        public new OverlayViewModel Element => (OverlayViewModel) base.Element;

        public IElementVisualViewModel<IElementViewModel> OverlayElement
        {
            get
            {
                var element = Element.SelectedItem;
                return (IElementVisualViewModel<IElementViewModel>)VisualContainer.Resolve(element.GetType(), element);
            }
        }

        public Rect OverRect => VisualOptions.PixelPerUnit.Transform(Element.OverRect);
    }
}
