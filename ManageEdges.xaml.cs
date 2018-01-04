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
    /// Interaction logic for ManageEdges.xaml
    /// </summary>
    public partial class ManageEdges : Window
    {
        private ListRepository _repo = new ListRepository();
        private Edge _selectedEdge = new Edge();
        private List<Edge> _allEdges = new List<Edge>();

        public ManageEdges()
        {
            InitializeComponent();
            DataContext = _selectedEdge;
            GetAllEdges();
        }

        private void GetAllEdges()
        {
            _allEdges = _repo.GetItems<Edge>().ToList();
            lsvEdge.ItemsSource = _allEdges.OrderBy(x => x.Id);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void lsvEdge_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _selectedEdge = lsvEdge.SelectedValue as Edge;
            DataContext = _selectedEdge;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (_allEdges.Count(x => x.Name == _selectedEdge.Name) == 0 && !string.IsNullOrEmpty(_selectedEdge.Name))
            {
                _selectedEdge.Id = 0;
                _repo.InsertItem<Edge>(_selectedEdge);
                GetAllEdges();
                _selectedEdge = new Edge();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (_allEdges.Count(x => x.Name == _selectedEdge.Name) == 0)
            {
                if (_selectedEdge.Id > 0)
                {

                    _repo.UpdateItem<Edge>(_selectedEdge.Id, _selectedEdge);
                }
                else
                {
                    _repo.InsertItem<Edge>(_selectedEdge);
                }
            }

            GetAllEdges();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this Edge?", "Are you sure?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _repo.DeleteItem<Edge>(((sender as Button).DataContext as Edge).Id);
                GetAllEdges();
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}