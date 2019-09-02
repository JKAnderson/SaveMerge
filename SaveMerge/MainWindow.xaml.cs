using System.Windows;

namespace SaveMerge
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly Properties.Settings Settings = Properties.Settings.Default;

        public MainWindow()
        {
            InitializeComponent();
            Left = Settings.WindowLeft;
            Top = Settings.WindowTop;
            Width = Settings.WindowWidth;
            Height = Settings.WindowHeight;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title = "SaveMerge " + System.Windows.Forms.Application.ProductVersion;
            if (Settings.WindowMaximized)
                WindowState = WindowState.Maximized;

            if (Settings.Save1Path != "")
                savePanel1.Load(Settings.Save1Path, true);

            if (Settings.Save2Path != "")
                savePanel2.Load(Settings.Save2Path, true);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Settings.WindowMaximized = WindowState == WindowState.Maximized;
            if (WindowState == WindowState.Normal)
            {
                Settings.WindowLeft = Left;
                Settings.WindowTop = Top;
                Settings.WindowWidth = Width;
                Settings.WindowHeight = Height;
            }
            else
            {
                Settings.WindowLeft = RestoreBounds.Left;
                Settings.WindowTop = RestoreBounds.Top;
                Settings.WindowWidth = RestoreBounds.Width;
                Settings.WindowHeight = RestoreBounds.Height;
            }

            if (savePanel1.DataContext != null)
                Settings.Save1Path = ((SL2)savePanel1.DataContext).Path;

            if (savePanel2.DataContext != null)
                Settings.Save2Path = ((SL2)savePanel2.DataContext).Path;
        }
    }
}
