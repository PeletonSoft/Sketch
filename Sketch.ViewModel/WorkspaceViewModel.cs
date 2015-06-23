using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PeletonSoft.Sketch.ViewModel.Container;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.NotifyChanged;
using PeletonSoft.Tools.Model.Setting;

namespace PeletonSoft.Sketch.ViewModel
{
    public class WorkspaceViewModel : IWorkspaceViewModel
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

        public ISettingProvider SettingProvider { get; set; }

        public WorkspaceViewModel()
        {

            var workModes = new WorkModeViewModels(this);
            var presents = new PresentViewModels(this);
            WorkModes = workModes;
            Presents = presents;


            WorkMode = workModes.Editor;
            Present = presents.LayoutPresent;

            _saveCommandLazy = new Lazy<ICommand>(() =>
                CommandFactory.CreateCommand(Save,
                    () => !SettingProvider.GetSettingData().ReadOnly));
            _restoreCommandLazy = new Lazy<ICommand>(() =>
                CommandFactory.CreateCommand(Restore));
            _unselectCommandLazy = new Lazy<ICommand>(() =>
                CommandFactory.CreateCommand(() => ElementList.SelectedIndex = -1));

            ElementList = new ElementListViewModel(new WorkspaceBit(this));
        }

        public FactoryCollection Factories { get; set; }

        public IElementListViewModel ElementList { get; private set; }

        public IScreenViewModel Screen { get; set; }

        private IPresentViewModel _present;
        public IPresentViewModel Present
        {
            get { return _present; }
            set { SetField(ref _present, value); }
        }

        private byte[] _imageData;
        public byte[] ImageData
        {
            get { return _imageData; }
            set { SetField(ref _imageData, value); }
        }

        public IContainerOriginator<IWorkModeViewModel> WorkModes { get; private set; }

        private IWorkModeViewModel _workMode;
        public IWorkModeViewModel WorkMode
        {
            get{return _workMode;}
            set
            {
                if (SetField(ref _workMode, value))
                {
                    if (ElementList != null)
                    {
                        ElementList.SelectedIndex = -1;
                    }
                }
            }
        }
        public IContainerOriginator<IPresentViewModel> Presents { get; private set; }
        private readonly Lazy<ICommand> _saveCommandLazy;

        public ICommand SaveCommand
        {
            get { return _saveCommandLazy.Value; }
        }

        private readonly Lazy<ICommand> _restoreCommandLazy;

        public ICommand RestoreCommand
        {
            get { return _restoreCommandLazy.Value; }
        }
        private readonly Lazy<ICommand> _unselectCommandLazy;
        public ICommand UnselectCommand
        {
            get { return _unselectCommandLazy.Value; }
        }

        public void Save()
        {
            var settingData = SettingProvider.GetSettingData();

            if (settingData.ReadOnly)
            {
                return;
            }

            Caretaker.GetState(this);
            if (!Directory.Exists(settingData.SavePath))
            {
                return;
            }

            var path = settingData.GetOrderSavePath();

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            Caretaker.Save(path);
            ImageData.ExportToFile(Path.Combine(path, "content.png"));
        }

        public void Restore()
        {
            var settingData = SettingProvider.GetSettingData();
            var path = settingData.GetOrderSavePath();

            if (!Directory.Exists(path) ||
                !File.Exists(Path.Combine(path, "content.xml")))
            {
                return;
            }

            Caretaker.Load(path);
            Caretaker.SetState(this);
        }

        public WorkspaceCaretaker Caretaker { get; set; }
        public ICommandFactory CommandFactory { get; set; }

        public void RestoreDefault()
        {
            ElementList.Clear();
        }

    }

    public class WorkspaceCaretaker : Caretaker<WorkspaceViewModel>
    {
    }

}
