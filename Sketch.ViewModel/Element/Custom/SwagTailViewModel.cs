using System;
using System.Collections.Generic;
using System.Windows;
using PeletonSoft.Sketch.Model.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model.Logic;
using PeletonSoft.Tools.Model.NotifyChanged;
using PeletonSoft.Tools.Model.SketchMath.Wave;

namespace PeletonSoft.Sketch.ViewModel.Element.Custom
{
    public abstract class SwagTailViewModel : AlignableElementViewModel, INotifyViewModel<SwagTail>
    {
        public new SwagTail Model => (SwagTail) base.Model;

        protected ShoulderViewModel LeftShoulder { get; }
        protected ShoulderViewModel RightShoulder { get; }

        protected SwagTailViewModel(IWorkspaceBit workspaceBit, SwagTail model)
            : base(workspaceBit, model)

        {
            Height = 0.5*Screen.Height;
            Width = 0.5*Screen.Width;

            IndentDepth = 0.3*Layout.Height;
            WaveCount = 3;

            LeftShoulder = new ShoulderViewModel(Model.LeftShoulder);
            RightShoulder = new ShoulderViewModel(Model.RightShoulder);

            Action visualChanged = () =>
            {
                if (Width > 5e-4 && Height > 5e-4 && WaveCount > 0
                    && LeftShoulder.Length > 5e-4 && RightShoulder.Length > 5e-4)
                {
                    OnPropertyChanged(nameof(Circuit));
                    OnPropertyChanged(nameof(WavySurface));
                }
            };

            this
                .SetPropertyChanged(
                    new[]
                    {
                        nameof(Width), nameof(Height),
                        nameof(IndentDepth), nameof(WaveCount)
                    },
                    visualChanged)
                .PropertyIterate(
                    new[]
                    {
                        this.ExtractGetter(nameof(LeftShoulder), el => el.LeftShoulder),
                        this.ExtractGetter(nameof(RightShoulder), el => el.RightShoulder)
                    },
                    (shoulder, propertyName) => shoulder.SetPropertyChanged(
                        new[]
                        {
                            nameof(shoulder.Length), nameof(shoulder.WaveHeight),
                            nameof(shoulder.OffsetY), nameof(shoulder.Slope)
                        },
                        visualChanged));
        }

        public double IndentDepth
        {
            get { return Model.IndentDepth; }
            set { SetField(() => Model.IndentDepth, v => Model.IndentDepth = v, value); }
        }

        public int WaveCount
        {
            get { return Model.WaveCount; }
            set { SetField(() => Model.WaveCount, v => Model.WaveCount = v, value); }
        }

        public IEnumerable<Point> Circuit => Model.GetCircuit();
        public IWavyBorder<IEnumerable<Point>> WavySurface => Model.GetWavySurface();
    }
}
