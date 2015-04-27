using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PeletonSoft.Sketch.ViewModel.Interface.Visual;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Visual
{
    public class ScreenVisualViewModel : IScreenVisualViewModel
    {
        #region implement INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(PropertyChanged, propertyName);
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            Action notificator = () => OnPropertyChanged(propertyName);
            return notificator.SetField(ref field, value);
        }

        #endregion


        public ScreenVisualViewModel(VisualOptions visualOptions, ScreenViewModel screen)
        {
            VisualOptions = visualOptions;
            Screen = screen;
            Screen.PropertyChanged += ScreenOnPropertyChanged;
        }

        private VisualOptions VisualOptions { get; set; }

        private void ScreenOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            switch (args.PropertyName)
            {
                case "Width":
                    OnPropertyChanged("Width");
                    break;
                case "Height":
                    OnPropertyChanged("Height");
                    break;
            }
        }

        public double Width
        {
            get
            {
                return VisualOptions.PixelPerUnit.Transform(Screen.Width);
            }
        }

        public double Height
        {
            get
            {
                return VisualOptions.PixelPerUnit.Transform(Screen.Height);
            }
        }
        private ScreenViewModel Screen { get; set; }

    }
}
