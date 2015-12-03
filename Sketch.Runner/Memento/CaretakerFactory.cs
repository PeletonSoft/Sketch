using System;
using PeletonSoft.Sketch.ViewModel.DataTransfer;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.Setting;

namespace PeletonSoft.Sketch.Runner.Memento
{
    public sealed class CaretakerFactory : ICaretakerFactory<WorkspaceDataTransfer>
    {
        public ICaretaker<WorkspaceDataTransfer> CreateCareTaker(ISettingData settingData) => 
            new Caretaker(settingData);
    }
}
