using PeletonSoft.Tools.Model.Setting;

namespace PeletonSoft.Sketch.View.Setting
{
    public class SettingData : ISettingData
    {
        public string ConnectionString { get; set; }
        public string SavePath { get; set; }
        public int OrderId { get; set; }
        public bool ReadOnly { get; set; }
    }
}
