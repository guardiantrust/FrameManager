using DataLayer;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for DoorSettings.xaml
    /// </summary>
    public partial class DoorSettings : Window
    {

        private SettingsClass sc = new SettingsClass();
        private ListRepository _repo = new ListRepository();
        public DoorSettings()
        {
            InitializeComponent();
            sc.SetVisibility("Settings");
            grpSetting.Header = "Settings";
            sc.Panels = _repo.GetItems<DataLayer.Panel>().ToList();
            sc.Copes = _repo.GetItems<Cope>().ToList();
            sc.Drawers = _repo.GetItems<Drawer>().ToList();
            sc.Edges = _repo.GetItems<Edge>().ToList();
            sc.Species = _repo.GetItems<Species>().ToList();
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                sc.Printers.Add(printer);
            }

            DataContext = sc;
        }

        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            sc.SetVisibility("Settings");
            grpSetting.Header = "Settings";
        }

        private void TreeViewItem_Selected_1(object sender, RoutedEventArgs e)
        {
            sc.SetVisibility("Printing");

            grpSetting.Header = "Printing";
        }

        private void TreeViewItem_Selected_2(object sender, RoutedEventArgs e)
        {
            sc.SetVisibility("Cope");
            grpSetting.Header = "Cope";
        }

        private void TreeViewItem_Selected_3(object sender, RoutedEventArgs e)
        {
            sc.SetVisibility("Species");
            grpSetting.Header = "Species";
        }

        private void TreeViewItem_Selected_4(object sender, RoutedEventArgs e)
        {
            sc.SetVisibility("Panel");
            grpSetting.Header = "Panel";
        }

        private void TreeViewItem_Selected_5(object sender, RoutedEventArgs e)
        {
            sc.SetVisibility("Edge");
            grpSetting.Header = "Edge";
        }

        private void TreeViewItem_Selected_6(object sender, RoutedEventArgs e)
        {
            sc.SetVisibility("Drawer");
            grpSetting.Header = "Drawer";
        }

        private void TreeViewItem_Selected_7(object sender, RoutedEventArgs e)
        {
            sc.SetVisibility("ArchType");
            grpSetting.Header = "Arch Type";
        }

        private void TreeViewItem_Selected_8(object sender, RoutedEventArgs e)
        {
            sc.SetVisibility("ArchSize");
            grpSetting.Header = "Arch Size";
        }

        private void btnAddCope_Click(object sender, RoutedEventArgs e)
        {
            if (sc.Copes.Count(x => x.Name == sc.SelectedCope.Name) == 0 && !string.IsNullOrEmpty(sc.SelectedCope.Name))
            {
                sc.SelectedCope.Id = 0;
                _repo.InsertItem<Cope>(sc.SelectedCope);
                GetAllCopes();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this Cope?", "Are you sure?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _repo.DeleteItem<Cope>(((sender as Button).DataContext as Cope).Id);
                GetAllCopes();
            }
        }

        private void GetAllCopes()
        {
            sc.Copes = _repo.GetItems<Cope>().ToList();
        }

        private void lsvCope_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lsvCope.SelectedValue != null)
            {
                sc.SelectedCope = lsvCope.SelectedValue as Cope;
                
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this Species?", "Are you sure?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _repo.DeleteSpecie(((sender as Button).DataContext as Species).Id);
                GetAllSpecies();
            }
        }

        private void GetAllSpecies()
        {
            sc.Species = _repo.GetItems<Species>().ToList();
        }

        private void btnUpdateCope_Click(object sender, RoutedEventArgs e)
        {
            if (sc.SelectedCope.Id > 0)
            {

                _repo.UpdateItem<Cope>(sc.SelectedCope.Id, sc.SelectedCope);
            }
            else
            {
                _repo.InsertItem<Cope>(sc.SelectedCope);
            }


            GetAllCopes();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (sc.Species.Count(x => x.Name == sc.SelectedSpecies.Name) == 0 && !string.IsNullOrEmpty(sc.SelectedSpecies.Name))
            {
                sc.SelectedSpecies.Id = 0;
                _repo.InsertSpecie(sc.SelectedSpecies);
                GetAllSpecies();
                sc.SelectedSpecies = new Species();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (sc.SelectedSpecies.Id > 0)
            {
                _repo.UpdateSpecie(sc.SelectedSpecies);
            }
            else
            {
                _repo.InsertSpecie(sc.SelectedSpecies);
            }
            GetAllSpecies();
        }

        private void lsvSpecies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lsvSpecies.SelectedValue != null)
            {
                sc.SelectedSpecies = lsvSpecies.SelectedValue as Species;
                
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this Panel?", "Are you sure?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _repo.DeleteItem<DataLayer.Panel>(((sender as Button).DataContext as DataLayer.Panel).Id);
                GetAllPanels();
            }
        }

        private void GetAllPanels()
        {
            sc.Panels = _repo.GetItems<DataLayer.Panel>().ToList();
        }

        private void lsvPanel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sc.SelectedPanel = lsvPanel.SelectedValue as DataLayer.Panel;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (sc.Panels.Count(x => x.Name == sc.SelectedPanel.Name) == 0 && !string.IsNullOrEmpty(sc.SelectedPanel.Name))
            {
                sc.SelectedPanel.Id = 0;
                _repo.InsertItem<DataLayer.Panel>(sc.SelectedPanel);
                GetAllPanels();
                sc.SelectedPanel = new DataLayer.Panel();
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (sc.Panels.Count(x => x.Name == sc.SelectedPanel.Name) == 0)
            {
                if (sc.SelectedPanel.Id > 0)
                {

                    _repo.UpdateItem<DataLayer.Panel>(sc.SelectedPanel.Id, sc.SelectedPanel);
                }
                else
                {
                    _repo.InsertItem<DataLayer.Panel>(sc.SelectedPanel);
                }
            }

            GetAllPanels();
        }

        private void lsvEdge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            if (sc.Edges.Count(x => x.Name == sc.SelectedEdge.Name) == 0 && !string.IsNullOrEmpty(sc.SelectedEdge.Name))
            {
                sc.SelectedEdge.Id = 0;
                _repo.InsertItem<Edge>(sc.SelectedEdge);
                GetAllEdges();
                sc.SelectedEdge = new Edge();
            }
        }

        private void GetAllEdges()
        {
            sc.Edges = _repo.GetItems<Edge>().ToList();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            if (sc.Edges.Count(x => x.Name == sc.SelectedEdge.Name) == 0)
            {
                if (sc.SelectedEdge.Id > 0)
                {

                    _repo.UpdateItem<Edge>(sc.SelectedEdge.Id, sc.SelectedEdge);
                }
                else
                {
                    _repo.InsertItem<Edge>(sc.SelectedEdge);
                }
            }

            GetAllEdges();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this Edge?", "Are you sure?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _repo.DeleteItem<Edge>(((sender as Button).DataContext as Edge).Id);
                GetAllEdges();
            }
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            if (sc.Drawers.Count(x => x.Name == sc.SelectedDrawer.Name) == 0 && !string.IsNullOrEmpty(sc.SelectedDrawer.Name))
            {
                sc.SelectedDrawer.Id = 0;
                _repo.InsertItem<Drawer>(sc.SelectedDrawer);
                GetAllDrawers();
                sc.SelectedDrawer = new Drawer();
            }
        }

        private void GetAllDrawers()
        {
            sc.Drawers = _repo.GetItems<Drawer>().ToList();
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            if (sc.Drawers.Count(x => x.Name == sc.SelectedDrawer.Name) == 0)
            {
                if (sc.SelectedDrawer.Id > 0)
                {

                    _repo.UpdateItem<Drawer>(sc.SelectedDrawer.Id, sc.SelectedDrawer);
                }
                else
                {
                    _repo.InsertItem<Drawer>(sc.SelectedDrawer);
                }
            }

            GetAllDrawers();
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this Drawer?", "Are you sure?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _repo.DeleteItem<Drawer>(((sender as Button).DataContext as Drawer).Id);
                GetAllDrawers();
            }
        }

        private void lsvDrawer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            dialog.Multiselect = false;
            dialog.InitialDirectory = sc.DataLocation;
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                sc.DataLocation = dialog.FileName;
            }
        }
    }

    public class SettingsClass : INotifyPropertyChanged
    {
        private double _tenonLength;
        private string _dataLocation;
        private string _printer;
        private bool _printLandscape;
        private string _paperSize;
        private Cope _selectedCope = new Cope();
        private Drawer _selectedDrawer = new Drawer();
        private DataLayer.Panel _selectedPanel = new DataLayer.Panel();
        private Species _selectedSpecies = new DataLayer.Species();
        private Edge _selectedEdge = new Edge();

        private Visibility _settingsVisibility;
        private Visibility _printingVisibility;
        private Visibility _speciesVisibility;
        private Visibility _copeVisibility;
        private Visibility _panelVisibility;
        private Visibility _edgeVisibility;
        private Visibility _drawerVisibility;
        private Visibility _archTypeVisibility;
        private Visibility _archSizeVisibility;

        private List<Cope> _copes;
        private List<Drawer> _drawers;
        private List<Edge> _edges;
        private List<DataLayer.Panel> _panels;
        private List<Species> _species;
        private List<string> _printers = new List<string>();

        public Cope SelectedCope
        {
            get { return _selectedCope; }
            set { _selectedCope = value; OnPropertyChanged("SelectedCope"); }
        }

        public Edge SelectedEdge
        {
            get { return _selectedEdge; }
            set
            {
                _selectedEdge = value;
                OnPropertyChanged("SelectedEdge");
            }
        }

        public Species SelectedSpecies
        {
            get { return _selectedSpecies; }
            set
            {
                _selectedSpecies = value; OnPropertyChanged("SelectedSpecies");
            }
        }

        public DataLayer.Panel SelectedPanel
        {
            get { return _selectedPanel; }
            set
            {
                _selectedPanel = value;
                OnPropertyChanged("SelectedPanel");
            }
        }

        public Drawer SelectedDrawer
        {
            get { return _selectedDrawer; }
            set
            {
                _selectedDrawer = value;
                OnPropertyChanged("SelectedDrawer");

            }
        }

        public double TenonLength
        {
            get { return _tenonLength; }
            set { _tenonLength = value; OnPropertyChanged("TenonLength"); }
        }

        public string DataLocation
        {
            get { return _dataLocation; }
            set { _dataLocation = value; OnPropertyChanged("DataLocation"); }
        }

        public string Printer
        {
            get { return _printer; }
            set { _printer = value; OnPropertyChanged("Printer"); }
        }

        public string PaperSize
        {
            get { return _paperSize; }
            set { _paperSize = value; OnPropertyChanged("PaperSize"); }
        }

        public bool PrintLandscape
        {
            get { return _printLandscape; }
            set { _printLandscape = value; OnPropertyChanged("PrintLandscape"); }
        }

        public List<string> Printers
        {
            get { return _printers; }
            set
            {
                _printers = value; OnPropertyChanged("Printers");
            }
        }

        public List<Cope> Copes
        {
            get { return _copes; }
            set
            {
                _copes = value; OnPropertyChanged("Copes");
            }
        }

        public List<Drawer> Drawers
        {
            get { return _drawers; }
            set { _drawers = value; OnPropertyChanged("Drawers"); }
        }

        public List<Edge> Edges
        {
            get { return _edges; }
            set { _edges = value; OnPropertyChanged("Edges"); }
        }

        public List<DataLayer.Panel> Panels
        {
            get { return _panels; }
            set { _panels = value; OnPropertyChanged("Panels"); }
        }

        public List<Species> Species
        {
            get { return _species; }
            set { _species = value; OnPropertyChanged("Species"); }
        }
        public void SetVisibility(string visible)
        {
            SettingsVisiblity = Visibility.Collapsed;
            PrintingVisibility = Visibility.Collapsed;
            SpeciesVisibility = Visibility.Collapsed;
            CopeVisibility = Visibility.Collapsed;
            PanelVisibility = Visibility.Collapsed;
            EdgeVisibility = Visibility.Collapsed;
            DrawerVisibility = Visibility.Collapsed;
            ArchSizeVisibility = Visibility.Collapsed;
            ArchTypeVisibility = Visibility.Collapsed;


            switch (visible)
            {
                case "Settings":
                    SettingsVisiblity = Visibility.Visible;
                    break;

                case "Printing":
                    PrintingVisibility = Visibility.Visible;
                    break;

                case "Species":
                    SpeciesVisibility = Visibility.Visible;
                    break;

                case "Cope":
                    CopeVisibility = Visibility.Visible;
                    break;

                case "Panel":
                    PanelVisibility = Visibility.Visible;
                    break;

                case "Edge":
                    EdgeVisibility = Visibility.Visible;
                    break;

                case "Drawer":
                    DrawerVisibility = Visibility.Visible;
                    break;

                case "ArchType":
                    ArchTypeVisibility = Visibility.Visible;
                    break;

                case "ArchSize":
                    ArchSizeVisibility = Visibility.Visible;
                    break;
            }
        }

        public Visibility ArchSizeVisibility
        {
            get { return _archSizeVisibility; }
            set { _archSizeVisibility = value; OnPropertyChanged("ArchSizeVisibility"); }
        }
        public Visibility ArchTypeVisibility
        {
            get { return _archTypeVisibility; }
            set { _archTypeVisibility = value; OnPropertyChanged("ArchTypeVisibility"); }
        }
        public Visibility DrawerVisibility
        {
            get { return _drawerVisibility; }
            set { _drawerVisibility = value; OnPropertyChanged("DrawerVisibility"); }
        }
        public Visibility EdgeVisibility
        {
            get { return _edgeVisibility; }
            set { _edgeVisibility = value; OnPropertyChanged("EdgeVisibility"); }
        }

        public Visibility PanelVisibility
        {
            get { return _panelVisibility; }
            set { _panelVisibility = value; OnPropertyChanged("PanelVisibility"); }
        }
        public Visibility CopeVisibility
        {
            get { return _copeVisibility; }
            set { _copeVisibility = value; OnPropertyChanged("CopeVisibility"); }
        }

        public Visibility SettingsVisiblity
        {
            get { return _settingsVisibility; }
            set
            {
                _settingsVisibility = value;
                OnPropertyChanged("SettingsVisiblity");
            }
        }

        public Visibility PrintingVisibility
        {
            get { return _printingVisibility; }
            set
            {
                _printingVisibility = value;
                OnPropertyChanged("PrintingVisibility");
            }
        }

        public Visibility SpeciesVisibility
        {
            get { return _speciesVisibility; }
            set { _speciesVisibility = value; OnPropertyChanged("SpeciesVisibility"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
