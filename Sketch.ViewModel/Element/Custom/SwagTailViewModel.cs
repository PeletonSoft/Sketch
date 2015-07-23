using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows;
using PeletonSoft.Sketch.Model.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model.Logic;
using PeletonSoft.Tools.Model.NotifyChanged;
using PeletonSoft.Tools.Model.SketchMath.Wave;
using Expression = System.Linq.Expressions.Expression;

namespace PeletonSoft.Sketch.ViewModel.Element.Custom
{
    public abstract class SwagTailViewModel : AlignableElementViewModel, INotifyViewModel<SwagTail>
    {
        private void OnPropertyChanged<T>(Expression<Func<SwagTailViewModel, T>> expression)
        {
            expression.OnPropertyChanged(OnPropertyChanged);
        }

        public new SwagTail Model
        {
            get { return (SwagTail) base.Model; }
        }

        protected ShoulderViewModel LeftShoulder { get; private set; }
        protected ShoulderViewModel RightShoulder { get; private set; }

        public SwagTailViewModel(IWorkspaceBit workspaceBit, SwagTail model)
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
                    OnPropertyChanged(el => el.Circuit);
                    OnPropertyChanged(el => el.WavySurface);
                }
            };

            this
                .SetPropertyChanged(
                    new[]
                    {
                        this.GetPropertyName(el => el.Width),
                        this.GetPropertyName(el => el.Height),
                        this.GetPropertyName(el => el.IndentDepth),
                        this.GetPropertyName(el => el.WaveCount)
                    },
                    visualChanged)
                .PropertyIterate(
                    new Expression<Func<SwagTailViewModel, ShoulderViewModel>>[]
                    {el => el.LeftShoulder, el => el.RightShoulder},
                    (shoulder, propertyName) => shoulder.SetPropertyChanged(
                        new[]
                        {
                            shoulder.GetPropertyName(sh => sh.Length),
                            shoulder.GetPropertyName(sh => sh.WaveHeight),
                            shoulder.GetPropertyName(sh => sh.OffsetY),
                            shoulder.GetPropertyName(sh => sh.Slope)
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

        public IEnumerable<Point> Circuit
        {
            get { return Model.GetCircuit(); }
        }

        public IWavyBorder<IEnumerable<Point>> WavySurface
        {
            get { return Model.GetWavySurface(); }
        }

    }
}
