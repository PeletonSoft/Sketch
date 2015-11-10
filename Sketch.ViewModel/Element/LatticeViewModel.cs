using System.Collections.Generic;
using System.Windows;
using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Element;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Element
{
    public sealed class LatticeViewModel : AlignableElementViewModel, IOriginator<LatticeDataTransfer>
    {
        private new Lattice Model => (Lattice) base.Model;

        public double CellWidth
        {
            get { return Model.CellWidth; }
            set { SetField(() => Model.CellWidth, v => Model.CellWidth = v, value); }
        }

        public double CellHeight
        {
            get { return Model.CellHeight; }
            set { SetField(() => Model.CellHeight, v => Model.CellHeight = v, value); }
        }

        public IEnumerable<Rect> Lines => Model.GetLines();

        public LatticeViewModel(IWorkspaceBit workspaceBit, Lattice model)
            : base(workspaceBit, model)
        {
            CellHeight = 0.5;
            CellWidth = 0.5;
            this.SetPropertyChanged(
                new[]
                {
                    nameof(CellWidth), nameof(CellHeight),
                    nameof(Width), nameof(Height)
                },
                () => OnPropertyChanged(nameof(Lines)));
        }

        LatticeDataTransfer IOriginator<LatticeDataTransfer>.CreateState() => new LatticeDataTransfer();

        void IOriginator<LatticeDataTransfer>.Save(LatticeDataTransfer state)
        {
            base.Save(state);
            state.CellWidth = CellWidth;
            state.CellHeight = CellHeight;
        }

        void IOriginator<LatticeDataTransfer>.Restore(LatticeDataTransfer state)
        {
            base.Restore(state);
            CellWidth = state.CellWidth;
            CellHeight = state.CellHeight;
        }

        public override IElementDataTransfer CreateState() => (this as IOriginator<LatticeDataTransfer>).CreateState();

        public override void Save(IElementDataTransfer state) => (this as IOriginator<LatticeDataTransfer>).Save((LatticeDataTransfer) state);

        public override void Restore(IElementDataTransfer state) => (this as IOriginator<LatticeDataTransfer>).Restore((LatticeDataTransfer)state);
    }

}
