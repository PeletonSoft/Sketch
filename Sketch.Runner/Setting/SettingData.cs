﻿using PeletonSoft.Tools.Model.Setting;

namespace PeletonSoft.Sketch.Runner.Setting
{
    public class SettingData : ISettingData
    {
        public string ConnectionString { get; set; }
        public string SavePath { get; set; }
        public int OrderId { get; set; }
        public string Folder { get; set; }
        public bool ReadOnly { get; set; }
        public string ProgramName { get; set; }
        public string Version { get; set; }
    }
}
