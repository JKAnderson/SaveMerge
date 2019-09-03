using SoulsFormats;
using System;
using System.Media;
using System.Windows;
using System.Windows.Controls;

namespace SaveMerge
{
    /// <summary>
    /// Interaction logic for SavePanel.xaml
    /// </summary>
    public partial class SavePanel : UserControl
    {
        public string Header
        {
            get => gbxSave.Header.ToString();
            set => gbxSave.Header = value;
        }

        private SL2 Save => DataContext as SL2;

        public SavePanel()
        {
            InitializeComponent();
        }

        public void Load(string path, bool silent = false)
        {
            try
            {
                DataContext = new SL2(path);
                if (!silent)
                    SystemSounds.Asterisk.Play();
            }
            catch (Exception ex)
            {
                if (!silent)
                    MessageBox.Show($"Error loading save:\n{path}\n\n{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UserControl_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effects = DragDropEffects.Copy;
        }

        private void UserControl_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                Load(((string[])e.Data.GetData(DataFormats.FileDrop))[0]);
        }

        private void GbxSave_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            splSlots.Children.Clear();
            for (int i = 0; i < 10; i++)
            {
                SaveSlot slot = null;
                if (Save.Menu.OccupiedSlots[i])
                    slot = Save.Slots[i];
                splSlots.Children.Add(new SlotPanel() { DataContext = slot });
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                var slotPanel = (SlotPanel)splSlots.Children[i];
                var slot = slotPanel.DataContext as SaveSlot;
                if (slot == null)
                {
                    Save.Menu.OccupiedSlots[i] = false;
                }
                else
                {
                    Save.Menu.OccupiedSlots[i] = true;
                    Save.Slots[i] = slot;
                }
            }

            try
            {
                SFUtil.Backup(Save.Path);
                Save.Write();
                SystemSounds.Asterisk.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving save:\n{Save.Path}\n\n{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
