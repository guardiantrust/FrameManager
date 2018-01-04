using DataLayer;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FrameManager
{
    /// <summary>
    /// Interaction logic for ManageArches.xaml
    /// </summary>
    public partial class ManageArches : Window
    {
        private ListRepository _repo = new ListRepository();
        private Arch _selectedArch = new Arch();
        private List<Arch> _allArchs = new List<Arch>();

        public ManageArches()
        {
            InitializeComponent();
            DataContext = _selectedArch;
            GetAllArchs();
        }

        private void GetAllArchs()
        {
            _allArchs = _repo.GetItems<Arch>().ToList();
            lsvArch.ItemsSource = _allArchs.OrderBy(x => x.Min);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void lsvArch_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _selectedArch = lsvArch.SelectedValue as Arch;
            DataContext = _selectedArch;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (_allArchs.Count(x => x.Name == _selectedArch.Name) == 0 && !string.IsNullOrEmpty(_selectedArch.Name) && _selectedArch.Max > 0)
            {
                _selectedArch.Id = 0;
                _repo.InsertItem<Arch>(_selectedArch);
                GetAllArchs();
                _selectedArch = new Arch();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (_allArchs.Count(x => x.Name == _selectedArch.Name) == 0)
            {
                if (_selectedArch.Id > 0)
                {
                    _repo.UpdateItem<Arch>(_selectedArch.Id, _selectedArch);
                }
                else
                {
                    _repo.InsertItem<Arch>(_selectedArch);
                }
            }

            GetAllArchs();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this Arch?", "Are you sure?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _repo.DeleteItem<Arch>(((sender as Button).DataContext as Arch).Id);
                GetAllArchs();
            }
        }

        private void lsvEdge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedArch = lsvArch.SelectedValue as Arch;
            DataContext = _selectedArch;
        }

        private void TextBox_GotMouseCapture(object sender, MouseEventArgs e)
        {
            if (sender is TextBox)
            {
                (sender as TextBox).SelectAll();
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox)
            {
                (sender as TextBox).SelectAll();
            }
        }
    }
}