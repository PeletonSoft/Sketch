using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel
{
    public sealed class ScreenViewModel : IScreenViewModel
    {
        private double _width;
        public double Width
        {
            get { return _width; }
            set { SetField(ref _width, value); }
        }

        private double _height;
        public double Height
        {
            get { return _height; }
            set { SetField(ref _height, value); }
        }

        #region implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(PropertyChanged, propertyName);
        }

        private bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            Action notificator = () => OnPropertyChanged(propertyName);
            return notificator.SetField(ref field, value);
        }
        #endregion

        #region implement IOriginator
        public void RestoreDefault()
        {
        }
        #endregion

    }
}
