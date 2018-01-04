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
    /// Interaction logic for ManageCathedrals.xaml
    /// </summary>
    public partial class ManageCathedrals : Window
    {
        private ListRepository _repo = new ListRepository();
        private Cathedral _selectedCathedral = new Cathedral();
        private List<Cathedral> _allCathedrals = new List<Cathedral>();

        public ManageCathedrals()
        {
            InitializeComponent();
            DataContext = _selectedCathedral;
            GetAllCathedrals();
        }

        private void GetAllCathedrals()
        {
            _allCathedrals = _repo.GetItems<Cathedral>().ToList();
            lsvCathedral.ItemsSource = _allCathedrals.OrderBy(x => x.Min);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void lsvCathedral_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _selectedCathedral = lsvCathedral.SelectedValue as Cathedral;
            DataContext = _selectedCathedral;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (_allCathedrals.Count(x => x.Name == _selectedCathedral.Name) == 0 && !string.IsNullOrEmpty(_selectedCathedral.Name) && _selectedCathedral.Max > 0)
            {
                _selectedCathedral.Id = 0;
                _repo.InsertItem<Cathedral>(_selectedCathedral);
                GetAllCathedrals();
                _selectedCathedral = new Cathedral();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (_allCathedrals.Count(x => x.Name == _selectedCathedral.Name) == 0)
            {
                if (_selectedCathedral.Id > 0)
                {
                    _repo.UpdateItem<Cathedral>(_selectedCathedral.Id, _selectedCathedral);
                }
                else
                {
                    _repo.InsertItem<Cathedral>(_selectedCathedral);
                }
            }

            GetAllCathedrals();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this Cathedral?", "Are you sure?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _repo.DeleteItem<Cathedral>(((sender as Button).DataContext as Cathedral).Id);
                GetAllCathedrals();
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
