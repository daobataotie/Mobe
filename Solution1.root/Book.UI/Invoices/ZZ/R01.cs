using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Invoices.ZZ
{
    public partial class R01 : DevExpress.XtraReports.UI.XtraReport
    {
        protected BL.InvoiceZZManager invoiceManager = new Book.BL.InvoiceZZManager();

        private Model.InvoiceZZ invoice;

        public R01(string invoiceId)
        {
            InitializeComponent();

            this.invoice = this.invoiceManager.Get(invoiceId);

            if (this.invoice == null)
                return;

            this.xrLabelCompanyInfoAddress.Text = BL.Settings.CompanyAddress1;
            this.xrLabelCompanyInfoFAX.Text = BL.Settings.CompanyFax;
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName + Properties.Resources.InvoiceZZ;
            this.xrLabelCompanyInfoTelphone.Text = BL.Settings.CompanyPhone;
            this.xrLabelInvoiceId.Text = this.invoice.InvoiceId;
            this.xrLabelInvoiceDate.Text = this.invoice.InvoiceDate.Value.ToString();

            this.xrSubreport1.ReportSource = new R01_I(invoice);
            this.xrSubreport2.ReportSource = new R01_O(invoice);
        }

    }
}
