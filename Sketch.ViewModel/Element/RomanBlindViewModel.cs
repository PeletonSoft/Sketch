using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows;
using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model.Logic;
using PeletonSoft.Tools.Model.NotifyChanged;
using PeletonSoft.Tools.Model.SketchMath.Wave;

namespace PeletonSoft.Sketch.ViewModel.Element
{
    public class RomanBlindViewModel : AlignableElementViewModel, INotifyViewModel<RomanBlind>
    {
        private void OnPropertyChanged<T>(Expression<Func<RomanBlindViewModel, T>> expression)
        {
            expression.OnPropertyChanged(OnPropertyChanged);
        }

        public new RomanBlind Model
        {
            get { return (RomanBlind) base.Model; }
        }

        public RomanBlindViewModel(IWorkspaceBit workspaceBit, RomanBlind model)
            : base(workspaceBit, model)
        {
            Height = workspaceBit.Screen.Height;
            Width = workspaceBit.Screen.Width/2;
            WaveCount = 5;
            CoulisseThickness = 0.1;
            DenseHeight = 0.8*Height;
            DecorativeBorder = new DecorativeBorderViewModel(workspaceBit, Model.DecorativeBorder) {Width = Width, Height = 0};

            this
                .SetPropertyChanged(
                    new[]
                    {
                        this.GetPropertyName(el => el.Width),
                        this.GetPropertyName(el => el.Height),
                        this.GetPropertyName(el => el.CoulisseThickness),
                        this.GetPropertyName(el => el.DenseHeight),
                        this.GetPropertyName(el => el.WaveCount)
                    },
                    () =>
                    {
                        OnPropertyChanged(el => el.WavySurface);
                        OnPropertyChanged(el => el.Points);
                        OnPropertyChanged(el => el.Circuit);
                    })
                .SetPropertyChanged(el => el.Width, () => { DecorativeBorder.Width = Width; });

            DecorativeBorder
                .SetPropertyChanged(db => db.Height,
                    () =>
                    {
                        DenseHeight = DenseHeight;
                        OnPropertyChanged(el => el.WavySurface);
                        OnPropertyChanged(el => el.Circuit);
                    })
                .SetPropertyChanged(db => db.Points, () => OnPropertyChanged(el => el.Points));

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
        public DecorativeBorderViewModel DecorativeBorder { get; private set; }

        public IWavyBorder<IEnumerable<Point>> WavySurface
        {
            get { return Model.GetWavySurface(); }
        }

        public IEnumerable<Point> Points
        {
            get { return Model.GetPoints(); }
        }

        public IEnumerable<Point> Circuit
        {
            get { return Model.GetCircuit(); }
        }
    }
}
