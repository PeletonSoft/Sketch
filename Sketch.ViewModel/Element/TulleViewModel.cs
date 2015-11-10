using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Element;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Element
{
    public sealed class TulleViewModel : SheetViewModel, IOriginator<TulleDataTransfer>
    {
        public TulleViewModel(IWorkspaceBit workspaceBit, Tulle model)
            : base(workspaceBit, model)
        {
        }

        public override IElementDataTransfer CreateState()
        {
            return (this as IOriginator<TulleDataTransfer>).CreateState();
        }

        public override void Save(IElementDataTransfer state)
        {
            (this as IOriginator<TulleDataTransfer>).Save((TulleDataTransfer)state);
        }

        public override void Restore(IElementDataTransfer state)
        {
            (this as IOriginator<TulleDataTransfer>).Restore((TulleDataTransfer)state);
        }

        TulleDataTransfer IOriginator<TulleDataTransfer>.CreateState()
        {
            return new TulleDataTransfer();
        }

        void IOriginator<TulleDataTransfer>.Save(TulleDataTransfer state)
        {
            base.Save(state);
        }

        void IOriginator<TulleDataTransfer>.Restore(TulleDataTransfer state)
        {
            base.Restore(state);
        }
    }
}
