using PeletonSoft.Sketch.Model.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;

namespace PeletonSoft.Sketch.ViewModel.Element.Custom
{
    public abstract class SheetViewModel : AlignableElementViewModel, ISheetElementViewModel
    {
        protected SheetViewModel(IWorkspaceBit workspaceBit, Sheet model)
            : base(workspaceBit, model)
        {
        }
    }
}
