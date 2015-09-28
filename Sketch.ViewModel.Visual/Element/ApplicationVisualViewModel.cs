using System.Collections.Generic;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Visual;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Custom;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element
{
    public sealed class ApplicationVisualViewModel : ElementVisualViewModel, IElementVisualViewModel<ApplicationViewModel>
    {
        public ApplicationVisualViewModel(VisualOptions visualOptions, ApplicationViewModel element)
            : base(visualOptions, element)
        {
            Element.SetPropertyChanged(nameof(Element.Points), () => OnPropertyChanged(nameof(Points)));
        }

        public new ApplicationViewModel Element => (ApplicationViewModel)base.Element;

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
