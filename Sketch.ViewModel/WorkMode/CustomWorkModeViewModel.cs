using System.ComponentModel;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Sketch.ViewModel.DataTransfer.WorkMode;
using PeletonSoft.Sketch.ViewModel.Interface;
using static PeletonSoft.Tools.Model.ObjectEvent.EventAction;

namespace PeletonSoft.Sketch.ViewModel.WorkMode
{
    public abstract class CustomWorkModeViewModel : IWorkModeViewModel
    {
        #region implement INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public IWorkspaceViewModel Workspace { get; }

        protected CustomWorkModeViewModel(IWorkspaceViewModel workspace)
        {
            Workspace = workspace;
        }

        public virtual void RestoreDefault()
        {
        }

        public virtual IWorkModeDataTransfer CreateState() => new WorkModeDataTransfer();

        public virtual void Save(IWorkModeDataTransfer state) => DoNothing();

        public virtual void Restore(IWorkModeDataTransfer state) => DoNothing();
    }
}
