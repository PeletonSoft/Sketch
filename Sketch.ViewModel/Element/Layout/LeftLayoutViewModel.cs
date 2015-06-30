using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;

namespace PeletonSoft.Sketch.ViewModel.Element.Layout
{
    public sealed class LeftLayoutViewModel : LayoutViewModel
    {

        public LeftLayoutViewModel(IWorkspaceBit workspaceBit, IAlignableElementViewModel element)
            : base(workspaceBit, element)
        {
        }

    }
}
