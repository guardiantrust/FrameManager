using DataLayer;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace FrameManager
{
    /// <summary>
    /// Interaction logic for PrintOrder.xaml
    /// </summary>
    public partial class PrintOrder : Window
    {
        public FlowDocument fd = new FlowDocument();
        
        public PrintOrder()
        {
            InitializeComponent();
        }
        public FlowDocument MainDocument
        {
            get { return fd; }
        }
        
        public void LoadData(string customerName, string jobName, string batch, List<FrameRail> rails)
        {
            Table tblRails = new Table();
            tblRails.Columns.Add(new TableColumn());
            tblRails.Columns.Add(new TableColumn());
            tblRails.Columns.Add(new TableColumn());
            tblRails.Columns.Add(new TableColumn());
            tblRails.Columns.Add(new TableColumn());
            tblRails.Columns.Add(new TableColumn());
            tblRails.Columns.Add(new TableColumn());

            TableRowGroup titleGroup = new TableRowGroup();
            titleGroup.Rows.Add(new TableRow());
            titleGroup.Rows[0].FontSize = 22;
            titleGroup.FontWeight = FontWeights.Bold;
            var cell = new TableCell(new Paragraph(new Run("FRAME RAILS")));
            cell.ColumnSpan = 7;
            cell.LineHeight = 35;
            titleGroup.Rows[0].Cells.Add(cell);
           

            TableRowGroup infoGroup = new TableRowGroup();
            infoGroup.Background = Brushes.LightGray;
            
            infoGroup.Rows.Add(new TableRow());
            infoGroup.Rows[0].FontSize = 14;
            TableCell customerCell = new TableCell();
            Paragraph customerTitle = new Paragraph(new Run("Customer:"));
            customerTitle.FontWeight = FontWeights.Bold;
            Paragraph customerInfo = new Paragraph(new Run(customerName));
            Paragraph jobTitle = new Paragraph(new Run("Job Number:"));
            jobTitle.FontWeight = FontWeights.Bold;
            Paragraph jobInfo = new Paragraph(new Run(jobName));
            Paragraph batchName = new Paragraph(new Run("Batch:"));
            batchName.FontWeight = FontWeights.Bold;
            Paragraph batchInfo = new Paragraph(new Run(batch));

            infoGroup.Rows[0].Cells.Add(customerCell);
            infoGroup.Rows[0].Cells.Add(new TableCell(customerTitle));
            infoGroup.Rows[0].Cells.Add(new TableCell(customerInfo));
            infoGroup.Rows[0].Cells.Add(new TableCell(jobTitle));
            infoGroup.Rows[0].Cells.Add(new TableCell(jobInfo));
            infoGroup.Rows[0].Cells.Add(new TableCell(batchName));
            infoGroup.Rows[0].Cells.Add(new TableCell(batchInfo));
            infoGroup.Rows[0].Cells[0].LineHeight = 35;

            TableRowGroup columnHeaders = new TableRowGroup();
            columnHeaders.Rows.Add(new TableRow());
            columnHeaders.Rows[0].Cells.Add(new TableCell(new Paragraph(new Run("ID"))));
            columnHeaders.Rows[0].Cells.Add(new TableCell(new Paragraph(new Run("Width"))));
            columnHeaders.Rows[0].Cells.Add(new TableCell(new Paragraph(new Run("Thickness"))));
            columnHeaders.Rows[0].Cells.Add(new TableCell(new Paragraph(new Run("Length"))));
            columnHeaders.Rows[0].Cells.Add(new TableCell(new Paragraph(new Run("Material"))));
            columnHeaders.Rows[0].Cells.Add(new TableCell(new Paragraph(new Run("Cope"))));
            columnHeaders.Rows[0].Cells.Add(new TableCell(new Paragraph(new Run("Type"))));
            columnHeaders.Background = Brushes.LightBlue;

            tblRails.RowGroups.Add(titleGroup);
            tblRails.RowGroups.Add(infoGroup);
            tblRails.RowGroups.Add(columnHeaders);

            TableRowGroup columns = new TableRowGroup();
            bool backDrop = false;
            foreach (var rail in rails)
            {

               var row = new TableRow();
                row.Cells.Add(new TableCell(new Paragraph(new Run(rail.Id.ToString()))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(rail.RailWidth.ToString()))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(rail.RailThickness.ToString()))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(rail.RailLength.ToString()))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(rail.Material))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(rail.CopeName))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(rail.Type.ToString()))));
                if (backDrop)
                {
                    row.Background = Brushes.LightCoral;
                }

                columns.Rows.Add(row);
                backDrop = !backDrop;
            }

            tblRails.RowGroups.Add(columns);
            fd.Blocks.Add(tblRails);

            dv1.Document = fd;
        }

    }
}
