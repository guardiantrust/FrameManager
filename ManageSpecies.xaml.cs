using DataLayer;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FrameManager
{
    /// <summary>
    /// Interaction logic for ManageSpecies.xaml
    /// </summary>
    public partial class ManageSpecies : Window
    {
        private Species _selectedSpecies = new Species();
        private List<Species> _allSpecies;
        private ListRepository _repo = new ListRepository();

        public ManageSpecies()
        {
            InitializeComponent();
            
            DataContext = _selectedSpecies;
            GetAllSpecies();
        }

        private void GetAllSpecies()
        {
            _allSpecies = _repo.GetSpecies().ToList();
            lsvSpecies.ItemsSource = _allSpecies.OrderBy(x=> x.Id);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this Species?", "Are you sure?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _repo.DeleteSpecie(((sender as Button).DataContext as Species).Id);
                GetAllSpecies();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (_allSpecies.Count(x => x.Name == _selectedSpecies.Name) == 0 && !string.IsNullOrEmpty(_selectedSpecies.Name))
            {
                _selectedSpecies.Id = 0;
                _repo.InsertSpecie(_selectedSpecies);
                GetAllSpecies();
                _selectedSpecies = new Species();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void lsvSpecies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lsvSpecies.SelectedValue != null)
            {
                _selectedSpecies = lsvSpecies.SelectedValue as Species;
                DataContext = _selectedSpecies;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if(_selectedSpecies.Id>0)
            {
                _repo.UpdateSpecie(_selectedSpecies);
            }
            else
            {
                _repo.InsertSpecie(_selectedSpecies);
            }
            GetAllSpecies();
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
