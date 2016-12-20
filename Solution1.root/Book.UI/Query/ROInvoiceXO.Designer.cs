namespace Book.UI.Query
{
    partial class ROInvoiceXO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ROInvoiceXO));
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrSubreport1 = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrLine3 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLabelTotalZongJi = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelTotalHeJi = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelTotalTax1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelTotalHeJi1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelTotalTax = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblInvoiceTax = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblInvoiceDate = new DevExpress.XtraReports.UI.XRLabel();
            this.lblCustomer = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblYJRQ = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.lblChuHuoCustomer = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblInvoiceId = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblLotNumber = new DevExpress.XtraReports.UI.XRLabel();
            this.lblInvoiceTotal = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblCustomerInvoiceXOID = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelEmpCheck = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblInvoiceHeji = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1,
            this.xrLine3,
            this.xrSubreport1});
            resources.ApplyResources(this.Detail, "Detail");
            // 
            // PageHeader
            // 
            resources.ApplyResources(this.PageHeader, "PageHeader");
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabelTotalTax1,
            this.xrLabelTotalTax,
            this.xrLabel2,
            this.xrLabelTotalHeJi1,
            this.xrLabelTotalHeJi,
            this.xrLabelTotalZongJi});
            resources.ApplyResources(this.ReportFooter, "ReportFooter");
            this.ReportFooter.Expanded = true;
            // 
            // PageFooter
            // 
            resources.ApplyResources(this.PageFooter, "PageFooter");
            // 
            // xrLabelDateRange
            // 
            resources.ApplyResources(this.xrLabelDateRange, "xrLabelDateRange");
            this.xrLabelDateRange.StylePriority.UseTextAlignment = false;
            // 
            // xrLabelReportName
            // 
            resources.ApplyResources(this.xrLabelReportName, "xrLabelReportName");
            this.xrLabelReportName.StylePriority.UseFont = false;
            this.xrLabelReportName.StylePriority.UseTextAlignment = false;
            // 
            // xrTableCell13
            // 
            resources.ApplyResources(this.xrTableCell13, "xrTableCell13");
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrTableCell13.Weight = 0;
            // 
            // xrSubreport1
            // 
            resources.ApplyResources(this.xrSubreport1, "xrSubreport1");
            this.xrSubreport1.Name = "xrSubreport1";
            this.xrSubreport1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrSubreport1_BeforePrint);
            // 
            // xrLine3
            // 
            resources.ApplyResources(this.xrLine3, "xrLine3");
            this.xrLine3.LineWidth = 3;
            this.xrLine3.Name = "xrLine3";
            // 
            // xrLabelTotalZongJi
            // 
            this.xrLabelTotalZongJi.CanGrow = false;
            resources.ApplyResources(this.xrLabelTotalZongJi, "xrLabelTotalZongJi");
            this.xrLabelTotalZongJi.Name = "xrLabelTotalZongJi";
            this.xrLabelTotalZongJi.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabelTotalZongJi.StylePriority.UseForeColor = false;
            this.xrLabelTotalZongJi.StylePriority.UseTextAlignment = false;
            // 
            // xrLabelTotalHeJi
            // 
            this.xrLabelTotalHeJi.CanGrow = false;
            resources.ApplyResources(this.xrLabelTotalHeJi, "xrLabelTotalHeJi");
            this.xrLabelTotalHeJi.Name = "xrLabelTotalHeJi";
            this.xrLabelTotalHeJi.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabelTotalHeJi.StylePriority.UseForeColor = false;
            this.xrLabelTotalHeJi.StylePriority.UseTextAlignment = false;
            // 
            // xrLabelTotalTax1
            // 
            this.xrLabelTotalTax1.CanGrow = false;
            resources.ApplyResources(this.xrLabelTotalTax1, "xrLabelTotalTax1");
            this.xrLabelTotalTax1.Name = "xrLabelTotalTax1";
            this.xrLabelTotalTax1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabelTotalTax1.StylePriority.UseForeColor = false;
            this.xrLabelTotalTax1.StylePriority.UseTextAlignment = false;
            // 
            // xrLabelTotalHeJi1
            // 
            this.xrLabelTotalHeJi1.CanGrow = false;
            resources.ApplyResources(this.xrLabelTotalHeJi1, "xrLabelTotalHeJi1");
            this.xrLabelTotalHeJi1.Name = "xrLabelTotalHeJi1";
            this.xrLabelTotalHeJi1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabelTotalHeJi1.StylePriority.UseForeColor = false;
            this.xrLabelTotalHeJi1.StylePriority.UseTextAlignment = false;
            // 
            // xrLabelTotalTax
            // 
            this.xrLabelTotalTax.CanGrow = false;
            resources.ApplyResources(this.xrLabelTotalTax, "xrLabelTotalTax");
            this.xrLabelTotalTax.Name = "xrLabelTotalTax";
            this.xrLabelTotalTax.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabelTotalTax.StylePriority.UseForeColor = false;
            this.xrLabelTotalTax.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel2
            // 
            this.xrLabel2.CanGrow = false;
            resources.ApplyResources(this.xrLabel2, "xrLabel2");
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel2.StylePriority.UseForeColor = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            // 
            // xrTable1
            // 
            resources.ApplyResources(this.xrTable1, "xrTable1");
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.StylePriority.UseBackColor = false;
            this.xrTable1.StylePriority.UseBorderColor = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3});
            resources.ApplyResources(this.xrTableRow1, "xrTableRow1");
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblInvoiceTax,
            this.xrLabel6,
            this.xrLabel7,
            this.lblInvoiceDate,
            this.lblCustomer,
            this.xrLabel9,
            this.xrLabel8,
            this.lblYJRQ});
            resources.ApplyResources(this.xrTableCell1, "xrTableCell1");
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Weight = 0.96621622319827349;
            // 
            // lblInvoiceTax
            // 
            resources.ApplyResources(this.lblInvoiceTax, "lblInvoiceTax");
            this.lblInvoiceTax.Name = "lblInvoiceTax";
            this.lblInvoiceTax.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblInvoiceTax.StylePriority.UseFont = false;
            this.lblInvoiceTax.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel6
            // 
            resources.ApplyResources(this.xrLabel6, "xrLabel6");
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel7
            // 
            resources.ApplyResources(this.xrLabel7, "xrLabel7");
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            // 
            // lblInvoiceDate
            // 
            resources.ApplyResources(this.lblInvoiceDate, "lblInvoiceDate");
            this.lblInvoiceDate.Name = "lblInvoiceDate";
            this.lblInvoiceDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblInvoiceDate.StylePriority.UseFont = false;
            this.lblInvoiceDate.StylePriority.UseTextAlignment = false;
            // 
            // lblCustomer
            // 
            resources.ApplyResources(this.lblCustomer, "lblCustomer");
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblCustomer.StylePriority.UseFont = false;
            this.lblCustomer.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel9
            // 
            resources.ApplyResources(this.xrLabel9, "xrLabel9");
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel8
            // 
            resources.ApplyResources(this.xrLabel8, "xrLabel8");
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            // 
            // lblYJRQ
            // 
            resources.ApplyResources(this.lblYJRQ, "lblYJRQ");
            this.lblYJRQ.Name = "lblYJRQ";
            this.lblYJRQ.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblYJRQ.StylePriority.UseFont = false;
            this.lblYJRQ.StylePriority.UseTextAlignment = false;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblChuHuoCustomer,
            this.xrLabel1,
            this.xrLabel3,
            this.lblInvoiceId,
            this.xrLabel12,
            this.lblLotNumber,
            this.lblInvoiceTotal,
            this.xrLabel15});
            resources.ApplyResources(this.xrTableCell2, "xrTableCell2");
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Weight = 0.89581991602786981;
            // 
            // lblChuHuoCustomer
            // 
            resources.ApplyResources(this.lblChuHuoCustomer, "lblChuHuoCustomer");
            this.lblChuHuoCustomer.Name = "lblChuHuoCustomer";
            this.lblChuHuoCustomer.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.lblChuHuoCustomer.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel1
            // 
            resources.ApplyResources(this.xrLabel1, "xrLabel1");
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel3
            // 
            resources.ApplyResources(this.xrLabel3, "xrLabel3");
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            // 
            // lblInvoiceId
            // 
            resources.ApplyResources(this.lblInvoiceId, "lblInvoiceId");
            this.lblInvoiceId.Name = "lblInvoiceId";
            this.lblInvoiceId.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblInvoiceId.StylePriority.UseFont = false;
            this.lblInvoiceId.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel12
            // 
            resources.ApplyResources(this.xrLabel12, "xrLabel12");
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel12.StylePriority.UseFont = false;
            this.xrLabel12.StylePriority.UseTextAlignment = false;
            // 
            // lblLotNumber
            // 
            resources.ApplyResources(this.lblLotNumber, "lblLotNumber");
            this.lblLotNumber.Name = "lblLotNumber";
            this.lblLotNumber.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblLotNumber.StylePriority.UseFont = false;
            this.lblLotNumber.StylePriority.UseTextAlignment = false;
            // 
            // lblInvoiceTotal
            // 
            resources.ApplyResources(this.lblInvoiceTotal, "lblInvoiceTotal");
            this.lblInvoiceTotal.Name = "lblInvoiceTotal";
            this.lblInvoiceTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblInvoiceTotal.StylePriority.UseFont = false;
            this.lblInvoiceTotal.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel15
            // 
            resources.ApplyResources(this.xrLabel15, "xrLabel15");
            this.xrLabel15.Name = "xrLabel15";
            this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel15.StylePriority.UseFont = false;
            this.xrLabel15.StylePriority.UseTextAlignment = false;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel4,
            this.lblCustomerInvoiceXOID,
            this.xrLabel16,
            this.xrLabelEmpCheck,
            this.xrLabel11,
            this.lblInvoiceHeji});
            resources.ApplyResources(this.xrTableCell3, "xrTableCell3");
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Weight = 1.1379638607738569;
            // 
            // xrLabel4
            // 
            resources.ApplyResources(this.xrLabel4, "xrLabel4");
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            // 
            // lblCustomerInvoiceXOID
            // 
            resources.ApplyResources(this.lblCustomerInvoiceXOID, "lblCustomerInvoiceXOID");
            this.lblCustomerInvoiceXOID.Name = "lblCustomerInvoiceXOID";
            this.lblCustomerInvoiceXOID.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            // 
            // xrLabel16
            // 
            resources.ApplyResources(this.xrLabel16, "xrLabel16");
            this.xrLabel16.Name = "xrLabel16";
            this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel16.StylePriority.UseFont = false;
            this.xrLabel16.StylePriority.UseTextAlignment = false;
            // 
            // xrLabelEmpCheck
            // 
            resources.ApplyResources(this.xrLabelEmpCheck, "xrLabelEmpCheck");
            this.xrLabelEmpCheck.Name = "xrLabelEmpCheck";
            this.xrLabelEmpCheck.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabelEmpCheck.StylePriority.UseFont = false;
            this.xrLabelEmpCheck.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel11
            // 
            resources.ApplyResources(this.xrLabel11, "xrLabel11");
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.StylePriority.UseTextAlignment = false;
            // 
            // lblInvoiceHeji
            // 
            resources.ApplyResources(this.lblInvoiceHeji, "lblInvoiceHeji");
            this.lblInvoiceHeji.Name = "lblInvoiceHeji";
            this.lblInvoiceHeji.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblInvoiceHeji.StylePriority.UseFont = false;
            this.lblInvoiceHeji.StylePriority.UseTextAlignment = false;
            // 
            // ROInvoiceXO
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.ReportFooter,
            this.PageFooter});
            resources.ApplyResources(this, "$this");
            this.Version = "10.2";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.XRTableCell xrTableCell13;
        private DevExpress.XtraReports.UI.XRSubreport xrSubreport1;
        private DevExpress.XtraReports.UI.XRLine xrLine3;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRLabel xrLabelTotalTax;
        private DevExpress.XtraReports.UI.XRLabel xrLabelTotalHeJi1;
        private DevExpress.XtraReports.UI.XRLabel xrLabelTotalTax1;
        private DevExpress.XtraReports.UI.XRLabel xrLabelTotalHeJi;
        private DevExpress.XtraReports.UI.XRLabel xrLabelTotalZongJi;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel16;
        private DevExpress.XtraReports.UI.XRLabel xrLabelEmpCheck;
        private DevExpress.XtraReports.UI.XRLabel lblInvoiceId;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRLabel lblInvoiceDate;
        private DevExpress.XtraReports.UI.XRLabel xrLabel7;
        private DevExpress.XtraReports.UI.XRLabel lblYJRQ;
        private DevExpress.XtraReports.UI.XRLabel xrLabel8;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRLabel lblCustomer;
        private DevExpress.XtraReports.UI.XRLabel xrLabel9;
        private DevExpress.XtraReports.UI.XRLabel lblLotNumber;
        private DevExpress.XtraReports.UI.XRLabel xrLabel12;
        private DevExpress.XtraReports.UI.XRLabel lblChuHuoCustomer;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel lblCustomerInvoiceXOID;
        private DevExpress.XtraReports.UI.XRLabel xrLabel4;
        private DevExpress.XtraReports.UI.XRLabel lblInvoiceTax;
        private DevExpress.XtraReports.UI.XRLabel xrLabel6;
        private DevExpress.XtraReports.UI.XRLabel xrLabel11;
        private DevExpress.XtraReports.UI.XRLabel lblInvoiceHeji;
        private DevExpress.XtraReports.UI.XRLabel lblInvoiceTotal;
        private DevExpress.XtraReports.UI.XRLabel xrLabel15;


    }
}
