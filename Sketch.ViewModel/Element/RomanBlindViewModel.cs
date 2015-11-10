using System.Collections.Generic;
using System.Windows;
using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Element;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model.Logic;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;
using PeletonSoft.Tools.Model.SketchMath.Wave;

namespace PeletonSoft.Sketch.ViewModel.Element
{
    public sealed class RomanBlindViewModel : AlignableElementViewModel, INotifyViewModel<RomanBlind>,
        IOriginator<RomanBlindDataTransfer>
    {

        #region implement IOriginator
        RomanBlindDataTransfer IOriginator<RomanBlindDataTransfer>.CreateState() => new RomanBlindDataTransfer();

        void IOriginator<RomanBlindDataTransfer>.Save(RomanBlindDataTransfer state)
        {
            base.Save(state);
            state.CoulisseThickness = CoulisseThickness;
            state.DenseHeight = DenseHeight;
            state.WaveCount = WaveCount;
            state.DecorativeBorder = DecorativeBorder.Save();
        }

        void IOriginator<RomanBlindDataTransfer>.Restore(RomanBlindDataTransfer state)
        {
            base.Restore(state);
            CoulisseThickness = state.CoulisseThickness;
            DenseHeight = state.DenseHeight;
            WaveCount = state.WaveCount;
            DecorativeBorder.Restore(state.DecorativeBorder);
        }

        public override IElementDataTransfer CreateState() =>
            (this as IOriginator<RomanBlindDataTransfer>).CreateState();

        public override void Save(IElementDataTransfer state) =>
            (this as IOriginator<RomanBlindDataTransfer>).Save((RomanBlindDataTransfer)state);

        public override void Restore(IElementDataTransfer state) =>
            (this as IOriginator<RomanBlindDataTransfer>).Restore((RomanBlindDataTransfer)state);

        #endregion
        public new RomanBlind Model => (RomanBlind) base.Model;

        public RomanBlindViewModel(IWorkspaceBit workspaceBit, RomanBlind model)
            : base(workspaceBit, model)
        {
            Height = workspaceBit.Screen.Height;
            Width = workspaceBit.Screen.Width/2;
            WaveCount = 5;
            CoulisseThickness = 0.1;
            DenseHeight = 0.8*Height;
            DecorativeBorder = new DecorativeBorderViewModel(workspaceBit, Model.DecorativeBorder)
            {
                Width = Width,
                Height = 0
            };

            this
                .SetPropertyChanged(
                    new[]
                    {
                        nameof(Width), nameof(Height), nameof(CoulisseThickness),
                        nameof(DenseHeight), nameof(WaveCount)
                    },
                    () =>
                    {
                        OnPropertyChanged(nameof(WavySurface));
                        OnPropertyChanged(nameof(Points));
                        OnPropertyChanged(nameof(Circuit));
                    })
                .SetPropertyChanged(nameof(Width), () => { DecorativeBorder.Width = Width; });

            DecorativeBorder
                .SetPropertyChanged(nameof(DecorativeBorder.Height),
                    () =>
                    {
                        DenseHeight = DenseHeight;
                        OnPropertyChanged(nameof(WavySurface));
                        OnPropertyChanged(nameof(Circuit));
                    })
                .SetPropertyChanged(nameof(DecorativeBorder.Points), () => OnPropertyChanged(nameof(Points)));

            DecorativeBorder.ResetChains();
        }

        public double CoulisseThickness
        {
            get { return Model.CoulisseThickness; }
            set { SetField(() => Model.CoulisseThickness, v => Model.CoulisseThickness = v, value); }
        }

        public double DenseHeight
        {
            get { return Model.DenseHeight; }
            set { SetField(() => DenseHeight, v => Model.DenseHeight = v, Model.CheckedDenseHeight(value)); }
        }

        public int WaveCount
        {
            get { return Model.WaveCount; }
            set { SetField(() => Model.WaveCount, v => Model.WaveCount = v, value); }
        }

        public DecorativeBorderViewModel DecorativeBorder { get; }

        public IWavyBorder<IEnumerable<Point>> WavySurface => Model.GetWavySurface();
        public IEnumerable<Point> Points => Model.GetPoints();
        public IEnumerable<Point> Circuit => Model.GetCircuit();
    }
}
