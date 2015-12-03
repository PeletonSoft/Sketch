using PeletonSoft.Tools.Model.Setting;

namespace PeletonSoft.Tools.Model.Memento
{
    public interface ICaretakerFactory<T> where T : IDataTransfer
    {
        ICaretaker<T> CreateCareTaker(ISettingData settingData);
    }
}
