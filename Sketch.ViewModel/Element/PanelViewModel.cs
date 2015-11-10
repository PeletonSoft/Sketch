using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Element;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model.Logic;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Element
{
    public sealed class PanelViewModel : AlignableElementViewModel, IViewModel<Panel>, IOriginator<PanelDataTransfer>
    {

        public PanelViewModel(IWorkspaceBit workspaceBit, Panel model)
            : base(workspaceBit, model)
        {
        }

        public new Panel Model => (Panel) base.Model;

        PanelDataTransfer IOriginator<PanelDataTransfer>.CreateState() => new PanelDataTransfer();
        void IOriginator<PanelDataTransfer>.Save(PanelDataTransfer state) => base.Save(state);
        void IOriginator<PanelDataTransfer>.Restore(PanelDataTransfer state) => base.Restore(state);

        public override IElementDataTransfer CreateState() => (this as IOriginator<PanelDataTransfer>).CreateState();
        public override void Save(IElementDataTransfer state) => (this as IOriginator<PanelDataTransfer>).Save((PanelDataTransfer) state);
        public override void Restore(IElementDataTransfer state) => (this as IOriginator<PanelDataTransfer>).Restore((PanelDataTransfer)state);
    }
}
