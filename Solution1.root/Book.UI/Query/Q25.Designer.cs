namespace Book.UI.Query
{
    partial class Q25
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Q25));
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrSubreport1 = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrSubreport2 = new DevExpress.XtraReports.UI.XRSubreport();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubreport2,
            this.xrSubreport1});
            this.Detail.Height = 135;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.PageHeader.Height = 198;
            // 
            // xrLabelReportName
            // 
            resources.ApplyResources(this.xrLabelReportName, "xrLabelReportName");
            this.xrLabelReportName.StylePriority.UseFont = false;
            this.xrLabelReportName.StylePriority.UseTextAlignment = false;
            // 
            // xrLabelDateRange
            // 
            resources.ApplyResources(this.xrLabelDateRange, "xrLabelDateRange");
            this.xrLabelDateRange.StylePriority.UseTextAlignment = false;
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTable1.Dpi = 254F;
            resources.ApplyResources(this.xrTable1, "xrTable1");
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.StylePriority.UseBorders = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2});
            this.xrTableRow1.Dpi = 254F;
            this.xrTableRow1.Name = "xrTableRow1";
            resources.ApplyResources(this.xrTableRow1, "xrTableRow1");
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell1.Dpi = 254F;
            resources.ApplyResources(this.xrTableCell1, "xrTableCell1");
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrTableCell1.StylePriority.UseBorders = false;
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Borders = DevExpress.XtraPrinting.BorderSide.Left;
            this.xrTableCell2.Dpi = 254F;
            resources.ApplyResources(this.xrTableCell2, "xrTableCell2");
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrTableCell2.StylePriority.UseBorders = false;
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrSubreport1
            // 
            this.xrSubreport1.Dpi = 254F;
            resources.ApplyResources(this.xrSubreport1, "xrSubreport1");
            this.xrSubreport1.Name = "xrSubreport1";
            this.xrSubreport1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrSubreport1_BeforePrint);
            // 
            // xrSubreport2
            // 
            this.xrSubreport2.Dpi = 254F;
            resources.ApplyResources(this.xrSubreport2, "xrSubreport2");
            this.xrSubreport2.Name = "xrSubreport2";
            this.xrSubreport2.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrSubreport2_BeforePrint);
            // 
            // Q25
            // 
            this.ExportOptions.Csv.EncodingType = ((DevExpress.XtraPrinting.EncodingType)(resources.GetObject("Q25.ExportOptions.Csv.EncodingType")));
            this.ExportOptions.Html.CharacterSet = resources.GetString("Q25.ExportOptions.Html.CharacterSet");
            this.ExportOptions.Html.Title = resources.GetString("Q25.ExportOptions.Html.Title");
            this.ExportOptions.Mht.CharacterSet = resources.GetString("Q25.ExportOptions.Mht.CharacterSet");
            this.ExportOptions.Mht.Title = resources.GetString("Q25.ExportOptions.Mht.Title");
            this.ExportOptions.Pdf.DocumentOptions.Application = resources.GetString("Q25.ExportOptions.Pdf.DocumentOptions.Application");
            this.ExportOptions.Pdf.DocumentOptions.Author = resources.GetString("Q25.ExportOptions.Pdf.DocumentOptions.Author");
            this.ExportOptions.Pdf.DocumentOptions.Keywords = resources.GetString("Q25.ExportOptions.Pdf.DocumentOptions.Keywords");
            this.ExportOptions.Pdf.DocumentOptions.Subject = resources.GetString("Q25.ExportOptions.Pdf.DocumentOptions.Subject");
            this.ExportOptions.Pdf.DocumentOptions.Title = resources.GetString("Q25.ExportOptions.Pdf.DocumentOptions.Title");
            this.ExportOptions.Text.EncodingType = ((DevExpress.XtraPrinting.EncodingType)(resources.GetObject("Q25.ExportOptions.Text.EncodingType")));
            this.ExportOptions.Xls.SheetName = resources.GetString("Q25.ExportOptions.Xls.SheetName");
            this.Version = "8.1";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRSubreport xrSubreport2;
        private DevExpress.XtraReports.UI.XRSubreport xrSubreport1;
    }
}
