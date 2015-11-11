using System;
using System.Collections.Generic;
using System.Windows;
using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Element;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Tools.Model.Logic;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.ObjectEvent;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Element
{
    public sealed class TieBackViewModel : PleatableViewModel, INotifyViewModel<TieBack>, IClotheableViewModel
    {

        #region implment ICollectionItem

        public override void AfterInsert()
        {
            base.AfterInsert();
            DenseWidth = Math.Min(0.6*Sheet.Height, 0.6*Sheet.Width);
            OffsetY = 0.6*Sheet.Height;
        }

        #endregion

        #region implement IViewModel

        public new TieBack Model => (TieBack) base.Model;

        #endregion

        #region implement IClotheableViewModel

        public IClotheViewModel Clothe { get; }

        #endregion

        #region implement IOriginator

        public override IElementDataTransfer CreateState() => new TieBackDataTransfer();
        public override void Save(IElementDataTransfer state) => Save((TieBackDataTransfer)state);
        public override void Restore(IElementDataTransfer state) => Restore((TieBackDataTransfer)state);

        private void Save(TieBackDataTransfer state)
        {
            base.Save(state);
            state.OffsetX = OffsetX;
            state.OffsetY = OffsetY;
            state.Length = Length;
            state.Depth = Depth;
            state.DropHeight = DropHeight;
            state.Protrusion = Protrusion;
            state.LeftSide = LeftSide.Save();
            state.RightSide = RightSide.Save();
        }

        private void Restore(TieBackDataTransfer state)
        {
            base.Restore(state);
            OffsetX = state.OffsetX;
            OffsetY = state.OffsetY;
            Length = state.Length;
            Depth = state.Depth;
            DropHeight = state.DropHeight;
            Protrusion = state.Protrusion;
            LeftSide.Restore(state.LeftSide);
            RightSide.Restore(state.RightSide);
        }

        #endregion

        public TieBackViewModel(IWorkspaceBit workspaceBit, TieBack model) : base(workspaceBit, model)
        {
            Clothe = new ClotheViewModel(WorkspaceBit, Model.Clothe);

            LeftSide = new TieBackSideViewModel(Model.LeftSide) {Weight = 0.1};
            RightSide = new TieBackSideViewModel(Model.RightSide) {Weight = 0.5};


            Length = 0.25;
            Depth = 0.05;
            DropHeight = 0;

            this
                .SetPropertyChanged(
                    new[]
                    {
                        nameof(OffsetX), nameof(OffsetY), nameof(Protrusion),
                    },
                    () => OnPropertyChanged(nameof(WavySurface)))
                .SetPropertyChanged(
                    new[]
                    {
                        nameof(OffsetX), nameof(OffsetY), nameof(Alignment),
                        nameof(Protrusion), nameof(Sheet)
                    },
                    () =>
                    {
                        OnPropertyChanged(nameof(Rect));
                        OnPropertyChanged(nameof(Lane));
                    })
                .SetPropertyChanged(
                    new[]
                    {
                        nameof(Length), nameof(Depth), nameof(DropHeight)
                    },
                    () =>
                    {
                        OnPropertyChanged(nameof(Lane));
                        OnPropertyChanged(nameof(WavySurface));
                    })
                .PropertyIterate(
                    new[]
                    {
                        this.ExtractGetter(nameof(LeftSide), el => el.LeftSide),
                        this.ExtractGetter(nameof(RightSide), el => el.RightSide)
                    },
                    (side, propertyName) => side.SetPropertyChanged(
                        new[] {nameof(side.Weight), nameof(side.TailScatter)},
                        () => OnPropertyChanged(nameof(WavySurface))));
        }

        public TieBackSideViewModel LeftSide { get; }
        public TieBackSideViewModel RightSide { get; }

        public double Length
        {
            get { return Model.Length; }
            set { SetField(() => Model.Length, v => Model.Length = v, value); }
        }

        public double Depth
        {
            get { return Model.Depth; }
            set { SetField(() => Model.Depth, v => Model.Depth = v, value); }
        }

        public double DropHeight
        {
            get { return Model.DropHeight; }
            set { SetField(() => Model.DropHeight, v => Model.DropHeight = v, value); }
        }

        public double OffsetX
        {
            get { return Model.OffsetX; }
            set { SetField(() => Model.OffsetX, v => Model.OffsetX = v, value); }
        }

        public double OffsetY
        {
            get { return Model.OffsetY; }
            set { SetField(() => Model.OffsetY, v => Model.OffsetY = v, value); }
        }

        public double Protrusion
        {
            get { return Model.Protrusion; }
            set { SetField(() => Model.Protrusion, v => Model.Protrusion = v, value); }
        }

        public IEnumerable<Point> Lane => Model.GetLane();
    }
}
