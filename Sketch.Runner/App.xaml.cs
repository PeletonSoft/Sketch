using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Sketch.Runner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            /*
            var assembly = System.Reflection.Assembly.GetExecutingAssembly().GetName();
            var settings = new Dictionary<string, object>
            {
                {"Args", e.Args},
                {"Preferences", Settings.Default},
                {"Assembly", new {assembly.Name, Version = assembly.Version.ToString()}}
            };

            var register = (ISettingRegister)Resources["SettingRegister"];

            register.Register(settings);
            */
        }

    }
}
