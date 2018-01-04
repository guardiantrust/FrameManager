using DataLayer;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FrameManager
{
    /// <summary>
    /// Interaction logic for ManagePanels.xaml
    /// </summary>
    public partial class ManagePanels : Window
    {
        private ListRepository _repo = new ListRepository();
        private DataLayer.Panel _selectedPanel = new DataLayer.Panel();
        private List<DataLayer.Panel> _allPanels = new List<DataLayer.Panel>();

        public ManagePanels()
        {
            InitializeComponent();
            DataContext = _selectedPanel;
            GetAllPanels();
        }

        private void GetAllPanels()
        {
            _allPanels = _repo.GetItems<DataLayer.Panel>().ToList();
            lsvPanel.ItemsSource = _allPanels.OrderBy(x => x.Id);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void lsvPanel_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _selectedPanel = lsvPanel.SelectedValue as DataLayer.Panel;
            DataContext = _selectedPanel;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (_allPanels.Count(x => x.Name == _selectedPanel.Name) == 0 && !string.IsNullOrEmpty(_selectedPanel.Name))
            {
                _selectedPanel.Id = 0;
                _repo.InsertItem<DataLayer.Panel>(_selectedPanel);
                GetAllPanels();
                _selectedPanel = new DataLayer.Panel();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (_allPanels.Count(x => x.Name == _selectedPanel.Name) == 0)
            {
                if (_selectedPanel.Id > 0)
                {

                    _repo.UpdateItem<DataLayer.Panel>(_selectedPanel.Id, _selectedPanel);
                }
                else
                {
                    _repo.InsertItem<DataLayer.Panel>(_selectedPanel);
                }
            }

            GetAllPanels();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this Panel?", "Are you sure?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _repo.DeleteItem<DataLayer.Panel>(((sender as Button).DataContext as DataLayer.Panel).Id);
                GetAllPanels();
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
