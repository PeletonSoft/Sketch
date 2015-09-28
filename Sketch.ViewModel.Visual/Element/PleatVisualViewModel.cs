using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Visual;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Custom;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element
{
    public sealed class PleatVisualViewModel : PleatableVisualViewModel, IElementVisualViewModel<PleatViewModel>
    {
        public PleatVisualViewModel(VisualOptions visualOptions, PleatViewModel element)
            : base(visualOptions, element)
        {
        }

        public new PleatViewModel Element
        {
            get { return (PleatViewModel)base.Element; }
        }
    }
}
