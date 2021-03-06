﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PeletonSoft.Sketch.Model;
using PeletonSoft.Sketch.Model.Interface;
using PeletonSoft.Sketch.ViewModel.Container;
using PeletonSoft.Sketch.ViewModel.DataTransfer;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Container;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Tools.Model.Collection;
using PeletonSoft.Tools.Model.Dependency;
using PeletonSoft.Tools.Model.File;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.ObjectEvent;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;
using PeletonSoft.Tools.Model.Setting;
using static PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged.NotifyPropertyChangedHelper;

namespace PeletonSoft.Sketch.ViewModel
{
    public sealed class WorkspaceViewModel : IWorkspaceViewModel
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

        #region implement IViewModel
        public IWorkspace Model { get; }
        #endregion

        #region implement IVisualOriginator
        public WorkspaceDataTransfer CreateState() => new WorkspaceDataTransfer();
        public void Save(WorkspaceDataTransfer state)
        {
            var settingData = SettingProvider.GetSettingData();

            state.Present = Presents.GetKeyByValue(Present);
            state.ProgramName = settingData.ProgramName;
            state.Version = settingData.Version;
            state.Screen = Screen.Save();
            state.ElementList = ElementList.Save();
            state.WorkModes = WorkModes.Save();
            state.Presents = Presents.Save();
        }

        public void Restore(WorkspaceDataTransfer state)
        {
            Presents.Restore(state.Presents);
            WorkModes.Restore(state.WorkModes);
            Present = (IPresentViewModel)Presents.GetValueByKeyOrDefault(state.Present);
            Screen.Restore(state.Screen);
            ElementList.Restore(state.ElementList);
        }

        public ImageBox ImageBox
        {
            get { return Model.ImageBox; }
            set { SetField(() => Model.ImageBox, v => Model.ImageBox = v, value); }
        }
        #endregion

        public ISettingProvider SettingProvider { get; set; }
        public ICaretakerFactory<WorkspaceDataTransfer> CaretakerFactory { get; set; }


        private readonly Lazy<ISettingData> _settingDataLazy;
        public ISettingData SettingData => _settingDataLazy.Value;

        private readonly Lazy<ICaretaker<WorkspaceDataTransfer>> _caretakerLazy;
        public ICaretaker<WorkspaceDataTransfer> Caretaker => _caretakerLazy.Value;

        private readonly Lazy<ICommand> _saveCommandLazy;
        public ICommand SaveCommand => _saveCommandLazy.Value;

        private readonly Lazy<ICommand> _restoreCommandLazy;
        public ICommand RestoreCommand => _restoreCommandLazy.Value;

        public ICommandFactory CommandFactory { get; set; }
        public IEnumerable<IElementFactoryViewModel<IElementViewModel>> Factories { get; set; }

        public IElementListViewModel ElementList { get; }

        private IScreenViewModel _screen;
        public IScreenViewModel Screen
        {
            get { return _screen; }
            set { SetField(ref _screen, value); }
        }

        public IPresentListViewModel Presents { get; }
        private IPresentViewModel _present;
        public IPresentViewModel Present
        {
            get { return _present; }
            set { SetField(ref _present, value); }
        }

        public IWorkModeListViewModel WorkModes { get; }
        private IWorkModeViewModel _workMode;
        public IWorkModeViewModel WorkMode
        {
            get { return _workMode; }
            set { SetField(ref _workMode, value); }
        }

        public WorkspaceViewModel()
        {
            Model = new Workspace();
            WorkModes = new WorkModeListViewModel(this);
            Presents = new PresentListViewModel(this);

            WorkMode = WorkModes.Default;
            Present = Presents.Default;

            _saveCommandLazy = new Lazy<ICommand>(() =>
                CommandFactory.CreateCommand(() => Caretaker.SaveToFile(this),
                    () => !SettingProvider.GetSettingData().ReadOnly));
            _restoreCommandLazy = new Lazy<ICommand>(() =>
                CommandFactory.CreateCommand(() => Caretaker.RestoreFromFile(this)));

            _settingDataLazy = new Lazy<ISettingData>(() => SettingProvider.GetSettingData());
            _caretakerLazy = new Lazy<ICaretaker<WorkspaceDataTransfer>>(
                () => CaretakerFactory.CreateCareTaker(SettingData));

            ElementList = new ElementListViewModel(new WorkspaceBit(this));
            this
                .SetPropertyChanged(nameof(WorkMode), () => ElementList.Unselect())
                .SetPropertyChanged(
                    this.ExtractGetter(nameof(Screen), el => el.Screen),
                    new[] {nameof(Screen.Width), nameof(Screen.Height)},
                    () => ElementList.Restore(ElementList.Save()));
        }
    }
}
