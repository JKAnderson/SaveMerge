using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SaveMerge
{
    /// <summary>
    /// Interaction logic for SlotPanel.xaml
    /// </summary>
    public partial class SlotPanel : UserControl
    {
        public SlotPanel()
        {
            InitializeComponent();
        }

        private void UclParent_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Focus();
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var command = (RoutedCommand)e.Command;
            if (command.Name == "Copy" && DataContext != null)
                Clipboard.SetData("SlotPanel", DataContext);
            else if (command.Name == "Paste" && Clipboard.ContainsData("SlotPanel"))
                DataContext = Clipboard.GetData("SlotPanel") as SaveSlot;
            else if (command.Name == "Delete")
                DataContext = null;
        }
    }
}
