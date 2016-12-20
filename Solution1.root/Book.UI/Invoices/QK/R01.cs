using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Invoices.QK
{
    public partial class R01 : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.InvoiceQKManager invoiceManager = new Book.BL.InvoiceQKManager();
        private Model.InvoiceQK invoice;

        public R01(string invoiceId)
        {
            InitializeComponent();

            this.invoice = this.invoiceManager.Get(invoiceId);

            if (this.invoice == null)
                throw new ArgumentException();

            this.xrTableCellCompany.Text = this.invoice.Customer.ToString();
            this.xrTableCellTotal0.Text = this.invoice.InvoiceTotal0.ToString();
            this.xrTableCellTotal1.Text = this.invoice.InvoiceTotal1.ToString();
            this.xrLabelInvoiceId.Text = this.invoice.InvoiceId;
            this.xrLabelInvoiceDate.Text = this.invoice.InvoiceDate.Value.ToShortDateString();

        }

    }
}
