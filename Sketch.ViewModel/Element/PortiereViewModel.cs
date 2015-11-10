using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Element;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Element
{
    public sealed class PortiereViewModel : SheetViewModel, IOriginator<PortiereDataTransfer>
    {

        public PortiereViewModel(IWorkspaceBit workspaceBit, Portiere model)
            : base(workspaceBit, model)
        {
        }

        PortiereDataTransfer IOriginator<PortiereDataTransfer>.CreateState()
        {
            return new PortiereDataTransfer();
        }

        void IOriginator<PortiereDataTransfer>.Save(PortiereDataTransfer state)
        {
            base.Save(state);
        }

        void IOriginator<PortiereDataTransfer>.Restore(PortiereDataTransfer state)
        {
            base.Restore(state);
        }

        public override IElementDataTransfer CreateState()
        {
            return (this as IOriginator<PortiereDataTransfer>).CreateState();
        }

        public override void Save(IElementDataTransfer state)
        {
            (this as IOriginator<PortiereDataTransfer>).Save((PortiereDataTransfer)state);
        }

        public override void Restore(IElementDataTransfer state)
        {
            (this as IOriginator<PortiereDataTransfer>).Restore((PortiereDataTransfer)state);
        }

    }
}
