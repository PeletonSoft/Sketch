using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.NotifyChanged;
using PeletonSoft.Tools.Model.SketchMath;
using PeletonSoft.Tools.Model.SketchMath.Wave;
using PeletonSoft.Tools.Model.SketchMath.Wave.WavyBorderBuilder;
using PeletonSoft.Tools.Model.SketchMath.Wave.WavyBorderBuilder.ExtraStrategy;

namespace PeletonSoft.Sketch.ViewModel.Element.Primitive
{
    public class ShoulderViewModel : INotifyPropertyChanged, IOriginator
    {
        #region implement INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(PropertyChanged, propertyName);
        }

        private void SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            Action notificator = () => OnPropertyChanged(propertyName);
            notificator.SetField(ref field, value);
        }

        #endregion


        private double _length;
        public double Length
        {
            get { return _length; }
            set { SetField(ref _length, value); }
        }

        private double _offsetY;

        public double OffsetY
        {
            get { return _offsetY; }
            set { SetField(ref _offsetY, value); }
        }

        private double _waveHeight;

        public double WaveHeight
        {
            get { return _waveHeight; }
            set { SetField(ref _waveHeight, value); }
        }

        private double _slope;

        public double Slope
        {
            get { return _slope; }
            set { SetField(ref _slope, value); }
        }

        public double Angle
        {
            get { return Math.Asin(Slope/Length); }
        }

        public Func<double, Point> Transformer
        {
            get { return x => new Point(x*Math.Cos(Angle), x*Math.Sin(Angle) + OffsetY); }
        }
        public IWavyBorder<double> GetWavyBorder(
            Func<WavyBorderParameters,IExtraStrategy,IWavyBorderBuilder> createBuilder, int waveCount)
        {
            var parameters = new WavyBorderParameters(Length, WaveHeight, waveCount);
            var builder = createBuilder(parameters, new HalfStepExtraFinishStrategy());
            return builder.WavyBorder.Transform(pos => pos.X);
        }

        public void RestoreDefault()
        {
        }
    }
}
