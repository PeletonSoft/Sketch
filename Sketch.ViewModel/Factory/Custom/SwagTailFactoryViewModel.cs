using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Interface.Element;

namespace PeletonSoft.Sketch.ViewModel.Factory.Custom
{
    public abstract class SwagTailFactoryViewModel : CustomElementFactoryViewModel
    {
        public int PointCount { get; set; }

        protected override void TweakElement(IElementViewModel element)
        {
            base.TweakElement(element);
            ((SwagTailViewModel)element).PointCount = PointCount;
        }

    }
}
