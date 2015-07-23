using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;

namespace PeletonSoft.Sketch.ViewModel.Factory
{
    public sealed class EqualTailFactoryViewModel : SwagTailFactoryViewModel, 
        IElementFactoryViewModel<EqualTailViewModel>
    {
        protected override IElementViewModel CreateRawElement(IWorkspaceBit workspaceBit)
        {
            return new EqualTailViewModel(workspaceBit, new Tail(PointCount));
        }

        public new EqualTailViewModel CreateElement(IWorkspaceBit workspaceBit)
        {
            return (EqualTailViewModel)base.CreateElement(workspaceBit);
        }
    }
}
