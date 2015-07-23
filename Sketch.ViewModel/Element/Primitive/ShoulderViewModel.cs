using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PeletonSoft.Sketch.Model.Element.Primitive;
using PeletonSoft.Tools.Model.Logic;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Element.Primitive
{
    public sealed class ShoulderViewModel : IOriginator, INotifyViewModel<Shoulder>, ISynchViewModel<ShoulderViewModel>
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

        private void SetField<T>(Func<T> getValue, Action<T> setValue, T value, [CallerMemberName] string propertyName = null)
        {
            Action notificator = () => OnPropertyChanged(propertyName);
            notificator.SetField(getValue, setValue, value);
        }
        #endregion
        
        #region implement IOriginator 
        public void RestoreDefault()
        {
        }
        #endregion

        #region implement ViewModel
        public Shoulder Model { get; private set; }

        public void Synchronize(ShoulderViewModel destination)
        {
            this
                .SetPropertyChanged(el => el.Length, () => destination.Length = Length)
                .SetPropertyChanged(el => el.WaveHeight, () => destination.WaveHeight = WaveHeight)
                .SetPropertyChanged(el => el.OffsetY, () => destination.OffsetY = OffsetY)
                .SetPropertyChanged(el => el.Slope, () => destination.Slope = Slope);
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

    }
}
