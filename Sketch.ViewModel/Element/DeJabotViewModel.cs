using System.Collections.Generic;
using System.Windows;
using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model;
using PeletonSoft.Tools.Model.Logic;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;
using PeletonSoft.Tools.Model.SketchMath.Wave;

namespace PeletonSoft.Sketch.ViewModel.Element
{
    public sealed class DeJabotViewModel : AlignableElementViewModel, INotifyViewModel<DeJabot>
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
    }
}
