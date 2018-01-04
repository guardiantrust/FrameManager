using DataLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace FrameManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        private ListRepository _repo = new ListRepository(Properties.Settings.Default.DataLocation);
        private FrameOrder frame;
        private List<Arch> _arches = new List<Arch>();
        private List<Classic> _classics = new List<Classic>();
        private List<Cathedral> _cathedrals = new List<Cathedral>();

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            Frame = new FrameOrder();
            LoadCombos();

        }

        private void LoadCombos()
        {
            cmbSpecies.ItemsSource = new ObservableCollection<Species>(_repo.GetSpecies().ToList());
            cmbPanel.ItemsSource = new ObservableCollection<DataLayer.Panel>(_repo.GetItems<DataLayer.Panel>().ToList());
            cmbCope.ItemsSource = new ObservableCollection<Cope>(_repo.GetItems<Cope>().ToList());
            cmbDrawer.ItemsSource = new ObservableCollection<Drawer>(_repo.GetItems<Drawer>().ToList());
            cmbEdge.ItemsSource = new ObservableCollection<Edge>(_repo.GetItems<Edge>().ToList());
            cmbBatch.ItemsSource = new ObservableCollection<Batch>(_repo.GetItems<Batch>().ToList());
            _arches = _repo.GetItems<Arch>().OrderBy(x => x.Min).ToList();
            _classics = _repo.GetItems<Classic>().OrderBy(x => x.Min).ToList();
            _cathedrals = _repo.GetItems<Cathedral>().OrderBy(x => x.Min).ToList();

            cmbPanelType.ItemsSource = new String[] { "", "Single", "Double" };

            DataContext = Frame;
            lsvDoorOpening.ItemsSource = Frame.FrameCollection;
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ManageSpecies ms = new ManageSpecies();
            ms.ShowDialog();
            LoadCombos();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            ManagePanels mp = new ManagePanels();
            mp.ShowDialog();
            LoadCombos();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            ManageEdges me = new ManageEdges();
            me.ShowDialog();
            LoadCombos();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            ManageDrawers md = new ManageDrawers();
            md.ShowDialog();
            LoadCombos();

        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            ManageCope mc = new ManageCope();
            mc.ShowDialog();
            LoadCombos();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.NewFrameItem();
            lsvDoorOpening.ItemsSource = Frame.FrameCollection;
        }

        private void lsvDoorOpening_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.-]+");

            e.Handled = regex.IsMatch(e.Text);
        }

        public List<string> DoorStyles
        {
            get
            {
                var ds = new List<string>();
                ds.Add("A");
                ds.Add("C");
                ds.Add("NC");
                ds.Add("FF");
                ds.Add("5P");

                return ds;
            }
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            ManageArches ma = new ManageArches();
            ma.ShowDialog();
            LoadCombos();
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            ManageCathedrals mc = new ManageCathedrals();
            mc.ShowDialog();
            LoadCombos();
        }

        private void MenuItem_Click_7(object sender, RoutedEventArgs e)
        {
            ManageClassics mc = new ManageClassics();
            mc.ShowDialog();
            LoadCombos();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this Frame Item?", "Are you sure?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Frame.RemoveFrame((sender as Button).DataContext as FrameItem);
                lsvDoorOpening.ItemsSource = Frame.FrameCollection;
            }
        }

        private void MenuItem_Click_8(object sender, RoutedEventArgs e)
        {
            Frame = new FrameOrder();
            DataContext = Frame;
            lsvDoorOpening.ItemsSource = frame.FrameCollection;
            lsvRails.ItemsSource = frame.Rails;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (frame.Id == 0)
            {
                Frame.Created =
                Frame.Modified = DateTime.Now;
                _repo.InsertItem<FrameOrder>(Frame);
            }
            else
            {
                frame.Modified = DateTime.Now;
                _repo.UpdateItem<FrameOrder>(Frame.Id, Frame);
            }

        }

        private void MenuItem_Click_9(object sender, RoutedEventArgs e)
        {
            SearchFrames sf = new SearchFrames();
            sf.ShowDialog();

            if (sf.SelectedFrame != null)
            {
                Frame = sf.SelectedFrame;
                //DataContext = null;
                DataContext = Frame;
                Frame.RemapFrameItems();

                lsvDoorOpening.ItemsSource = Frame.FrameCollection;

                LoadCombos();
                cmbCope.SelectedItem = Frame.SelectedCope;
                cmbDrawer.SelectedItem = Frame.SelectedDrawer;
                cmbEdge.SelectedItem = Frame.SelectedEdge;
                cmbPanel.SelectedItem = Frame.SelectedPanel;
                cmbSpecies.SelectedItem = Frame.SelectedSpecies;
                //Get Rails

                var rails = _repo.GetRails(Frame.Frames.Select(x => x.UniqueId).ToList());
                Frame.Rails = rails.ToList();
                lsvRails.ItemsSource = null;
                lsvRails.ItemsSource = Frame.Rails;
            }
        }



        public FrameOrder Frame
        {
            get { return frame; }
            set
            {
                frame = value;
                OnPropertyChanged("Frame");
                UpdateLayout();
            }
        }
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox)
            {
                (sender as TextBox).SelectAll();
            }
        }

        private void TextBox_GotMouseCapture(object sender, MouseEventArgs e)
        {
            if (sender is TextBox)
            {
                (sender as TextBox).SelectAll();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Frame = new FrameOrder();
            DataContext = null;
            LoadCombos();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (frame.Id == 0)
            {
                MessageBox.Show("Please save the Frame Order before generating.");
                return;
            }

            //Create frame rails
            List<FrameRail> allRails = new List<FrameRail>();
            int i = 1;
            foreach (var fi in frame.Frames)
            {
                // Top
                FrameRail fr = new FrameRail();
                fr.ParentId = i;
                fr.ParentUniqueId = fi.UniqueId;
                fr.CopeName = frame.SelectedCope == null ? "" : frame.SelectedCope.Name;
                fr.Material = frame.SelectedSpecies == null ? "" : frame.SelectedSpecies.Name;
                fr.RailThickness = frame.Thickness;
                fr.RailWidth = frame.TopRailWidth;
                fr.Type = RailType.Top;
                fr.RailLength = fi.DoorWidth - frame.RightRailWidth - frame.LeftRailWidth + (Properties.Settings.Default.TenonLength * 2) + (frame.Haunch ? (frame.SelectedCope.ProfileWidth * 2) : 0);
                allRails.Add(fr);

                // Bottom
                fr = new FrameRail();
                fr.ParentId = i;
                fr.ParentUniqueId = fi.UniqueId;
                fr.CopeName = frame.SelectedCope.Name;
                fr.Material = frame.SelectedSpecies.Name;
                fr.RailThickness = frame.Thickness;
                fr.RailWidth = frame.BottomRailWidth;
                fr.Type = RailType.Bottom;
                fr.RailLength = fi.DoorWidth - frame.RightRailWidth - frame.LeftRailWidth + (Properties.Settings.Default.TenonLength * 2) + (frame.Haunch ? (frame.SelectedCope.ProfileWidth * 2) : 0);
                allRails.Add(fr);

                // Left
                fr = new FrameRail();
                fr.ParentId = i;
                fr.ParentUniqueId = fi.UniqueId;
                fr.CopeName = frame.SelectedCope.Name;
                fr.Material = frame.SelectedSpecies.Name;
                fr.RailThickness = frame.Thickness;
                fr.RailWidth = frame.LeftRailWidth;
                fr.Type = RailType.Left;
                fr.RailLength = fi.DoorHeight + frame.SelectedCope.ChangeAmount;
                allRails.Add(fr);

                // Right
                fr = new FrameRail();
                fr.ParentId = i;
                fr.ParentUniqueId = fi.UniqueId;
                fr.CopeName = frame.SelectedCope.Name;
                fr.Material = frame.SelectedSpecies.Name;
                fr.RailThickness = frame.Thickness;
                fr.RailWidth = frame.RightRailWidth;
                fr.Type = RailType.Right;
                fr.RailLength = fi.DoorHeight + frame.SelectedCope.ChangeAmount;
                allRails.Add(fr);

                // Middle
                if (frame.MiddleRailWidth > 0 && frame.SelectedPanelType.ToUpper() == "DOUBLE")
                {
                    fr = new FrameRail();
                    fr.ParentId = i;
                    fr.ParentUniqueId = fi.UniqueId;
                    fr.CopeName = frame.SelectedCope.Name;
                    fr.Material = frame.SelectedSpecies.Name;
                    fr.RailThickness = frame.Thickness;
                    fr.RailWidth = frame.MiddleRailWidth;
                    fr.Type = RailType.Middle;
                    fr.RailLength = fi.DoorHeight + frame.SelectedCope.ChangeAmount;
                    allRails.Add(fr);
                }
                i++;
            }

            
            //delete any mis-mathces
            foreach(var r in Frame.Rails)
            {
                if(!allRails.Any(x=> x.ParentUniqueId == r.ParentUniqueId && x.Type == r.Type))
                {
                    _repo.DeleteRails(r.Id);
                }
            }

            //insert and update
            foreach (var r in allRails)
            {
                if (frame.Rails.Any(x => x.ParentUniqueId == r.ParentUniqueId && x.Type == r.Type))
                {
                    r.Id = frame.Rails.First(x => x.ParentUniqueId == r.ParentUniqueId && x.Type == r.Type).Id;
                    _repo.UpdateRails(r.Id, r);
                }
                else
                {
                    _repo.InsertRails(r);
                }
            }

            frame.Rails.Clear();
            frame.Rails.AddRange(allRails);

            lsvRails.ItemsSource = null;
            lsvRails.ItemsSource = frame.Rails;




        }

        private void MenuItem_Click_10(object sender, RoutedEventArgs e)
        {
            DoorSettings ds = new DoorSettings();
            ds.ShowDialog();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {


            // Test
            PrintOrder p = new PrintOrder();
            p.LoadData(frame.CustomerName, frame.JobNumber, frame.BatchName, frame.Rails);

            p.ShowDialog();

            PrintDialog pd = new PrintDialog();
            p.MainDocument.PageHeight = pd.PrintableAreaHeight;
            p.MainDocument.PageWidth = pd.PrintableAreaWidth;
            p.MainDocument.PagePadding = new Thickness(50);
            p.MainDocument.ColumnGap = 0;
            p.MainDocument.ColumnWidth = pd.PrintableAreaWidth;

            IDocumentPaginatorSource dps = p.MainDocument;
            pd.PrintDocument(dps.DocumentPaginator, "flow doc");
            //var ht = new HeaderTemplate();
        }
    }
}
