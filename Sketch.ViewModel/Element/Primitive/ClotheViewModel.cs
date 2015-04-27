using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Element.Primitive
{
    public sealed class ClotheViewModel : IClotheViewModel
    {

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

        private double? _height;

        public double? Height
        {
            get { return _height; }
            set { SetField(ref _height, value); }
        }

        private double? _width;

        public double? Width
        {
            get { return _width; }
            set { SetField(ref _width, value); }
        }

        public double? Area
        {
            get { return Width != null && Height != null ? Width*Height : null; }
        }

        public ICommand CalculateCommand { get; private set; }

        public ClotheViewModel(IWorkspaceBit workspaceBit, IClotheCalculateStrategy strategy)
        {
            this.SetPropertyChanged(
                new[] {"Height", "Width"},
                () => OnPropertyChanged("Area"));
            var commandFactory = workspaceBit.CommandFactory;
            CalculateCommand = commandFactory.CreateCommand(() => strategy.Calculate(this));
        }

    }
}
