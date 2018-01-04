using DataLayer;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FrameManager
{
    /// <summary>
    /// Interaction logic for ManageCope.xaml
    /// </summary>
    public partial class ManageCope : Window
    {
        private Cope _selectedCope = new Cope();
        private List<Cope> _allCopes = new List<Cope>();
        private ListRepository _repo = new ListRepository();

        public ManageCope()
        {
            InitializeComponent();
            DataContext = _selectedCope;
            GetAllCopes();
        }

        private void GetAllCopes()
        {
            _allCopes = _repo.GetItems<Cope>().ToList();
            lsvCope.ItemsSource = _allCopes.OrderBy(x => x.Id);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this Cope?", "Are you sure?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _repo.DeleteItem<Cope>(((sender as Button).DataContext as Cope).Id);
                GetAllCopes();
            }
        }

        private void lsvCope_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lsvCope.SelectedValue != null)
            {
                _selectedCope = lsvCope.SelectedValue as Cope;
                DataContext = _selectedCope;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (_allCopes.Count(x => x.Name == _selectedCope.Name) == 0 && !string.IsNullOrEmpty(_selectedCope.Name))
            {
                _selectedCope.Id = 0;
                _repo.InsertItem<Cope>(_selectedCope);
                GetAllCopes();
                _selectedCope = new Cope();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            
                if (_selectedCope.Id > 0)
                {

                    _repo.UpdateItem<Cope>(_selectedCope.Id, _selectedCope);
                }
                else
                {
                    _repo.InsertItem<Cope>(_selectedCope);
                }
            

            GetAllCopes();
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
