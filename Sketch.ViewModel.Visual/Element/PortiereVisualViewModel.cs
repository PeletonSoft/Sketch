using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Visual;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Custom;
using PeletonSoft.Tools.Model.Logic;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element
{
    public sealed class PortiereVisualViewModel : SheetVisualViewModel, IElementVisualViewModel<PortiereViewModel>
    {
        public PortiereVisualViewModel(VisualOptions visualOptions, PortiereViewModel element)
            : base(visualOptions, element)
        {
        }

        public new PortiereViewModel Element
        {
            get { return (PortiereViewModel) base.Element; }
        }
    }
}
