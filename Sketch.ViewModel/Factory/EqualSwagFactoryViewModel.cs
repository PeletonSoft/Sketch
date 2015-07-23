using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;

namespace PeletonSoft.Sketch.ViewModel.Factory
{
    public sealed class EqualSwagFactoryViewModel : SwagTailFactoryViewModel, 
        IElementFactoryViewModel<EqualSwagViewModel>
    {
        protected override IElementViewModel CreateRawElement(IWorkspaceBit workspaceBit)
        {
            return new EqualSwagViewModel(workspaceBit, new Swag(PointCount));
        }

        public new EqualSwagViewModel CreateElement(IWorkspaceBit workspaceBit)
        {
            return (EqualSwagViewModel)base.CreateElement(workspaceBit);
        }
    }
}
