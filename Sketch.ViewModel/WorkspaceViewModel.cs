using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Xml.Linq;
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
using PeletonSoft.Tools.Model.Memento.Serialize;
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

        public ISettingProvider SettingProvider { get; set; }
        public ICaretaker<WorkspaceViewModel> Caretaker { get; set; }
        public ICommandFactory CommandFactory { get; set; }
        public IEnumerable<IElementFactoryViewModel<IElementViewModel>> Factories { get; set; }

        public WorkspaceViewModel()
        {
            Model = new Workspace();
            WorkModes = new WorkModeListViewModel(this);
            Presents = new PresentListViewModel(this);

            WorkMode = WorkModes.Default;
            Present = Presents.Default;

            _saveCommandLazy = new Lazy<ICommand>(() =>
                CommandFactory.CreateCommand(SaveToFile,
                    () => !SettingProvider.GetSettingData().ReadOnly));
            _restoreCommandLazy = new Lazy<ICommand>(() =>
                CommandFactory.CreateCommand(RestoreFromFile));

            ElementList = new ElementListViewModel(new WorkspaceBit(this));
            this
                .SetPropertyChanged(nameof(WorkMode), () => ElementList.Unselect())
                .SetPropertyChanged(
                    this.ExtractGetter(nameof(Screen), el => el.Screen),
                    new[] {nameof(Screen.Width), nameof(Screen.Height)},
                    () => ElementList.Restore(ElementList.Save()));
        }

        public IElementListViewModel ElementList { get; }

        private IScreenViewModel _screen;
        public IScreenViewModel Screen
        {
            get { return _screen; }
            set { SetField( ref _screen, value); }
        }

        public IPresentListViewModel Presents { get; }
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

        public IWorkModeListViewModel WorkModes { get; }
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

        public void SaveToFile()
        {
            var settingData = SettingProvider.GetSettingData();

            if (settingData.ReadOnly)
            {
                return;
            }

            if (!Directory.Exists(settingData.SavePath))
            {
                return;
            }

            var path = settingData.GetOrderSavePath();

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var dataTransfer = this.Save();
            var serializer = new XmlSerializer(StandardXmlPrimitive.Primitives, dataTransfer);
            var xml = serializer.Serialize();

            xml.Save(Path.Combine(path, "content.xml"));
            ImageBox?.WriteToFile(Path.Combine(path, "content.png"));

            foreach (var file in serializer.List)
            {
                file.Value.WriteToFile(Path.Combine(path, file.Key));
            }
        }


        public void RestoreFromFile()
        {
            var settingData = SettingProvider.GetSettingData();
            var path = settingData.GetOrderSavePath();

            if (!Directory.Exists(path) ||
                !File.Exists(Path.Combine(path, "content.xml")))
            {
                return;
            }

            var xml = XElement.Load(Path.Combine(path, "content.xml"));

            var deserializer = new XmlDeserializer(StandardXmlPrimitive.Primitives,
                AppDomain.CurrentDomain.GetAssemblies()
                    .Where(x => x.GetName().Name == "Sketch.ViewModel.DataTransfer")
                    .SelectMany(x => x.GetTypes())
                    .Where(x => x.GetInterfaces().Any(i => i == typeof (IDataTransfer)))
                    .ToArray(),
                (fileName, size) =>
                {
                    var fullFileName = Path.Combine(path, fileName);
                    if (!File.Exists(fullFileName))
                    {
                        return null;
                    }
                    return new PngImageBox(
                        File.ReadAllBytes(fullFileName),
                        (int) size.Width, (int) size.Height);
                });

            var dataTransfer = (WorkspaceDataTransfer)deserializer.Deserialize(xml, typeof(WorkspaceDataTransfer));
            Restore(dataTransfer);
        }

        public WorkspaceDataTransfer CreateState() => new WorkspaceDataTransfer();

        public void Restore(WorkspaceDataTransfer state)
        {
            Presents.Restore(state.Presents);
            WorkModes.Restore(state.WorkModes);
            Present = (IPresentViewModel)Presents.GetValueByKeyOrDefault(state.Present);
            Screen.Restore(state.Screen);
            ElementList.Restore(state.ElementList);
        }

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
    }
}
