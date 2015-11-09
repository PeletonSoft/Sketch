using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PeletonSoft.Sketch.Model;
using PeletonSoft.Sketch.Model.Interface;
using PeletonSoft.Sketch.ViewModel.Container;
using PeletonSoft.Sketch.ViewModel.DataTransfer;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Tools.Model.Collection;
using PeletonSoft.Tools.Model.Dependency;
using PeletonSoft.Tools.Model.File;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.Memento.Container;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;
using PeletonSoft.Tools.Model.Setting;
using static PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged.NotifyPropertyChangedHelper;

namespace PeletonSoft.Sketch.ViewModel
{
    public sealed class WorkspaceViewModel : IWorkspaceViewModel, IOriginator<WorkspaceDataTransfer>
    {
        #region implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) =>
            this.OnPropertyChanged(PropertyChanged, propertyName);

        private void SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null) =>
            SetFieldValue(() => OnPropertyChanged(propertyName), ref field, value);

        private void SetField<T>(Func<T> getValue, Action<T> setValue, T value, [CallerMemberName] string propertyName = null) =>
            SetFieldValue(() => OnPropertyChanged(propertyName), getValue, setValue, value);
        #endregion

        #region implement IOriginator
        public void RestoreDefault()
        {
            ElementList.Clear();
        }
        #endregion

        #region implement IViewModel
        public IWorkspace Model { get; }
        #endregion

        public ISettingProvider SettingProvider { get; set; }
        public ICaretaker<WorkspaceViewModel> Caretaker { get; set; }
        public ICommandFactory CommandFactory { get; set; }
        public IEnumerable<IElementFactoryViewModel<IElementViewModel>> Factories { get; set; }

        public WorkspaceViewModel()
        {
            Model = new Workspace();
            WorkModes = new WorkModeViewModels(this);
            Presents = new PresentViewModels(this);

            WorkMode = WorkModes.Default;
            Present = Presents.Default;

            _saveCommandLazy = new Lazy<ICommand>(() =>
                CommandFactory.CreateCommand(Save,
                    () => !SettingProvider.GetSettingData().ReadOnly));
            _restoreCommandLazy = new Lazy<ICommand>(() =>
                CommandFactory.CreateCommand(Restore));

            ElementList = new ElementListViewModel(new WorkspaceBit(this));
            this.SetPropertyChanged(nameof(WorkMode), () => ElementList.Unselect());
        }

        public IElementListViewModel ElementList { get; }
        public IScreenViewModel Screen { get; set; }

        public IContainerOriginator<IPresentViewModel> Presents { get; }
        private IPresentViewModel _present;
        public IPresentViewModel Present
        {
            get { return _present; }
            set { SetField(ref _present, value); }
        }

        public ImageBox ImageBox
        {
            get { return Model.ImageBox; }
            set { SetField(() => Model.ImageBox, v => Model.ImageBox = v, value); }
        }

        public IContainerOriginator<IWorkModeViewModel> WorkModes { get; }
        private IWorkModeViewModel _workMode;
        public IWorkModeViewModel WorkMode
        {
            get { return _workMode; }
            set { SetField(ref _workMode, value); }
        }

        
        private readonly Lazy<ICommand> _saveCommandLazy;
        public ICommand SaveCommand => _saveCommandLazy.Value;

        private readonly Lazy<ICommand> _restoreCommandLazy;
        public ICommand RestoreCommand => _restoreCommandLazy.Value;

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
            ImageBox?.WriteToFile(Path.Combine(path, "content.png"));
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

        void IOriginator<WorkspaceDataTransfer>.Restore(WorkspaceDataTransfer state)
        {
            Present = Presents.GetValueByKeyOrDefault(state.Present);
            WorkMode = WorkModes.GetValueByKeyOrDefault(state.WorkMode);
            Screen.Restore(state.Screen);
        }

        WorkspaceDataTransfer IOriginator<WorkspaceDataTransfer>.Save()
        {
            var settingData = SettingProvider.GetSettingData();

            var state = new WorkspaceDataTransfer
            {
                Present = Presents.GetKeyByValue(Present),
                WorkMode = WorkModes.GetKeyByValue(WorkMode),
                ProgramName = settingData.ProgramName,
                Version = settingData.Version,
                Screen = Screen.Save(),
            };

            return state;
        }
    }
}
