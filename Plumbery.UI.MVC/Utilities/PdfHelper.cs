using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System.Data;
using Plumbery.Domain.Entities;

namespace Plumbery.UI.MVC.Utilities {
    public class PdfHelper {
        #region Private Fields
        /// <summary>
        /// The MigraDoc document that represents the invoice.
        /// </summary>
        private Document document;

        /// <summary>
        /// Time Sheet Information
        /// </summary>
        private TimeSheet _timeSheet;

        /// <summary>
        /// An XML invoice based on a sample created with Microsoft InfoPath.
        /// </summary>
        private DataTable dt;
        /// <summary>
        /// The root navigator for the XML document.
        /// </summary>


        /// <summary>
        /// The text frame of the MigraDoc document that contains the address.
        /// </summary>
        private TextFrame locationFrame;


        private TextFrame descriptionFrame;


        private TextFrame timeFrame;


        private TextFrame quoteFrame;

        private TextFrame plumberFrame;

        /// <summary>
        /// The table of the MigraDoc document that contains the invoice items.
        /// </summary>
        private Table table;

        #endregion

        #region Constructors

        public PdfHelper() {

        }

        /// <summary>
        /// Initializes a new instance of the class BillFrom and opens the specified XML document.
        /// </summary>
        public PdfHelper(TimeSheet timeSheet, DataTable dtIn) {
            this.dt = dtIn;
            this._timeSheet = timeSheet;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates the invoice document.
        /// </summary>
        public Document CreateDocument() {
            // Create a new MigraDoc document
            this.document = new Document();
            this.document.Info.Title = "Daily Time Sheet";
            this.document.Info.Subject = _timeSheet.Code;
            this.document.Info.Author = "Plumbery";
            this.document.DefaultPageSetup.Orientation = Orientation.Portrait;
            this.document.DefaultPageSetup.PageFormat = PageFormat.A4;
            this.document.DefaultPageSetup.LeftMargin = Unit.FromCentimeter(1);
            this.document.DefaultPageSetup.RightMargin = Unit.FromCentimeter(1);
            this.document.DefaultPageSetup.TopMargin = Unit.FromCentimeter(1);
            this.document.DefaultPageSetup.BottomMargin = Unit.FromCentimeter(1);

            DefineStyles();

            CreatePage();

            FillContent();

            return this.document;
        }

        /// <summary>
        /// Defines the styles used to format the MigraDoc document.
        /// </summary>
        void DefineStyles() {
            // Get the predefined style Normal.
            Style style = this.document.Styles["Normal"];
            // Because all styles are derived from Normal, the next line changes the 
            // font of the whole document. Or, more exactly, it changes the font of
            // all styles and paragraphs that do not redefine the font.
            style.Font.Name = "Verdana";

            style = this.document.Styles[StyleNames.Header];
            style.ParagraphFormat.AddTabStop("2cm", TabAlignment.Right);

            style = this.document.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("2cm", TabAlignment.Center);

            // Create a new style called Table based on style Normal
            style = this.document.Styles.AddStyle("Table", "Normal");
            style.Font.Name = "Verdana";
            style.Font.Name = "Times New Roman";
            style.Font.Size = 11;

            // Create a new style called Reference based on style Normal
            style = this.document.Styles.AddStyle("Reference", "Normal");
            style.ParagraphFormat.SpaceBefore = "5mm";
            style.ParagraphFormat.SpaceAfter = "5mm";
            style.ParagraphFormat.TabStops.AddTabStop("2cm", TabAlignment.Right);
        }

        /// <summary>
        /// Creates the static parts of the invoice.
        /// </summary>

        void CreatePage() {
            // Each MigraDoc document needs at least one section.
            Section section = this.document.AddSection();
            
            
            Paragraph paragraph = section.Headers.Primary.AddParagraph("Daily Time Sheet");
            paragraph.Format.Font.Name = "Times New Roman";
            paragraph.Format.Font.Size = 16;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            paragraph = section.Headers.Primary.AddParagraph("No." + _timeSheet.Code);
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Bold = false;
            paragraph.Format.Font.Name = "Times New Roman";
            paragraph.Format.Alignment = ParagraphAlignment.Right;
            
            // Create footer
            paragraph = section.Footers.Primary.AddParagraph();
            paragraph.AddText("Signed: ________________");
            paragraph.AddSpace(15);
            paragraph.AddText("Name: " + _timeSheet.Plumber.User.FullName);
            paragraph.AddSpace(15);
            paragraph.AddText("Date: " + _timeSheet.DateCreated.ToString("dd-MM-yy"));
            paragraph.Format.Font.Name = "Times New Roman";
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            this.plumberFrame = section.AddTextFrame();
            this.plumberFrame = section.AddTextFrame();
            this.plumberFrame.Height = "1.75cm";
            this.plumberFrame.Width = "18.4cm";
            this.plumberFrame.Left = ShapePosition.Left;
            this.plumberFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            this.plumberFrame.Top = "3.75cm";
            this.plumberFrame.RelativeVertical = RelativeVertical.Page;

            paragraph = this.plumberFrame.AddParagraph();
            paragraph.AddFormattedText("Plumber:  ",TextFormat.Bold);
            paragraph.AddText(_timeSheet.Plumber.User.FullName);
            paragraph.AddSpace(85);
            paragraph.AddFormattedText("Site:  ",TextFormat.Bold);
            paragraph.AddText(_timeSheet.Site.Name);
            paragraph.Format.Font.Name = "Times New Roman";
            paragraph.Format.Font.Size = 12;
            paragraph.Format.SpaceAfter = 3;
            paragraph.Format.LeftIndent = 5;
            paragraph.Format.SpaceBefore = 3;            

            // Create the text frame for the Location and Status
            this.locationFrame = section.AddTextFrame();
            this.locationFrame.Height = "4cm";
            this.locationFrame.Width = "18.4cm";
            this.locationFrame.Left = ShapePosition.Left;
            this.locationFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            this.locationFrame.Top = "6cm";
            this.locationFrame.RelativeVertical = RelativeVertical.Page;
            this.locationFrame.FillFormat.Color = MigraDoc.DocumentObjectModel.Colors.Azure;

            // Put Specific location in frame
            paragraph = this.locationFrame.AddParagraph("Specific location:");
            paragraph.Format.Font.Name = "Times New Roman";
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Font.Size = 12;
            paragraph.Format.SpaceAfter = 3;
            paragraph.Format.LeftIndent = 5;
            paragraph.Format.SpaceBefore = 3;

            paragraph = this.locationFrame.AddParagraph();
            if (_timeSheet.SpecificLocation == null)
                paragraph.AddText("N/A");
            else
                paragraph.AddText(_timeSheet.SpecificLocation);
            paragraph.Format.Font.Name = "Times New Roman";
            paragraph.Format.Font.Bold = false;
            paragraph.Format.Font.Size = 12;
            paragraph.Format.SpaceAfter = 3;
            paragraph.Format.LeftIndent = 5;
            paragraph.Format.SpaceBefore = 3;

            // Put Detailed Point in frame


            paragraph = this.locationFrame.AddParagraph("Detailed Point:");
            paragraph.Format.Font.Name = "Times New Roman";
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Font.Size = 12;
            paragraph.Format.SpaceAfter = 3;
            paragraph.Format.LeftIndent = 5;
            paragraph.Format.SpaceBefore = 3;

            if (_timeSheet.DetailedPoint == null)
                paragraph = this.locationFrame.AddParagraph("-");
            else
                paragraph = this.locationFrame.AddParagraph(_timeSheet.DetailedPoint);
            paragraph.Format.Font.Name = "Times New Roman";
            paragraph.Format.Font.Bold = false;
            paragraph.Format.Font.Size = 12;
            paragraph.Format.SpaceAfter = 3;
            paragraph.Format.LeftIndent = 5;
            paragraph.Format.SpaceBefore = 3;

            // Put Detailed Point in frame
            paragraph = this.locationFrame.AddParagraph("Completion Status:");
            paragraph.Format.Font.Name = "Times New Roman";
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Font.Size = 12;
            paragraph.Format.SpaceAfter = 3;
            paragraph.Format.LeftIndent = 5;
            paragraph.Format.SpaceBefore = 3;

            paragraph = this.locationFrame.AddParagraph(_timeSheet.SheetStatus.ToString());
            paragraph.Format.Font.Name = "Times New Roman";
            paragraph.Format.Font.Bold = false;
            paragraph.Format.Font.Size = 12;
            paragraph.Format.SpaceAfter = 3;
            paragraph.Format.LeftIndent = 5;
            paragraph.Format.SpaceBefore = 3;


            // Description Frame
            this.descriptionFrame = section.AddTextFrame();
            this.descriptionFrame.Height = "1.75cm";
            this.descriptionFrame.Width = "18.4cm";
            this.descriptionFrame.Left = ShapePosition.Left;
            this.descriptionFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            this.descriptionFrame.Top = "10.5cm";
            this.descriptionFrame.RelativeVertical = RelativeVertical.Page;
            this.descriptionFrame.FillFormat.Color = MigraDoc.DocumentObjectModel.Colors.Azure;

            // Add label for description
            paragraph = this.descriptionFrame.AddParagraph();
            paragraph.AddText("Description of work done");
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Font.Name = "Times New Roman";
            paragraph.Format.Font.Size = 12;
            paragraph.Format.SpaceAfter = 3;
            paragraph.Format.LeftIndent = 5;
            paragraph.Format.SpaceBefore = 3;

            paragraph = this.descriptionFrame.AddParagraph();
            if (_timeSheet.Description == null) {
                paragraph.AddText("N/A");
            } else {
                paragraph.AddText(_timeSheet.Description);
            }
            paragraph.Format.Font.Bold = false;
            paragraph.Format.Font.Name = "Times New Roman";
            paragraph.Format.Font.Size = 12;
            paragraph.Format.SpaceAfter = 3;
            paragraph.Format.LeftIndent = 5;
            paragraph.Format.SpaceBefore = 3;

            // Time Frame
            this.timeFrame = section.AddTextFrame();
            this.timeFrame.Height = "1.75cm";
            this.timeFrame.Width = "18.4cm";
            this.timeFrame.Left = ShapePosition.Left;
            this.timeFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            this.timeFrame.Top = "12.75cm";
            this.timeFrame.RelativeVertical = RelativeVertical.Page;
            this.timeFrame.FillFormat.Color = MigraDoc.DocumentObjectModel.Colors.Azure;

            paragraph = this.timeFrame.AddParagraph();
            paragraph.AddText("Time spent:");
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Font.Name = "Times New Roman";
            paragraph.Format.Font.Size =12;
            paragraph.Format.SpaceAfter = 3;
            paragraph.Format.LeftIndent = 5;
            paragraph.Format.SpaceBefore = 3;

            paragraph = this.timeFrame.AddParagraph();
            paragraph.AddText("Plumber " + _timeSheet.PlumberTime.Hours + " h " + _timeSheet.PlumberTime.Minutes + " min");
            paragraph.AddSpace(30);
            paragraph.AddText("Assistant(s) " + _timeSheet.AssistantTime.Hours + " h " + _timeSheet.AssistantTime.Minutes + " min");
            paragraph.Format.Font.Bold = false;
            paragraph.Format.Font.Name = "Times New Roman";
            paragraph.Format.Font.Size = 12;
            paragraph.Format.SpaceAfter = 3;
            paragraph.Format.LeftIndent = 5;
            paragraph.Format.SpaceBefore = 3;


            // Quote Frame
            this.quoteFrame = section.AddTextFrame();
            this.quoteFrame.Height = "3.5cm";
            this.quoteFrame.Width = "18.4cm";
            this.quoteFrame.Left = ShapePosition.Left;
            this.quoteFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            this.quoteFrame.Top = "15.0cm";
            this.quoteFrame.RelativeVertical = RelativeVertical.Page;
            this.quoteFrame.FillFormat.Color = MigraDoc.DocumentObjectModel.Colors.Azure;

            // Add Quote Info
            string yesNo = "";
            if (_timeSheet.OriginalQuote)
                yesNo = "Yes";
            else
                yesNo = "No";
            paragraph = this.quoteFrame.AddParagraph();
            paragraph.AddText("Part of Original Quote?");
            paragraph.AddSpace(50);
            paragraph.AddText("Quote No:");
            paragraph.Format.Font.Name = "Times New Roman";
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Font.Size = 12;
            paragraph.Format.SpaceAfter = 3;
            paragraph.Format.LeftIndent = 5;
            paragraph.Format.SpaceBefore = 3;

            paragraph = this.quoteFrame.AddParagraph();
            paragraph.AddText(yesNo);
            paragraph.AddSpace(85);
            if (_timeSheet.QuoteNo == null)
                paragraph.AddText("N/A");
            else
                paragraph.AddText(_timeSheet.QuoteNo);
            paragraph.Format.Font.Name = "Times New Roman";
            paragraph.Format.Font.Bold = false;
            paragraph.Format.Font.Size = 12;
            paragraph.Format.SpaceAfter = 6;
            paragraph.Format.LeftIndent = 5;
            paragraph.Format.SpaceBefore = 3;

            paragraph = this.quoteFrame.AddParagraph();
            paragraph.AddText("Additional Work: S.I. Number ");
            paragraph.AddSpace(35);
            paragraph.AddText("Signature (Site Manager)");
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Font.Name = "Times New Roman";
            paragraph.Format.Font.Size = 12;
            paragraph.Format.SpaceAfter = 5;
            paragraph.Format.LeftIndent = 5;
            paragraph.Format.SpaceBefore = 6;

            paragraph = this.quoteFrame.AddParagraph();
            if (_timeSheet.SINumber == null)
                paragraph.AddText("N/A");
            else
                paragraph.AddText(_timeSheet.SINumber.ToString());
            paragraph.Format.Font.Bold = false;
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Name = "Times New Roman";
            paragraph.Format.SpaceAfter = 6;
            paragraph.Format.LeftIndent = 5;
            paragraph.Format.SpaceBefore = 6;

            paragraph = section.AddParagraph();
            paragraph.Format.SpaceBefore = "10cm";

            // Add the print date field
            paragraph = section.AddParagraph();
            paragraph.Format.SpaceBefore = "5cm";
            paragraph.Style = "Reference";
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Font.Name = "Times New Roman";
            paragraph.AddFormattedText("Material used", TextFormat.Bold);

            // Create the item table
            this.table = section.AddTable();
            
            this.table.Style = "Table";
            this.table.Borders.Color = TableBorder;
            this.table.Borders.Width = 0.25;
            this.table.Borders.Left.Width = 0.5;
            this.table.Borders.Right.Width = 0.5;
            this.table.Rows.LeftIndent = 0;
            
            // Before you can add a row, you must define the columns
            Column column;
            foreach (DataColumn col in dt.Columns) {
                if (col.ColumnName == "BOM No.") {
                    column = this.table.AddColumn(Unit.FromCentimeter(2));
                    column.Format.Alignment = ParagraphAlignment.Center;
                } else if (col.ColumnName == "Code") {
                    column = this.table.AddColumn(Unit.FromCentimeter(3));
                    column.Format.Alignment = ParagraphAlignment.Left;
                } else if(col.ColumnName=="Description") {
                    column = this.table.AddColumn(Unit.FromCentimeter(7));
                    column.Format.Alignment = ParagraphAlignment.Left;
                } else if (col.ColumnName=="Quantity") {
                    column = this.table.AddColumn(Unit.FromCentimeter(2));
                    column.Format.Alignment = ParagraphAlignment.Right;
                } else {
                    column = this.table.AddColumn(Unit.FromCentimeter(4.5));
                    column.Format.Alignment = ParagraphAlignment.Left;
                }
            }

            // Create the header of the table
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            row.Shading.Color = TableBlue;


            for (int i = 0; i < dt.Columns.Count; i++) {

                row.Cells[i].AddParagraph(dt.Columns[i].ColumnName);
                row.Cells[i].Format.Font.Bold = false;
                row.Cells[i].Format.Alignment = ParagraphAlignment.Left;
                row.Cells[i].VerticalAlignment = VerticalAlignment.Bottom;


            }


            this.table.SetEdge(0, 0, dt.Columns.Count, 1, Edge.Box, BorderStyle.Single, 0.75, Color.Empty);


        }

        /// <summary>
        /// Creates the dynamic parts of the invoice.
        /// </summary>
        void FillContent() {
            Row row1;
            for (int i = 0; i < dt.Rows.Count; i++) {
                row1 = this.table.AddRow();
                row1.TopPadding = 1.5;
                for (int j = 0; j < dt.Columns.Count; j++) {
                    row1.Cells[j].Shading.Color = TableGray;
                    row1.Cells[j].VerticalAlignment = VerticalAlignment.Center;
                    row1.Cells[j].Format.Alignment = ParagraphAlignment.Left;
                    row1.Cells[j].Format.FirstLineIndent = 1;
                    row1.Cells[j].AddParagraph(dt.Rows[i][j].ToString());
                    this.table.SetEdge(0, this.table.Rows.Count - 2, dt.Columns.Count, 1, Edge.Box, BorderStyle.Single, 0.75);
                }
            }
        }
        #endregion

        // Some pre-defined colors
#if true
        // RGB colors
        readonly static Color TableBorder = new Color(81, 125, 192);
        readonly static Color TableBlue = new Color(235, 240, 249);
        readonly static Color TableGray = new Color(242, 242, 242);
#else
    // CMYK colors
    readonly static Color tableBorder = Color.FromCmyk(100, 50, 0, 30);
    readonly static Color tableBlue = Color.FromCmyk(0, 80, 50, 30);
    readonly static Color tableGray = Color.FromCmyk(30, 0, 0, 0, 100);
#endif
     
    }
}
