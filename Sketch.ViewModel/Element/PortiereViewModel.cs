using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Element;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Element
{
    public sealed class PortiereViewModel : SheetViewModel
    {

        public PortiereViewModel(IWorkspaceBit workspaceBit, Portiere model)
            : base(workspaceBit, model)
        {
        }
    }
}
