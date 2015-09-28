using System.ComponentModel;
using PeletonSoft.Sketch.ViewModel.Interface;

namespace PeletonSoft.Sketch.ViewModel.WorkMode
{
    public class CustomWorkModeViewModel : IWorkModeViewModel
    {
        #region implement INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public IWorkspaceViewModel Workspace { get; }

        public CustomWorkModeViewModel(IWorkspaceViewModel workspace)
        {
            Workspace = workspace;
        }



        public virtual void RestoreDefault()
        {
        }
    }
}
