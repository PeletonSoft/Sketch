using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;

namespace PeletonSoft.Sketch.ViewModel.Factory
{
    public sealed class ScaleneTailFactoryViewModel : WavySurfaceFactoryViewModel,
        IElementFactoryViewModel<ScaleneTailViewModel>
    {
        protected override IElementViewModel CreateRawElement(IWorkspaceBit workspaceBit)
        {
            return new ScaleneTailViewModel(workspaceBit, new Tail(PointCount));
        }

        public new ScaleneTailViewModel CreateElement(IWorkspaceBit workspaceBit)
        {
            return (ScaleneTailViewModel)base.CreateElement(workspaceBit);
        }
    }
}
