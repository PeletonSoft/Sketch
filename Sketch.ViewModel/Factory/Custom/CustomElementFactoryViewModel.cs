using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;

namespace PeletonSoft.Sketch.ViewModel.Factory.Custom
{
    public abstract class CustomElementFactoryViewModel
    {
        protected virtual void TweakElement(IElementViewModel element)
        {
        }
        protected abstract IElementViewModel CreateRawElement(IWorkspaceBit workspaceBit);

        protected IElementViewModel CreateElement(IWorkspaceBit workspaceBit)
        {
            var element = CreateRawElement(workspaceBit);
            TweakElement(element);
            return element;
        }
    }
}
