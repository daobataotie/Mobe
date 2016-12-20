using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Invoices.XJ
{
    public partial class ROBaoJia : DevExpress.XtraReports.UI.XtraReport
    {
        public ROBaoJia()
        {
            InitializeComponent();
        }

        private Model.InvoiceXJ _InvoiceXJ;

        public ROBaoJia(Model.InvoiceXJ invoicexj)
            : this()
        {
            this._InvoiceXJ = invoicexj;

            if (this._InvoiceXJ == null)
                return;

            //Company
            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lblReportName.Text = Properties.Resources.InvoiceXJBJ;
            this.lblReportDate.Text += DateTime.Now.ToShortDateString();

            //Controls
            this.lblInvoiceId.Text = this._InvoiceXJ.InvoiceId;
            this.lblInvoiceDate.Text = this._InvoiceXJ.InvoiceDate.HasValue ? this._InvoiceXJ.InvoiceDate.Value.ToString("yyyy-MM-dd") : "";
            this.lblInvoiceYxrq.Text = this._InvoiceXJ.InvoiceYxrq.HasValue ? this._InvoiceXJ.InvoiceYxrq.Value.ToString("yyyy-MM-dd") : "";
            this.lblCustomer.Text = this._InvoiceXJ.Customer == null ? "" : this._InvoiceXJ.Customer.ToString();
            this.lblProduct.Text = this._InvoiceXJ.ProductModel;
            this.lblCompany.Text = this._InvoiceXJ.Company == null ? "" : this._InvoiceXJ.Company.ToString();
            this.lblEmployee0.Text = this._InvoiceXJ.Employee0 == null ? "" : this._InvoiceXJ.Employee0.ToString();
            this.lblInvoiceNote.Text = this._InvoiceXJ.InvoiceNote;

            //SubReport
            this.SubReport_Pro.ReportSource = new RoSubBJ_Pro();
            this.SubReport_Pack.ReportSource = new RoSubBJ_Pack();
            this.SubReport_Proc.ReportSource = new RoSubBJ_Proc();
        }

        private void SubReport_Pro_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            RoSubBJ_Pro SCB_Pro = this.SubReport_Pro.ReportSource as RoSubBJ_Pro;
            SCB_Pro._InvoiceXJ = this._InvoiceXJ;
        }

        private void SubReport_Pack_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            RoSubBJ_Pack SCB_Pack = this.SubReport_Pack.ReportSource as RoSubBJ_Pack;
            SCB_Pack._InvoiceXJ = this._InvoiceXJ;
        }

        private void SubReport_Proc_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            RoSubBJ_Proc SCB_Proc = this.SubReport_Proc.ReportSource as RoSubBJ_Proc;
            SCB_Proc._InvoiceXJ = this._InvoiceXJ;
        }
    }
}
