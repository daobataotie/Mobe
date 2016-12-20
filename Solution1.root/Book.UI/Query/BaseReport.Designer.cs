namespace Book.UI.Query
{
    partial class BaseReport
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseReport));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrLabelReportName = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelDateRange = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelPrintDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelCompanyInfoName = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            resources.ApplyResources(this.Detail, "Detail");
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            // 
            // TopMargin
            // 
            resources.ApplyResources(this.TopMargin, "TopMargin");
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            // 
            // BottomMargin
            // 
            resources.ApplyResources(this.BottomMargin, "BottomMargin");
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabelReportName,
            this.xrLabelDateRange,
            this.xrLabelPrintDate});
            resources.ApplyResources(this.PageHeader, "PageHeader");
            this.PageHeader.Name = "PageHeader";
            // 
            // xrLabelReportName
            // 
            resources.ApplyResources(this.xrLabelReportName, "xrLabelReportName");
            this.xrLabelReportName.Name = "xrLabelReportName";
            this.xrLabelReportName.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabelReportName.StylePriority.UseFont = false;
            this.xrLabelReportName.StylePriority.UseTextAlignment = false;
            // 
            // xrLabelDateRange
            // 
            resources.ApplyResources(this.xrLabelDateRange, "xrLabelDateRange");
            this.xrLabelDateRange.Name = "xrLabelDateRange";
            this.xrLabelDateRange.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabelDateRange.StylePriority.UseTextAlignment = false;
            // 
            // xrLabelPrintDate
            // 
            resources.ApplyResources(this.xrLabelPrintDate, "xrLabelPrintDate");
            this.xrLabelPrintDate.Name = "xrLabelPrintDate";
            this.xrLabelPrintDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabelPrintDate.StylePriority.UseTextAlignment = false;
            // 
            // xrLabelCompanyInfoName
            // 
            resources.ApplyResources(this.xrLabelCompanyInfoName, "xrLabelCompanyInfoName");
            this.xrLabelCompanyInfoName.Name = "xrLabelCompanyInfoName";
            this.xrLabelCompanyInfoName.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabelCompanyInfoName.StylePriority.UseFont = false;
            this.xrLabelCompanyInfoName.StylePriority.UseTextAlignment = false;
            // 
            // ReportFooter
            // 
            resources.ApplyResources(this.ReportFooter, "ReportFooter");
            this.ReportFooter.Expanded = false;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1});
            resources.ApplyResources(this.PageFooter, "PageFooter");
            this.PageFooter.Name = "PageFooter";
            // 
            // xrPageInfo1
            // 
            resources.ApplyResources(this.xrPageInfo1, "xrPageInfo1");
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrPageInfo1.StylePriority.UseTextAlignment = false;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabelCompanyInfoName});
            resources.ApplyResources(this.ReportHeader, "ReportHeader");
            this.ReportHeader.Name = "ReportHeader";
            // 
            // BaseReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.ReportFooter,
            this.PageFooter,
            this.ReportHeader});
            this.DataSource = this.bindingSource1;
            resources.ApplyResources(this, "$this");
            this.Margins = new System.Drawing.Printing.Margins(150, 80, 80, 80);
            this.PageHeight = 2794;
            this.PageWidth = 2159;
            this.Version = "10.2";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRLabel xrLabelCompanyInfoName;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo1;
        private DevExpress.XtraReports.UI.XRLabel xrLabelPrintDate;
        protected System.Windows.Forms.BindingSource bindingSource1;
        protected DevExpress.XtraReports.UI.DetailBand Detail;
        protected DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        protected DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        protected DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        protected DevExpress.XtraReports.UI.XRLabel xrLabelDateRange;
        protected DevExpress.XtraReports.UI.XRLabel xrLabelReportName;
    }
}
