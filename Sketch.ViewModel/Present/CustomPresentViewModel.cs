using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Present
{
    public class CustomPresentViewModel : IPresentViewModel
    {
        #region implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(PropertyChanged, propertyName);
        }

        protected void SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            Action notificator = () => OnPropertyChanged(propertyName);
            notificator.SetField(ref field, value);
        }
        #endregion


        public IWorkspaceViewModel Workspace { get; set; }

        private double _zoom;

        public double Zoom
        {
            get { return _zoom; }
            set { SetField(ref _zoom, value); }
        }


        public CustomPresentViewModel(IWorkspaceViewModel workspace)
        {
            Workspace = workspace;
            Zoom = 1;
        }

        public virtual void RestoreDefault()
        {
        }
    }
}
