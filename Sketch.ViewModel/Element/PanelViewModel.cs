using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model.Logic;

namespace PeletonSoft.Sketch.ViewModel.Element
{
    public sealed class PanelViewModel : AlignableElementViewModel, IViewModel<Panel>
    {

        public PanelViewModel(IWorkspaceBit workspaceBit, Panel model)
            : base(workspaceBit, model)
        {
        }

        public new Panel Model => (Panel) base.Model;
    }
}
