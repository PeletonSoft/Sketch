using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;

namespace PeletonSoft.Sketch.ViewModel.Element
{
    public class TulleViewModel : SheetViewModel
    {
        public TulleViewModel(IWorkspaceBit workspaceBit, Tulle model)
            : base(workspaceBit, model)
        {
        }
    }
}
