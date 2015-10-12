using System.Windows;

namespace PeletonSoft.Sketch.Runner
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
