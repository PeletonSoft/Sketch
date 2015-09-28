using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;

namespace PeletonSoft.Sketch.ViewModel.Factory
{
    public sealed class ScaleneSwagFactoryViewModel : WavySurfaceFactoryViewModel,
        IElementFactoryViewModel<ScaleneSwagViewModel>
    {
        protected override IElementViewModel CreateRawElement(IWorkspaceBit workspaceBit)
        {
            return new ScaleneSwagViewModel(workspaceBit, new Swag(PointCount));
        }

        public new ScaleneSwagViewModel CreateElement(IWorkspaceBit workspaceBit)
        {
            return (ScaleneSwagViewModel) base.CreateElement(workspaceBit);
        }
    }
}
