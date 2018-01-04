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
    /// Interaction logic for SearchFrames.xaml
    /// </summary>
    public partial class SearchFrames : Window
    {
        private List<FrameOrder> frames = new List<FrameOrder>();
        private ListRepository _repo = new ListRepository();

        public SearchFrames()
        {
            InitializeComponent();

            lsvFrameOrders.ItemsSource = frames;
        }
        public FrameOrder SelectedFrame
        {
            get; set;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void lsvFrameOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            
            if (item.Content as FrameOrder != null)
            {
                SelectedFrame = item.Content as FrameOrder;
                Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FindOrders();
        }

        private void FindOrders()
        {
            var f = _repo.FindOrders(txtCustomer.Text, txtJobNumber.Text, txtBatchName.Text);
            if(f != null)
            {
                frames = f.OrderBy(x => x.Modified).ToList();
            }
            lsvFrameOrders.ItemsSource = frames;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this Frame Order, this is not reversable?", "Are you sure?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _repo.DeleteItem<FrameOrder>(((sender as Button).DataContext as FrameOrder).Id);
                FindOrders();
            }
        }

        private void txtCustomer_GotFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}
