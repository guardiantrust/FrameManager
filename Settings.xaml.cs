using System.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace FrameManager
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        private Setting se = new Setting();
        public Settings()
        {
            InitializeComponent();
            se.DataLocation = Properties.Settings.Default.DataLocation;
            se.TenonLength = Properties.Settings.Default.TenonLength;
            DataContext = se;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.DataLocation = se.DataLocation;
            Properties.Settings.Default.TenonLength = se.TenonLength;

            Properties.Settings.Default.Save();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            dialog.Multiselect = false;
            dialog.InitialDirectory = se.DataLocation;
            CommonFileDialogResult result = dialog.ShowDialog();
            if(result == CommonFileDialogResult.Ok)
            {
                se.DataLocation = dialog.FileName;
            }
        }
    }

    public class Setting
    {
        public string DataLocation
        {
            get; set;
        }

        public double TenonLength
        {
            get; set;
        }
       
    }
}
