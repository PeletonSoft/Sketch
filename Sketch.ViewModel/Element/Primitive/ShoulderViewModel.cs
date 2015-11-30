using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PeletonSoft.Sketch.Model.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Element.Primitive;
using PeletonSoft.Tools.Model.Logic;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;
using static PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged.NotifyPropertyChangedHelper;

namespace PeletonSoft.Sketch.ViewModel.Element.Primitive
{
    public sealed class ShoulderViewModel : INotifyViewModel<Shoulder>, ISynchViewModel<ShoulderViewModel>, IOriginator<ShoulderDataTransfer>
    {
        #region implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) =>
            this.OnPropertyChanged(PropertyChanged, propertyName);
        private void SetField<T>(Func<T> getValue, Action<T> setValue, T value, [CallerMemberName] string propertyName = null) =>
            SetFieldValue(() => OnPropertyChanged(propertyName), getValue, setValue, value);
        #endregion

        #region implement ViewModel
        public Shoulder Model { get; }

        public void Synchronize(ShoulderViewModel destination)
        {
            this
                .SetPropertyChanged(nameof(Length), () => destination.Length = Length)
                .SetPropertyChanged(nameof(WaveHeight), () => destination.WaveHeight = WaveHeight)
                .SetPropertyChanged(nameof(OffsetY), () => destination.OffsetY = OffsetY)
                .SetPropertyChanged(nameof(Slope), () => destination.Slope = Slope);
        }

        #endregion

        public ShoulderViewModel(Shoulder model)
        {
            Model = model;
        }
        public double Length
        {
            get { return Model.Length; }
            set { SetField(() => Model.Length, v => Model.Length = v, value); }
        }
        public double OffsetY
        {
            get { return Model.OffsetY; }
            set { SetField(() => Model.OffsetY, v => Model.OffsetY = v, value); }
        }

        public double WaveHeight
        {
            get { return Model.WaveHeight; }
            set { SetField(() => Model.WaveHeight, v => Model.WaveHeight = v, value); }
        }
        public double Slope
        {
            get { return Model.Slope; }
            set { SetField(() => Model.Slope, v => Model.Slope = v, value); }
        }

        public ShoulderDataTransfer CreateState() => new ShoulderDataTransfer();
        public void Save(ShoulderDataTransfer state)
        {
            state.WaveHeight = WaveHeight;
            state.Length = Length;
            state.OffsetY = OffsetY;
            state.Slope = Slope;
        }
        public void Restore(ShoulderDataTransfer state)
        {
            WaveHeight = state.WaveHeight;
            Length = state.Length;
            OffsetY = state.OffsetY;
            Slope = state.Slope;

        }
    }
}
