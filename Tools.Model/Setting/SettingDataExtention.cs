using System.Globalization;
using System.IO;

namespace PeletonSoft.Tools.Model.Setting
{
    public static class SettingDataExtention
    {
        public static string GetOrderSavePath(this ISettingData settingData) => 
            Path.Combine(settingData.SavePath,settingData.Folder);
    }
}
