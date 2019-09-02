using System.Windows;

namespace SaveMerge
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Properties.Settings settings = SaveMerge.Properties.Settings.Default;
            if (settings.UpgradeRequired)
            {
                settings.Upgrade();
                settings.UpgradeRequired = false;
                settings.Save();
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            SaveMerge.Properties.Settings.Default.Save();
        }
    }
}
