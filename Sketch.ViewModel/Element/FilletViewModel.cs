using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Element;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model.Logic;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Element
{
    public sealed class FilletViewModel : AlignableElementViewModel, IViewModel<Fillet>, IOriginator<FilletDataTransfer>
    {

        public FilletViewModel(IWorkspaceBit workspaceBit, Fillet model)
            : base(workspaceBit, model)
        {
            Height = 0.05;
        }

        public new Fillet Model => (Fillet) base.Model;

        public override IElementDataTransfer CreateState()
        {
            return (this as IOriginator<FilletDataTransfer>).CreateState();
        }

        public override void Save(IElementDataTransfer state)
        {
            (this as IOriginator<FilletDataTransfer>).Save((FilletDataTransfer)state);
        }

        public override void Restore(IElementDataTransfer state)
        {
            (this as IOriginator<FilletDataTransfer>).Restore((FilletDataTransfer)state);
        }

        FilletDataTransfer IOriginator<FilletDataTransfer>.CreateState()
        {
            return new FilletDataTransfer();
        }

        void IOriginator<FilletDataTransfer>.Save(FilletDataTransfer state)
        {
            base.Save(state);
        }

        void IOriginator<FilletDataTransfer>.Restore(FilletDataTransfer state)
        {
            base.Restore(state);
        }
    }
}
