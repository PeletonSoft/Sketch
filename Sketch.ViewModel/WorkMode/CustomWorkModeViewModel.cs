using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.WorkMode
{
    public class CustomWorkModeViewModel : IWorkModeViewModel
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

        public IWorkspaceViewModel Workspace { get; private set; }

        public CustomWorkModeViewModel(WorkspaceViewModel workspace)
        {
            Workspace = workspace;
        }



        public virtual void RestoreDefault()
        {
        }
    }
}
