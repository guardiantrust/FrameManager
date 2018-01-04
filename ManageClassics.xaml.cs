using DataLayer;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FrameManager
{
    /// <summary>
    /// Interaction logic for ManageClassics.xaml
    /// </summary>
    public partial class ManageClassics : Window
    {

        private ListRepository _repo = new ListRepository();
        private Classic _selectedClassic = new Classic();
        private List<Classic> _allClassics = new List<Classic>();

        public ManageClassics()
        {
            InitializeComponent();
            DataContext = _selectedClassic;
            GetAllClassics();
        }

        private void GetAllClassics()
        {
            _allClassics = _repo.GetItems<Classic>().ToList();
            lsvClassic.ItemsSource = _allClassics.OrderBy(x => x.Min);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void lsvClassic_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _selectedClassic = lsvClassic.SelectedValue as Classic;
            DataContext = _selectedClassic;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (_allClassics.Count(x => x.Name == _selectedClassic.Name) == 0 && !string.IsNullOrEmpty(_selectedClassic.Name) && _selectedClassic.Max > 0)
            {
                _selectedClassic.Id = 0;
                _repo.InsertItem<Classic>(_selectedClassic);
                GetAllClassics();

                _selectedClassic = new Classic();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (_allClassics.Count(x => x.Name == _selectedClassic.Name) == 0)
            {
                if (_selectedClassic.Id > 0)
                {
                    _repo.UpdateItem<Classic>(_selectedClassic.Id, _selectedClassic);
                }
                else
                {
                    _repo.InsertItem<Classic>(_selectedClassic);
                }
            }

            GetAllClassics();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this Classic?", "Are you sure?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _repo.DeleteItem<Classic>(((sender as Button).DataContext as Classic).Id);
                GetAllClassics();
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
