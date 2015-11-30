using System;
using System.Collections.Generic;
using PeletonSoft.Tools.Model.Register;
using PeletonSoft.Tools.Model.Setting;

namespace PeletonSoft.Sketch.Runner.Setting
{
    internal class SettingService : ISettingService
    {
        private static ISettingData _settingData;

        public ISettingData SettingData
        {
            get
            {
                return _settingData; 
            }
            set
            {
                _settingData = value;
            }
        }
    }

    internal class SettingServiceRegister : ISettingRegister
    {
        public void Register(IDictionary<string, object> settings)
        {
            var service = new SettingService();

            dynamic preferences = settings["Preferences"];
            dynamic assembly =  settings["Assembly"];
            var args = (string[])settings["Args"];

            bool readOnly;
            try
            {
                readOnly = Convert.ToBoolean(args[2]);
            }
            catch
            {
                readOnly = true;
            }

            var settingData = new SettingData
            {
                ConnectionString = preferences.ConnectionString,
                SavePath = preferences.SavePath,
                OrderId = Convert.ToInt32(args[0]),
                Folder = args[1],
                ReadOnly = readOnly,
                Version =  assembly.Version,
                ProgramName =  assembly.Name
            };

            service.SettingData = settingData;

        }
    }
}
