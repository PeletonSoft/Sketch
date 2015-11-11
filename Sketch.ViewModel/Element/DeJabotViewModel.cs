using System.Collections.Generic;
using System.Windows;
using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Element;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model;
using PeletonSoft.Tools.Model.Logic;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;
using PeletonSoft.Tools.Model.SketchMath.Wave;

namespace PeletonSoft.Sketch.ViewModel.Element
{
    public sealed class DeJabotViewModel : AlignableElementViewModel, INotifyViewModel<DeJabot>, IOriginator<DeJabotDataTransfer>
    {
        public new DeJabot Model => (DeJabot) base.Model;

        public DeJabotViewModel(IWorkspaceBit workspaceBit, DeJabot model)
            : base(workspaceBit, model)
        {
            Alignment = ElementAlignment.Left;
            Height = 0.5*Screen.Height;
            SmallHeight = 0.6*Height;
            Width = 0.5*Screen.Width;
            WaveCount = 5;
            WaveHeight = 1.3*Layout.Width/WaveCount;
            WaveAlignment = ElementAlignment.Right;

            this.SetPropertyChanged(
                new[]
                {
                    nameof(Width), nameof(Height),
                    nameof(SmallHeight), nameof(Alignment),
                    nameof(WaveCount), nameof(WaveHeight),
                    nameof(WaveAlignment)
                },
                () =>
                {
                    if (Width > 0.001 && Height > 0.005 && WaveCount > 0)
                    {
                        OnPropertyChanged(nameof(WavySurface));
                    }
                });
        }

        public double SmallHeight
        {
            get { return Model.SmallHeight; }
            set { SetField(() => Model.SmallHeight, v => Model.SmallHeight = v, value); }
        }

        public int WaveCount
        {
            get { return Model.WaveCount; }
            set { SetField(() => Model.WaveCount, v => Model.WaveCount = v, value); }
        }

        public double WaveHeight
        {
            get { return Model.WaveHeight; }
            set { SetField(() => Model.WaveHeight, v => Model.WaveHeight = v, value); }
        }

        public ElementAlignment Alignment
        {
            get { return Model.Alignment; }
            set { SetField(() => Model.Alignment, v => Model.Alignment = v, value); }
        }

        public ElementAlignment WaveAlignment
        {
            get { return Model.WaveAlignment; }
            set { SetField(() => Model.WaveAlignment, v => Model.WaveAlignment = v, value); }
        }

        public IWavyBorder<IEnumerable<Point>> WavySurface => Model.GetWavySurface();

        #region implement IOriginator
        DeJabotDataTransfer IOriginator<DeJabotDataTransfer>.CreateState() => new DeJabotDataTransfer();

        void IOriginator<DeJabotDataTransfer>.Save(DeJabotDataTransfer state)
        {
            base.Save(state);
            state.Alignment = Alignment;
            state.SmallHeight = SmallHeight;
            state.WaveAlignment = WaveAlignment;
            state.WaveHeight = WaveHeight;
            state.WaveCount = WaveCount;
        }

        void IOriginator<DeJabotDataTransfer>.Restore(DeJabotDataTransfer state)
        {
            base.Restore(state);
            Alignment = state.Alignment;
            SmallHeight = state.SmallHeight;
            WaveAlignment = state.WaveAlignment;
            WaveHeight = state.WaveHeight;
            WaveCount = state.WaveCount;
        }

        public override IElementDataTransfer CreateState() => 
            (this as IOriginator<DeJabotDataTransfer>).CreateState();
        public override void Save(IElementDataTransfer state) => 
            (this as IOriginator<DeJabotDataTransfer>).Save((DeJabotDataTransfer)state);
        public override void Restore(IElementDataTransfer state) =>
            (this as IOriginator<DeJabotDataTransfer>).Restore((DeJabotDataTransfer)state);
        #endregion
    }
}
