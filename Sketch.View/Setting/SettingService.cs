using System;
using System.Collections.Generic;
using PeletonSoft.Tools.Model.Register;
using PeletonSoft.Tools.Model.Setting;

// ReSharper disable once CheckNamespace
namespace PeletonSoft.Sketch.View.Setting
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
            var args = (string[])settings["Args"];

            bool readOnly;
            try
            {
                readOnly = Convert.ToBoolean(args[1]);
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
                ReadOnly = readOnly
            };

            service.SettingData = settingData;

        }
    }
}
