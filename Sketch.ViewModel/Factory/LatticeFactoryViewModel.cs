using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;

namespace PeletonSoft.Sketch.ViewModel.Factory
{
    public class LatticeFactoryViewModel : ElementFactoryViewModel,
            IElementFactoryViewModel<LatticeViewModel>
    {
        protected override IElementViewModel CreateRawElement(IWorkspaceBit workspaceBit)
        {
            return new LatticeViewModel(workspaceBit, new Lattice());
        }

        public new LatticeViewModel CreateElement(IWorkspaceBit workspaceBit)
        {
            return (LatticeViewModel)base.CreateElement(workspaceBit);
        }
    }
}
