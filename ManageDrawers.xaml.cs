using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FrameManager
{
    /// <summary>
    /// Interaction logic for ManageDrawers.xaml
    /// </summary>
    public partial class ManageDrawers : Window
    {
        private ListRepository _repo = new ListRepository();
        private Drawer _selectedDrawer = new Drawer();
        private List<Drawer> _allDrawers = new List<Drawer>();

        public ManageDrawers()
        {
            InitializeComponent();
            DataContext = _selectedDrawer;
            GetAllDrawers();
        }

        private void GetAllDrawers()
        {
            _allDrawers = _repo.GetItems<Drawer>().ToList();
            lsvDrawer.ItemsSource = _allDrawers.OrderBy(x => x.Id);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void lsvDrawer_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _selectedDrawer = lsvDrawer.SelectedValue as Drawer;
            DataContext = _selectedDrawer;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (_allDrawers.Count(x => x.Name == _selectedDrawer.Name) == 0 && !string.IsNullOrEmpty(_selectedDrawer.Name))
            {
                _selectedDrawer.Id = 0;
                _repo.InsertItem<Drawer>(_selectedDrawer);
                GetAllDrawers();
                _selectedDrawer = new Drawer();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (_allDrawers.Count(x => x.Name == _selectedDrawer.Name) == 0)
            {
                if (_selectedDrawer.Id > 0)
                {

                    _repo.UpdateItem<Drawer>(_selectedDrawer.Id, _selectedDrawer);
                }
                else
                {
                    _repo.InsertItem<Drawer>(_selectedDrawer);
                }
            }

            GetAllDrawers();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this Drawer?", "Are you sure?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _repo.DeleteItem<Drawer>(((sender as Button).DataContext as Drawer).Id);
                GetAllDrawers();
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
