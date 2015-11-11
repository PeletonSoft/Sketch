using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;

namespace PeletonSoft.Sketch.ViewModel.Element
{
    public sealed class PleatViewModel : PleatableViewModel
    {

        public PleatViewModel(IWorkspaceBit workspaceBit, Pleat model)
            : base(workspaceBit, model)
        {
        }

    }
}
