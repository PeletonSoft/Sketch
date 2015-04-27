using System.Collections.Generic;
using System.Windows;
using PeletonSoft.Sketch.View.Properties;
using PeletonSoft.Tools.Model.Register;

namespace PeletonSoft.Sketch.View
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
            Startup += Application_Startup;
        }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var settings = new Dictionary<string, object>
            {
                {"Args", e.Args},
                {"Prefereces", Settings.Default}
            };

            var register = (ISettingRegister)Resources["SettingRegister"];

            register.Register(settings);
        }
    }
}
