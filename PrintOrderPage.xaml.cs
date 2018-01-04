using DataLayer;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace FrameManager
{
    /// <summary>
    /// Interaction logic for PrintOrderxaml.xaml
    /// </summary>
    public partial class PrintOrderPage : UserControl
    {
        public PrintOrderPage()
        {
            InitializeComponent();
        }

        public void LoadData(string customerName, string jobName, string batch, List<FrameRail> rails)
        {
            TableRowGroup titleGroup = new TableRowGroup();
            titleGroup.Rows.Add(new TableRow());
            titleGroup.Rows[0].FontSize = 20;
            titleGroup.Rows[0].Cells.Add(new TableCell(new Paragraph(new Run("FRAME RAILS"))));

            TableRowGroup infoGroup = new TableRowGroup();
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

            TableRowGroup columnHeaders = new TableRowGroup();
            columnHeaders.Rows.Add(new TableRow());
            columnHeaders.Rows[0].Cells.Add(new TableCell(new Paragraph(new Run("ID"))));
            columnHeaders.Rows[0].Cells.Add(new TableCell(new Paragraph(new Run("Width"))));
            columnHeaders.Rows[0].Cells.Add(new TableCell(new Paragraph(new Run("Thickness"))));
            columnHeaders.Rows[0].Cells.Add(new TableCell(new Paragraph(new Run("Length"))));
            columnHeaders.Rows[0].Cells.Add(new TableCell(new Paragraph(new Run("Material"))));
            columnHeaders.Rows[0].Cells.Add(new TableCell(new Paragraph(new Run("Cope"))));
            columnHeaders.Rows[0].Cells.Add(new TableCell(new Paragraph(new Run("Type"))));

            tblRails.RowGroups.Add(titleGroup);
            tblRails.RowGroups.Add(infoGroup);
            tblRails.RowGroups.Add(columnHeaders);


            foreach(var rail in rails)
            {
                TableRowGroup columns = new TableRowGroup();
                columns.Rows.Add(new TableRow());
                columns.Rows[0].Cells.Add(new TableCell(new Paragraph(new Run(rail.Id.ToString()))));
                columns.Rows[0].Cells.Add(new TableCell(new Paragraph(new Run(rail.RailWidth.ToString()))));
                columns.Rows[0].Cells.Add(new TableCell(new Paragraph(new Run(rail.RailThickness.ToString()))));
                columns.Rows[0].Cells.Add(new TableCell(new Paragraph(new Run(rail.RailLength.ToString()))));
                columns.Rows[0].Cells.Add(new TableCell(new Paragraph(new Run(rail.Material))));
                columns.Rows[0].Cells.Add(new TableCell(new Paragraph(new Run(rail.CopeName))));
                columns.Rows[0].Cells.Add(new TableCell(new Paragraph(new Run(rail.Type.ToString()))));
            }


        }
        
    }
}
