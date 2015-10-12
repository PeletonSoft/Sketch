using PeletonSoft.Tools.Model.Setting;

namespace PeletonSoft.Sketch.Runner.Setting
{
    public class SettingProvider : ISettingProvider
    {
        public ISettingData GetSettingData()
        {
            var service = new SettingService();
            return service.SettingData;
        }
    }
}
